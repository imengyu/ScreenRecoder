#include "stdafx.h"
#include "CMediaFileRecorder.h"
#include <windows.h>
#include <MMSystem.h>
#include "log.h"

void av_log_cb(void* data, int level, const char* msg, va_list args)
{
	char log[1024] = { 0 };
	vsprintf_s(log, msg, args);
	if (level > AV_LOG_INFO)
		MediaFileRecorder::call_log_func(MediaFileRecorder::LOG_DEBUG, msg, args);
	else if (level == AV_LOG_INFO)
		MediaFileRecorder::call_log_func(MediaFileRecorder::LOG_INFO, msg, args);
	else if (level == AV_LOG_WARNING)
		MediaFileRecorder::call_log_func(MediaFileRecorder::LOG_WARNING, msg, args);
	else if (level < AV_LOG_WARNING)
		MediaFileRecorder::call_log_func(MediaFileRecorder::LOG_ERROR, msg, args);
}

namespace MediaFileRecorder
{
	CMediaFileRecorder::CMediaFileRecorder()
		:m_bInited(false),
		m_pFormatCtx(NULL),
		m_pVideoCodecCtx(NULL),
		m_pAudioCodecCtx(NULL),
		m_pVideoPacket(NULL),
		m_pAudioPacket(NULL),
		m_nVideoStreamIndex(0),
		m_nAudioStreamIndex(0),
		m_nStartTime(0),
		m_nDuration(0),
		m_nVideoPacketIndex(0),
		m_nAudioPacketIndex(0),
		m_pVideoRecorder(new CVideoRecord()),
		m_pMicRecorder(new CAudioRecord()),
		m_pSpeakerRecorder(new CAudioRecord()),
		m_nMainAudioStream(0)
	{
		m_bRun = false;
		av_log_set_callback(av_log_cb);
	}

	CMediaFileRecorder::~CMediaFileRecorder()
	{
		if (m_bInited)
		{
			UnInit();
		}
	}

	int CMediaFileRecorder::Init(const RECORD_INFO& record_info)
	{
		if (m_bInited)
		{
			Error("Inited already!");
			return -1;
		}

		int ret = 0;

		m_stRecordInfo = record_info;

		av_register_all();

		ret = avformat_alloc_output_context2(&m_pFormatCtx, NULL, NULL, m_stRecordInfo.file_name);
		if (ret < 0)
		{
			Error("avformat_alloc_output_context2, ret: %d", ret);
			CleanUp();
			return -1;
		}

		ret = avio_open(&m_pFormatCtx->pb, m_stRecordInfo.file_name, AVIO_FLAG_READ_WRITE);
		if (ret < 0)
		{
			Error("avio_open failed, ret: %d", ret);
			CleanUp();
			return -1;
		}

		
		if (m_stRecordInfo.is_record_video)
		{
			if (InitVideoRecord() != 0)
			{
				Error("Init video record failed");
			}
		}

		if (m_stRecordInfo.is_record_mic || m_stRecordInfo.is_record_speaker)
		{
			if (InitAudioRecord() != 0)
			{
				Error("Init audio record failed");
			}
		}

		InitializeCriticalSection(&m_WriteFileSection);

		//write file header
		avformat_write_header(m_pFormatCtx, NULL);

		m_bInited = true;
		
		Info("Init media file recorder succeed!");

		return 0;
	}


	int CMediaFileRecorder::InitVideoRecord()
	{

		AVStream* pVideoStream = avformat_new_stream(m_pFormatCtx, NULL);

		if (pVideoStream == NULL)
		{
			Error("Create new video stream failed\n");
			VideoCleanUp();
			return -1;
		}

		m_nVideoStreamIndex = m_pFormatCtx->nb_streams - 1;
		/*m_pVideoCodecCtx = avcodec_alloc_context3(NULL);
		if (!m_pVideoCodecCtx)
		{
		Error("Create video codec context failed");
		VideoCleanUp();
		return -1;
		}
		avcodec_parameters_to_context(m_pVideoCodecCtx, pVideoStream->codecpar);*/

		m_pVideoCodecCtx = pVideoStream->codec;
		AVCodecID avcodecid = AV_CODEC_ID_H264;
		switch (m_stRecordInfo.format)
		{
		case VIDEO_FORMAT_H264_MP4:
			avcodecid = AV_CODEC_ID_H264;
			break;
		case VIDEO_FORMAT_MPEG4_MP4:
			avcodecid = AV_CODEC_ID_MPEG4;
			break;
		case VIDEO_FORMAT_MPEG4_AVI:
			avcodecid = AV_CODEC_ID_MPEG4;
			break;
		case VIDEO_FORMAT_H264_AVI:
			avcodecid = AV_CODEC_ID_H264;
			break;
		case VIDEO_FORMAT_FLV:
			avcodecid = AV_CODEC_ID_FLV1;
			break;
		case VIDEO_FORMAT_WMV:
			avcodecid = AV_CODEC_ID_WMV2;
			break;
		default:
			break;
		}

		m_pVideoCodecCtx->codec_id = AV_CODEC_ID_H264;
		m_pVideoCodecCtx->codec_type = AVMEDIA_TYPE_VIDEO;
		m_pVideoCodecCtx->pix_fmt = AV_PIX_FMT_YUV420P;
		m_pVideoCodecCtx->width = m_stRecordInfo.video_dst_width;
		m_pVideoCodecCtx->height = m_stRecordInfo.video_dst_height;

		m_pVideoCodecCtx->time_base.num = 1;
		m_pVideoCodecCtx->time_base.den = m_stRecordInfo.video_frame_rate;
		m_pVideoCodecCtx->thread_count = 5;

		m_pVideoCodecCtx->qcompress = (float)0.6;
		m_pVideoCodecCtx->max_qdiff = 4;
		m_pVideoCodecCtx->qmin = 0;
		m_pVideoCodecCtx->qmax = 50;
		m_pVideoCodecCtx->delay = 0;

		m_pVideoCodecCtx->keyint_min = m_stRecordInfo.video_frame_rate;
		m_pVideoCodecCtx->gop_size = m_stRecordInfo.video_frame_rate * 10;

		const char* crf = "23";
		if (m_stRecordInfo.quality == NORMAL)
		{
			crf = "28";
		}
		else if (m_stRecordInfo.quality == HIGH)
		{
			crf = "23";
		}
		else if (m_stRecordInfo.quality == VERY_HIGH)
		{
			crf = "18";
		}
		AVDictionary *param = 0;
		av_dict_set(&param, "preset", "veryfast", 0);
		av_dict_set(&param, "tune", "zerolatency", 0);
		av_dict_set(&param, "crf", crf, 0);

		AVCodec* pEncoder = avcodec_find_encoder(m_pVideoCodecCtx->codec_id);
		if (!pEncoder)
		{
			Error("avcodec_findo_encoder failed\n");
			VideoCleanUp();
			return -1;
		}

		if (m_pFormatCtx->oformat->flags & AVFMT_GLOBALHEADER)
		{
			m_pVideoCodecCtx->flags |= CODEC_FLAG_GLOBAL_HEADER;
		}

		int ret = avcodec_open2(m_pVideoCodecCtx, pEncoder, &param);
		if (ret < 0)
		{
			Error("avcodec_open2 failed, ret: %d", ret);
			VideoCleanUp();
			return -1;
		}

		int nPicSize = av_image_get_buffer_size(m_pVideoCodecCtx->pix_fmt, m_pVideoCodecCtx->width, m_pVideoCodecCtx->height, 1);
		m_pVideoPacket = av_packet_alloc();
		av_new_packet(m_pVideoPacket, nPicSize);

		ret = m_pVideoRecorder->Init(m_pVideoCodecCtx);
		if (ret != 0)
		{
			Error("Failed to init video recorder");
			return -1;
		}
		return 0;	

	}

