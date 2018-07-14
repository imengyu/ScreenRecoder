#include "stdafx.h"
#include "CScreenGdiGrabber.h"
#include <MMSystem.h>
#include "log.h"

namespace MediaFileRecorder {

	CScreenGdiGrabber::CScreenGdiGrabber()
	{
		started_ = false;
		run_ = false;
		frame_rate_ = 15;
		grab_rect_.left = grab_rect_.top = 0;
		grab_rect_.right = GetSystemMetrics(SM_CXSCREEN);
		grab_rect_.bottom = GetSystemMetrics(SM_CYSCREEN);
		InitializeCriticalSection(&m_sectionDataCb);
	}

	CScreenGdiGrabber::~CScreenGdiGrabber()
	{
		if (started_)
		{
			StopGrab();
		}
	}

	int CScreenGdiGrabber::RegisterDataCb(IScreenGrabberDataCb* cb)
	{
		if (find(vec_data_cb_.begin(), vec_data_cb_.end(), cb) ==
			vec_data_cb_.end())
		{
			EnterCriticalSection(&m_sectionDataCb);
			vec_data_cb_.push_back(cb);
			LeaveCriticalSection(&m_sectionDataCb);
			return 0;
		}
		return -1;
	}

	int CScreenGdiGrabber::UnRegisterDataCb(IScreenGrabberDataCb* cb)
	{
		auto iter = std::find(vec_data_cb_.begin(), vec_data_cb_.end(), cb);
		if (iter != vec_data_cb_.end())
		{
			EnterCriticalSection(&m_sectionDataCb);
			vec_data_cb_.erase(iter);
			LeaveCriticalSection(&m_sectionDataCb);
			return 0;
		}
		return -1;
	}

	int CScreenGdiGrabber::SetGrabRect(const RECT& rect)
	{
		if (!started_)
		{
			grab_rect_ = rect;
			return 0;
		}
		return -1;
	}

	int CScreenGdiGrabber::SetGrabFrameRate(int frame_rate)
	{
		if (!started_)
		{
			frame_rate_ = frame_rate;
			return 0;
		}
		return -1;
	}

	int CScreenGdiGrabber::StartGrab()
	{
		if (started_)
		{
			Error("CScreenGdiGrabber: Already started");
			return -1;
		}

		src_dc_ = GetDC(NULL);
		dst_dc_ = CreateCompatibleDC(src_dc_);

		int width = grab_rect_.right - grab_rect_.left;
		int height = grab_rect_.bottom - grab_rect_.top;

		bmi_.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
		bmi_.bmiHeader.biWidth = width;
		bmi_.bmiHeader.biHeight = -height;
		bmi_.bmiHeader.biPlanes = 1;
		bmi_.bmiHeader.biBitCount = 24;
		bmi_.bmiHeader.biCompression = BI_RGB;
		bmi_.bmiHeader.biSizeImage = 0;
		bmi_.bmiHeader.biXPelsPerMeter = 0;
		bmi_.bmiHeader.biYPelsPerMeter = 0;
		bmi_.bmiHeader.biClrUsed = 0;
		bmi_.bmiHeader.biClrImportant = 0;

		hbmp_ = CreateDIBSection(dst_dc_, &bmi_, DIB_RGB_COLORS, &bmp_buffer_, NULL, 0);
		if (!hbmp_)
		{
			Error("Create DIB section failed");
			CleanUp();
			return -1;
		}
		SelectObject(dst_dc_, hbmp_);

		video_info_.width = width;
		video_info_.height = height;
		video_info_.pix_fmt = PIX_FMT_BGR24;

		StartGrabThread();

		started_ = true;

		Info("ScreenGdiGrabber: start grab succeed!");
		return 0;
	}

	int CScreenGdiGrabber::StopGrab()
	{
		if (started_)
		{
			StopGrabThread();
			CleanUp();
			started_ = false;
			return 0;
		}
		return -1;
	}


	void CScreenGdiGrabber::CleanUp()
	{
		if (src_dc_)
		{
			ReleaseDC(NULL, src_dc_);
			src_dc_ = NULL;
		}
		if (dst_dc_)
		{
			DeleteDC(dst_dc_);
			dst_dc_ = NULL;
		}
		if (hbmp_)
		{
			DeleteObject(hbmp_);
			hbmp_ = NULL;
		}
	}


	int CScreenGdiGrabber::StartGrabThread()
	{
		if (!run_)
		{
			run_ = true;
			grab_thread_.swap(std::thread(std::bind(&CScreenGdiGrabber::GrabThreadProc, this)));
			SetThreadPriority(grab_thread_.native_handle(), THREAD_PRIORITY_TIME_CRITICAL);
			return 0;
		}
		return -1;
	}

	int CScreenGdiGrabber::StopGrabThread()
	{
		if (run_)
		{
			run_ = false;
			if (grab_thread_.joinable())
				grab_thread_.join();
			return 0;
		}
		return -1;
	}

	void CScreenGdiGrabber::GrabThreadProc()
	{
		int nInterval = 1000 / frame_rate_;
		HANDLE hWaitableTimer = CreateWaitableTimer(NULL, FALSE, NULL);
		LARGE_INTEGER fireTime;
		fireTime.QuadPart = -1;
		SetWaitableTimer(hWaitableTimer, &fireTime, nInterval, NULL, NULL, FALSE);
		while (run_)
		{
			WaitForSingleObject(hWaitableTimer, INFINITE);
			BitBlt(dst_dc_, 0, 0,
				video_info_.width, video_info_.height, src_dc_,
				grab_rect_.left, grab_rect_.top,
				SRCCOPY);

			EnterCriticalSection(&m_sectionDataCb);
			for (IScreenGrabberDataCb* cb : vec_data_cb_)
			{
				cb->OnScreenData(bmp_buffer_, video_info_);
			}
			LeaveCriticalSection(&m_sectionDataCb);
		}

		CloseHandle(hWaitableTimer);
	}

}