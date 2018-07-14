#include "stdafx.h"
#include "CScreenAudioRecord.h"
#include "log.h"
namespace MediaFileRecorder
{

	CScreenAudioRecord::CScreenAudioRecord()
		:m_nRecordState(NOT_BEGIN),
		m_bMicRecording(false),
		m_bSpeakerRecording(false),
		m_bVideoRecording(false)
	{
		m_pFileRecorder = CreateMediaFileRecorder();
		m_pScreenGrabber = CreateScreenGrabber();
		m_pMicAudioCapturer = CreateAudioCapture(MICROPHONE);
		m_pSpeakerAudioCapturer = CreateAudioCapture(SPEAKER);

		m_pScreenGrabber->RegisterDataCb(this);
		m_pMicAudioCapturer->RegisterCaptureDataCb(this);
		m_pSpeakerAudioCapturer->RegisterCaptureDataCb(this);
	}

	CScreenAudioRecord::~CScreenAudioRecord()
	{
		if (m_nRecordState != NOT_BEGIN)
		{
			CleanUp();
		}
		m_pScreenGrabber->UnRegisterDataCb(this);
		m_pScreenGrabber->UnRegisterDataCb(this);
		DestroyMediaFileRecorder(m_pFileRecorder);
		DestroyScreenGrabber(m_pScreenGrabber);
		DestroyAudioCatpure(m_pMicAudioCapturer);
		DestroyAudioCatpure(m_pSpeakerAudioCapturer);
	}

	int CScreenAudioRecord::SetRecordInfo(const RECORD_INFO& recordInfo)
	{
		if (m_nRecordState != NOT_BEGIN)
		{
			Error("SetRecordInfo: State not right,state: %d", m_nRecordState);
			return -1;
		}

		m_stRecordInfo = recordInfo;

		return 0;
	}

	int CScreenAudioRecord::CheckRecordInfo()
	{
		const char* fileName = m_stRecordInfo.file_name;
		if (strlen(fileName) <= 4
			|| ((m_stRecordInfo.format == VIDEO_FORMAT_H264_MP4 || m_stRecordInfo.format == VIDEO_FORMAT_UNKOWN || m_stRecordInfo.format == VIDEO_FORMAT_MPEG4_MP4) && strcmp(fileName + strlen(fileName) - 4, ".mp4") != 0)
			|| ((m_stRecordInfo.format == VIDEO_FORMAT_H264_AVI || m_stRecordInfo.format == VIDEO_FORMAT_MPEG4_AVI) && strcmp(fileName + strlen(fileName) - 4, ".avi") != 0)
			|| (m_stRecordInfo.format == VIDEO_FORMAT_FLV && strcmp(fileName + strlen(fileName) - 4, ".flv") != 0)
			|| (m_stRecordInfo.format == VIDEO_FORMAT_WMV && strcmp(fileName + strlen(fileName) - 4, ".wmv") != 0))
		{
			Error("File name not invalid, file name: %s", fileName);
			return -1;
		}

		if (m_stRecordInfo.is_record_video)
		{
			const RECT& captureRect = m_stRecordInfo.video_capture_rect;
			if (captureRect.Width() <= 0 || captureRect.Height() <= 0)
			{
				Error("Capture rect not right, left: %d, right: %d, top: %d, bottom: %d",
					captureRect.left, captureRect.right, captureRect.top, captureRect.bottom);
				return -1;
			}

			if (m_stRecordInfo.video_dst_width < 4)
				m_stRecordInfo.video_dst_width = 4;
			if (m_stRecordInfo.video_dst_height < 4)
				m_stRecordInfo.video_dst_height = 4;

			m_stRecordInfo.video_dst_width = (m_stRecordInfo.video_dst_width / 4) * 4;
			m_stRecordInfo.video_dst_height = (m_stRecordInfo.video_dst_height / 4) * 4;

			if (m_stRecordInfo.video_dst_width <= 0 || m_stRecordInfo.video_dst_height <= 0)
			{
				Error("Target width or height not right, dst width: %d, dst height: %d",
					m_stRecordInfo.video_dst_width, m_stRecordInfo.video_dst_height);
				return -1;
			}

			if (m_stRecordInfo.video_frame_rate <= 0)
			{
				Error("Capture framerate not right, frame_rate: %d", m_stRecordInfo.video_frame_rate);
				return -1;
			}
		}

		const RECT& captureRect = m_stRecordInfo.video_capture_rect;
		Info("Record Info: record video: %d, record mic: %d, record speaker: %d; "
			"capture_rect: left: %d, right: %d, top: %d, bottom: %d; frame_rate: %d;"
			"dst width: %d, dst height: %d; quality: %d; file_name %s",
			m_stRecordInfo.is_record_video, m_stRecordInfo.is_record_mic, 
			m_stRecordInfo.is_record_speaker, captureRect.left, captureRect.right,
			captureRect.top, captureRect.bottom, m_stRecordInfo.video_frame_rate, 
			m_stRecordInfo.video_dst_width, m_stRecordInfo.video_dst_height,
			m_stRecordInfo.quality, m_stRecordInfo.file_name);
		
		return 0;

	}

