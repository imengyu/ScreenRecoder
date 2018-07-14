
#include "stdafx.h"
#include "CScreenDXGIGrabber.h"
#include <mmsystem.h>

namespace MediaFileRecorder
{

	CSCreenDXGIGrabber::CSCreenDXGIGrabber()
		:m_bStarted(false),
		m_hWaitableTimer(NULL)
	{
		m_bRun = false;
		m_rectGrab.left = m_rectGrab.top = 0;
		m_rectGrab.right = GetSystemMetrics(SM_CXSCREEN);
		m_rectGrab.bottom = GetSystemMetrics(SM_CYSCREEN);
		m_nFrameRate = 25;
	}

	CSCreenDXGIGrabber::~CSCreenDXGIGrabber()
	{
		if (m_bStarted)
		{
			StopGrab();
		}
	}

	int CSCreenDXGIGrabber::RegisterDataCb(IScreenGrabberDataCb* cb)
	{
		if (find(m_vecDataCb.begin(), m_vecDataCb.end(), cb) 
			== m_vecDataCb.end())
		{
			m_vecDataCb.push_back(cb);
			return 0;
		}
		return -1;
	}

	int CSCreenDXGIGrabber::UnRegisterDataCb(IScreenGrabberDataCb* cb)
	{
		auto iter = find(m_vecDataCb.begin(), m_vecDataCb.end(), cb);
		if (iter != m_vecDataCb.end())
		{
			m_vecDataCb.erase(iter);
			return 0;
		}
		return -1;
	}

	int CSCreenDXGIGrabber::SetGrabRect(int left, int top, int right, int bottom)
	{
		if (!m_bStarted)
		{
			m_rectGrab = { left, top, right, bottom };
			return 0;
		}
		return -1;
	}

	int CSCreenDXGIGrabber::SetGrabFrameRate(int frame_rate)
	{
		if (!m_bStarted)
		{
			m_nFrameRate = frame_rate;
			return 0;
		}
		return -1;
	}

	int CSCreenDXGIGrabber::StartGrab()
	{
		if (m_bStarted)
		{
			OutputDebugStringA("Already started \n");
			return -1;
		}

		if (InitDXGI() != 0)
		{
			OutputDebugStringA("InitDXGI failed \n");
			return -1;
		}

		m_hWaitableTimer = CreateWaitableTimer(NULL, FALSE, NULL);
		if (!m_hWaitableTimer)
		{
			OutputDebugStringA("Create waitable timer failed \n");
			return -1;
		}

		LARGE_INTEGER fireTime;
		fireTime.QuadPart = -1;
		LONG nPeriod = 1000 / m_nFrameRate;
		SetWaitableTimer(m_hWaitableTimer, &fireTime, nPeriod, NULL, NULL, FALSE);

		StartGrabThread();

		int width = m_rectGrab.right - m_rectGrab.left;
		int height = m_rectGrab.bottom - m_rectGrab.top;

		m_pImgBuffer = new int8_t[width * height * 4];
		m_bStarted = true;

		return 0;
	}

	int CSCreenDXGIGrabber::StopGrab()
	{
		if (m_bStarted)
		{
			StopGrabThread();
			UnInitDXGI();
			delete m_pImgBuffer;
			m_bStarted = false;
			return 0;
		}
		return -1;
	}

