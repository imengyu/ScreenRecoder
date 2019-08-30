#include "stdafx.h"
#include "Recorder.h"
#include "StringHlp.h"
#include "App.h"
#include <mmsystem.h>
#include <mmdeviceapi.h>
#include <audioclient.h>
#include <propsys.h>
#include <vector>
#include <atomic>
#include <thread>
#include <atlbase.h>
#include <Functiondiscoverykeys_devpkey.h>

//
// 录制底层承载类
//

extern App*app;

int64_t startCaptureTime = 0;
int64_t recorderDuration = 0;
void* m_pRecorder = NULL;

int capture_left, capture_top, capture_width, capture_height;
int capture_frame_rate = 15;
bool capture_recorder_speaker = true;
bool capture_recorder_mic = true;
int capture_recorder_quality = 1;
int capture_recorder_format = 0;
int capture_recorder_mic_dev_index = 0;
wchar_t capture_out_file[512];
wchar_t capture_last_out_file[512];
wchar_t capture_out_dir[MAX_PATH];
wchar_t capture_last_error[256];
int capture_cpu_count = 1;

void recorder_log_cb(MediaFileRecorder::SDK_LOG_LEVEL level, const wchar_t* msg)
{
#if _DEBUG
	if (level >= MediaFileRecorder::LOG_INFO)
	{
		char *astr = UnicodeToAnsi(msg);
		fwrite(astr, 1, strlen(astr) + 1, stdout);
		printf_s("\n");
		free(astr);
		OutputDebugString(msg);
		OutputDebugString(L"\n");
	}
#else
	if (level > MediaFileRecorder::LOG_INFO)
	{
		char *astr = UnicodeToAnsi(msg);
		fwrite(astr, 1, strlen(astr) + 1, stdout);
		printf_s("\n");
		free(astr);
		OutputDebugString(msg);
		OutputDebugString(L"\n");
	}
#endif
	if (level >= MediaFileRecorder::LOG_ERROR)
	{
		int len = wcslen(msg);
		wcsncpy_s(capture_last_error, msg, len > 256 ? 256 : len);
	}
}

void Recorder::InitICalls()
{

	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::Destroy", &Destroy);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::Create", &Create);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::SetUpdateCallback", &SetUpdateCallback);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::PauseButton", &PauseButton);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::StopButton", &StopButton);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::StartButton", &StartButton);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::SetNextOutFileName", &SetNextOutFileName);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::SetCaptureFrameRate", &SetCaptureFrameRate);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::SetCaptureRect", &SetCaptureRect);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::SetOutFileDir", &SetOutFileDir);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::DrawPreviewRect", &DrawPreviewRect);

	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::get_IsRecording", &IsRecording);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::get_IsInterrupt", &IsInterrupt);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::get_Duration", &GetRecSec);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::get_LastError", &GetRecorderLastError);

	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::get_State", &GetState);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::get_RecordSound", &GetRecSound);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::set_RecordSound", &SetRecSound);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::get_RecordMouse", &GetRecMic);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::set_RecordMouse", &SetRecMic);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::get_RecordQuality", &GetRecQua);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::set_RecordQuality", &SetRecQua);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::get_RecordFormat", &GetRecFormat);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::set_RecordFormat", &SetRecFormat);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::get_RecordMicDevIndex", &GetRecMicDevIndex);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::set_RecordMicDevIndex", &SetRecMicDevIndext);

	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::GetLastOutFileName", &GetLastOutFileName);
	mono_add_internal_call("ScreenRecoder.App.Core.Recorder::GetAudioCaptureDevices", &GetAudioCaptureDevices);
}

