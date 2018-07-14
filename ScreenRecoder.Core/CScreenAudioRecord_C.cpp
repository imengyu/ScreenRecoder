#include "stdafx.h"
#include "IScreeAudioRecord.h"
#include "IScreenAudioRecord_C.h"
#include "log.h"
#include "strsafe.h"

using namespace MediaFileRecorder;

RECORDAPI int MR_Add(int a, int b)
{
	return a + b;
}

RECORDAPI void MR_SetLogCallBack(MediaFileRecorder::sdk_log_cb_t cb)
{
	set_log_func(cb);
}


RECORDAPI void* MR_CreateScreenAudioRecorder()
{
	return (void*)CreateScreenAudioRecorder();
}


RECORDAPI void MR_DestroyScreenAudioRecorder(void* pObject)
{
	DestroyScreenAudioRecorder((IScreenAudioRecord*)pObject);
}


RECORDAPI int MR_SetRecordInfo(void* pObject, const MediaFileRecorder::RECORD_INFO& recordInfo)
{
	IScreenAudioRecord* pRecorder = (IScreenAudioRecord*)pObject;
	if (pRecorder)
	{
		return pRecorder->SetRecordInfo(recordInfo);
	}
	return -1;
}
RECORDAPI int MR_GetRecordState(void* pObject)
{
	IScreenAudioRecord* pRecorder = (IScreenAudioRecord*)pObject;
	if (pRecorder)
	{
		return pRecorder->GetState();
	}
	return -1;
}

RECORDAPI int MR_StartRecord(void* pObject)
{
	IScreenAudioRecord* pRecorder = (IScreenAudioRecord*)pObject;
	if (pRecorder)
	{
		return pRecorder->StartRecord();
	}
	return -1;
}


RECORDAPI int MR_SuspendRecord(void* pObject)
{
	IScreenAudioRecord* pRecorder = (IScreenAudioRecord*)pObject;
	if (pRecorder)
	{
		return pRecorder->SuspendRecord();
	}
	return -1;
}


RECORDAPI int MR_ResumeRecord(void* pObject)
{
	IScreenAudioRecord* pRecorder = (IScreenAudioRecord*)pObject;
	if (pRecorder)
	{
		return pRecorder->ResumeRecord();
	}
	return -1;
}


RECORDAPI int MR_StopRecord(void* pObject)
{
	IScreenAudioRecord* pRecorder = (IScreenAudioRecord*)pObject;
	if (pRecorder)
	{
		return pRecorder->StopRecord();
	}
	return -1;
}