	int CScreenAudioRecord::StartRecord()
	{
		int ret = 0;
		if (m_nRecordState != NOT_BEGIN)
		{
			Error("StartRecord: State not right, state: %d", m_nRecordState);
			ret |= STATE_NOT_RIGHT;
			return ret;
		}

		if (CheckRecordInfo() != 0)
		{
			Error("Parameter invalid");
			ret |= PARAMETER_INVALID;
			return ret;
		}

		StartCapture(ret);

		if (m_pFileRecorder->Init(m_stRecordInfo) != 0)
		{
			Error("Init media file recorder failed");
			ret |= INIT_MEDIA_FILE_RECORDER_FAILED;
			CleanUp();
			return ret;
		}
		
		if (m_pFileRecorder->Start() != 0)
		{
			Error("Start media file recorder failed");
			ret |= START_MEDIA_FILE_RECORDER_FAILED;
			CleanUp();
			return ret;
		}

		m_nRecordState = RECORDING;

		return ret;
	}

	int CScreenAudioRecord::SuspendRecord()
	{
		if (m_nRecordState != RECORDING)
		{
			Error("SuspendRecord: State not right, state: %d", m_nRecordState);
			return -1;
		}
		StopCapture();
		m_pFileRecorder->Stop();
		m_nRecordState = SUSPENDED;
		Info("Suspend record!");
		return 0;
	}

	int CScreenAudioRecord::ResumeRecord()
	{
		if (m_nRecordState != SUSPENDED)
		{
			Error("ResumeRecord: State not right,state: %d", m_nRecordState);
			return -1;
		}
		int ret = 0;
		StartCapture(ret);
		m_pFileRecorder->Start();
		m_nRecordState = RECORDING;
		Info("Resume record!");
		return ret;
	}

	int CScreenAudioRecord::StopRecord()
	{
		if (m_nRecordState == NOT_BEGIN)
		{
			Error("StopRecord: State not right,state: %d", m_nRecordState);
			return -1;
		}
		CleanUp();
		m_nRecordState = NOT_BEGIN;
		Info("Stop record!");
		return 0;
	}

	int CScreenAudioRecord::GetState()
	{
		return m_nRecordState;
	}

	void CScreenAudioRecord::OnScreenData(void* data, const VIDEO_INFO& videoInfo)
	{
		if (m_nRecordState == RECORDING)
		{
			m_pFileRecorder->FillVideo(data, videoInfo);
		}
	}

	void CScreenAudioRecord::OnCapturedData(const void* audioSamples, int nSamples, 
		DEV_TYPE devType, const AUDIO_INFO& audioInfo)
	{
		if (m_nRecordState == RECORDING)
		{
			if (devType == MICROPHONE)
			{
				m_pFileRecorder->FillMicAudio(audioSamples, nSamples, audioInfo);
			}
			else if (devType == SPEAKER)
			{
				m_pFileRecorder->FillSpeakerAudio(audioSamples, nSamples, audioInfo);
			}
		}
	}

	void CScreenAudioRecord::CleanUp()
	{
		StopCapture();
		m_pFileRecorder->Stop();
		m_pFileRecorder->UnInit();
	}

	void CScreenAudioRecord::StartCapture(int& ret)
	{
		if (m_stRecordInfo.is_record_mic)
		{
			m_bMicRecording = (m_pMicAudioCapturer->StartCapture() == 0);
			if (!m_bMicRecording)
			{
				Error("Start mic capture failed");
				ret |= START_MIC_CAPTURE_FAILED;
			}
		}

		if (m_stRecordInfo.is_record_speaker)
		{
			m_bSpeakerRecording = (m_pSpeakerAudioCapturer->StartCapture() == 0);
			if (!m_bSpeakerRecording)
			{
				Error("Start speaker capture failed");
				ret |= START_SPEAKER_CAPTURE_FAILED;
			}
		}

		if (m_stRecordInfo.is_record_video)
		{
			m_pScreenGrabber->SetGrabFrameRate(m_stRecordInfo.video_frame_rate);
			m_pScreenGrabber->SetGrabRect(m_stRecordInfo.video_capture_rect);

			m_bVideoRecording = (m_pScreenGrabber->StartGrab() == 0);
			if (!m_bVideoRecording)
			{
				Error("Start screen capture failed");
				ret |= START_SCRREEN_CAPTURE_FAILED;
			}
		}
	}

	void CScreenAudioRecord::StopCapture()
	{
		if (m_bMicRecording)
		{
			m_pMicAudioCapturer->StopCapture();
			m_bMicRecording = false;
		}
		if (m_bSpeakerRecording)
		{
			m_pSpeakerAudioCapturer->StopCapture();
			m_bSpeakerRecording = false;
		}
			
		if (m_bVideoRecording)
		{
			m_pScreenGrabber->StopGrab();
			m_bVideoRecording = false;
		}	
	}

	IScreenAudioRecord* CreateScreenAudioRecorder()
	{
		IScreenAudioRecord* pRecorder = new CScreenAudioRecord();
		return pRecorder;
	}

	void DestroyScreenAudioRecorder(IScreenAudioRecord* pRecorder)
	{
		delete pRecorder;
	}

	void SetLogCallback(sdk_log_cb_t cb)
	{
		set_log_func(cb);
	}
}