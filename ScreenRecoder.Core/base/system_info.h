#pragma once

#include <string>
#include <stdint.h>

class SystemInfo
{
public:
    SystemInfo() = default;
    virtual ~SystemInfo() = default;

    enum WindowsVersion
    {
        WINDOWS_XP = 0,
        WINDOWS_SERVER_2003,
        WINDOWS_SERVER_2003_R2,
        WINDOWS_VISTA,
        WINDOWS_SERVER_2008,
        WINDOWS_SERVER_2008_R2,
        WINDOWS_7,
        WINDOWS_SERVER_2012,
        WINDOWS_8,
        WINDOWS_SERVER_2012_R2,
        WINDOWS_8_1,
        WINDOWS_SERVER_2016_TECHNICAL_PREVIEW,
        WINDOWS_10,
        WINDOWS_UNSUPPORT_VERSION = 0xfff0,
        WINDOWS_UNKNOWN_VERSION = 0xfff1
    };

public:

    WindowsVersion windows_version();
    std::string windows_version_string(WindowsVersion ver);

    std::string cpu_model();
    uint64_t physical_memory_size();
    bool is_x64_system();
};