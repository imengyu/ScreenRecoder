using System;
using System.Runtime.CompilerServices;

namespace ScreenRecoder.App.Api
{
    static class WindowUtils
    {

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Show(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Hide(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsVisible(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void SetForeground(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Move(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Top(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Bottom(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsTop(IntPtr hWnd);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void ShowAnim(IntPtr hWnd);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool HideAnim(IntPtr hWnd);
    }
}
