#include "stdafx.h"
#include "App.h"
#include "Recorder.h"
#include "Utils.h"
#include "StringHlp.h"
#include <Shlwapi.h> 
#include <shellapi.h>
#include <DbgHelp.h>
#include <mono/metadata/exception.h>
#include <mono/metadata/threads.h>
#include <io.h>
#include <functional>
#include <iostream>

#define STDOUT_FILENO 1
#define STDERR_FILENO 2

//
// 应用基础承载加载类
//

#define CAN_DEBUG  0
#define SHOW_COSOLE  0

extern LPWSTR *szArgList;
extern int argCount;
extern HINSTANCE hInst;
extern App*app;

/*



"--soft-breakpoints",
"--debugger-agent=transport=dt_socket,address=127.0.0.1:10000,server=y"
*/

App::App()
{
}
App::~App()
{
}

void App::InitICrashFilter() 
{
	SetUnhandledExceptionFilter(NULL);
	SetUnhandledExceptionFilter(NULL);
	SetUnhandledExceptionFilter(ExceptionFilter);
}
void App::InitIPath()
{
	//初始目录
	GetModuleFileName(NULL, startDir, MAX_PATH);
	PathRemoveFileSpec(startDir);
	SetCurrentDirectory(startDir);

	//初始目录A
	char*s = UnicodeToAnsi(startDir);
	strcpy_s(startDirA, s);
	StrDel(s);

	//默认导出目录
	memset(defaultExportDir, 0, sizeof(defaultExportDir));
	wcscpy_s(defaultExportDir, startDir);
	wcscat_s(defaultExportDir, L"\\");
	wcscat_s(defaultExportDir, L"output");
	if (_waccess(defaultExportDir, 0) != 0) {
		_wmkdir(defaultExportDir);
	}

	//Ini
	wcscpy_s(iniPath, startDir);
	wcscat_s(iniPath, L"\\ScreenRecoder.ini");
}

BOOL App::Load()
{
	CoInitialize(NULL);

	return LoadMono();
}
BOOL App::Clear()
{
	FreeMono();
	CoUninitialize();

	return true;
}
BOOL App::LoadMono()
{
	//初始化mono并加载C#程序集

	mono_trace_set_log_handler(MonoLogCallback, NULL);
	mono_trace_set_print_handler(MonoPrintCallback);
	mono_trace_set_printerr_handler(MonoPrintCallback);

	mono_trace_set_level_string("warning");
	//mono_trace_set_mask_string("all");

	SDIRMAKER(monopath, L"\\mono\\lib\\");
	UTF8MAKER(monopath2, monopath);
	SDIRMAKER(monoetcpath, L"\\mono\\etc\\");
	UTF8MAKER(monoetcpath2, monoetcpath);

	mono_set_dirs(monopath2, monoetcpath2);

	//设置jit初始参数，用于调试
	if (argCount > 1)
	{
		LPCSTR* argvAnsi = (LPCSTR*)malloc(sizeof(LPCSTR) *argCount);

		for (int i = 0; i < argCount; i++)
			argvAnsi[i] = UnicodeToAnsi(szArgList[i]);

		mono_jit_parse_options(argCount, (char**)argvAnsi);

		for (int i = 0; i < argCount; i++)
			free((LPVOID)argvAnsi[i]);
		free(argvAnsi);
	}

	SDIRMAKER(csharpDllPath, L"\\ScreenRecoder.App.dll");
	UTF8MAKER(csharpDllPathUtf8, csharpDllPath);
	if (_waccess(csharpDllPath, 0) != 0)
	{
		MessageBox(0, FormatString(L"加载模块 %s 时发生错误。\n找不到文件 ", csharpDllPath).c_str(),
			L"系统错误", MB_ICONERROR);
		goto EXIT;
	}

#if CAN_DEBUG
	//初始化调试
	if (IsDebuggerPresent())
		mono_debug_init(MONO_DEBUG_FORMAT_MONO);
#endif

	//获取应用域
	domain = mono_jit_init(csharpDllPathUtf8);
	if (!domain) {
		MessageBox(0, L"加载程序时发生错误。", L"系统错误", MB_ICONERROR);
		goto EXIT;
	}

	mono_install_unhandled_exception_hook(MonoUnhandledException, this);

#if CAN_DEBUG
	//初始化调试
	if (IsDebuggerPresent())
		mono_debug_domain_create(domain);
#endif

	//加载程序集
	assembly = mono_domain_assembly_open(domain, csharpDllPathUtf8);
	if (!assembly) {
		MessageBox(0, FormatString(L"加载模块 %s 时发生错误。", csharpDllPath).c_str(), L"系统错误", MB_ICONERROR);
		goto EXIT;
	}

	image = mono_assembly_get_image(assembly);
	if (image)return TRUE;

EXIT:
	CHARDEL(monoetcpath2);
	CHARDEL(csharpDllPathUtf8);
	CHARDEL(monopath2);
	return 0;
}
BOOL App::InitICalls()
{
	//初始化一些 C# 层使用的 API
	//ICALL

	mono_add_internal_call("ScreenRecoder.App.Api.AppUtils::GetInIPath", &GetInIPath);
	mono_add_internal_call("ScreenRecoder.App.Api.AppUtils::GetStartDir", &GetStartDir);
	mono_add_internal_call("ScreenRecoder.App.Api.AppUtils::GetDefExportDir", &GetDefExportDir);
	mono_add_internal_call("ScreenRecoder.App.Api.AppUtils::StartNewWhenExit", &StartNewWhenExit);

	mono_add_internal_call("ScreenRecoder.App.Utils.DebugUtils::WriteDebugString", &WriteDebugString);
	mono_add_internal_call("ScreenRecoder.App.Utils.DebugUtils::ShouldShowDebugWindow", &ShouldShowDebugWindow);
	mono_add_internal_call("ScreenRecoder.App.Utils.DebugUtils::Test1", &Test1);
	mono_add_internal_call("ScreenRecoder.App.Utils.DebugUtils::Test2", &Test2);
	mono_add_internal_call("ScreenRecoder.App.Utils.DebugUtils::Test3", &Test3);

	Recorder::InitICalls();
	Utils::InitICalls();

	return TRUE;
}
void App::FreeMono()
{
	if (domain)
		mono_jit_cleanup(domain);
}

