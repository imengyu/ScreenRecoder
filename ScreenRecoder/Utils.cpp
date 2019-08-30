#include "stdafx.h"
#include "App.h"
#include "Utils.h"
#include "StringHlp.h"
#include <Shlwapi.h> 
#include <shellapi.h>
#include <list>
#include <commdlg.h>
#include <MMSystem.h>

extern App*app;

//
// 顶层工具类
//

void Utils::InitICalls()
{
	mono_add_internal_call("ScreenRecoder.App.Api.WindowUtils::Move", &WindowMove);
	mono_add_internal_call("ScreenRecoder.App.Api.WindowUtils::Show", &WindowShow);
	mono_add_internal_call("ScreenRecoder.App.Api.WindowUtils::Hide", &WindowHide);
	mono_add_internal_call("ScreenRecoder.App.Api.WindowUtils::Top", &WindowTop);
	mono_add_internal_call("ScreenRecoder.App.Api.WindowUtils::Bottom", &WindowBottom);
	mono_add_internal_call("ScreenRecoder.App.Api.WindowUtils::IsTop", &WindowIsTop);
	mono_add_internal_call("ScreenRecoder.App.Api.WindowUtils::SetForeground", &WindowSetForeground);
	mono_add_internal_call("ScreenRecoder.App.Api.WindowUtils::IsVisible", &WindowIsVisible);
	mono_add_internal_call("ScreenRecoder.App.Api.WindowUtils::ShowAnim", &ShowAnim);
	mono_add_internal_call("ScreenRecoder.App.Api.WindowUtils::HideAnim", &HideAnim);

	mono_add_internal_call("ScreenRecoder.App.Api.DialogUtils::ShowSaveImageFileDialog", &ShowSaveImageFileDialog);

	mono_add_internal_call("ScreenRecoder.App.Utils.Utils::OpenFile", &OpenFile);
	mono_add_internal_call("ScreenRecoder.App.Utils.Utils::PlayTip", &PlayTip);
	mono_add_internal_call("ScreenRecoder.App.Utils.Utils::SelectFileInExplorer", &SelectFileInExplorer);

	mono_add_internal_call("ScreenRecoder.App.Api.API::IniWriteValue", &IniWriteValue);
	mono_add_internal_call("ScreenRecoder.App.Api.API::IniReadValue", &IniReadValue);

	mono_add_internal_call("ScreenRecoder.App.Api.AppUtils::RegisterHotKey", &RegisterHotKey);
	mono_add_internal_call("ScreenRecoder.App.Api.AppUtils::UnRegisterAllHotKey", &UnRegisterAllHotKey);
}

void Utils::WindowMove(HWND hWnd)
{
	ReleaseCapture();
	SendMessage(hWnd, WM_SYSCOMMAND, SC_MOVE | HTCAPTION, 0);
}
void Utils::WindowShow(HWND hWnd)
{
	if (!IsWindowVisible(hWnd))
		ShowWindow(hWnd, SW_SHOW);
}
void Utils::WindowHide(HWND hWnd)
{
	if (IsWindowVisible(hWnd))
		ShowWindow(hWnd, SW_HIDE);
}
void Utils::WindowTop(HWND hWnd)
{
	SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
}
void Utils::WindowBottom(HWND hWnd)
{
	SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
}
BOOL Utils::WindowIsTop(HWND hWnd)
{
	return GetForegroundWindow() == hWnd;
}
BOOL Utils::WindowIsVisible(HWND hWnd)
{
	return  IsWindowVisible(hWnd);
}
void Utils::WindowSetForeground(HWND hWnd) {
	SetForegroundWindow(hWnd);
}

void Utils::ShowAnim(HWND hWnd)
{
	AnimateWindow(hWnd, 1000, AW_SLIDE | AW_VER_POSITIVE | AW_ACTIVATE);
}
void Utils::HideAnim(HWND hWnd)
{
	AnimateWindow(hWnd, 1000, AW_CENTER | AW_HIDE);
}

void Utils::PlayTip(MonoString* name, BOOL ansyc) {
	LPWSTR w = (LPWSTR)mono_string_to_utf16(name);
	if (wcscmp(w, L"error") == 0) {
		MessageBeep(MB_ICONERROR);
	}else if (wcscmp(w, L"warning") == 0) {
		MessageBeep(MB_ICONEXCLAMATION);
	}
	else {
		WCHAR path[MAX_PATH] = { 0 };
		swprintf_s(path, L"%s\\sounds\\%s.wav", app->startDir, w);
		mono_free(w);
		if (_waccess_s(path, 0) == 0)
			PlaySound(path, NULL, ansyc ? SND_ASYNC | SND_FILENAME : SND_FILENAME);
	}
}
void Utils::OpenFile(MonoString* file)
{
	LPWSTR w = (LPWSTR)mono_string_to_utf16(file);
	ShellExecute(NULL, L"open", w, NULL, NULL, 5);
	mono_free(w);
}
void Utils::SelectFileInExplorer(MonoString* file)
{
	LPWSTR w = (LPWSTR)mono_string_to_utf16(file);
	ShellExecute(NULL, L"explorer", FormatString(L"/select, %s", w).c_str(), NULL, NULL, 5);
	mono_free(w);
}

