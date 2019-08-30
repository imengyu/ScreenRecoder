using System;
using System.Runtime.CompilerServices;

namespace ScreenRecoder.App.Api
{
    static class DialogUtils
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string ShowSaveImageFileDialog(IntPtr hWnd, string title, string fileName);
    }
}
