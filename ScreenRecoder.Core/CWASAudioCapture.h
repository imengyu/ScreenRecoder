#ifndef CWASAUDIOCAPTURE_H
#define CWASAUDIOCAPTURE_H

#include "IAudioCapture.h"

#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <mmdeviceapi.h>
#include <audioclient.h>
#include <propsys.h>
#include <vector>
#include <atomic>
#include <thread>
#include <atlbase.h>

namespace MediaFileRecorder
{
	class CWASAudioCapture : public IAudioCapture
	{
	public:
		CWASAudioCapture(DEV_TYPE devType);
		~CWASAudioCapture();
		int RegisterCaptureDataCb(IAudioCaptureDataCb* pDataCb) override;
		int UnRegisterCaptureDataCb(IAudioCaptureDataCb* pDataCb) override;
		int SetDev(int nIndex) override;
		int StartCapture() override;
		int StopCapture() override;

	private:
		int InitCapture();
		int UnInitCapture();
		int InitRender();
		void CleanUp();
		int InitFormat(const WAVEFORMATEX* pWfex, AUDIO_INFO& audioInfo);
		CHANNEL_LAYOUT CovertChannelLayout(DWORD dwLayout, int nChannels);
		
		static DWORD WINAPI ReconnectThread(LPVOID param);
		static DWORD WINAPI CaptureThread(LPVOID param);
		
		void CaptureThreadProc();
		void ReconnectThreadProc();

		int ProcessCaptureData();
	private:
		DEV_TYPE m_nDevType;
		int m_nDevIndex;
		std::atomic_bool m_bInited;
		std::atomic_bool m_bCapturing;
		std::atomic_bool m_bReconnecting;
		std::vector<IAudioCaptureDataCb*> m_vecDataCb;
		CRITICAL_SECTION m_sectionDataCb;
		CComPtr<IMMDevice> m_pDev;
		CComPtr<IAudioClient> m_pAudioClient;
		CComPtr<IAudioCaptureClient> m_pCaptureClient;
		CComPtr<IAudioRenderClient> m_pRenderClient;
		HANDLE m_hCaptureStopEvent;
		HANDLE m_hCaptureReadyEvent;
		AUDIO_INFO m_stAudioInfo;

		HANDLE m_hCaptureThread;
		HANDLE m_hReconnectThread;
	};
}
#endif