	void CMediaFileRecorder::UnInitVideoRecord()
	{
		m_pVideoRecorder->UnInit();

		Info("Write video packet count: %lld", m_nVideoPacketIndex);

		VideoCleanUp();
	}

	void CMediaFileRecorder::VideoCleanUp()
	{
		if (m_pVideoPacket)
		{
			av_packet_unref(m_pVideoPacket);
			av_packet_free(&m_pVideoPacket);
			m_pVideoPacket = NULL;
		}
		
		if (m_pVideoCodecCtx)
		{
			avcodec_close(m_pVideoCodecCtx);
			m_pVideoCodecCtx = NULL;
		}
		m_nVideoPacketIndex = 0;
	}

	

	int CMediaFileRecorder::InitAudioRecord()
	{
		AVStream* pStream = avformat_new_stream(m_pFormatCtx, NULL);
		if (!pStream)
		{
			Error("Create audio stream failed! \n");
			AudioCleanUp();
			return -1;
		}

		m_nAudioStreamIndex = m_pFormatCtx->nb_streams - 1;

		/*m_pAudioCodecCtx = avcodec_alloc_context3(NULL);
		if (!m_pAudioCodecCtx)
		{
			Error("Create audio codec context failed");
			return -1;
		}
		avcodec_parameters_to_context(m_pAudioCodecCtx, pStream->codecpar);*/

		m_pAudioCodecCtx = pStream->codec;
		m_pAudioCodecCtx->codec_id = AV_CODEC_ID_AAC;
		m_pAudioCodecCtx->codec_type = AVMEDIA_TYPE_AUDIO;
		m_pAudioCodecCtx->sample_fmt = AV_SAMPLE_FMT_FLTP;
		m_pAudioCodecCtx->sample_rate = 44100;
		m_pAudioCodecCtx->channel_layout = AV_CH_LAYOUT_STEREO;
		m_pAudioCodecCtx->channels = av_get_channel_layout_nb_channels(m_pAudioCodecCtx->channel_layout);
		//m_pAudioCodecCtx->bit_rate = 128000;

		AVCodec* audio_encoder = avcodec_find_encoder(m_pAudioCodecCtx->codec_id);
		if (!audio_encoder)
		{
			Error("Failed to find audio encoder! \n");
			AudioCleanUp();
			return -1;
		}

		if (m_pFormatCtx->oformat->flags & AVFMT_GLOBALHEADER)
		{
			m_pAudioCodecCtx->flags |= CODEC_FLAG_GLOBAL_HEADER;
		}

		if (avcodec_open2(m_pAudioCodecCtx, audio_encoder, NULL) < 0)
		{
			Error("Failed to open audio codec context");
			AudioCleanUp();
			return -1;
		}

		int nAudioSize = av_samples_get_buffer_size(NULL, m_pAudioCodecCtx->channels,
			m_pAudioCodecCtx->frame_size, m_pAudioCodecCtx->sample_fmt, 1);

		m_pAudioPacket = av_packet_alloc();
		av_new_packet(m_pAudioPacket, nAudioSize);
		//av_new_packet(m_pAudioPacket, nAudioSize);

		if (m_stRecordInfo.is_record_mic)
		{
			if (m_pMicRecorder->Init(m_pAudioCodecCtx) != 0)
			{
				Error("Failed to init mic recorder \n");
			}
		}

		if (m_stRecordInfo.is_record_speaker)
		{
			if (m_pSpeakerRecorder->Init(m_pAudioCodecCtx) != 0)
			{
				Error("Failed to init speaker recorder \n");
			}
		}

		return 0;
	}

