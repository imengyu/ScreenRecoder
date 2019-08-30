using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ScreenRecoder.App.Api
{
    //一些api
    public static class API
    {
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_TRANSPARENT = 0x00000020;

        public const int WM_HOTKEY = 0x0312;
        public const int WM_DISPLAYCHANGE = 0x007E;


        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        public const int SM_CXFULLSCREEN = 16;
        public const int SM_CYFULLSCREEN = 17;

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

            public int Width { get { return Right - Left; } }
            public int Height { get { return Bottom - Top; } }

        }
 
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
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int index);

        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void IniWriteValue(string Section, string Key, string Value);
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string IniReadValue(string Section, string Key, string DefaultValue = "");
    }
}