void Utils::IniWriteValue(MonoString* Section, MonoString* Key, MonoString* Value) {

	LPWSTR wSection = (LPWSTR)mono_string_to_utf16(Section);
	LPWSTR wKey = (LPWSTR)mono_string_to_utf16(Key);
	LPWSTR wValue = (LPWSTR)mono_string_to_utf16(Value);
	WritePrivateProfileStringW(wSection, wKey, wValue, app->iniPath);
	mono_free(wSection);
	mono_free(wKey);
	mono_free(wValue);
}
MonoString* Utils::IniReadValue(MonoString* Section, MonoString* Key, MonoString* DefaultValue) {

	LPWSTR wSection = (LPWSTR)mono_string_to_utf16(Section);
	LPWSTR wKey = (LPWSTR)mono_string_to_utf16(Key);
	LPWSTR wDefaultValue = (LPWSTR)mono_string_to_utf16(DefaultValue);

	WCHAR wValueBuffer[256] = { 0 };
	GetPrivateProfileString(wSection, wKey, wDefaultValue, wValueBuffer, 256, app->iniPath);
	mono_free(wSection);
	mono_free(wKey);
	mono_free(wDefaultValue);

	return mono_string_new_utf16(app->domain, wValueBuffer, wcslen(wValueBuffer));
}

std::list<int> regedKey;

bool Utils::IsModKey(int k, bool* shift, bool* alt, bool* ctrl, UINT *mk) {
	if (k == 65536 && !*shift) {
		*mk |= MOD_SHIFT;
		*shift = true;
		return true;
	}
	else if (k == 262144 && !*alt) {
		*mk |= MOD_ALT;
		*alt = true;
		return true;
	}
	else if (k == 131072 && !*ctrl) {
		*mk |= MOD_CONTROL;
		*ctrl = true;
		return true;
	}
	else return false;
}
BOOL Utils::RegisterHotKey(HWND hWnd, int i, int k1, int k2, int k3)
{
	bool shift = false, alt = false, ctrl = false;
	UINT mk = 0, key = 0;

	if (k1 != 0 && !IsModKey(k1, &shift, &alt, &ctrl, &mk))
		key = k1;
	if (k2 != 0 && !IsModKey(k2, &shift, &alt, &ctrl, &mk))
		key = k2;
	if (k3 != 0 && !IsModKey(k3, &shift, &alt, &ctrl, &mk))
		key = k3;

	return ::RegisterHotKey(hWnd, 0x8886 + i, mk, key);
}
void  Utils::UnRegisterAllHotKey(HWND hWnd)
{
	std::list<int>::iterator it;
	for (it = regedKey.begin(); it != regedKey.end(); it++)
	{
		int item = *it;
		UnregisterHotKey(hWnd, item);
	}
}

MonoString* Utils::ShowSaveImageFileDialog(HWND hWnd, MonoString*title, MonoString*fileName)
{
	BOOL result = FALSE;
	LPWSTR wTitle = (LPWSTR)mono_string_to_utf16(title);
	LPWSTR wFileName = (LPWSTR)mono_string_to_utf16(fileName);
	WCHAR path[MAX_PATH] = { 0 };
	result = ShowSaveFileDialog(hWnd, NULL, wTitle, L"JPG 图像(*.jpg;*.jpeg)\0*.jpg;*.jpeg\0PNG 图像(*.png)\0*.png\0位图(*.bmp)\0*.bmp\0Gif(*.gif)\0*.gif\0所有文件(*.*)\0*.*\0\0\0", wFileName, L".jpg", path, MAX_PATH);
	mono_free(wTitle);
	mono_free(wFileName);
	if (result)
		return mono_string_new_utf16(app->domain, path, wcslen(path));
	return nullptr;
}
BOOL Utils::ShowSaveFileDialog(HWND hWnd, LPCWSTR startDir, LPCWSTR title, LPCWSTR fileFilter, LPCWSTR fileName, LPCWSTR defExt, LPWSTR strrs, size_t bufsize)
{
	if (strrs) {
		OPENFILENAME ofn;
		TCHAR szFile[MAX_PATH];
		ZeroMemory(szFile, sizeof(szFile));
		ZeroMemory(&ofn, sizeof(OPENFILENAME));
		ofn.lStructSize = sizeof(OPENFILENAME);
		ofn.hwndOwner = hWnd;
		ofn.lpstrFile = szFile;
		if (fileName != 0 && wcslen(fileName) != 0) wcscpy_s(szFile, fileName);
		else ofn.lpstrFile[0] = '\0';
		ofn.nMaxFile = sizeof(szFile);
		ofn.nFilterIndex = 1;
		ofn.lpstrFilter = fileFilter;
		ofn.lpstrDefExt = defExt;
		ofn.lpstrFileTitle = (LPWSTR)title;
		ofn.nMaxFileTitle = 0;
		ofn.lpstrInitialDir = startDir;
		ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
		if (GetSaveFileName(&ofn))
		{
			//显示选择的文件。 szFile
			wcscpy_s(strrs, bufsize, szFile);
			return TRUE;
		}
	}
	return 0;
}
