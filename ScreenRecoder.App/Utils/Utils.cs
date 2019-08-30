using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScreenRecoder.App.Utils
{
    class Utils
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void PlayTip(string name, bool ansyc);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void OpenFile(string path);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void SelectFileInExplorer(string path);

        public static void WriteDebugString(string str)
        {
            DebugUtils.WriteDebugString(str);
        }
    }
}
