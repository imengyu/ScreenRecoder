using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ScreenRecoder.App
{
    //一些api
    public static class API
    {
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        public struct POINT
        {
            public POINT(int x,int y)
            {
                cx = x;
                cy = y;
            }

            public int cx;
            public int cy;
        }
        public struct RECT
        {
            public RECT(int l,int t,int r ,int b )
            {
                Left = l;
                Top = t;
                Right = r;
                Bottom = b;
            }

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr hwnd, POINT point, uint flags);
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        private static string inipath = "";

        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public static void IniWriteValue(string Section, string Key, string Value)
        {
            if (inipath == "") inipath = GetInIPath();
            WritePrivateProfileString(Section, Key, Value, inipath);
        }
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public static string IniReadValue(string Section, string Key, string DefaultValue = "")
        {
            if (inipath == "") inipath = GetInIPath();
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, DefaultValue, temp, 500, inipath);
            return temp.ToString();
        }

        /*[MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void WindowMove(IntPtr hWnd);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void WindowShow(IntPtr hWnd);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void WindowHide(IntPtr hWnd);*/

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void AppOpenFile(string path);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void WindowSetForeground(IntPtr hWnd);        
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Test();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string GetDefExportDir();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void WindowMove(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void WindowShow(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void WindowHide(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool WindowIsVisible(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string GetInIPath();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void StartNewWhenExit();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int i, int k1, int k2, int k3);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void UnRegisterAllHotKey(IntPtr hWnd);

    }
}
