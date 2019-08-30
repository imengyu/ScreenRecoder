using ScreenRecoder.App.Api;
using System;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    //设置保存读取类
    static class Settings
    {
        public static bool close_act_exit = true,
            fullscreen = false,
            recmic = false,
            recsound = true,
            rempos = true,
            notify_when_finish = true,
            show_mini_recing = true,
            show_preview = true,
            window_top = true,
            playsound = true,
            hide_wnd_when_rec = false;
        public static int last_x = 0,
            last_y = 0,
            last_w = 0,
            last_h = 0,
            last_appx = 0,
            last_appy = 0,
            quality = 1,
            mic_index;
        public static int frame_rate = 15;
        public static string VideoType = "DEFAULT",
            SaveDir = "DEFAULT";
        public static Keys[] hotkey_start = new Keys[3];
        public static Keys[] hotkey_stop = new Keys[3];
        public static Keys[] hotkey_pause = new Keys[3];
        public static Keys[] hotkey_showehide = new Keys[3];
        public static Keys[] hotkey_screenshutcut = new Keys[3];

        //读取
        public static void LoadSettings()
        {
            string f = "";
            close_act_exit = API.IniReadValue("AppSetting", "CloseAction", "") == "Close";
            int.TryParse(API.IniReadValue("AppSetting", "LastAppX", "0"), out last_appx);
            int.TryParse(API.IniReadValue("AppSetting", "LastAppY", "0"), out last_appy);
            f = API.IniReadValue("AppSetting", "NotifyWhenFinished", "True");
            notify_when_finish = f == "True" || f == "true" || f == "1" || f == "TRUE";
            f = API.IniReadValue("AppSetting", "HideWindowWhenRecording", "False");
            hide_wnd_when_rec = f == "True" || f == "true" || f == "1" || f == "TRUE";
            f = API.IniReadValue("AppSetting", "UseMiniWindowWhenRecording", "True");
            show_mini_recing = f == "True" || f == "true" || f == "1" || f == "TRUE";
            f = API.IniReadValue("AppSetting", "ShowPreview", "True");
            show_preview = f == "True" || f == "true" || f == "1" || f == "TRUE";
            f = API.IniReadValue("AppSetting", "PlaySoundTip", "True");
            playsound = f == "True" || f == "true" || f == "1" || f == "TRUE";
            f = API.IniReadValue("AppSetting", "TopMost", "False");
            window_top = f == "True" || f == "true" || f == "1" || f == "TRUE";

            string s = API.IniReadValue("HotKeySetting", "Start", "F1,None,None");
            if (s.Contains(","))
            {
                try
                {
                    string[] ss = s.Split(',');
                    for (int i = 0; i < 3 && i < ss.Length; i++)
                        hotkey_start[i] = (Keys)Enum.Parse(typeof(Keys), ss[i]);
                }
                catch
                {

                }
            }
            s = API.IniReadValue("HotKeySetting", "PauseContinue", "F2,None,None");
            if (s.Contains(","))
            {
                try
                {
                    string[] ss = s.Split(',');
                    for (int i = 0; i < 3 && i < ss.Length; i++)
                        hotkey_pause[i] = (Keys)Enum.Parse(typeof(Keys), ss[i]);
                }
                catch
                {

                }
            }
            s = API.IniReadValue("HotKeySetting", "Stop", "F3,None,None");
            if (s.Contains(","))
            {
                try
                {
                    string[] ss = s.Split(',');
                    for (int i = 0; i < 3 && i < ss.Length; i++)
                        hotkey_stop[i] = (Keys)Enum.Parse(typeof(Keys), ss[i]);
                }
                catch
                {

                }
            }
            s = API.IniReadValue("HotKeySetting", "ShowHide", "R,Shift,None");
            if (s.Contains(","))
            {
                try
                {
                    string[] ss = s.Split(',');
                    for (int i = 0; i < 3 && i < ss.Length; i++)
                        hotkey_showehide[i] = (Keys)Enum.Parse(typeof(Keys), ss[i]);
                }
                catch
                {

                }
            }
            s = API.IniReadValue("HotKeySetting", "ScreenShutcut", "None,None,None");
            if (s.Contains(","))
            {
                try
                {
                    string[] ss = s.Split(',');
                    for (int i = 0; i < 3 && i < ss.Length; i++)
                        hotkey_screenshutcut[i] = (Keys)Enum.Parse(typeof(Keys), ss[i]);
                }
                catch
                {

                }
            }

            f = API.IniReadValue("RecorderSetting", "FullScreen", "");
            fullscreen = f == "True" || f == "true" || f == "1" || f == "TRUE";
            f = API.IniReadValue("RecorderSetting", "RecordSound", "True");
            recsound = f == "True" || f == "true" || f == "1" || f == "TRUE";
            f = API.IniReadValue("RecorderSetting", "RecordMouse", "");
            recmic = f == "True" || f == "true" || f == "1" || f == "TRUE";
            f = API.IniReadValue("AppSetting", "Rempos", "True");
            rempos = f == "True" || f == "true" || f == "1" || f == "TRUE";

            int.TryParse(API.IniReadValue("RecorderSetting", "Quality", "1"), out quality);
            int.TryParse(API.IniReadValue("RecorderSetting", "LastX", "0"), out last_x);
            int.TryParse(API.IniReadValue("RecorderSetting", "LastY", "0"), out last_y);
            int.TryParse(API.IniReadValue("RecorderSetting", "LastW", "0"), out last_w);
            int.TryParse(API.IniReadValue("RecorderSetting", "LastH", "0"), out last_h);
            int.TryParse(API.IniReadValue("RecorderSetting", "MicIndex", "-1"), out mic_index);
            int.TryParse(API.IniReadValue("RecorderSetting", "FrameRate", "15"), out frame_rate);
            if (frame_rate < 10) frame_rate = 10;
            if (frame_rate > 30) frame_rate = 30;
            VideoType = API.IniReadValue("RecorderSetting", "VideoType", "DEFAULT");
            SaveDir = API.IniReadValue("RecorderSetting", "SaveDir", "DEFAULT");
        }
        //保存
        public static void SaveSettings()
        {
            API.IniWriteValue("AppSetting", "HideWindowWhenRecording", hide_wnd_when_rec ? "True" : "False");
            API.IniWriteValue("AppSetting", "ShowPreview", show_preview ? "True" : "False");
            API.IniWriteValue("AppSetting", "TopMost", window_top ? "True" : "False");
            API.IniWriteValue("AppSetting", "UseMiniWindowWhenRecording", show_mini_recing ? "True" : "False");
            API.IniWriteValue("AppSetting", "NotifyWhenFinished", notify_when_finish ? "True" : "False");
            API.IniWriteValue("AppSetting", "Rempos", rempos ? "True" : "False");
            API.IniWriteValue("AppSetting", "CloseAction", close_act_exit ? "Close" : "Hide");
            API.IniWriteValue("AppSetting", "LastAppX", last_appx.ToString());
            API.IniWriteValue("AppSetting", "LastAppY", last_appy.ToString());
            API.IniWriteValue("AppSetting", "PlaySoundTip", playsound ? "True" : "False");

            API.IniWriteValue("RecorderSetting", "RecordSound", recsound ? "True" : "False");
            API.IniWriteValue("RecorderSetting", "RecordMouse", recmic ? "True" : "False");
            API.IniWriteValue("RecorderSetting", "FullScreen", fullscreen ? "True" : "False");
            API.IniWriteValue("RecorderSetting", "Quality", quality.ToString());
            API.IniWriteValue("RecorderSetting", "LastX", last_x.ToString());
            API.IniWriteValue("RecorderSetting", "LastY", last_y.ToString());
            API.IniWriteValue("RecorderSetting", "LastW", last_w.ToString());
            API.IniWriteValue("RecorderSetting", "LastH", last_h.ToString());
            API.IniWriteValue("RecorderSetting", "MicIndex", mic_index.ToString());

            API.IniWriteValue("RecorderSetting", "VideoType", VideoType);
            API.IniWriteValue("RecorderSetting", "SaveDir", SaveDir);

            string s = "";
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) s += hotkey_start[i].ToString();
                else s += "," + hotkey_start[i].ToString();
            }
            API.IniWriteValue("HotKeySetting", "Start", s);
            s = "";
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) s += hotkey_pause[i].ToString();
                else s += "," + hotkey_pause[i].ToString();
            }
            API.IniWriteValue("HotKeySetting", "PauseContinue", s);
            s = "";
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) s += hotkey_stop[i].ToString();
                else s += "," + hotkey_stop[i].ToString();
            }
            API.IniWriteValue("HotKeySetting", "Stop", s);
            s = "";
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) s += hotkey_showehide[i].ToString();
                else s += "," + hotkey_showehide[i].ToString();
            }
            API.IniWriteValue("HotKeySetting", "ShowHide", s);
            s = "";
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) s += hotkey_screenshutcut[i].ToString();
                else s += "," + hotkey_screenshutcut[i].ToString();
            }
            API.IniWriteValue("HotKeySetting", "ScreenShutcut", s);
        }
    }
}
