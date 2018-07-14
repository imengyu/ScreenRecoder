#include "stdafx.h"
#include "CWAVEAudioCapture.h"
#include "log.h"

namespace MediaFileRecorder
{
	CWAVEAudioCapture::CWAVEAudioCapture(DEV_TYPE devType)
		:m_nDevType(devType),
		 m_nDevIndex(-1),
		 m_hWaveIn(NULL),
		 m_hRecordThread(NULL)
	{
		m_bInited = false;
		m_bCapturing = false;
		m_bRunning = false;
		InitializeCriticalSection(&m_sectionDataCb);
		//InitializeCriticalSection(&m_sectionReturnBuffer);
	}

	CWAVEAudioCapture::~CWAVEAudioCapture()
	{
		if (m_bCapturing)
		{
			StopCapture();
		}
	}

	int CWAVEAudioCapture::RegisterCaptureDataCb(IAudioCaptureDataCb* pDataCb)
	{
		if (std::find(m_vecDataCb.begin(), m_vecDataCb.end(), pDataCb) ==
			m_vecDataCb.end())
		{
			EnterCriticalSection(&m_sectionDataCb);
			m_vecDataCb.push_back(pDataCb);
			LeaveCriticalSection(&m_sectionDataCb);
			return 0;
		}
		return -1;
	}

	int CWAVEAudioCapture::UnRegisterCaptureDataCb(IAudioCaptureDataCb* pDataCb)
	{
		auto iter = std::find(m_vecDataCb.begin(), m_vecDataCb.end(), pDataCb);
		if (iter != m_vecDataCb.end())
		{
			EnterCriticalSection(&m_sectionDataCb);
			m_vecDataCb.erase(iter);
			LeaveCriticalSection(&m_sectionDataCb);
			return 0;
		}
		return -1;
	}

	int CWAVEAudioCapture::SetDev(int index)
	{
		m_nDevIndex = index;
		return 0;
	}

	int CWAVEAudioCapture::StartCapture()
	{
		if (m_nDevType == SPEAKER)
			return -1;

		if (m_bCapturing)
		{
			Error("CWAVEAudioCapture: Already running");
			return -1;
		}

		int ret = InitCapture();
		if (ret != 0)
		{
			Error("CWAVEAudioCapture: InitMic failed");
			return -1;
		}

		/*m_hReturnBufferEvent = CreateEvent(NULL, FALSE, FALSE, NULL);*/
		//m_RecordThread.swap(std::thread(std::bind(&CWAVEAudioCapture::ReturnWaveBufferThreadProc, this)));

		MMRESULT res = waveInStart(m_hWaveIn);
		if (res != MMSYSERR_NOERROR)
		{
			Error("CWAVEAudioCapture: waveInStart failed");
			return -1;
		}

		StartCaptureThread();

		m_bCapturing = true;

		Info("WAVEAudioCapture: Start capture succeed!");

		return 0;
	}

	int CWAVEAudioCapture::StopCapture()
	{
		if (m_bCapturing)
		{
			StopCaptureThread();
			waveInStop(m_hWaveIn);
			waveInReset(m_hWaveIn);
			m_bCapturing = false;

			UnInitCapture();
			return 0;
		}
		return -1;
	}

