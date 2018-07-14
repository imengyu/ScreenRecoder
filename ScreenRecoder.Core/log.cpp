#include "stdafx.h"
#include "log.h"
#include <windows.h>
#include <strsafe.h>

namespace MediaFileRecorder
{
	sdk_log_cb_t g_log_func;

	void set_log_func(sdk_log_cb_t log_func)
	{
		g_log_func = log_func;
	}

	void call_log_func_w(SDK_LOG_LEVEL level, const wchar_t* fmt, va_list vl)
	{
		if (g_log_func == NULL || fmt == NULL)
			return;

		wchar_t buf_w[4096] = {0};
		StringCchVPrintfW(buf_w, 4096, fmt, vl);
		g_log_func(level, buf_w);
	}

	void call_log_func(SDK_LOG_LEVEL level, const char* fmt, va_list vl)
	{
		if (g_log_func == NULL || fmt == NULL)
			return;

		char buf[4096] = { 0 };
		wchar_t buf_w[4096] = { 0 };
		StringCchVPrintfA(buf, 4096, fmt, vl);
		int len = ::MultiByteToWideChar(CP_UTF8, 0, buf, -1, buf_w, 4096);
		if (len > 0)
		{
			g_log_func(level, buf_w);
		}
	}

	void Debug_W(const wchar_t *fmt, ...)
	{
		va_list vl;
		va_start(vl, fmt);
		call_log_func_w(SDK_LOG_LEVEL::LOG_DEBUG, fmt, vl);
		va_end(vl);
	}

	void Info_W(const wchar_t *fmt, ...)
	{
		va_list vl;
		va_start(vl, fmt);
		call_log_func_w(SDK_LOG_LEVEL::LOG_INFO, fmt, vl);
		va_end(vl);
	}

	void Warning_W(const wchar_t *fmt, ...)
	{
		va_list vl;
		va_start(vl, fmt);
		call_log_func_w(SDK_LOG_LEVEL::LOG_WARNING, fmt, vl);
		va_end(vl);
	}

	void Error_W(const wchar_t *fmt, ...)
	{
		va_list vl;
		va_start(vl, fmt);
		call_log_func_w(SDK_LOG_LEVEL::LOG_ERROR, fmt, vl);
		va_end(vl);
	}

	void Debug(const char *fmt, ...)
	{
		va_list vl;
		va_start(vl, fmt);
		call_log_func(SDK_LOG_LEVEL::LOG_DEBUG, fmt, vl);
		va_end(vl);
	}

	void Info(const char *fmt, ...)
	{
		va_list vl;
		va_start(vl, fmt);
		call_log_func(SDK_LOG_LEVEL::LOG_INFO, fmt, vl);
		va_end(vl);
	}

	void Warning(const char *fmt, ...)
	{
		va_list vl;
		va_start(vl, fmt);
		call_log_func(SDK_LOG_LEVEL::LOG_WARNING, fmt, vl);
		va_end(vl);
	}

	void Error(const char *fmt, ...)
	{
		va_list vl;
		va_start(vl, fmt);
		call_log_func(SDK_LOG_LEVEL::LOG_ERROR, fmt, vl);
		va_end(vl);
	}
}


