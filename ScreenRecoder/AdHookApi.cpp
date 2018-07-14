#include "stdafx.h"
#include "AdHookApi.h"
#include <stdio.h>  
#include <string.h>  
#include <assert.h>  
#include <Windows.h>  

static BOOL gUseAPI = TRUE;

static BOOL WINAPI myReadMemory(HANDLE hProcess, LPVOID lpAddress, LPVOID lpBuffer, SIZE_T nSize)
{
	BOOL bRet = FALSE;
	DWORD dwOldProtect = 0;
	bRet = VirtualProtect(lpAddress, nSize, PAGE_READONLY, &dwOldProtect);
	if (gUseAPI)
	{
		DWORD dwRead = 0;
		bRet = ReadProcessMemory(hProcess, lpAddress, lpBuffer, nSize, &dwRead);
	}
	else
	{
		memcpy(lpBuffer, lpAddress, nSize);
	}
	VirtualProtect(lpAddress, nSize, dwOldProtect, &dwOldProtect);
	assert(bRet);
	return bRet;
}
static BOOL WINAPI myWriteMemory(HANDLE hProcess, LPVOID lpAddress, LPCVOID lpBuffer, SIZE_T nSize)
{
	BOOL bRet = FALSE;
	DWORD dwOldProtect = 0;
	bRet = VirtualProtect(lpAddress, nSize, PAGE_READWRITE, &dwOldProtect);
	if (gUseAPI)
	{
		DWORD dwWrite = 0;
		bRet = WriteProcessMemory(hProcess, lpAddress, lpBuffer, nSize, &dwWrite);
	}
	else
	{
		memcpy(lpAddress, lpBuffer, nSize);
	}
	VirtualProtect(lpAddress, nSize, dwOldProtect, &dwOldProtect);
	assert(bRet);
	return bRet;
}

// class CAdAutoHookApi  
CAdAutoHookApi::CAdAutoHookApi(CAdHookApi *pHookApi, void *pAddr)
{
	m_pHookApi = pHookApi;
	m_pAddr = pAddr;

	assert(m_pHookApi != NULL);

	if (m_pHookApi != NULL)
	{
		m_pHookApi->End2(m_pAddr);
	}
}
CAdAutoHookApi::~CAdAutoHookApi()
{
	if (m_pHookApi != NULL)
	{
		m_pHookApi->Begin2(m_pAddr);
	}
}

// class CAdHookApi  
CAdHookApi::CAdHookApi()
{
}
CAdHookApi::~CAdHookApi()
{
	EndAll();
}

BOOL CAdHookApi::VerifyAddress(void *pAddr, const BYTE *verifyData, DWORD verifySize)
{
	BOOL isPassed = FALSE;
	if ((verifyData != NULL) && (verifySize > 0))
	{
		BYTE *addrData = new BYTE[verifySize];
		if (myReadMemory(GetCurrentProcess(), pAddr, addrData, verifySize))
		{
			if (memcmp(addrData, verifyData, verifySize) == 0)
			{
				isPassed = TRUE;
			}
		}
		delete[]addrData;
	}
	else
	{
		isPassed = TRUE;
	}
	return isPassed;
}
BOOL CAdHookApi::PatchCode(void *pAddr, const BYTE *pCode, DWORD dwCode,
	const BYTE *verifyData, DWORD verifySize)
{
	if (!VerifyAddress(pAddr, verifyData, verifySize))
	{
		return FALSE;
	}
	BOOL bRet = myWriteMemory(GetCurrentProcess(), pAddr, pCode, dwCode);
	return bRet;
}