int App::Run()
{
	//运行时初始化完毕
	//开始运行

	CreateMainWindow();

	//运行主窗口线程
	StartMainWindow();
	
	//运行主线程观察窗口
	return CreateAndRunDebugWindow();
}
void App::StartMainWindow() 
{
	hMainWindowThread = CreateThread(NULL, NULL, MainWindowThread, this, NULL, NULL);
}
DWORD WINAPI App::MainWindowThread(LPVOID lpThreadParameter) 
{
#ifdef _DEBUG
	Sleep(1000);
	printf_s("DEBUG : MainWindowThread start!\n");
#endif
	int rs = ((App*)lpThreadParameter)->RunMainWindow();
	((App*)lpThreadParameter)->CloseDebugWindow();
	printf_s("DEBUG : MainWindowThread stop!\n");
	return rs;
}
void App::CreateMainWindow() 
{
	MonoClass* main_window_class = mono_class_from_name(image, "ScreenRecoder.App", "FormMain");
	MonoClass* form_class = mono_class_get_parent(main_window_class);

	//Create FormMain obect
	main_window_instance = mono_object_new(domain, main_window_class);
	
	//Get FormMain ctor
	MonoMethodDesc* main_window_ctor_method_desc = mono_method_desc_new(":.ctor", false);
	MonoMethod* main_window_ctor_method = mono_method_desc_search_in_class(main_window_ctor_method_desc, main_window_class);

	//Create string[] args
	MonoObject* pararm[1];
	if (argCount > 1)
	{
		pararm[0] = (MonoObject*)mono_array_new(domain, mono_class_from_name(mono_get_corlib(), "System", "String"), argCount - 1);
		for (int i = 1; i < argCount; i++)
		{
			char*utf8str = UnicodeToUtf8(szArgList[i]);
			mono_array_set((MonoArray*)pararm[0], MonoString*, i - 1, mono_string_new(domain, utf8str));
			StrDel(utf8str);
		}
	}
	else pararm[0] = (MonoObject*)mono_array_new(domain, mono_class_from_name(mono_get_corlib(), "System", "String"), 0);
	//Call ctor
	mono_runtime_invoke(main_window_ctor_method, main_window_instance, (void**)&pararm, NULL);
}
int App::RunMainWindow()
{
	mono_thread_attach(domain);

	MonoClass* main_window_class = mono_class_from_name(image, "ScreenRecoder.App", "FormMain");
	MonoClass* form_class = mono_class_get_parent(main_window_class);
	MonoClass* application_class = mono_class_from_name(mono_class_get_image(form_class), "System.Windows.Forms", "Application");

	//Get and call  System.Windows.Forms.Application.Run() 
	//public static void Run(System.Windows.Forms.Form mainForm)
	//And send main_window_instance by pararm
	MonoMethod* application_run_method = mono_class_get_method_from_name(application_class, "Run", 1);
	void *params[1] = { main_window_instance };
	mono_runtime_invoke(application_run_method, NULL, (void**)&params, NULL);

	//Start new app if need restart when Quit
	if (startNewWhenExit)
		ShellExecute(NULL, L"open", szArgList[0], L"-ignore-multi-check", NULL, 5);

	return 0;
}
void App::CloseDebugWindow()
{
	if (debug_window_instance)
	{
		MonoClass* debug_window_class = mono_class_from_name(image, "ScreenRecoder.App", "FormDebug");
		MonoClass* form_class = mono_class_get_parent(debug_window_class);

		//Get and call  FormMain ShowDialog
		MonoMethod* debug_window_close_method = mono_class_get_method_from_name(form_class, "Close", -1);
		mono_runtime_invoke(debug_window_close_method, debug_window_instance, NULL, NULL);
	}
}
int App::CreateAndRunDebugWindow()
{
	MonoClass* debug_window_class = mono_class_from_name(image, "ScreenRecoder.App", "FormDebug");
	MonoClass* form_class = mono_class_get_parent(debug_window_class);
	MonoImage*  image_System_Windows_Forms = mono_class_get_image(form_class);
	MonoClass* application_class = mono_class_from_name(image_System_Windows_Forms, "System.Windows.Forms", "Application");

	//Create obect
	debug_window_instance = mono_object_new(domain, debug_window_class);
	mono_runtime_object_init(debug_window_instance);

	//Get and call  System.Windows.Forms.Application.Run() 
	//public static void Run(System.Windows.Forms.Form mainForm)
	//Add send debug_window_instance by pararm
	MonoMethod* application_run_method = mono_class_get_method_from_name( application_class, "Run", 1);
	void *params[1] = { debug_window_instance };
	mono_runtime_invoke(application_run_method, NULL, (void**)&params, NULL);

	return 0;
}

