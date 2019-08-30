using ScreenRecoder.App.Utils;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenRecoder.App.Api
{
    static class AppUtils
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string GetStartDir();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string GetDefExportDir();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string GetInIPath();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void StartNewWhenExit();

        public static bool RegisterHotKeyEx(IntPtr hWnd, int i, Keys[] keys)
        {
            if (!HotKeySelecter.IsEmepty(keys))
            {
                bool bSuccess = RegisterHotKey(hWnd, i, (int)HotKeySelecter.ModKeyRealloc(keys[0]),
                    (int)HotKeySelecter.ModKeyRealloc(keys[1]),
                    (int)HotKeySelecter.ModKeyRealloc(keys[2]));
                if (!bSuccess)
                    DebugUtils.WriteDebugString("WARN : Register Hotkey " + i + " failed : " + Marshal.GetLastWin32Error() + ".\n");
                return bSuccess;
            }
            else
            {
                DebugUtils.WriteDebugString("WARN : Register Hotkey " + i + " Empty.\n");
            }
            
            return true;
        }
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int i, int k1, int k2, int k3);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void UnRegisterAllHotKey(IntPtr hWnd);
    }
}