HANDLE CAdHookApi::Add(LPCTSTR lpszModule, LPCSTR lpcFuncName, void *pNewAddr, DWORD dwData, LPVOID *pzOldAdd)
{
	HMODULE hModule = LoadLibrary(lpszModule);
	if (hModule == NULL)
	{
		return NULL;
	}

	void *pOldAddr = (void *)GetProcAddress(hModule, lpcFuncName);
	if (pOldAddr == NULL)
	{
		return NULL;
	}

	return Add(pOldAddr, pNewAddr, NULL, 0, dwData, pzOldAdd);
}
HANDLE CAdHookApi::Add(void *pOldAddr, void *pNewAddr, const BYTE *verifyData, DWORD verifySize, DWORD dwData, LPVOID *pzOldAdd)
{
	BOOL bRet = FALSE;
	HookMap *pHook = new HookMap;
	do
	{
		ZeroMemory(pHook, sizeof(HookMap));

		pHook->hProcess = GetCurrentProcess();

		pHook->pOldAddr = pOldAddr;
		if (pHook->pOldAddr == NULL)
			break;
		else if (pzOldAdd != NULL)
			*pzOldAdd = pOldAddr;

		DWORD dwRead = 8;
		if ((verifyData != NULL) && (verifySize > 0) && (verifySize > dwRead))
		{
			dwRead = verifySize;
		}
		BYTE *addrData = new BYTE[dwRead];
		if (!myReadMemory(pHook->hProcess, pHook->pOldAddr, addrData, dwRead))
		{
			delete[]addrData;
			break;
		}
		if ((verifyData != NULL) && (verifySize > 0) && (memcmp(addrData, verifyData, verifySize) != 0))
		{
			delete[]addrData;
			break;
		}
		memcpy(pHook->chOldCode, addrData, 8);
		delete[]addrData;

		DWORD dwTemp = (DWORD)pNewAddr;
		pHook->pNewAddr = pNewAddr;

		// mov eax, pNewAddr  
		// jmp eax  
		pHook->chNewCode[0] = 0xB8;
		memcpy(pHook->chNewCode + 1, &dwTemp, sizeof(DWORD));
		pHook->chNewCode[5] = 0xFF;
		pHook->chNewCode[6] = 0xE0;

		pHook->bHooked = FALSE;

		pHook->dwData = dwData;

		m_obHooks.push_back(pHook);

		bRet = TRUE;
	} while (0);
	if (!bRet)
	{
		delete pHook;
		pHook = NULL;
	}

	return (HANDLE)pHook;
}
BOOL CAdHookApi::Remove(HANDLE hHook)
{
	BOOL bRet = FALSE;
	HookMap *pHook = (HookMap *)hHook;
	for (int i = 0; i < (int)m_obHooks.size(); i++)
	{
		HookMap *pTemp = m_obHooks[i];
		if (pTemp == pHook)
		{
			End((HANDLE)pTemp);
			delete pHook;
			m_obHooks.erase(m_obHooks.begin() + i);
			bRet = TRUE;
			break;
		}
	}

	return bRet;
}
BOOL CAdHookApi::Begin(HANDLE hHook)
{
	if (!HasHook(hHook))
	{
		return FALSE;
	}
	HookMap *pHook = (HookMap *)hHook;
	if (pHook->bHooked)
	{
		return TRUE;
	}
	DWORD dwWrite = 8;
	BOOL bRet = myWriteMemory(pHook->hProcess, pHook->pOldAddr, pHook->chNewCode, dwWrite);
	if (bRet)
	{
		pHook->bHooked = TRUE;
	}
	return bRet;
}
BOOL CAdHookApi::End(HANDLE hHook)
{
	if (!HasHook(hHook))
	{
		return FALSE;
	}
	HookMap *pHook = (HookMap *)hHook;
	if (!pHook->bHooked)
	{
		return FALSE;
	}
	DWORD dwWrite = 8;
	BOOL bRet = myWriteMemory(pHook->hProcess, pHook->pOldAddr, pHook->chOldCode, dwWrite);
	if (bRet)
	{
		pHook->bHooked = FALSE;
	}
	return bRet;
}
BOOL CAdHookApi::Begin2(void *pNewAddr)
{
	HookMap *pHook = FromNewAddr(pNewAddr);
	if (pHook == NULL)
	{
		return FALSE;
	}

	return Begin((HANDLE)pHook);
}
BOOL CAdHookApi::End2(void *pNewAddr)
{
	HookMap *pHook = FromNewAddr(pNewAddr);
	if (pHook == NULL)
	{
		return FALSE;
	}

	return End((HANDLE)pHook);
}
void *CAdHookApi::OldAddr2NewAddr(void *pOldAddr)
{
	HookMap *pHook = FromOldAddr(pOldAddr);
	if (pHook == NULL)
	{
		return NULL;
	}

	return pHook->pNewAddr;
}
void *CAdHookApi::NewAddr2OldAddr(void *pNewAddr)
{
	HookMap *pHook = FromNewAddr(pNewAddr);
	if (pHook == NULL)
	{
		return NULL;
	}

	return pHook->pOldAddr;
}
CAdHookApi::HookMap *CAdHookApi::FromNewAddr(void *pNewAddr)
{
	HookMap *pHook = NULL;
	for (int i = 0; i < (int)m_obHooks.size(); i++)
	{
		HookMap *pTemp = m_obHooks[i];
		if (pTemp->pNewAddr == pNewAddr)
		{
			pHook = pTemp;
			break;
		}
	}

	return pHook;
}
CAdHookApi::HookMap *CAdHookApi::FromOldAddr(void *pOldAddr)
{
	HookMap *pHook = NULL;
	for (int i = 0; i < (int)m_obHooks.size(); i++)
	{
		HookMap *pTemp = m_obHooks[i];
		if (pTemp->pOldAddr == pOldAddr)
		{
			pHook = pTemp;
			break;
		}
	}

	return pHook;
}
BOOL CAdHookApi::HasHook(HANDLE hHook)
{
	BOOL bRet = FALSE;
	HookMap *pHook = (HookMap *)hHook;
	for (int i = 0; i < (int)m_obHooks.size(); i++)
	{
		HookMap *pTemp = m_obHooks[i];
		if (pTemp == pHook)
		{
			bRet = TRUE;
			break;
		}
	}

	return bRet;
}
int CAdHookApi::BeginAll()
{
	int nRet = 0;
	for (int i = 0; i < (int)m_obHooks.size(); i++)
	{
		HookMap *pTemp = m_obHooks[i];
		BOOL bRet = Begin((HANDLE)pTemp);
		if (bRet)
		{
			nRet++;
		}
	}

	return nRet;
}
int CAdHookApi::EndAll()
{
	int nRet = 0;
	for (int i = 0; i < (int)m_obHooks.size(); i++)
	{
		HookMap *pTemp = m_obHooks[i];
		BOOL bRet = End((HANDLE)pTemp);
		delete pTemp;
		if (bRet)
		{
			nRet++;
		}
	}
	m_obHooks.clear();

	return nRet;
}
int CAdHookApi::GetCount()
{
	return (int)m_obHooks.size();
}