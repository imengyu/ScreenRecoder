#ifndef ISCREENAUDIORECORD_C_H
#define ISCREENAUDIORECORD_C_H

#ifdef SCREEN_AUDIO_RECORDER_EXPORTS
#define RECORDAPI __declspec(dllexport)
#else
#define RECORDAPI __declspec(dllimport)
#endif

#include "MediaRecordTypeDef.h"

extern "C"
{
	RECORDAPI int  MR_Add(int a, int b);

	RECORDAPI void MR_SetLogCallBack(MediaFileRecorder::sdk_log_cb_t cb);

	RECORDAPI void* MR_CreateScreenAudioRecorder();
	
	RECORDAPI void MR_DestroyScreenAudioRecorder(void* pObject);

	RECORDAPI int MR_SetRecordInfo(void* pObject, const MediaFileRecorder::RECORD_INFO& recordInfo);

	RECORDAPI int MR_StartRecord(void* pObject);

	RECORDAPI int MR_GetRecordState(void* pObject);

	RECORDAPI int MR_SuspendRecord(void* pObject);

	RECORDAPI int MR_ResumeRecord(void* pObject);

	RECORDAPI int MR_StopRecord(void* pObject);
}
#endif