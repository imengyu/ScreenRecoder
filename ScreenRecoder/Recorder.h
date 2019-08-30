#pragma once
#include "ScreenRecoder.h"
#include "../ScreenRecoder.Core/IScreenAudioRecord_C.h"

enum UpdateCallbackID
{
	OnStart,
	OnPause,
	OnContinue,
	OnStop,
};
enum RECORD_STATE
{
	NOT_BEGIN,
	RECORDING,
	SUSPENDED,
};

typedef void(_cdecl*UpdateCallback)(UpdateCallbackID id, void*data);

class Recorder
{
public:
	static void InitICalls();

	static LONG GetRecSec();
	static BOOL IsRecording();
	static BOOL IsInterrupt();
	static void Destroy();
	static void Create();
	static RECORD_STATE GetState();
	static void SetUpdateCallback(UpdateCallback updateCallback);
	static BOOL PauseButton();
	static BOOL StopButton();
	static BOOL StartButton();
	static void SetNextOutFileName(MonoString* file);
	static void SetOutFileDir(MonoString* dir);
	static MonoString* GetLastOutFileName();
	static MonoString* GetRecorderLastError();
	static void SetCaptureFrameRate(int rate);
	static void SetCaptureRect(int x, int y, int w, int h);
	static void SetRecMic(BOOL rmic);
	static BOOL GetRecMic();
	static int GetRecMicDevIndex();
	static void SetRecMicDevIndext(int devIdex);
	static void SetRecSound(BOOL rmic);
	static BOOL GetRecSound();
	static void SetRecQua(int rmic);
	static int GetRecQua();;
	static void SetRecFormat(int format);
	static int GetRecFormat();
	static void DrawPreviewRect(HDC hdc, int x, int y, int w, int h, int tw, int th);
	static MonoArray * GetAudioCaptureDevices();
};