	int CWAVEAudioCapture::InitCapture()
	{
		if (m_bInited)
		{
			Error("CWAVEAudioCapture: Mic already inited");
			return -1;
		}

		WAVEFORMATEX waveFormat;
		waveFormat.wFormatTag = WAVE_FORMAT_PCM;
		waveFormat.nChannels = N_REC_CHANNELS;
		waveFormat.nSamplesPerSec = N_REC_SAMPLES_PER_SEC;
		waveFormat.wBitsPerSample = N_REC_BITS_PER_SAMPLE;
		waveFormat.nBlockAlign = waveFormat.nChannels * (waveFormat.wBitsPerSample / 8);
		waveFormat.nAvgBytesPerSec = waveFormat.nSamplesPerSec * waveFormat.nBlockAlign;
		waveFormat.cbSize = 0;

		HWAVEIN hWaveIn = NULL;
		uint32_t devID = m_nDevIndex == -1 ? WAVE_MAPPER : (uint32_t)m_nDevIndex;
		MMRESULT res = waveInOpen(NULL, devID, &waveFormat, 0, 0, CALLBACK_NULL | WAVE_FORMAT_QUERY);
		if (res != MMSYSERR_NOERROR)
		{
			Error("CWAVEAudioCapture: waveInOpen failed for query format, res: %d", res);
			CleanUp();
			return -1;
		}

		res = waveInOpen(&hWaveIn, devID, &waveFormat, NULL, NULL, CALLBACK_NULL);
		if (res != MMSYSERR_NOERROR)
		{
			Error("CWAVEAudioCapture: Open wave in handle failed,res: %d");
			CleanUp();
			return -1;
		}

		
		m_stAudioInfo.audio_format = AUDIO_FORMAT_16BIT;

		m_stAudioInfo.sample_rate = N_REC_SAMPLES_PER_SEC;
		if (waveFormat.nChannels == 1)
			m_stAudioInfo.chl_layout = SPEAKERS_MONO;
		else if (waveFormat.nChannels == 2)
			m_stAudioInfo.chl_layout = SPEAKERS_STEREO;
		else
		{
			Error("CWAVEAudioCapture: Unsupported channels number, chl_number: %d",
				waveFormat.nChannels);
			CleanUp();
			return -1;
		}

		m_hWaveIn = hWaveIn;

		for (int n = 0; n < N_BUFFERS_IN; n++)
		{
			uint8_t nBytesPerSample = N_REC_CHANNELS * (N_REC_BITS_PER_SAMPLE / 8);

			m_WaveHeaderIn[n].lpData = (LPSTR)(&m_RecBuffer[n]);
			m_WaveHeaderIn[n].dwBufferLength = nBytesPerSample * REC_BUF_SIZE_IN_SAMPLES;
			m_WaveHeaderIn[n].dwFlags = 0;
			m_WaveHeaderIn[n].dwBytesRecorded = 0;
			m_WaveHeaderIn[n].dwUser = 0;
			m_WaveHeaderIn[n].dwLoops = 1;
			m_WaveHeaderIn[n].lpNext = NULL;
			m_WaveHeaderIn[n].reserved = 0;

			memset(m_RecBuffer[n], 0, nBytesPerSample * REC_BUF_SIZE_IN_SAMPLES);

			res = waveInPrepareHeader(m_hWaveIn, &m_WaveHeaderIn[n], sizeof(WAVEHDR));
			if (res != MMSYSERR_NOERROR)
			{
				Error("CWAVEAudioCapture: waveInPrepareHeader failed");
				CleanUp();
				return -1;
			}

			res = waveInAddBuffer(m_hWaveIn, &m_WaveHeaderIn[n], sizeof(WAVEHDR));
			if (res != MMSYSERR_NOERROR)
			{
				Error("CWAVEAudioCapture: waveInAddBuffer failed");
				CleanUp();
				return -1;
			}
		}

		m_bInited = true;
		return 0;
	}

	int CWAVEAudioCapture::UnInitCapture()
	{
		if (m_bInited)
		{
			CleanUp();
			m_bInited = false;
			return 0;
		}
		return -1;
	}


	void CWAVEAudioCapture::CleanUp()
	{
		if (m_hWaveIn)
		{
			for (int i = 0; i < N_BUFFERS_IN; i++)
			{
				waveInUnprepareHeader(m_hWaveIn, &m_WaveHeaderIn[i], sizeof(WAVEHDR));
			}
			waveInClose(m_hWaveIn);
			m_hWaveIn = NULL;
		}
	}

	int CWAVEAudioCapture::StartCaptureThread()
	{
		if (!m_bRunning)
		{
			m_bRunning = true;
			m_hRecordThread = CreateThread(NULL, 0, &CWAVEAudioCapture::CaptureThread, (LPVOID)this,
				0, NULL);
			return 0;
		}
		return -1;
	}

	int CWAVEAudioCapture::StopCaptureThread()
	{
		if (m_bRunning)
		{
			m_bRunning = false;
			WaitForSingleObject(m_hRecordThread, INFINITE);
			m_hRecordThread = NULL;
			return 0;
		}
		return -1;
	}

	DWORD WINAPI CWAVEAudioCapture::CaptureThread(LPVOID param)
	{
		CWAVEAudioCapture* pThis = (CWAVEAudioCapture*)param;
		if (pThis)
		{
			pThis->CaptureThreadProc();
		}
		return 0;
	}


