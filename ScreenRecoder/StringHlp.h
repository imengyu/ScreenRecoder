#pragma once
#include "stdafx.h"
#include <string>

std::string & FormatString(std::string & _str, const char * _Format, ...);

std::wstring & FormatString(std::wstring & _str, const wchar_t * _Format, ...);

std::wstring FormatString(const wchar_t * format, ...);

std::string FormatString(const char * format, ...);

#define FormatStringPtr FormatStringPtrW

B_CAPI(std::string*)FormatStringPtr2A(std::string *_str, const char * _Format, ...);
B_CAPI(std::wstring *)FormatStringPtr2W(std::wstring *_str, const wchar_t * _Format, ...);
B_CAPI(std::wstring *)FormatStringPtrW(const wchar_t *format, ...);
B_CAPI(std::string *)FormatStringPtrA(const char *format, ...);

#define DIRMAKER(dirname,add) WCHAR dirname[MAX_PATH];\
wcscpy_s(dirname, startDir);\
wcscat_s(dirname, add)
#define CHARMAKER(strs,wstr) CHAR *strs=UnicodeToAnsi(wstr)
#define UTF8MAKER(strs,wstr) CHAR *strs=UnicodeToUtf8(wstr)
#define CHARDEL(strs) delete(strs)

B_CAPI(char*) UnicodeToAnsi(const wchar_t* szStr);
B_CAPI(void) StrDel(const void * szStr);
B_CAPI(char*) UnicodeToUtf8(const wchar_t* unicode);
std::wstring Utf8ToUnicode(const char* szU8);
B_CAPI(wchar_t*) AnsiToUnicode(const char* szStr);