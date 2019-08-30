using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ScreenRecoder.App.Utils
{
    static class DebugUtils
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Test1();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Test2(string str);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string Test3();

        public static void InitExceptionCatcher()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
            {
                string str = "UnhandledException !\n" + ((Exception)e.ExceptionObject).ToString();
                Utils.WriteDebugString(str);
                MessageBox.Show(str, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = "ThreadException !\n" + (e.Exception).ToString();
            Utils.WriteDebugString(str);
            MessageBox.Show(str, "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShouldShowDebugWindow();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void WriteDebugString(string str);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void DestroyDebugOutPutPippe();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string ReadDebugOutPutData();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CreateDebugOutPutPippe();
    }
}
