#pragma once
#include "ScreenRecoder.h"

class Utils
{
public:
	static void InitICalls();

	static void WindowMove(HWND hWnd);
	static void WindowShow(HWND hWnd);
	static void WindowHide(HWND hWnd);
	static void WindowTop(HWND hWnd);
	static void WindowBottom(HWND hWnd);
	static BOOL WindowIsTop(HWND hWnd);
	static BOOL WindowIsVisible(HWND hWnd);
	static void WindowSetForeground(HWND hWnd);

	static void ShowAnim(HWND hWnd);
	static void HideAnim(HWND hWnd);

	static void PlayTip(MonoString* name, BOOL ansyc);
	static void OpenFile(MonoString* file);
	static void SelectFileInExplorer(MonoString * file);

	static void IniWriteValue(MonoString * Section, MonoString * Key, MonoString * Value);
	static MonoString * IniReadValue(MonoString * Section, MonoString * Key, MonoString * DefaultValue);

	static bool IsModKey(int k, bool * shift, bool * alt, bool * ctrl, UINT *mk);
	static BOOL RegisterHotKey(HWND hWnd, int i, int k1, int k2, int k3);
	static void  UnRegisterAllHotKey(HWND hWnd);

	static MonoString* ShowSaveImageFileDialog(HWND hWnd, MonoString*title, MonoString*fileName);
	static BOOL ShowSaveFileDialog(HWND hWnd, LPCWSTR startDir, LPCWSTR title, LPCWSTR fileFilter, LPCWSTR fileName, LPCWSTR defExt, LPWSTR strrs, size_t bufsize);
};

