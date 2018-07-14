using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ScreenRecoder.App
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

        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void UpdateCallback(UpdateCallbackID id, IntPtr data);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void RecoderDestroy();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void RecoderCreate();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void RecoderPauseButton();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void RecoderStopButton();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern bool RecoderStartButton();
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void RecoderSetUpdateCallback(IntPtr callback);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void RecoderSetOutFileDir(string filename);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void RecoderSetNextOutFileName(string filename);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void RecoderSetCaptureRect(int x, int y, int w, int h);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void RecoderSetCaptureFrameRate(int rate);
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern string RecoderGetLastOutFileName();

        public static extern VIDEO_FORMAT RecordFormat
        {
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            get;
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            set;
        }
        public static extern int RecordQuality
        {
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            get;
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            set;
        }
        public static extern Record_State State
        {
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern string LastError
        {
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern bool IsRecording
        {
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern bool IsInterrupt
        {
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern int Duration
        {
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern bool RecordSound
        {
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            get;
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            set;
        }
        public static extern bool RecordMouse
        {
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            get;
            [MethodImplAttribute(MethodImplOptions.InternalCall)]
            set;
        }
    }
}
