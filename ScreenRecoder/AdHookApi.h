#pragma once
#include "stdafx.h"
#include <tchar.h>  
#include <vector>  
using namespace std;

// class CAdAutoHookApi  
class CAdHookApi;
class CAdAutoHookApi
{
public:
	CAdAutoHookApi(CAdHookApi *pHookApi, void *pAddr);
	virtual ~CAdAutoHookApi();

private:
	CAdHookApi * m_pHookApi;
	void       *m_pAddr;
};

// class CAdAutoHook  
class CAdHookApi
{
public:
	CAdHookApi();
	virtual ~CAdHookApi();

protected:
	struct HookMap
	{
		HANDLE hProcess;
		void  *pOldAddr;
		void  *pNewAddr;
		BYTE   chOldCode[8];
		BYTE   chNewCode[8];
		BOOL   bHooked;
		DWORD  dwData;
	};
public:
	HANDLE Add(LPCTSTR lpszModule, LPCSTR lpcFuncName, void *pNewAddr, DWORD dwData = 0, LPVOID *pzOldAddr = 0);
	HANDLE Add(void *pOldAddr, void *pNewAddr, const BYTE *verifyData = NULL, DWORD verifySize = 0, DWORD dwData = 0, LPVOID *pzOldAddr = 0);
	BOOL   Remove(HANDLE hHook);
	BOOL   Begin(HANDLE hHook);
	BOOL   End(HANDLE hHook);
	BOOL   Begin2(void *pNewAddr);
	BOOL   End2(void *pNewAddr);
	int    BeginAll();
	int    EndAll();
	int    GetCount();
	void  *OldAddr2NewAddr(void *pOldAddr);
	void  *NewAddr2OldAddr(void *pNewAddr);

public:
	static BOOL VerifyAddress(void *pAddr, const BYTE *verifyData, DWORD verifySize);

	static BOOL PatchCode(void *pAddr, const BYTE *pCode, DWORD dwCode,
		const BYTE *verifyData = NULL, DWORD verifySize = 0);

protected:
	CAdHookApi::HookMap *FromNewAddr(void *pNewAddr);
	CAdHookApi::HookMap *FromOldAddr(void *pOldAddr);
	BOOL HasHook(HANDLE hHook);

protected:
	vector<HookMap *> m_obHooks;
};

