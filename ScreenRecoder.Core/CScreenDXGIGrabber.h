#ifndef CSCREENDXGIGRABBER_H
#define CSCREENDXGIGRABBER_H

#include <Windows.h>
#include "IScreenGrabber.h"
#include <thread>
#include <vector>
#include <dxgitype.h>
#include <d3d11.h>
#include <dxgi1_2.h>
#include <atlbase.h>
#include <atomic>

namespace MediaFileRecorder
{
	class CSCreenDXGIGrabber : public IScreenGrabber
	{
	public:
		CSCreenDXGIGrabber();
		~CSCreenDXGIGrabber();
		int RegisterDataCb(IScreenGrabberDataCb* cb) override;
		int UnRegisterDataCb(IScreenGrabberDataCb* cb) override;

		int SetGrabRect(int left, int top, int right, int bottom) override;
		int SetGrabFrameRate(int frame_rate) override;

		int StartGrab() override;
		int StopGrab() override;
	private:
		int InitDXGI();
		int UnInitDXGI();
		bool AttachToThread();
		bool QueryFrame(IDXGISurface** ppStagingSurf);
		void StartGrabThread();
		void StopGrabThread();
		void GrabThreadProc();

	private:
		bool m_bStarted;
		std::atomic_bool m_bRun;
		std::vector<IScreenGrabberDataCb*> m_vecDataCb;
		RECT m_rectGrab;
		int m_nFrameRate;
		std::thread m_GrabThread;
		HANDLE m_hWaitableTimer;

		CComPtr<ID3D11Device> m_pDevice;
		CComPtr<ID3D11DeviceContext> m_pDevContext;
		CComPtr<IDXGIOutputDuplication> m_pDeskDupl;
		DXGI_OUTPUT_DESC m_dxgiOutDesc;

		int8_t* m_pImgBuffer;
	};
}
#endif
