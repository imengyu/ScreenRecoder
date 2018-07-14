#ifndef LOG_H
#define LOG_H
#include "MediaRecordTypeDef.h"

namespace MediaFileRecorder
{
	void set_log_func(sdk_log_cb_t log_func);

	void call_log_func_w(SDK_LOG_LEVEL level, const wchar_t* fmt, va_list vl);
	void call_log_func(SDK_LOG_LEVEL level, const char* fmt, va_list vl);

	void Debug_W(const wchar_t *fmt, ...);
	void Info_W(const wchar_t *fmt, ...);
	void Warning_W(const wchar_t *fmt, ...);
	void Error_W(const wchar_t *fmt, ...);

	void Debug(const char *fmt, ...);
	void Info(const char *fmt, ...);
	void Warning(const char *fmt, ...);
	void Error(const char *fmt, ...);

}
#endif