LONG Recorder::GetRecSec() {
	return static_cast<LONG>(recorderDuration / 1000);
}
BOOL Recorder::IsRecording() {
	return GetState() == RECORD_STATE::RECORDING;
}
BOOL Recorder::IsInterrupt() {
	return GetState() == RECORD_STATE::SUSPENDED;
}
void Recorder::Destroy() {
	MR_DestroyScreenAudioRecorder(m_pRecorder);
}
void Recorder::Create() 
{
	m_pRecorder = MR_CreateScreenAudioRecorder();
	MR_SetLogCallBack(recorder_log_cb);

	SYSTEM_INFO sysInfo;
	GetSystemInfo(&sysInfo);

	capture_cpu_count = sysInfo.dwNumberOfProcessors;
}
RECORD_STATE Recorder::GetState() {
	int n = MR_GetRecordState(m_pRecorder);
	return (RECORD_STATE)n;
}
void Recorder::SetUpdateCallback(UpdateCallback updateCallback)
{
	//m_UpdateCallback = updateCallback;
}
BOOL Recorder::PauseButton()
{
	int ret = 0;
	RECORD_STATE state = GetState();
	if (state > RECORD_STATE::NOT_BEGIN)
	{
		if (state == RECORD_STATE::RECORDING)
		{
			ret = MR_SuspendRecord(m_pRecorder);
			recorderDuration += timeGetTime() - startCaptureTime;

			return ret == 0;
		}
		else
		{
			ret = MR_ResumeRecord(m_pRecorder);
			startCaptureTime = timeGetTime();

			return ret == 0;
		}
	}
	return false;
}
BOOL Recorder::StopButton()
{
	RECORD_STATE state = GetState();
	int ret = 0;
	if (state > RECORD_STATE::NOT_BEGIN) {

		if (state!= RECORD_STATE::SUSPENDED)
			recorderDuration += timeGetTime() - startCaptureTime;

		ret = MR_StopRecord(m_pRecorder);
		return ret == 0;
	}
	return false;
}
BOOL Recorder::StartButton()
{
	if (GetState() == RECORD_STATE::NOT_BEGIN)
	{
		MediaFileRecorder::RECT grab_rect;
		grab_rect.left = capture_left;
		grab_rect.top = capture_top;
		grab_rect.right = capture_left + capture_width;
		grab_rect.bottom = capture_top + capture_height;

		MediaFileRecorder::RECORD_INFO record_info;

		if (wcslen(capture_out_file) == 0)
		{
			LPCWSTR videoFileExt = L"mp4";
			switch (capture_recorder_format)
			{
			case MediaFileRecorder::VIDEO_FORMAT_MPEG4_AVI:
			case MediaFileRecorder::VIDEO_FORMAT_H264_AVI: videoFileExt = L"avi"; break;
			case MediaFileRecorder::VIDEO_FORMAT_FLV: videoFileExt = L"flv"; break;
			case MediaFileRecorder::VIDEO_FORMAT_H264_MP4:
			case MediaFileRecorder::VIDEO_FORMAT_MPEG4_MP4:
			case MediaFileRecorder::VIDEO_FORMAT_WMV: videoFileExt = L"mp4"; break;
			default:
				break;
			}

			SYSTEMTIME sys;
			GetLocalTime(&sys);
			WCHAR path[MAX_PATH];
			if (wcscmp(capture_out_dir, L"") == 0) 
				wsprintf(path, L"%s\\output\\%04d%02d%02d%02d%02d%02d.%s", app->startDir, sys.wYear, sys.wMonth, sys.wDay, sys.wHour, sys.wMinute, sys.wSecond, videoFileExt);
			else 
				wsprintf(path, L"%s\\%04d%02d%04d%02d%02d%02d%02d.%s", capture_out_dir, sys.wYear, sys.wMonth, sys.wDay, sys.wHour, sys.wMinute, sys.wSecond, videoFileExt);

			wcscpy_s(capture_last_out_file, path);

			char*pathUtf8 = UnicodeToUtf8(path);
			strcpy_s(record_info.file_name, pathUtf8);
			StrDel(pathUtf8);
		}
		else {
			char*pathUtf8 = UnicodeToUtf8(capture_out_file);
			strcpy_s(record_info.file_name, pathUtf8);
			StrDel(pathUtf8);
		}

		record_info.video_capture_rect = grab_rect;
		record_info.video_dst_width = capture_width;
		record_info.video_dst_height = capture_height;
		record_info.video_frame_rate = capture_frame_rate;
		record_info.quality = (MediaFileRecorder::VIDEO_QUALITY)capture_recorder_quality;
		record_info.format = (MediaFileRecorder::VIDEO_FORMAT)capture_recorder_format;
		record_info.is_record_speaker = capture_recorder_speaker;
		record_info.is_record_video = true;
		record_info.is_record_mic = capture_recorder_mic;
		record_info.thread_count = capture_cpu_count;
		record_info.record_mic_dev_index = capture_recorder_mic_dev_index;

		int ret = MR_SetRecordInfo(m_pRecorder, record_info);
		ret = MR_StartRecord(m_pRecorder);

		if (ret == 0)
		{
			startCaptureTime = timeGetTime();
			recorderDuration = 0;
			return 1;
		}

	}
	return 0;
}
void Recorder::SetNextOutFileName(MonoString* file)
{
	if (mono_string_length(file) == 0)
		wcscpy_s(capture_out_file, L"");
	else {
		LPWSTR w = (LPWSTR)mono_string_to_utf16(file);
		wcscpy_s(capture_out_file, w);
		mono_free(w);
	}
}
void Recorder::SetOutFileDir(MonoString* dir)
{
	if (mono_string_length(dir) == 0) 
		wcscpy_s(capture_out_dir, L"");
	else {
		LPWSTR w = (LPWSTR)mono_string_to_utf16(dir);
		wcscpy_s(capture_out_dir, w);
		mono_free(w);
	}
}
MonoString* Recorder::GetLastOutFileName()
{
	MonoString* rs = NULL;
	WCHAR path[MAX_PATH];
	memset(path, 0, sizeof(path));

	if (wcscpy_s(capture_out_dir, L"") == 0) {
		wcscpy_s(path, app->startDir);
		wcscat_s(path, L"\\");
	}

	wcscat_s(path, capture_last_out_file);

	char*utf8str = UnicodeToUtf8(path);
	rs = mono_string_new(app->domain, utf8str);
	StrDel(utf8str);
	return rs;
}
MonoString* Recorder::GetRecorderLastError()
{
	MonoString* rs = NULL;
	char*utf8str = UnicodeToUtf8(capture_last_error);
	rs = mono_string_new(app->domain, utf8str);
	StrDel(utf8str);
	return rs;
}
void Recorder::SetCaptureFrameRate(int rate)
{
	capture_frame_rate = rate;
}
void Recorder::SetCaptureRect(int x, int y, int w, int h)
{
	if (x == 0 && y == 0 && w == 0 && h == 0)
	{
		w = GetSystemMetrics(SM_CXSCREEN);
		h = GetSystemMetrics(SM_CYSCREEN);
	}
	capture_left = x;
	capture_top = y;
	capture_width = w;
	capture_height = h;
}
void Recorder::SetRecMic(BOOL rmic)
{
	capture_recorder_mic = rmic;
}
BOOL Recorder::GetRecMic()
{
	return capture_recorder_mic;
}
int Recorder::GetRecMicDevIndex()
{
	return capture_recorder_mic_dev_index;
}
void Recorder::SetRecMicDevIndext(int devIdex)
{
	capture_recorder_mic_dev_index = devIdex;
}
void Recorder::SetRecSound(BOOL rmic)
{
	capture_recorder_speaker = rmic;
}
BOOL Recorder::GetRecSound()
{
	return capture_recorder_speaker;
}
void Recorder::SetRecQua(int rmic)
{
	capture_recorder_quality = rmic;
}
int Recorder::GetRecQua()
{
	return capture_recorder_quality;
}
void Recorder::SetRecFormat(int format)
{
	capture_recorder_format = format;
}
int Recorder::GetRecFormat()
{
	return capture_recorder_format;
}
void Recorder::DrawPreviewRect(HDC hdc, int x, int y, int w, int h, int tw, int th)
{
	HDC hDcDesktop = GetDC(NULL);

	//计算按原图比例缩小后的图片的大小
	int nWidth = w, nHeight = h, tx = 0, ty = 0;

	if (nWidth > nHeight && nWidth > tw) //如果原图宽度大于高度,且比显示区域的宽度大
	{
		float zoom = (float)nHeight / (float)nWidth;
		nWidth = tw;
		nHeight = (int)(zoom * nWidth);
		ty = th / 2 - nHeight / 2;
	}
	else if (nHeight > nWidth && nHeight > th) //如果原图高度大于宽度,且比显示区域的高度大
	{
		float zoom = (float)nWidth / nHeight;
		nHeight = th;
		nWidth = (int)(zoom * nHeight);
		tx = tw / 2 - nWidth / 2;
	}

	SetStretchBltMode(hdc, STRETCH_HALFTONE);
	StretchBlt(hdc, tx, ty, nWidth, nHeight, hDcDesktop, x, y, w, h, SRCCOPY);
}
MonoArray* Recorder::GetAudioCaptureDevices()
{
	CComPtr<IMMDeviceEnumerator> enumerator;
	HRESULT res;

	res = enumerator.CoCreateInstance(__uuidof(MMDeviceEnumerator), NULL, CLSCTX_ALL);
	if (FAILED(res))
	{
		printf_s("ERROR : Create IMMDeviceEnumerator failed, res: %d", res);
		return nullptr;
	}

	EDataFlow dataFlow = eCapture;
	CComPtr<IMMDeviceCollection> pDevCollection;
	res = enumerator->EnumAudioEndpoints(dataFlow, DEVICE_STATE_ACTIVE, &pDevCollection);
	if (FAILED(res))
	{
		printf_s("ERROR : EnumAudioEndpoints failed, res: %d", res);
		return nullptr;
	}

	UINT count = 0;
	pDevCollection->GetCount(&count);

	MonoClass* stringClass = mono_class_from_name(mono_get_corlib(), "System", "String");
	MonoArray* arrayNames = mono_array_new(app->domain, stringClass, count);

	for (UINT i = 0; i < count; i++) 
	{
		CComPtr<IMMDevice> m_pDev;
		CComPtr<IPropertyStore> m_pProps;

		res = pDevCollection->Item(i, &m_pDev);
		if (FAILED(res))
		{
			printf_s("ERROR : Get device failed, res: %d, index: %d", res, i);
			continue;
		}

		res = m_pDev->OpenPropertyStore(STGM_READ, &m_pProps);
		if (FAILED(res))
		{
			printf_s("ERROR : OpenPropertyStore failed, res: %d, index: %d", res, i);
			continue;
		}

		PROPVARIANT varName;
		PropVariantInit(&varName);

		res = m_pProps->GetValue(PKEY_Device_FriendlyName, &varName);
		if (FAILED(res))
		{
			printf_s("ERROR : GetValue PKEY_Device_FriendlyName failed, res: %d, index: %d", res, i);
			continue;
		}

		MonoString*devName = mono_string_new_utf16(app->domain, (mono_unichar2*)varName.pwszVal, wcslen(varName.pwszVal));
		mono_array_set(arrayNames, MonoString*, i, devName);

		PropVariantClear(&varName);
	}

	return arrayNames;
}