bool App::HasArg(LPCWSTR arg) {
	for (int i = 0; i < argCount; i++) {
		if (wcscmp(szArgList[i], arg) == 0)
			return true;
	}		
	return false;
}
void App::StartNewWhenExit()
{
	app->startNewWhenExit = true;
}

LONG WINAPI  App::ExceptionFilter(EXCEPTION_POINTERS *pExptInfo)
{
	//异常处理

	if (IsDebuggerPresent())
		return EXCEPTION_CONTINUE_SEARCH;

	WCHAR tempFileDir[MAX_PATH] = { 0 };
	WCHAR dmpFile[MAX_PATH] = { 0 };
	swprintf_s(tempFileDir, L"%s\\crash\\", app->startDir);
	if (!PathFileExists(tempFileDir))
		CreateDirectory(tempFileDir, NULL);

	SYSTEMTIME tm;
	GetLocalTime(&tm);//获取时间
	swprintf_s(dmpFile, L"%s\\JiYuTrainerCrashDump%d%02d%02d-%02d%02d%02d.dmp", tempFileDir,
		tm.wYear, tm.wMonth, tm.wDay, tm.wHour, tm.wMinute, tm.wSecond);//设置dmp文件名称

	HANDLE hFile = ::CreateFile(dmpFile, GENERIC_WRITE,
		FILE_SHARE_WRITE, NULL, CREATE_NEW,
		FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile != INVALID_HANDLE_VALUE)
	{
		MINIDUMP_EXCEPTION_INFORMATION exptInfo;
		exptInfo.ThreadId = ::GetCurrentThreadId();
		exptInfo.ExceptionPointers = pExptInfo;
		//将内存堆栈dump到文件中
		//MiniDumpWriteDump需引入dbghelp头文件
		BOOL bOK = ::MiniDumpWriteDump(::GetCurrentProcess(),
			::GetCurrentProcessId(), hFile, MiniDumpNormal,
			&exptInfo, NULL, NULL);
	}

	MessageBox(NULL, L"程序出现了一个错误，我们对此深表歉意。\n程序生成了一个有关于此错误的错误报告（不包含您的个人信息），请将其发送给我们以解决此问题。", L"ScreenRecoder 应用程序错误", MB_ICONERROR | MB_TOPMOST);

	return EXCEPTION_EXECUTE_HANDLER;
}
void  App::MonoUnhandledException(MonoObject *exc, void *user_data) 
{
	printf_s("DEBUG : MonoUnhandledException! \n");
	mono_print_unhandled_exception(exc);
}

