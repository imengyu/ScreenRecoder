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

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UpdateCallback(UpdateCallbackID id, IntPtr data);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void RecoderDestroy();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void RecoderCreate();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void RecoderPauseButton();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void RecoderStopButton();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RecoderStartButton();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void RecoderSetUpdateCallback(IntPtr callback);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void RecoderSetOutFileDir(string filename);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void RecoderSetNextOutFileName(string filename);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void RecoderSetCaptureRect(int x, int y, int w, int h);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void RecoderSetCaptureFrameRate(int rate);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string RecoderGetLastOutFileName();

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
