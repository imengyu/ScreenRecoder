#ifndef MEDIAFILERECORDER_H
#define MEDIAFILERECORDER_H

#ifdef	__cplusplus
extern "C"
{
#endif
#include "libavcodec/avcodec.h"
#include "libavformat/avformat.h"
#include "libswscale/swscale.h"
#include "libavdevice/avdevice.h"
#include "libavutil/audio_fifo.h"
#include "libswresample/swresample.h"
#include "libavutil/imgutils.h"

#pragma comment(lib, "avcodec.lib")
#pragma comment(lib, "avformat.lib")
#pragma comment(lib, "avutil.lib")
#pragma comment(lib, "avdevice.lib")
#pragma comment(lib, "avfilter.lib")
#pragma comment(lib, "swresample.lib")
	//#pragma comment(lib, "avfilter.lib")
	//#pragma comment(lib, "postproc.lib")
	//#pragma comment(lib, "swresample.lib")
#pragma comment(lib, "swscale.lib")
#ifdef __cplusplus
};
#endif

#include "IMediaFileRecorder.h"
#include <thread>
#include <atomic>
#include <vector>
#include <list>
#include <memory>

#define MAX_AV_PLANES 8

namespace MediaFileRecorder
{
	class CVideoRecord;
	class CAudioRecord;

	class CMediaFileRecorder : public IMediaFileRecorder
	{
	public:
		CMediaFileRecorder();
		~CMediaFileRecorder();
		int Init(const RECORD_INFO& record_info) override;
		int UnInit() override;
		int Start() override;
		int Stop() override;
		int FillVideo(const void* data, const VIDEO_INFO& video_info) override;
		int FillMicAudio(const void* audioSamples, int nb_samples, const AUDIO_INFO& audio_info) override;
		int FillSpeakerAudio(const void* audioSamples, int nb_samples, const AUDIO_INFO& audio_info) override;

	private:
		int InitVideoRecord();
		void UnInitVideoRecord();
		int InitAudioRecord();
		void UnInitAudioRecord();
		void StartWriteFileThread();
		void StopWriteFileThread();
		void VideoWriteFileThreadProc();
		void AuidoWriteFileThreadProc();

		void CleanUp();
		void VideoCleanUp();
		void AudioCleanUp();

		void EncodeAndWriteVideo();
		void EncodeAndWriteAudio();

		void MixAudio(AVFrame* pDstFrame, const AVFrame* pSrcFrame);

	private:
		bool m_bInited;
		std::atomic_bool m_bRun;
		RECORD_INFO m_stRecordInfo;
		AVFormatContext* m_pFormatCtx;
		AVCodecContext* m_pVideoCodecCtx;
		AVCodecContext* m_pAudioCodecCtx;
		AVPacket* m_pVideoPacket;
		AVPacket* m_pAudioPacket;

		uint32_t m_nVideoStreamIndex;
		uint32_t m_nAudioStreamIndex;

		int64_t m_nStartTime;
		int64_t m_nDuration;

		int64_t m_nVideoPacketIndex;
		int64_t m_nAudioPacketIndex;

		std::shared_ptr<CVideoRecord> m_pVideoRecorder;
		std::shared_ptr<CAudioRecord> m_pMicRecorder;
		std::shared_ptr<CAudioRecord> m_pSpeakerRecorder;

		std::thread m_WriteVideoThread;
		std::thread m_WriteAudioThread;

		CRITICAL_SECTION m_WriteFileSection;

		int m_nMainAudioStream;
	};

	class CAudioRecord
	{
	public:
		CAudioRecord();
		~CAudioRecord();
		int Init(const AVCodecContext* pCodecCtx);
		int UnInit();
		int FillAudio(const void* audioSamples, int nb_samples, const AUDIO_INFO& audio_info);
		AVFrame* GetOneFrame();
	private:
		int ResetConvertCtx();
		void CleanUp();
	private:
		std::atomic_bool m_bInited;
		const AVCodecContext* m_pCodecCtx;
		AUDIO_INFO m_stAudioInfo;
		SwrContext* m_pConvertCtx;
		AVAudioFifo* m_pFifoBuffer;
		AVFrame* m_pFrame;
		uint8_t* m_pFrameBuffer;
		uint8_t* m_pResampleBuffer[MAX_AV_PLANES];
		int m_nResampleBufferSize;
		CRITICAL_SECTION m_BufferSection;
		int64_t m_nSavedAudioSamples;
		int64_t m_nDiscardAudioSamples;
	};

	struct VIDEO_FRAME
	{
		int64_t nCaptureTime;
		AVFrame* pAvFrame;
	};
	class CVideoRecord
	{
	public:
		CVideoRecord();
		~CVideoRecord();
		int Init(const AVCodecContext* pCodecCtx);
		int UnInit();
		int FillVideo(const void* video_data, const VIDEO_INFO& video_info, int64_t capture_time);
		VIDEO_FRAME* GetOneFrame();
	private:
		int ResetConvertCtx();
		void CheckBufferSpace();
		void CleanUp();
	private:
		std::atomic_bool m_bInited;
		const AVCodecContext* m_pCodecCtx;
		VIDEO_INFO m_stVideoInfo;
		SwsContext* m_pConvertCtx;
		AVFrame* m_pOutVideoFrame;
		uint8_t* m_pOutPicBuffer;
		AVFrame* m_pInVideoFrame;
		uint8_t* m_pInPicBuffer;
		VIDEO_FRAME m_VideoFrame;
		int m_nPicSize;
		AVFifoBuffer* m_pFifoBuffer;
		CRITICAL_SECTION m_BufferSection;
		int64_t m_nSavedFrames;
		int64_t m_nDiscardFrames;
	};


	static inline bool ConvertToAVSampleFormat(AUDIO_FORMAT audio_fmt, AVSampleFormat& av_sample_fmt);
	static inline bool ConvertToAVChannelLayOut(CHANNEL_LAYOUT channel_lay_out, int64_t& av_chl_layout);
	static inline bool ConvertToAVPixelFormat(PIX_FMT pix_fmt, AVPixelFormat& av_pix_fmt);
	static inline uint32_t GetAudioChannels(CHANNEL_LAYOUT chl_layout);
	static inline size_t GetAudioPlanes(AUDIO_FORMAT audio_fmt, CHANNEL_LAYOUT chl_layout);
	static inline bool IsAudioPlanar(AUDIO_FORMAT audio_fmt);
}



#endif