	int CSCreenDXGIGrabber::InitDXGI()
	{
		HRESULT hr = S_OK;

		D3D_DRIVER_TYPE driverTypes[] =
		{
			D3D_DRIVER_TYPE_HARDWARE,
			D3D_DRIVER_TYPE_WARP,
			D3D_DRIVER_TYPE_REFERENCE
		};

		uint32_t nDriverTypes = ARRAYSIZE(driverTypes);

		D3D_FEATURE_LEVEL featureLevels[] =
		{
			D3D_FEATURE_LEVEL_11_0,
			D3D_FEATURE_LEVEL_10_1,
			D3D_FEATURE_LEVEL_10_0,
			D3D_FEATURE_LEVEL_9_1
		};
		uint32_t nFeatureLevels = ARRAYSIZE(featureLevels);

		D3D_FEATURE_LEVEL featureLevel;

		for (uint32_t i = 0; i < nDriverTypes; ++i)
		{
			hr = D3D11CreateDevice(NULL, driverTypes[i], NULL, 0,
				featureLevels, nFeatureLevels, D3D11_SDK_VERSION,
				&m_pDevice, &featureLevel, &m_pDevContext);
			if (SUCCEEDED(hr))
			{
				break;
			}
		}
		if (FAILED(hr))
		{
			OutputDebugStringA("Create d3d11 device failed \n");
			return -1;
		}

		CComPtr<IDXGIDevice> pDxgiDevice = NULL;
		hr = m_pDevice->QueryInterface(__uuidof(IDXGIDevice), (void**)&pDxgiDevice);
		if (FAILED(hr))
		{
			OutputDebugStringA("Get DXGI interface failed \n");
			return -1;
		}

		CComPtr<IDXGIAdapter> pDxgiAdapter = NULL;
		hr = pDxgiDevice->GetParent(__uuidof(IDXGIAdapter), (void**)(&pDxgiAdapter));
		if (FAILED(hr))
		{
			OutputDebugStringA("Get DXGI adapter failed \n");
			return -1;
		}

		int nOutput = 0;
		CComPtr<IDXGIOutput> pDxgiOutput = NULL;
		hr = pDxgiAdapter->EnumOutputs(nOutput, &pDxgiOutput);
		if (FAILED(hr))
		{
			OutputDebugStringA("Get DXGI outputs failed \n");
			return -1;
		}

		pDxgiOutput->GetDesc(&m_dxgiOutDesc);

		CComPtr<IDXGIOutput1> pDxgiOutput1 = NULL;
		hr = pDxgiOutput->QueryInterface(__uuidof(IDXGIOutput1), (void**)(&pDxgiOutput1));
		if (FAILED(hr))
		{
			OutputDebugStringA("Get DXGI output1 failed \n");
			return -1;
		}

		hr = pDxgiOutput1->DuplicateOutput(m_pDevice, &m_pDeskDupl);
		if (FAILED(hr))
		{
			OutputDebugStringA("Create DXGI output duplication failed \n");
			return -1;
		}

		return 0;
	}

	int CSCreenDXGIGrabber::UnInitDXGI()
	{
		m_pDeskDupl.Release();
		m_pDevContext.Release();
		m_pDevice.Release();
		return 0;
	}

	bool CSCreenDXGIGrabber::AttachToThread()
	{
		HDESK hCurrentDeskTop = OpenInputDesktop(0, FALSE, GENERIC_ALL);
		if (!hCurrentDeskTop)
		{
			OutputDebugStringA("Open input desktop failed \n");
			return FALSE;
		}

		BOOL bDestopAttached = SetThreadDesktop(hCurrentDeskTop);
		CloseDesktop(hCurrentDeskTop);
		hCurrentDeskTop = NULL;

		return bDestopAttached != 0;
	}


