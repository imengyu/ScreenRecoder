#ifndef IAUDIOCAPTURE_H
#define IAUDIOCAPTURE_H

#include "MediaRecordTypeDef.h"

namespace MediaFileRecorder
{
	enum DEV_TYPE
	{
		MICROPHONE,
		SPEAKER,
	};

	class IAudioCaptureDataCb
	{
	public:
		virtual void OnCapturedData(const void* audioSamples, int nSamples, 
			DEV_TYPE devType, const AUDIO_INFO& audioInfo) = 0;

	protected:
		virtual ~IAudioCaptureDataCb(){};
	};

	class IAudioCapture
	{
	public:
		virtual int RegisterCaptureDataCb(IAudioCaptureDataCb* pDataCb) = 0;
		virtual int UnRegisterCaptureDataCb(IAudioCaptureDataCb* pDataCb) = 0;
		virtual int SetDev(int index) = 0;
		virtual int StartCapture() = 0;
		virtual int StopCapture() = 0;

		virtual ~IAudioCapture(){};
	};

	IAudioCapture* CreateAudioCapture(DEV_TYPE devType);
	void DestroyAudioCatpure(IAudioCapture* pAudioCapture);
}

#endif