	void CMediaFileRecorder::UnInitAudioRecord()
	{
		if (m_stRecordInfo.is_record_mic)
			m_pMicRecorder->UnInit();

		if (m_stRecordInfo.is_record_speaker)
			m_pSpeakerRecorder->UnInit();

		Info("Write audio packet count: %lld", m_nAudioPacketIndex);

		AudioCleanUp();
		return;
	}

	void CMediaFileRecorder::AudioCleanUp()
	{
		if (m_pAudioPacket)
		{
			av_packet_unref(m_pAudioPacket);
			av_packet_free(&m_pAudioPacket);
			m_pAudioPacket = NULL;
		}
		if (m_pAudioCodecCtx)
		{
			avcodec_close(m_pAudioCodecCtx);
			m_pAudioCodecCtx = NULL;
		}

		m_nAudioPacketIndex= 0;
		m_nMainAudioStream = 0;
	}


	int CMediaFileRecorder::UnInit()
	{
		if (m_bInited)
		{
			if (m_bRun)
			{
				StopWriteFileThread();
			}
			av_write_trailer(m_pFormatCtx);
			UnInitVideoRecord();
			UnInitAudioRecord();
			Info("MediaFileRecorder: total time: %lld", m_nDuration);
			CleanUp();
			m_bInited = false;
			return 0;
		}
		return -1;
	}


	void CMediaFileRecorder::CleanUp()
	{
		if (m_pFormatCtx)
		{
			avio_close(m_pFormatCtx->pb);
			avformat_free_context(m_pFormatCtx);
			m_pFormatCtx = NULL;

		}
		m_nStartTime = 0;
		m_nDuration = 0;
	}


	int CMediaFileRecorder::Start()
	{
		if (!m_bInited)
		{
			Error("Media recorder not inited\n");
			return -1;
		}
		m_nStartTime = timeGetTime();
		StartWriteFileThread();
		return 0;
	}

	int CMediaFileRecorder::Stop()
	{
		if (m_bRun)
		{
			StopWriteFileThread();
			m_nDuration += timeGetTime() - m_nStartTime;
			return 0;
		}
		return - 1;
	}

	void CMediaFileRecorder::StartWriteFileThread()
	{
		m_bRun = true;
		if (m_stRecordInfo.is_record_video)
		{
			m_WriteVideoThread.swap(std::thread(std::bind(&CMediaFileRecorder::VideoWriteFileThreadProc, this)));
			SetThreadPriority(m_WriteVideoThread.native_handle(), THREAD_PRIORITY_TIME_CRITICAL);
		}
		if (m_stRecordInfo.is_record_mic ||
			m_stRecordInfo.is_record_speaker)
		{
			m_WriteAudioThread.swap(std::thread(std::bind(&CMediaFileRecorder::AuidoWriteFileThreadProc, this)));
			SetThreadPriority(m_WriteAudioThread.native_handle(), THREAD_PRIORITY_TIME_CRITICAL);
		}
	}

	void CMediaFileRecorder::StopWriteFileThread()
	{
		m_bRun = false;
		if (m_WriteVideoThread.joinable())
		{
			m_WriteVideoThread.join();
		}
		if (m_WriteAudioThread.joinable())
		{
			m_WriteAudioThread.join();
		}
	}

	void CMediaFileRecorder::VideoWriteFileThreadProc()
	{
		while (m_bRun)
		{
			EncodeAndWriteVideo();
			Sleep(10);
		}
	}

	void CMediaFileRecorder::AuidoWriteFileThreadProc()
	{
		while (m_bRun)
		{
			EncodeAndWriteAudio();
			Sleep(10);
		}
	}

	void CMediaFileRecorder::EncodeAndWriteVideo()
	{
		VIDEO_FRAME* pFrame = NULL;
		while (true)
		{
			pFrame = m_pVideoRecorder->GetOneFrame();
			if (!pFrame)
				break;
			
			int got_picture = 0;
			int64_t encode_start_time = timeGetTime();
			int ret =avcodec_encode_video2(m_pVideoCodecCtx, m_pVideoPacket, pFrame->pAvFrame, &got_picture);
			int64_t encode_duration = timeGetTime() - encode_start_time;
			if (ret < 0)
			{
				Error("avcodec_encode_video2 failed");
				return;
			}

			if (got_picture == 1)
			{
				AVStream* pStream = m_pFormatCtx->streams[m_nVideoStreamIndex];
				m_pVideoPacket->stream_index = m_nVideoStreamIndex;
				m_pVideoPacket->pts = pFrame->nCaptureTime *(pStream->time_base.den/pStream->time_base.num) / 1000;
				m_pVideoPacket->dts = m_pVideoPacket->pts;
				//m_VideoPacket.duration = 1;

				EnterCriticalSection(&m_WriteFileSection);
				ret = av_interleaved_write_frame(m_pFormatCtx, m_pVideoPacket);
				LeaveCriticalSection(&m_WriteFileSection);

				Debug("encode frame : %lld", encode_duration);
				m_nVideoPacketIndex++;
			}
		}
	}