void App::WriteDebugString(MonoString* str)
{
	LPWSTR w = (LPWSTR)mono_string_to_utf16(str);
	wprintf_s(w);
	OutputDebugStringW(w);
	mono_free(w);
}
MonoString* App::GetInIPath()
{
	MonoString* rs = NULL;
	char*utf8str = UnicodeToUtf8(app->iniPath);
	rs = mono_string_new(app->domain, utf8str);
	StrDel(utf8str);
	return rs;
}
MonoString*App::GetDefExportDir()
{
	MonoString* rs = NULL;
	char*utf8str = UnicodeToUtf8(app->defaultExportDir);
	rs = mono_string_new(app->domain, utf8str);
	StrDel(utf8str);
	return rs;
}
MonoString*App::GetStartDir()
{
	MonoString* rs = NULL;
	char*utf8str = UnicodeToUtf8(app->startDir);
	rs = mono_string_new(app->domain, utf8str);
	StrDel(utf8str);
	return rs;
}

void App::Test1() {
	printf_s("This is test1");	

	char outPutBuffer[32];
	strcpy_s(outPutBuffer, "This is test1 use fwrite");
	fwrite(outPutBuffer, 1, sizeof(outPutBuffer), stdout);
	fflush(stdout);
}
void App::Test2(MonoString* str) {
	LPSTR w = (LPSTR)mono_string_to_utf8(str);
	DWORD bytesWritten = 0;
	if (app->hDebugWrite) 
		WriteFile(app->hDebugWrite, w, strlen(w), &bytesWritten, NULL);
	mono_free(w);
}
MonoString* App::Test3() {
	MonoString* rs = NULL;

	return rs;
}
MonoBoolean App::ShouldShowDebugWindow()
{
#ifdef _DEBUG
	return true;
#else
	return app->HasArg(L"-show-debug");
#endif
}

void App::MonoPrintCallback(const char *string, mono_bool is_stdout)
{
	if (IsDebuggerPresent()) {
		OutputDebugStringA(string);
		OutputDebugStringA("\n");
	}
}
void App::MonoPrintErrCallback(const char *string, mono_bool is_stdout)
{
	MonoPrintCallback(string, is_stdout);
	if (MessageBoxA(NULL, string, "Mono Error", MB_ICONEXCLAMATION | MB_YESNO) == IDYES) {
		ExitProcess(-1);
	}
}
void App::MonoLogCallback(const char *log_domain, const char *log_level, const char *message, mono_bool fatal, void *user_data)
{
	bool exit = false;
	if (fatal || strcmp(log_level, "error") == 0)
		if (MessageBoxA(NULL, message, "Mono Error", MB_ICONEXCLAMATION | MB_YESNO) == IDYES)
			exit = true;
	if (IsDebuggerPresent())
		OutputDebugStringA(FormatString("%s![%s] %s\n", log_domain, log_level, message).c_str());
	if (fatal || strcmp(log_level, "error") == 0 && exit) {
		ExitProcess(-1);
	}
}

