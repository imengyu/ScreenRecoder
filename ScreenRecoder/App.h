#pragma once
#include "ScreenRecoder.h"

class App
{
public:
	App();
	~App();

	void InitICrashFilter();
	void InitIPath();

	BOOL Load();
	BOOL Clear();
	BOOL LoadMono();
	BOOL InitICalls();
	void FreeMono();

	int Run();
	static DWORD WINAPI MainWindowThread(LPVOID lpThreadParameter);
	void CreateMainWindow();
	int RunMainWindow();
	void StartMainWindow();
	void CloseDebugWindow();
	int CreateAndRunDebugWindow();

	bool HasArg(LPCWSTR arg);
	static void StartNewWhenExit();

	static LONG __stdcall ExceptionFilter(EXCEPTION_POINTERS * pExptInfo);
	static void MonoUnhandledException(MonoObject * exc, void * user_data);

	static void WriteDebugString(MonoString * str);
	static MonoString * GetInIPath();
	static MonoString * GetDefExportDir();
	static MonoString * GetStartDir();


	static void Test1();
	static void Test2(MonoString* str);
	static MonoString * Test3();
	static MonoBoolean ShouldShowDebugWindow();
	static MonoBoolean CreateDebugOutPutPippe();
	static void DestroyDebugOutPutPippe();
	static MonoString * ReadDebugOutPutData();

	static void MonoPrintCallback(const char * string, mono_bool is_stdout);
	static void MonoPrintErrCallback(const char * string, mono_bool is_stdout);
	static void MonoLogCallback(const char * log_domain, const char * log_level, const char * message, mono_bool fatal, void * user_data);

	MonoDomain *domain;
	MonoAssembly *assembly;
	MonoImage* image;

	WCHAR startDir[MAX_PATH];
	CHAR startDirA[MAX_PATH];
	WCHAR iniPath[MAX_PATH];
	WCHAR defaultExportDir[MAX_PATH];

	bool startNewWhenExit = false;

	MonoObject *debug_window_instance = nullptr;
	MonoObject *main_window_instance = nullptr;

	HANDLE hOldStdOut, hOldStdErr;
	HANDLE hDebugRead, hDebugWrite; //stdout/stderr管道的读写句柄声明
	FILE* newStdoutFile = NULL;
	int oldStdoutFile = NULL;

	HANDLE hMainWindowThread = NULL;
	HANDLE hOutput = NULL;

	FILE *fileIn;
	FILE *fileOut;
	FILE *fileErr;
};

