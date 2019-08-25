using System;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] agrs)
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(agrs));
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
                MessageBox.Show("UnhandledException !\n" + ((Exception)e.ExceptionObject).ToString(), "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show("UnhandledException !\n" + (e.Exception).ToString(), "程序错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