	void CMediaFileRecorder::EncodeAndWriteAudio()
	{
		int64_t begin_time = timeGetTime();
		bool bGetFrame = false;
		AVFrame* pMainFrame = NULL;
		AVFrame* pChildFrame = NULL;
		if (m_stRecordInfo.is_record_mic && m_stRecordInfo.is_record_speaker)
		{
			if (m_nMainAudioStream == 0)
			{
				// main stream not decided yet
				AVFrame* pMicFrame = m_pMicRecorder->GetOneFrame();
				if (pMicFrame)
				{
					pMainFrame = pMicFrame;
					m_nMainAudioStream = 1;
				}
				AVFrame* pSpeakerFrame = m_pSpeakerRecorder->GetOneFrame();
				if (pSpeakerFrame && m_nMainAudioStream == 0)
				{
					pMainFrame = pSpeakerFrame;
					m_nMainAudioStream = 2;
				}
			}
			else if (m_nMainAudioStream == 1)
			{
				pMainFrame = m_pMicRecorder->GetOneFrame();
				if (!pMainFrame)
					return;
				AVFrame* pChildFrame = m_pSpeakerRecorder->GetOneFrame();
				if (pChildFrame)
					MixAudio(pMainFrame, pChildFrame);
			}
			else if (m_nMainAudioStream == 2)
			{
				pMainFrame = m_pSpeakerRecorder->GetOneFrame();
				if (!pMainFrame)
					return;
				AVFrame* pChildFrame = m_pMicRecorder->GetOneFrame();
				if (pChildFrame)
					MixAudio(pMainFrame, pChildFrame);
			}
		}
		else if (m_stRecordInfo.is_record_speaker)
			pMainFrame = m_pSpeakerRecorder->GetOneFrame();
		else
			pMainFrame = m_pMicRecorder->GetOneFrame();

		int64_t mix_time = timeGetTime() - begin_time;

		if (pMainFrame)
		{
			int got_packet = 0;
			//pSpeakerFrame->pts = m_nAudioFrameIndex * m_pAudioCodecCtx->frame_size;
			int ret = avcodec_encode_audio2(m_pAudioCodecCtx, m_pAudioPacket, pMainFrame, &got_packet);
			if (ret < 0)
			{
				Error("avcodec_encode_audio2 failed");
				return;
			}
			if (got_packet == 1)
			{
				m_pAudioPacket->stream_index = m_nAudioStreamIndex;
				int64_t pts = (m_nDuration + (timeGetTime() - m_nStartTime)) * m_pAudioCodecCtx->sample_rate / 1000;
				m_pAudioPacket->pts = pts/*m_nAudioFrameIndex * m_pAudioCodecCtx->frame_size*/;
				m_pAudioPacket->dts = pts/*m_nAudioFrameIndex * m_pAudioCodecCtx->frame_size*/;
				//m_pAudioPacket->duration = m_pAudioCodecCtx->frame_size;

				EnterCriticalSection(&m_WriteFileSection);
				av_interleaved_write_frame(m_pFormatCtx, m_pAudioPacket);
				LeaveCriticalSection(&m_WriteFileSection);
				m_nAudioPacketIndex++;
			}
		}
		/*int64_t duration = timeGetTime() - begin_time;
		char log[128] = { 0 };
		sprintf_s(log, "Mix audio time: %lld, total time: %lld \n", mix_time, duration);
		OutputDebugStringA(log);*/
	}

	void CMediaFileRecorder::MixAudio(AVFrame* pDstFrame, const AVFrame* pSrcFrame)
	{
		uint32_t planes = GetAudioPlanes(AUDIO_FORMAT_FLOAT_PLANAR, SPEAKERS_STEREO);
		for (uint32_t j = 0;  j < planes; j++)
		{
			float* dst = (float*)pDstFrame->data[j];
			float* src = (float*)pSrcFrame->data[j];
			for (int i = 0; i < m_pAudioCodecCtx->frame_size; i++)
			{
				if (dst[i] < 0 && src[i] < 0)
				{
					dst[i] = (dst[i] + src[i]) - dst[i] * src[i] / (-(pow(2, 32 - 1) - 1));
				}
				else
				{
					dst[i] = (dst[i] + src[i]) - dst[i] * src[i] / pow(2, 32 - 1);
				}
			}
		}
	}

	int CMediaFileRecorder::FillVideo(const void* data, const VIDEO_INFO& video_info)
	{
		int ret = -1;
		if (m_stRecordInfo.is_record_video && m_bInited && m_bRun)
		{
			int64_t capture_time = m_nDuration + (timeGetTime() - m_nStartTime);
			ret = m_pVideoRecorder->FillVideo(data, video_info, capture_time);
		}
		return ret;
	}

	int CMediaFileRecorder::FillMicAudio(const void* audioSamples, int nb_samples, const AUDIO_INFO& audio_info)
	{
		int ret = -1;
		if (m_stRecordInfo.is_record_mic && m_bInited && m_bRun)
		{
			ret = m_pMicRecorder->FillAudio(audioSamples, nb_samples, audio_info);
		}

		return ret;
	}

	int CMediaFileRecorder::FillSpeakerAudio(const void* audioSamples, int nb_samples, const AUDIO_INFO& audio_info)
	{
		int ret = -1;
		if (m_stRecordInfo.is_record_speaker && m_bInited & m_bRun)
		{
			ret = m_pSpeakerRecorder->FillAudio(audioSamples, nb_samples, audio_info);
		}

		return ret;
	}


//////////////////////////////////////////////////////////////////////////////
//// Class CAudioRecord

	CAudioRecord::CAudioRecord()
		:m_pCodecCtx(NULL),
		m_pConvertCtx(NULL),
		m_pFifoBuffer(NULL),
		m_pFrame(NULL),
		m_pFrameBuffer(NULL),
		m_nResampleBufferSize(0),
		m_nSavedAudioSamples(0),
		m_nDiscardAudioSamples(0)
	{
		m_bInited = false;
		memset(m_pResampleBuffer, 0, sizeof(m_pResampleBuffer));
	}

	CAudioRecord::~CAudioRecord()
	{
		if (m_bInited)
		{
			UnInit();
		}
	}

