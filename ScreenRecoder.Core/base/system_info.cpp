
#include "system_info.h"
#include <assert.h>
#include <shlobj.h>

typedef NTSTATUS(WINAPI *pRtlGetVersion)(
    _Out_ PRTL_OSVERSIONINFOW lpVersionInformation
    );

SystemInfo::WindowsVersion SystemInfo::windows_version()
{
    //getversionex在win8.1之后被微软复杂化了一下，在win10以及之后的系统直接调用的话，获取不了正确的版本
    //于是从更底层的函数来获取
    RTL_OSVERSIONINFOEXW os_info;
    os_info.dwOSVersionInfoSize = sizeof(os_info);
    BOOL get_ver_ret = FALSE;
    pRtlGetVersion ver_routine = (pRtlGetVersion)GetProcAddress(GetModuleHandleW(L"ntdll.dll"), "RtlGetVersion");
    if (ver_routine)
        get_ver_ret = (0 == ver_routine((PRTL_OSVERSIONINFOW)&os_info));
    
    if (get_ver_ret)
    {
        if (os_info.dwMajorVersion == 10 && os_info.dwMinorVersion == 0)
        {
            if (os_info.wProductType == VER_NT_WORKSTATION)
                return WindowsVersion::WINDOWS_10;
            else
                return WindowsVersion::WINDOWS_SERVER_2016_TECHNICAL_PREVIEW;
        }
        else if (os_info.dwMajorVersion == 6 && os_info.dwMinorVersion == 3)
        {
            if (os_info.wProductType == VER_NT_WORKSTATION)
                return WindowsVersion::WINDOWS_8_1;
            else
                return WindowsVersion::WINDOWS_SERVER_2012_R2;

        }
        else if (os_info.dwMajorVersion == 6 && os_info.dwMinorVersion == 2)
        {
            if (os_info.wProductType == VER_NT_WORKSTATION)
                return WindowsVersion::WINDOWS_8;
            else
                return WindowsVersion::WINDOWS_SERVER_2012;
        }
        else if (os_info.dwMajorVersion == 6 && os_info.dwMinorVersion == 1)
        {
            if (os_info.wProductType == VER_NT_WORKSTATION)
                return WindowsVersion::WINDOWS_7;
            else
                return WindowsVersion::WINDOWS_SERVER_2008_R2;
        }
        else if (os_info.dwMajorVersion == 6 && os_info.dwMinorVersion == 0)
        {
            if (os_info.wProductType == VER_NT_WORKSTATION)
                return WindowsVersion::WINDOWS_VISTA;
            else
                return WindowsVersion::WINDOWS_SERVER_2008;
        }
        else if (os_info.dwMajorVersion == 5 && os_info.dwMinorVersion == 2)
        {
            if (GetSystemMetrics(SM_SERVERR2) != 0)
                return WindowsVersion::WINDOWS_SERVER_2003_R2;
            else
                return WindowsVersion::WINDOWS_SERVER_2003;
        }
        else if (os_info.dwMajorVersion == 5 && os_info.dwMinorVersion == 1)
        {
            return WindowsVersion::WINDOWS_XP;
        }
        else
            return WindowsVersion::WINDOWS_UNSUPPORT_VERSION;
    }
    else
        return WindowsVersion::WINDOWS_UNKNOWN_VERSION;
}

std::string SystemInfo::windows_version_string(WindowsVersion ver)
{
    std::string ver_str;
    switch (ver)
    {
    case WINDOWS_XP:
        ver_str = "Windows XP";
        break;
    case WINDOWS_SERVER_2003:
        ver_str = "Windows Server 2003";
        break;
    case WINDOWS_SERVER_2003_R2:
        ver_str = "Windows Server 2003 R2";
        break;
    case WINDOWS_VISTA:
        ver_str = "Windows Vista";
        break;
    case WINDOWS_SERVER_2008:
        ver_str = "Windows Server 2008";
        break;
    case WINDOWS_SERVER_2008_R2:
        ver_str = "Windows Server 2008 R2";
        break;
    case WINDOWS_7:
        ver_str = "Windows 7";
        break;
    case WINDOWS_SERVER_2012:
        ver_str = "Windows Server 2012";
        break;
    case WINDOWS_8:
        ver_str = "Windows 8";
        break;
    case WINDOWS_SERVER_2012_R2:
        ver_str = "Windows Server 2010 R2";
        break;
    case WINDOWS_8_1:
        ver_str = "Windows 8.1";
        break;
    case WINDOWS_SERVER_2016_TECHNICAL_PREVIEW:
        ver_str = "Windows Server 2016 Preview";
        break;
    case WINDOWS_10:
        ver_str = "Windows 10";
        break;
    case WINDOWS_UNSUPPORT_VERSION:
        ver_str = "Unsupport Windows Version";
        break;
    case WINDOWS_UNKNOWN_VERSION:
        ver_str = "Unknown Windows Version";
        break;
    default:
        assert(0);
    }

    return ver_str;
}

std::string SystemInfo::cpu_model()
{
    int cpuInfo[4] = { -1 };
    char CPUBrandString[0x40] = { 0 };
    int nExIds_ = 0;
    try
    {
        /*__cpuid(cpuInfo, 0x80000000);
        nExIds_ = cpuInfo[0];

        if (nExIds_ >= 0x80000004)
        {
            memset(CPUBrandString, 0, sizeof(CPUBrandString));

            __cpuid(cpuInfo, 0x80000002);
            memcpy(CPUBrandString, cpuInfo, sizeof(cpuInfo));

            __cpuid(cpuInfo, 0x80000003);
            memcpy(CPUBrandString + 16, cpuInfo, sizeof(cpuInfo));

            __cpuid(cpuInfo, 0x80000004);
            memcpy(CPUBrandString + 32, cpuInfo, sizeof(cpuInfo));
        }*/
    }
    catch (...)
    {

    }

    return CPUBrandString;
}

uint64_t SystemInfo::physical_memory_size()
{
    MEMORYSTATUSEX memory_status;
    memory_status.dwLength = sizeof(memory_status);
    if (GlobalMemoryStatusEx(&memory_status))
        return memory_status.ullTotalPhys;
    else
        return 0;
}

bool SystemInfo::is_x64_system()
{
    SYSTEM_INFO sysinfo;
    ::GetNativeSystemInfo(&sysinfo);

    return sysinfo.wProcessorArchitecture != PROCESSOR_ARCHITECTURE_INTEL;
}

