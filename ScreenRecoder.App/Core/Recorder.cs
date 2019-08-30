using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ScreenRecoder.App.Core
{
    //录制底层库
    public class Recorder
    {
        public enum Record_State
        {
            NOT_BEGIN,
            RECORDING,
            SUSPENDED,
        };
        public enum UpdateCallbackID
        {
            OnStart,
            OnPause,
            OnContinue,
            OnStop,
        };
        public enum VIDEO_FORMAT
        {
            VIDEO_FORMAT_UNKOWN = 0,
            VIDEO_FORMAT_H264_MP4,
            VIDEO_FORMAT_MPEG4_MP4,
            VIDEO_FORMAT_H264_AVI,
            VIDEO_FORMAT_MPEG4_AVI,
            VIDEO_FORMAT_FLV,
            VIDEO_FORMAT_WMV
        };

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UpdateCallback(UpdateCallbackID id, IntPtr data);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Destroy();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Create();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PauseButton();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopButton();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StartButton();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void SetUpdateCallback(IntPtr callback);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void SetOutFileDir(string filename);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void SetNextOutFileName(string filename);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void SetCaptureRect(int x, int y, int w, int h);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void SetCaptureFrameRate(int rate);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string GetLastOutFileName();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void DrawPreviewRect(IntPtr hdc, int x, int y, int w, int h, int tw, int th);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string[] GetAudioCaptureDevices();

        public static extern Record_State State
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern string LastError
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern bool IsRecording
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern bool IsInterrupt
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern int Duration
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        public static extern VIDEO_FORMAT RecordFormat
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public static extern int RecordQuality
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public static extern int RecordMicDevIndex
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public static extern bool RecordSound
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public static extern bool RecordMouse
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
    }
}