	int CAudioRecord::Init(const AVCodecContext* pCodecCtx)
	{
		if (m_bInited)
		{
			Error("AudioRecorder: Already inited \n");
			return -1;
		}

		m_pCodecCtx = pCodecCtx;

		m_pFrame = av_frame_alloc();
		m_pFrame->nb_samples = m_pCodecCtx->frame_size;
		m_pFrame->format = m_pCodecCtx->sample_fmt;

		int nSize = av_samples_get_buffer_size(NULL, m_pCodecCtx->channels,
			m_pCodecCtx->frame_size, m_pCodecCtx->sample_fmt, 1);
		m_pFrameBuffer = (uint8_t *)av_malloc(nSize);
		avcodec_fill_audio_frame(m_pFrame, m_pCodecCtx->channels,
			m_pCodecCtx->sample_fmt, (const uint8_t*)m_pFrameBuffer, nSize, 1);

		m_pFifoBuffer = av_audio_fifo_alloc(pCodecCtx->sample_fmt,
			pCodecCtx->channels, 100 * m_pCodecCtx->frame_size);
		if (!m_pFifoBuffer)
		{
			CleanUp();
			return -1;
		}

		InitializeCriticalSection(&m_BufferSection);

		m_bInited = true;

		return 0;
	}


	int CAudioRecord::ResetConvertCtx()
	{
		AVSampleFormat src_av_sample_fmt;
		if (!ConvertToAVSampleFormat(m_stAudioInfo.audio_format, src_av_sample_fmt))
		{
			Error("FiilAudio: convert to av sample format failed, audio format: %d", m_stAudioInfo.audio_format);
			CleanUp();
			return -1;
		}

		int64_t src_av_ch_layout;
		if (!ConvertToAVChannelLayOut(m_stAudioInfo.chl_layout, src_av_ch_layout))
		{
			Error("FillAudio: convert to av channel layout failed, chl_layout: %d", m_stAudioInfo.chl_layout);
			CleanUp();
			return -1;
		}

		int src_nb_channels = av_get_channel_layout_nb_channels(src_av_sample_fmt);

		if (m_pConvertCtx)
			swr_free(&m_pConvertCtx);

		m_pConvertCtx = swr_alloc();
		// init audio resample context
		//设置源通道布局  
		av_opt_set_int(m_pConvertCtx, "in_channel_layout", src_av_ch_layout, 0);
		//设置源通道采样率  
		av_opt_set_int(m_pConvertCtx, "in_sample_rate", m_stAudioInfo.sample_rate, 0);
		//设置源通道样本格式  
		av_opt_set_sample_fmt(m_pConvertCtx, "in_sample_fmt", src_av_sample_fmt, 0);

		//目标通道布局  
		av_opt_set_int(m_pConvertCtx, "out_channel_layout", m_pCodecCtx->channel_layout, 0);
		//目标采用率  
		av_opt_set_int(m_pConvertCtx, "out_sample_rate", m_pCodecCtx->sample_rate, 0);
		//目标样本格式  
		av_opt_set_sample_fmt(m_pConvertCtx, "out_sample_fmt", m_pCodecCtx->sample_fmt, 0);

		if (swr_init(m_pConvertCtx) < 0)
		{
			Error("swr_init failed.");
			CleanUp();
			return -1;
		}
		return 0;
	}

	int CAudioRecord::FillAudio(const void* audioSamples, int nb_samples, const AUDIO_INFO& audio_info)
	{
		if (m_bInited)
		{
			int ret = -1;

			if (audio_info.audio_format != m_stAudioInfo.audio_format ||
				audio_info.chl_layout != m_stAudioInfo.chl_layout ||
				audio_info.sample_rate != m_stAudioInfo.sample_rate)
			{
				m_stAudioInfo = audio_info;
				ret = ResetConvertCtx();
				if (ret != 0)
				{
					Error("Get audio convert context failed");
					return -1;
				}
			}

			int dst_nb_samples = (int)av_rescale_rnd(
				swr_get_delay(m_pConvertCtx, m_stAudioInfo.sample_rate) + nb_samples,
				m_pCodecCtx->sample_rate, m_stAudioInfo.sample_rate, AV_ROUND_UP);

			if (dst_nb_samples <= 0)
			{
				Error("av_rescale_rnd failed.");
				return ret;
			}

			if (dst_nb_samples > m_nResampleBufferSize)
			{
				if (m_pResampleBuffer[0])
					av_freep(&m_pResampleBuffer[0]);
				av_samples_alloc(m_pResampleBuffer, NULL, m_pCodecCtx->channels,
					dst_nb_samples, m_pCodecCtx->sample_fmt, 0);
				m_nResampleBufferSize = dst_nb_samples;
			}

			int ret_samples = swr_convert(m_pConvertCtx, m_pResampleBuffer, dst_nb_samples,
				(const uint8_t**)&audioSamples, nb_samples);
			if (ret_samples <= 0)
			{
				Error("swr_convert failed.");
				return ret;
			}

			if (av_audio_fifo_space(m_pFifoBuffer) >= ret_samples)
			{
				EnterCriticalSection(&m_BufferSection);
				av_audio_fifo_write(m_pFifoBuffer, (void **)m_pResampleBuffer, ret_samples);
				LeaveCriticalSection(&m_BufferSection);
				m_nSavedAudioSamples += nb_samples;
				ret = 0;
			}
			else
			{
				m_nDiscardAudioSamples += nb_samples;
			}

			return ret;
		}
		return -1;
	}