	void CWAVEAudioCapture::CaptureThreadProc()
	{
		HANDLE hWaitableTimer = CreateWaitableTimer(NULL, FALSE, NULL);
		LARGE_INTEGER fireTime;
		fireTime.QuadPart = -1;
		SetWaitableTimer(hWaitableTimer, &fireTime, 10, NULL, NULL, FALSE);
		int nBufferIndex = 0;
		int nBytesPerSample = N_REC_CHANNELS * (N_REC_BITS_PER_SAMPLE / 8);
		int nBufferTotalSize = nBytesPerSample * REC_BUF_SIZE_IN_SAMPLES;
		while (true)
		{
			WaitForSingleObject(hWaitableTimer, INFINITE);
			if (!m_bRunning)
			{
				Info("CWAVEAudioCapture: CaptureThreadProc receives stop signal");
				break;
			}
				

			while (true)
			{
				if (nBufferIndex == N_BUFFERS_IN)
				{
					nBufferIndex = 0;
				}
				int nCapturedSize = m_WaveHeaderIn[nBufferIndex].dwBytesRecorded;
				if (nCapturedSize == nBufferTotalSize)
				{
					int nSamples = nCapturedSize / nBytesPerSample;
					EnterCriticalSection(&m_sectionDataCb);
					for (IAudioCaptureDataCb* pCb : m_vecDataCb)
					{
						pCb->OnCapturedData(m_WaveHeaderIn[nBufferIndex].lpData, nSamples, 
							m_nDevType, m_stAudioInfo);
					}
					LeaveCriticalSection(&m_sectionDataCb);

					m_WaveHeaderIn[nBufferIndex].dwBytesRecorded = 0;

					MMRESULT res = waveInUnprepareHeader(m_hWaveIn, &(m_WaveHeaderIn[nBufferIndex]), sizeof(WAVEHDR));
					if (res != MMSYSERR_NOERROR)
					{
						Error("CWAVEAudioCapture: waveInUnprepareHeader failed, index: %d", nBufferIndex);
						goto EXIT;
					}

					res = waveInPrepareHeader(m_hWaveIn, &m_WaveHeaderIn[nBufferIndex], sizeof(WAVEHDR));
					if (res != MMSYSERR_NOERROR)
					{
						Error("CWAVEAudioCapture: waveInPrepareHeader failed, index: %d", nBufferIndex);
						goto EXIT;
					}
					
					res = waveInAddBuffer(m_hWaveIn, &m_WaveHeaderIn[nBufferIndex], sizeof(WAVEHDR));
					if (res != MMSYSERR_NOERROR)
					{
						Error("CWAVEAudioCapture: waveInAddBuffer failed, index: %d", nBufferIndex);
						goto EXIT;
					}

					nBufferIndex++;
				}
				else
				{
					break;
				}
			}
		}
	EXIT:
		CloseHandle(hWaitableTimer);
		return;
	}


	/*void CWAVEAudioCapture::CapturedDataCb(HWAVEIN hwi, UINT uMsg,
		DWORD_PTR dwInstance, DWORD_PTR dwParam1, DWORD_PTR dwParam2)
		{
		if (uMsg == WIM_DATA)
		{
		WAVEHDR* pWaveHeader = (WAVEHDR*)dwParam1;
		CWAVEAudioCapture* pThis = (CWAVEAudioCapture*)dwInstance;
		if (pWaveHeader && pThis)
		{
		pThis->OnCapturedData(pWaveHeader);
		}
		}
		}*/


	/*void CWAVEAudioCapture::OnCapturedData(WAVEHDR* pHeader)
	{
	int nBytesPerSample = N_REC_CHANNELS * (N_REC_BITS_PER_SAMPLE / 8);
	int nSamples = pHeader->dwBytesRecorded / nBytesPerSample;
	EnterCriticalSection(&m_sectionDataCb);
	for (IAudioCaptureDataCb* pDataCb : m_vecDataCb)
	{
	pDataCb->OnCapturedMicData(pHeader->lpData, nSamples);
	}
	LeaveCriticalSection(&m_sectionDataCb);

	EnterCriticalSection(&m_sectionReturnBuffer);
	m_vecReturnBuffer.push_back(pHeader);
	LeaveCriticalSection(&m_sectionReturnBuffer);
	SetEvent(m_hReturnBufferEvent);
	}

	void CWAVEAudioCapture::ReturnWaveBufferThreadProc()
	{
	while (m_bRunning)
	{
	DWORD result = WaitForSingleObject(m_hReturnBufferEvent, 100);
	if (result == WAIT_OBJECT_0)
	{
	EnterCriticalSection(&m_sectionReturnBuffer);
	for (WAVEHDR* pHeader : m_vecReturnBuffer)
	{
	pHeader->dwBytesRecorded = 0;
	waveInAddBuffer(m_hWaveIn, pHeader, sizeof(WAVEHDR));
	}
	m_vecReturnBuffer.clear();
	LeaveCriticalSection(&m_sectionReturnBuffer);
	}
	}
	}*/

}