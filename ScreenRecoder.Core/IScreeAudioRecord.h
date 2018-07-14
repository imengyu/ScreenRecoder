#ifndef ISCREENAUDIORECORD_H
#define ISCREENAUDIORECORD_H

#include "MediaRecordTypeDef.h"

#ifdef SCREEN_AUDIO_RECORDER_EXPORTS
#define RECORDAPI __declspec(dllexport)
#else
#define RECORDAPI __declspec(dllimport)
#endif

namespace MediaFileRecorder
{
	class IScreenAudioRecord
	{
	public:
		virtual int SetRecordInfo(const RECORD_INFO& recordInfo) = 0;
		virtual int StartRecord() = 0;
		virtual int SuspendRecord() = 0;
		virtual int ResumeRecord() = 0;
		virtual int StopRecord() = 0;
		virtual int GetState() = 0;
		virtual ~IScreenAudioRecord(){}
	};


	RECORDAPI void SetLogCallback(sdk_log_cb_t cb);
	RECORDAPI IScreenAudioRecord* CreateScreenAudioRecorder();
	RECORDAPI void DestroyScreenAudioRecorder(IScreenAudioRecord* pRecorder);
}

#endif