	AVFrame* CAudioRecord::GetOneFrame()
	{
		if (m_bInited)
		{
			if (av_audio_fifo_size(m_pFifoBuffer) >= m_pCodecCtx->frame_size)
			{
				EnterCriticalSection(&m_BufferSection);
				av_audio_fifo_read(m_pFifoBuffer, (void**)m_pFrame->data,
					m_pCodecCtx->frame_size);
				LeaveCriticalSection(&m_BufferSection);
				return m_pFrame;
			}
		}
		return NULL;
	}


	int CAudioRecord::UnInit()
	{
		if (m_bInited)
		{
			Info("Audio: save samples: %lld, discard samples: %lld",
				m_nSavedAudioSamples, m_nDiscardAudioSamples);
			CleanUp();
			m_bInited = false;
			return 0;
		}
		return -1;
	}

	void CAudioRecord::CleanUp()
	{
		if (m_pFrame)
		{
			av_free(m_pFrame);
			m_pFrame = NULL;
		}
		if (m_pFrameBuffer)
		{
			av_free(m_pFrameBuffer);
			m_pFrameBuffer = NULL;
		}
		if (m_pConvertCtx)
		{
			swr_free(&m_pConvertCtx);
			m_pConvertCtx = NULL;
		}
		if (m_pFifoBuffer)
		{
			av_audio_fifo_free(m_pFifoBuffer);
			m_pFifoBuffer = NULL;
		}
		if (m_pResampleBuffer[0])
		{
			av_freep(m_pResampleBuffer);
			memset(m_pResampleBuffer, 0, sizeof(m_pResampleBuffer));
		}

		m_stAudioInfo.Reset();
		m_nResampleBufferSize = 0;
		m_nSavedAudioSamples = 0;
		m_nDiscardAudioSamples = 0;
	}

	
	// Video recorder

	CVideoRecord::CVideoRecord()
		:m_pCodecCtx(NULL),
		m_pConvertCtx(NULL),
		m_pOutVideoFrame(NULL),
		m_pOutPicBuffer(NULL),
		m_pInVideoFrame(NULL),
		m_pInPicBuffer(NULL),
		m_pFifoBuffer(NULL),
		m_nPicSize(0),
		m_nSavedFrames(0),
		m_nDiscardFrames(0)
	{
		m_bInited = false;
	}

	CVideoRecord::~CVideoRecord()
	{
		if (m_bInited)
		{
			UnInit();
		}
	}

	int CVideoRecord::Init(const AVCodecContext* pCodecCtx)
	{
		if (m_bInited)
		{
			Error("Already inited");
			return -1;
		}

		m_pCodecCtx = pCodecCtx;

		m_nPicSize = av_image_get_buffer_size(m_pCodecCtx->pix_fmt, m_pCodecCtx->width, m_pCodecCtx->height, 1);
		m_pOutVideoFrame = av_frame_alloc();
		m_pOutVideoFrame->width = m_pCodecCtx->width;
		m_pOutVideoFrame->height = m_pCodecCtx->height;
		m_pOutVideoFrame->format = AV_PIX_FMT_YUV420P;
		m_pOutPicBuffer = (uint8_t*)av_malloc(m_nPicSize);
		av_image_fill_arrays(m_pOutVideoFrame->data, m_pOutVideoFrame->linesize, m_pOutPicBuffer,
			m_pCodecCtx->pix_fmt, m_pCodecCtx->width, m_pCodecCtx->height, 1);

		m_pInVideoFrame = av_frame_alloc();
		m_pInPicBuffer = (uint8_t*)av_malloc(m_nPicSize);
		m_pInVideoFrame->width = m_pCodecCtx->width;
		m_pInVideoFrame->height = m_pCodecCtx->height;
		m_pInVideoFrame->format = AV_PIX_FMT_YUV420P;
		av_image_fill_arrays(m_pInVideoFrame->data, m_pInVideoFrame->linesize, m_pInPicBuffer, 
			m_pCodecCtx->pix_fmt, m_pCodecCtx->width, m_pCodecCtx->height, 1);

		//申请30帧缓存
		m_pFifoBuffer = av_fifo_alloc(30 * m_nPicSize);

		InitializeCriticalSection(&m_BufferSection);

		m_bInited = true;

		return 0;
	}

	int CVideoRecord::UnInit()
	{
		if (m_bInited)
		{
			Info("Video: saved frames: %lld, discarded frames: %lld\n",
				m_nSavedFrames, m_nDiscardFrames);
			CleanUp();
			m_bInited = false;
			return 0;
		}
		return -1;
	}


	int CVideoRecord::ResetConvertCtx()
	{
		// Init swscontext
		AVPixelFormat av_pix_fmt;
		if (!ConvertToAVPixelFormat(m_stVideoInfo.pix_fmt, av_pix_fmt))
		{
			Error("ResetConvertCtx: convert to av pixel format failed,pix fmt: %d",
				m_stVideoInfo.pix_fmt);
			CleanUp();
			return -1;
		}
		if (m_pConvertCtx)
			sws_freeContext(m_pConvertCtx);

		m_pConvertCtx = sws_getContext(
			m_stVideoInfo.width, m_stVideoInfo.height,
			av_pix_fmt, m_pCodecCtx->width,
			m_pCodecCtx->height, AV_PIX_FMT_YUV420P,
			NULL, NULL,
			NULL, NULL);
		if (!m_pConvertCtx)
		{
			Error("ResetConvertCtx: get video convert context failed");
			CleanUp();
			return -1;
		}
		return 0;
	}