	bool CSCreenDXGIGrabber::QueryFrame(IDXGISurface** ppStagingSurf)
	{
		CComPtr<IDXGIResource> pDesktopResource = NULL;
		DXGI_OUTDUPL_FRAME_INFO frameInfo;
		HRESULT hr = m_pDeskDupl->AcquireNextFrame(5, &frameInfo, &pDesktopResource);
		if (FAILED(hr))
		{
			//
			// 在一些win10的系统上,如果桌面没有变化的情况下，  
			// 这里会发生超时现象，但是这并不是发生了错误，而是系统优化了刷新动作导致的。  
			// 所以，这里没必要返回FALSE，返回不带任何数据的TRUE即可  
			// 
			*ppStagingSurf = NULL;
			OutputDebugStringA("AcquireNextFrame failed \n");
			return true;
		}

		CComPtr<ID3D11Texture2D> pAcquireDesktopImage = NULL;
		hr = pDesktopResource->QueryInterface(__uuidof(ID3D11Texture2D),
			(void**)&pAcquireDesktopImage);
		if (FAILED(hr))
		{
			OutputDebugStringA("Get desktop image failed");
			return false;
		}

		D3D11_TEXTURE2D_DESC frameDescriptor;
		pAcquireDesktopImage->GetDesc(&frameDescriptor);


		CComPtr<ID3D11Texture2D> pNewDesktopImage = NULL;
		frameDescriptor.Usage = D3D11_USAGE_STAGING;
		frameDescriptor.CPUAccessFlags = D3D11_CPU_ACCESS_READ;
		frameDescriptor.BindFlags = 0;
		frameDescriptor.MiscFlags = 0;
		frameDescriptor.MipLevels = 1;
		frameDescriptor.ArraySize = 1;
		frameDescriptor.SampleDesc.Count = 1;
		hr = m_pDevice->CreateTexture2D(&frameDescriptor, NULL, &pNewDesktopImage);
		if (FAILED(hr))
		{
		OutputDebugStringA("CreateTexture2D failed \n");
		m_pDeskDupl->ReleaseFrame();
		return false;
		}

		m_pDevContext->CopyResource(pNewDesktopImage, pAcquireDesktopImage);
		m_pDeskDupl->ReleaseFrame();

		CComPtr<IDXGISurface> pStagingSurf = NULL;
		hr = pNewDesktopImage->QueryInterface(__uuidof(IDXGISurface), (void**)&pStagingSurf);
		if (FAILED(hr))
		{
		OutputDebugStringA("Get staging surface failed \n");
		return false;
		}

		*ppStagingSurf = pStagingSurf.Detach();

		return true;
	}


	void CSCreenDXGIGrabber::StartGrabThread()
	{
		m_bRun = true;
		m_GrabThread.swap(std::thread(std::bind(&CSCreenDXGIGrabber::GrabThreadProc, this)));
		SetThreadPriority(m_GrabThread.native_handle(), THREAD_PRIORITY_TIME_CRITICAL);
	}

	void CSCreenDXGIGrabber::StopGrabThread()
	{
		m_bRun = false;
		if (m_GrabThread.joinable())
			m_GrabThread.join();
	}

	void CSCreenDXGIGrabber::GrabThreadProc()
	{
		/*if (!AttachToThread())
			return;*/

		while (true)
		{
			
			DWORD result = WaitForSingleObject(m_hWaitableTimer, INFINITE);
			if (!m_bRun)
				break;
			
			int64_t begin = timeGetTime();
			DXGI_MAPPED_RECT dxgiRect;
			IDXGISurface* pStagingSurf = NULL;
			
			bool bRet = QueryFrame(&pStagingSurf);
			if (!bRet)
				return;

			int width = m_rectGrab.right - m_rectGrab.left;
			int height = m_rectGrab.bottom - m_rectGrab.top;

			if (pStagingSurf)
			{
				pStagingSurf->Map(&dxgiRect, DXGI_MAP_READ);
				for (IScreenGrabberDataCb* cb : m_vecDataCb)
				{
					int nOffset = m_rectGrab.top * dxgiRect.Pitch + m_rectGrab.left * 4;
					memcpy(m_pImgBuffer, dxgiRect.pBits + nOffset, width * height * 4);
					cb->OnScreenData(m_pImgBuffer, width, height, PIX_FMT::PIX_FMT_BGRA);
				}
				pStagingSurf->Unmap();
				pStagingSurf->Release();
			}
			else
			{
				for (IScreenGrabberDataCb* cb : m_vecDataCb)
				{
					cb->OnScreenData(m_pImgBuffer, width, height, PIX_FMT::PIX_FMT_BGRA);
				}
			}
			
			
			int64_t nDuration = timeGetTime() - begin;
			char log[128] = { 0 };
			sprintf_s(log, "DXGI duration: %lld \n", nDuration);
			OutputDebugStringA(log);
		}
	}
}