	int CVideoRecord::FillVideo(const void* video_data, const VIDEO_INFO& video_info, int64_t capture_time)
	{
		int ret = -1;
		int64_t begin_time = timeGetTime();
		int64_t scale_time = 0;

		// first check the buffer size
		CheckBufferSpace();

		if (av_fifo_space(m_pFifoBuffer) >= m_nPicSize + (int)sizeof(int64_t))
		{
			if (video_info.width != m_stVideoInfo.width ||
				video_info.height != m_stVideoInfo.height ||
				video_info.pix_fmt != m_stVideoInfo.pix_fmt)
			{
				m_stVideoInfo = video_info;
				ret = ResetConvertCtx();
				if (ret != 0)
				{
					Error("Reset video convert context failed");
					return -1;
				}
			}
			int bytes_per_pix;
			if (m_stVideoInfo.pix_fmt == PIX_FMT_ARGB ||
				m_stVideoInfo.pix_fmt == PIX_FMT_BGRA)
				bytes_per_pix = 4;
			else if (m_stVideoInfo.pix_fmt == PIX_FMT_BGR24 ||
				m_stVideoInfo.pix_fmt == PIX_FMT_RGB24)
				bytes_per_pix = 3;
			else
				return -1;


			uint8_t* srcSlice[3] = { (uint8_t*)video_data, NULL, NULL };
			int srcStride[3] = { bytes_per_pix * m_stVideoInfo.width, 0, 0 };
			sws_scale(m_pConvertCtx, srcSlice, srcStride, 0, m_stVideoInfo.height,
				m_pInVideoFrame->data, m_pInVideoFrame->linesize);
			scale_time = timeGetTime() - begin_time;

			int size = m_pCodecCtx->width * m_pCodecCtx->height;

			EnterCriticalSection(&m_BufferSection);
			av_fifo_generic_write(m_pFifoBuffer, &capture_time, sizeof(int64_t), NULL);
			av_fifo_generic_write(m_pFifoBuffer, m_pInPicBuffer, m_nPicSize, NULL);
			LeaveCriticalSection(&m_BufferSection);
			m_nSavedFrames++;
			ret = 0;
		}
		else
		{
			m_nDiscardFrames++;
		}
		int64_t duration = timeGetTime() - begin_time;
		Debug("FillVideo spend time: %lld, scale time: %lld", duration, scale_time);
		return ret;
	}


	void CVideoRecord::CheckBufferSpace()
	{
		// first check the buffer size
		int space = av_fifo_space(m_pFifoBuffer);
		int used_size = av_fifo_size(m_pFifoBuffer);
		int total_size = space + used_size;
		int ratio = total_size / space;

		if (ratio >= 10)
		{
			// buffer may be insufficient, grow the buffer.
			EnterCriticalSection(&m_BufferSection);
			// grow 1/3
			Debug("total size: %d kb, space: %d kb, used: %d kb, grow: %d kb",
				total_size / 1024, space / 1024, used_size / 1024, total_size / (3 * 1024));
			int ret = av_fifo_grow(m_pFifoBuffer, total_size / 3);
			if (ret < 0)
				Debug("grow failed");
			else
				Debug("grow succeed!");

			LeaveCriticalSection(&m_BufferSection);
		}
		else if (ratio <= 2)
		{
			// buffer is too big, resize it 
			EnterCriticalSection(&m_BufferSection);
			// shrink 1 / 3
			Debug("total size: %d kb, space: %d kb, used: %d kb, shrink: %d kb",
				total_size / 1024, space / 1024, used_size / 1024, total_size / (3 * 1024));
			int ret = av_fifo_realloc2(m_pFifoBuffer, total_size * 2 / 3);
			if (ret < 0)
				Debug("shrink failed!");
			else
				Debug("shrink succeed!");

			LeaveCriticalSection(&m_BufferSection);
		}
	}


	VIDEO_FRAME* CVideoRecord::GetOneFrame()
	{
		if (av_fifo_size(m_pFifoBuffer) >= m_nPicSize + (int)sizeof(int64_t))
		{
			int64_t nCaptureTime;
			EnterCriticalSection(&m_BufferSection);
			av_fifo_generic_read(m_pFifoBuffer, &nCaptureTime, sizeof(int64_t), 0);
			av_fifo_generic_read(m_pFifoBuffer, m_pOutPicBuffer, m_nPicSize, 0);
			LeaveCriticalSection(&m_BufferSection);
			m_VideoFrame.nCaptureTime = nCaptureTime;
			m_VideoFrame.pAvFrame = m_pOutVideoFrame;
			return &m_VideoFrame;
		}
		return NULL;
	}

	void CVideoRecord::CleanUp()
	{
		if (m_pConvertCtx)
		{
			sws_freeContext(m_pConvertCtx);
			m_pConvertCtx = NULL;
		}
		if (m_pOutVideoFrame)
		{
			av_free(m_pOutVideoFrame);
			m_pOutVideoFrame = NULL;
		}
		if (m_pOutPicBuffer)
		{
			av_free(m_pOutPicBuffer);
			m_pOutPicBuffer = NULL;
		}
		if (m_pInVideoFrame)
		{
			av_free(m_pInVideoFrame);
			m_pInVideoFrame = NULL;
		}
		if (m_pInPicBuffer)
		{
			av_free(m_pInPicBuffer);
			m_pInPicBuffer = NULL;
		}
		if (m_pFifoBuffer)
		{
			av_fifo_free(m_pFifoBuffer);
			m_pFifoBuffer = NULL;
		}
		m_stVideoInfo.Reset();
		m_nPicSize = 0;
		m_nSavedFrames = 0;
		m_nDiscardFrames = 0;

	}

	bool ConvertToAVSampleFormat(AUDIO_FORMAT audio_format, AVSampleFormat& av_sample_fmt)
	{
		bool bRet = true;
		AVSampleFormat target_format = AV_SAMPLE_FMT_NONE;
		switch (audio_format)
		{
		case AUDIO_FORMAT_U8BIT:
			av_sample_fmt = AV_SAMPLE_FMT_U8;
			break;
		case AUDIO_FORMAT_16BIT:
			av_sample_fmt = AV_SAMPLE_FMT_S16;
			break;
		case AUDIO_FORMAT_32BIT:
			av_sample_fmt = AV_SAMPLE_FMT_S32;
			break;
		case AUDIO_FORMAT_FLOAT:
			av_sample_fmt = AV_SAMPLE_FMT_FLT;
			break;
		case AUDIO_FORMAT_U8BIT_PLANAR:
			av_sample_fmt = AV_SAMPLE_FMT_U8P;
			break;
		case AUDIO_FORMAT_16BIT_PLANAR:
			av_sample_fmt = AV_SAMPLE_FMT_S16P;
			break;
		case AUDIO_FORMAT_32BIT_PLANAR:
			av_sample_fmt = AV_SAMPLE_FMT_S32P;
			break;
		case AUDIO_FORMAT_FLOAT_PLANAR:
			av_sample_fmt = AV_SAMPLE_FMT_FLTP;
			break;
		default:
			bRet = false;
		}
		return bRet;
	}

	bool ConvertToAVChannelLayOut(MediaFileRecorder::CHANNEL_LAYOUT channel_lay_out, int64_t& av_chl_layout)
	{
		bool bRet = true;
		switch (channel_lay_out)
		{
		case SPEAKERS_MONO:
			av_chl_layout = AV_CH_LAYOUT_MONO;
			break;
		case SPEAKERS_STEREO:
			av_chl_layout = AV_CH_LAYOUT_STEREO;
			break;
		case SPEAKERS_2POINT1:
			av_chl_layout = AV_CH_LAYOUT_2_1;
			break;
		case SPEAKERS_QUAD:
			av_chl_layout = AV_CH_LAYOUT_QUAD;
			break;
		case SPEAKERS_4POINT1:
			av_chl_layout = AV_CH_LAYOUT_4POINT1;
			break;
		case SPEAKERS_5POINT1:
			av_chl_layout = AV_CH_LAYOUT_5POINT1;
			break;
		case SPEAKERS_5POINT1_SURROUND:
			av_chl_layout = AV_CH_LAYOUT_5POINT1_BACK;
			break;
		case SPEAKERS_7POINT1:
			av_chl_layout = AV_CH_LAYOUT_7POINT1;
			break;
		case SPEAKERS_7POINT1_SURROUND:
			av_chl_layout = AV_CH_LAYOUT_7POINT1_WIDE_BACK;
			break;
		case SPEAKERS_SURROUND:
			av_chl_layout = AV_CH_LAYOUT_SURROUND;
			break;
		default:
			bRet = false;
		}
		return bRet;
	}

	bool ConvertToAVPixelFormat(PIX_FMT pix_fmt, AVPixelFormat& av_pix_fmt)
	{
		bool bRet = true;
		switch (pix_fmt)
		{
		case PIX_FMT::PIX_FMT_ARGB:
			av_pix_fmt = AV_PIX_FMT_ARGB;
			break;
		case PIX_FMT::PIX_FMT_BGRA:
			av_pix_fmt = AV_PIX_FMT_BGRA;
			break;
		case PIX_FMT::PIX_FMT_BGR24:
			av_pix_fmt = AV_PIX_FMT_BGR24;
			break;
		case PIX_FMT::PIX_FMT_RGB24:
			av_pix_fmt = AV_PIX_FMT_RGB24;
			break;
		default:
			bRet = false;
			break;
		}

		return bRet;
	}

	uint32_t get_audio_channels(CHANNEL_LAYOUT chl_layout)
	{
		switch (chl_layout) {
		case SPEAKERS_MONO:             return 1;
		case SPEAKERS_STEREO:           return 2;
		case SPEAKERS_2POINT1:          return 3;
		case SPEAKERS_SURROUND:
		case SPEAKERS_QUAD:             return 4;
		case SPEAKERS_4POINT1:          return 5;
		case SPEAKERS_5POINT1:
		case SPEAKERS_5POINT1_SURROUND: return 6;
		case SPEAKERS_7POINT1:          return 8;
		case SPEAKERS_7POINT1_SURROUND: return 8;
		case SPEAKERS_UNKNOWN:          return 0;
		}

		return 0;
	}

	static inline size_t GetAudioPlanes(AUDIO_FORMAT audio_fmt, CHANNEL_LAYOUT chl_layout)
	{
		return (IsAudioPlanar(audio_fmt) ? get_audio_channels(chl_layout) : 1);
	}

	static inline bool IsAudioPlanar(AUDIO_FORMAT format)
	{
		switch (format) {
		case AUDIO_FORMAT_U8BIT:
		case AUDIO_FORMAT_16BIT:
		case AUDIO_FORMAT_32BIT:
		case AUDIO_FORMAT_FLOAT:
			return false;

		case AUDIO_FORMAT_U8BIT_PLANAR:
		case AUDIO_FORMAT_FLOAT_PLANAR:
		case AUDIO_FORMAT_16BIT_PLANAR:
		case AUDIO_FORMAT_32BIT_PLANAR:
			return true;

		case AUDIO_FORMAT_UNKNOWN:
			return false;
		}

		return false;
	}



	IMediaFileRecorder* CreateMediaFileRecorder()
	{
		IMediaFileRecorder* pMediaFileRecorder = new CMediaFileRecorder();
		return pMediaFileRecorder;
	}

	void DestroyMediaFileRecorder(IMediaFileRecorder* pMediaFileRecorder)
	{
		delete pMediaFileRecorder;
	}

}

