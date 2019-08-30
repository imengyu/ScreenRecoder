using ScreenRecoder.App.Api;
using ScreenRecoder.App.Core;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

/**
 * 程序主窗口
 * 
 * 2018/9 by DreamFish
 * 2019/8 by dk and DreamFish
 * 2019/8/28 Last check by 梦欤
 * 
 */

namespace ScreenRecoder.App
{
    //主窗口
    public partial class FormMain : Form
    {
        public static FormMain formMain;

        private IntPtr recoder_update_callback_ptr = IntPtr.Zero;
        private Font screenPreviewFont = new Font("微软雅黑", 13);
        private bool exit = false;
        private Size screenSize = new Size();

        public FormMain(string[] agrs)
        {
            ReadAgrs(agrs);
            InitializeComponent(); formMain = this;

            centerStringFormat = new StringFormat();
            centerStringFormat.LineAlignment = StringAlignment.Center;
            centerStringFormat.Alignment = StringAlignment.Center;
        }

        //两个选择区域窗口
        private FormRect formRect = null;
        private FormRectRecing formRectRecing = null;
        //迷你窗口
        private FormRecMini formRecMini = null;
        //设置窗口
        private FormSettings formSettings = null;
        //截图窗口
        private FormScreenShutcut formScreenShutcut = null;

        //初始化和退出
        private void FormMain_Load(object sender, EventArgs e)
        {
            //加载字体
            PrivateFontCollection prc = new PrivateFontCollection();
            prc.AddFontFile(AppUtils.GetStartDir() + "\\font\\bahnschrift.ttf");
            lb_time.Font = new Font(prc.Families[0], 16);
            lb_time.Top = 1;
            lb_time.Height = 60;

            //创建
            //recoder_update_callback_ptr = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate((Recorder.UpdateCallback)recoder_update);
            //GC.KeepAlive(recoder_update_callback_ptr);

            //创建录制底层库实例
            Recorder.Create();
            Recorder.SetUpdateCallback(recoder_update_callback_ptr);

            API.IniWriteValue("AppSetting", "LastWindow", Handle.ToString());

            //创建矩形选择框
            formRect = new FormRect();
            formRect.Show();
            formRectRecing = new FormRectRecing();
            formRectRecing.Show();

            formRecMini = new FormRecMini();
            formRecMini.lb_time.Font = new Font(prc.Families[0], 12);
            formRecMini.Show();

            //隐藏正在录制框
            WindowUtils.Hide(formRectRecing.Handle);
            //隐藏迷你窗口
            WindowUtils.Hide(formRecMini.Handle);

            //获取屏幕大小
            UpdateScreenSize();

            //加载设置
            Settings.LoadSettings();
            //检查设置是否合法
            if (Settings.frame_rate <= 0 || Settings.frame_rate > 30)
                Settings.frame_rate = 15;
            if (Settings.last_appx <= 0 || Settings.last_appx > screenSize.Width)
                Settings.last_appx = screenSize.Width - Width - 10;
            if (Settings.last_appy <= 0 || Settings.last_appy > screenSize.Height)
                Settings.last_appy = screenSize.Height - Height - 55;
            if (Settings.last_x <= 0 || Settings.last_x > screenSize.Width)
                Settings.last_x = 20;
            if (Settings.last_y <= 0 || Settings.last_y > screenSize.Height)
                Settings.last_y = 20;
            if (Settings.last_w <= 160 || Settings.last_w > screenSize.Width)
                Settings.last_w = 0;
            if (Settings.last_h <= 92 || Settings.last_h > screenSize.Height)
                Settings.last_h = 0;
            if (Settings.quality > 2)
                Settings.quality = 1;

            //设置默认保存路径
            if (Settings.SaveDir != "DEFAULT" && !System.IO.Directory.Exists(Settings.SaveDir))
            {
                Settings.SaveDir = AppUtils.GetDefExportDir();
                //如果默认目录不存在则创建
                if (!System.IO.Directory.Exists(Settings.SaveDir))
                    System.IO.Directory.CreateDirectory(Settings.SaveDir);
            }
            if (Settings.fullscreen) WindowUtils.Hide(formRect.Handle);
            if (Settings.VideoType != "DEFAULT")
            {
                try
                {
                    Recorder.RecordFormat = (Recorder.VIDEO_FORMAT)Enum.Parse(typeof(Recorder.VIDEO_FORMAT), Settings.VideoType);
                }
                catch { }
            }

            //注册热键
            bool regsuccess = true;
            regsuccess = AppUtils.RegisterHotKeyEx(Handle, 0, Settings.hotkey_start);
            regsuccess = AppUtils.RegisterHotKeyEx(Handle, 1, Settings.hotkey_pause);
            regsuccess = AppUtils.RegisterHotKeyEx(Handle, 2, Settings.hotkey_stop);
            regsuccess = AppUtils.RegisterHotKeyEx(Handle, 3, Settings.hotkey_showehide);
            regsuccess = AppUtils.RegisterHotKeyEx(Handle, 4, Settings.hotkey_screenshutcut);

            //注册热键失败提示
            if (!regsuccess)
                new FormMsgSmall("有可能是热键被其他软件占用。您可以尝试重新设置热键", "ScreenRecoder - 热键注册失败", "确定", "", 4000).Show(this);

            //读取上次录制区域
            if (Settings.last_y != 0)
            {
                formRect.Top = Settings.last_y;
                formRectRecing.Top = Settings.last_y;
            }
            if (Settings.last_x != 0)
            {
                formRect.Left = Settings.last_x;
                formRectRecing.Left = Settings.last_x;
            }
            if (Settings.last_w % 4 != 0)
                Settings.last_w -= Settings.last_w % 4;
            if (Settings.last_h % 4 != 0)
                Settings.last_h -= Settings.last_h % 4;
            if (Settings.last_w != 0)
            {
                formRect.Width = Settings.last_w;
                formRectRecing.Width = Settings.last_w;
            }
            if (Settings.last_h != 0)
            {
                formRect.Height = Settings.last_h;
                formRectRecing.Height = Settings.last_h;
            }

            formMain.SwitchTop();

            toggle_fullscreen.Checked = Settings.fullscreen;
            btn_rec_mic.Image = Settings.recmic ? Properties.Resources.ico_mic_on : Properties.Resources.ico_mic_off;
            btn_rec_sound.Image = Settings.recsound ? Properties.Resources.ico_sound_on : Properties.Resources.ico_sound_off;
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit || Settings.close_act_exit || e.CloseReason == CloseReason.WindowsShutDown)
            {
                e.Cancel = false;

                //保存设置
                if (Settings.rempos)
                {
                    Settings.last_appx = Left;
                    Settings.last_appy = Top;
                }
                Settings.last_x = formRect.Left;
                Settings.last_y = formRect.Top;
                Settings.last_w = formRect.Width;
                Settings.last_h = formRect.Height;
                Settings.last_appx = Left;
                Settings.last_appy = Top;
                Settings.SaveSettings();

                centerStringFormat.Dispose();
                centerStringFormat = null;

                timerPreview.Stop();

                //关闭窗口
                formRect.Close();
                formRectRecing.Close();
                if(formSettings != null)
                {
                    formSettings.isClose = true;
                    formSettings.Close();
                }

                //释放录制库
                Recorder.Destroy();

                //取消注册所有热键
                AppUtils.UnRegisterAllHotKey(Handle);
                API.IniWriteValue("AppSetting", "LastWindow", "0");
            }
            else
            {
                //隐藏
                e.Cancel = true;
                WindowUtils.Hide(Handle);
            }
        }
        private void FormMain_Shown(object sender, EventArgs e)
        {
            //设置本窗口上次位置
            if (Settings.last_appx != 0)
                Left = Settings.last_appx;
            if (Settings.last_appy != 0)
                Top = Settings.last_appy;
            timerPreview.Start();
            //formMain = null;
            //formMain.Close();
            FormDebug.HideShowedWindow();
        }

        private void ReadAgrs(string[] agrs)
        {

        }

        //窗体移动
        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            WindowUtils.Move(Handle);
        }

        //最小化 关闭 按钮
        private void btn_min_Click(object sender, EventArgs e)
        {
            if ((Recorder.State == Recorder.Record_State.RECORDING || Recorder.State == Recorder.Record_State.SUSPENDED) && !Settings.fullscreen)
            {
                WindowUtils.Hide(Handle);
                WindowUtils.Show(formRecMini.Handle);
            }
            else WindowState = FormWindowState.Minimized;
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            if (Settings.close_act_exit)
            {
                exit = true;
                if (close_arsk()) return;
            }
            Close();
        }
        private bool close_arsk()
        {
            //退出如果正在录制则提示
            if (Recorder.State != Recorder.Record_State.NOT_BEGIN)
            {
                if (new FormMsg("您的录像会在关闭时保存。", "正在录像中，您是否要退出？", "退出录像", "继续录像", "ScreenRecoder - 疑问", Properties.Resources.question_mark_r_o).ShowDialog() == DialogResult.Cancel)
                    return true;//取消录像
                //停止录像，退出。
                btn_stop_BtnClick(this, null);
            }
            return false;
        }
       

        //按钮闪烁
        private bool _btn_pause_flashing = false;
        private bool _btn_stop_flashing = false;
        private bool btn_pause_flashing
        {
            get { return _btn_pause_flashing; }
            set
            {
                _btn_pause_flashing = value;
                if (!value)
                {
                    if (btn_pause.Icon != Properties.Resources.ico_pause)
                        btn_pause.Icon = Properties.Resources.ico_pause;
                    formRecMini.SetPauseIcon(Properties.Resources.ico_pause_small);
                    if (!_btn_stop_flashing)
                        timerBtnFlashing.Stop();
                }
                else timerBtnFlashing.Start();
            }
        }
        private bool btn_stop_flashing
        {
            get { return _btn_stop_flashing; }
            set
            {
                _btn_stop_flashing = value;
                if (!value)
                {
                    if (btn_stop.Icon != Properties.Resources.ico_stop)
                        btn_stop.Icon = Properties.Resources.ico_stop;
                    formRecMini.SetStopIcon(Properties.Resources.ico_stop_small);
                    if (!_btn_pause_flashing)
                        timerBtnFlashing.Stop();
                }
                else timerBtnFlashing.Start();
            }
        }
        //按钮闪烁定时器
        private void timerBtnFlashing_Tick(object sender, EventArgs e)
        {
            if (btn_pause_flashing)
            {
                if (btn_pause.Light)
                {
                    btn_pause.Light = false;
                    btn_pause.Icon = Properties.Resources.ico_pause_light;
                    formRecMini.SetPauseIcon(Properties.Resources.ico_pause_light__smallpng);
                }
                else
                {
                    btn_pause.Light = true;
                    btn_pause.Icon = Properties.Resources.ico_pause;
                    formRecMini.SetPauseIcon(Properties.Resources.ico_pause_small);
                }
                btn_pause.Invalidate();
            }
            if (btn_stop_flashing)
            {
                if (btn_stop.Light)
                {
                    btn_stop.Icon = Properties.Resources.ico_stop_light;
                    btn_stop.Light = false;
                    formRecMini.SetStopIcon(Properties.Resources.ico_stop_light_small);
                }
                else
                {
                    btn_stop.Icon = Properties.Resources.ico_stop;
                    btn_stop.Light = true;
                    formRecMini.SetStopIcon(Properties.Resources.ico_stop_small);
                }
                btn_stop.Invalidate();
            }
        }

        //设置按钮
        private void btn_settings_BtnClick(object sender, EventArgs e)
        {
            if (formSettings == null)
            {
                formSettings = new FormSettings(this);
                formSettings.Show();
            }
            WindowUtils.Show(formSettings.Handle);
            WindowUtils.SetForeground(formSettings.Handle);
        }
        //截图按钮
        private void btn_screen_shotcut_BtnClick(object sender, EventArgs e)
        {
            if (Recorder.State == Recorder.Record_State.NOT_BEGIN)
            {
                WindowUtils.Hide(Handle);
                if (!Settings.fullscreen) WindowUtils.Hide(formRect.Handle); 

                if (formScreenShutcut == null)
                {
                    formScreenShutcut = new FormScreenShutcut();
                    formScreenShutcut.Show(this);
                    formScreenShutcut.FormClosed += (object sender2, FormClosedEventArgs e2) =>
                    {
                        WindowUtils.Show(Handle);
                        if (!Settings.fullscreen) WindowUtils.Show(formRect.Handle);
                        Invalidate();

                        formScreenShutcut = null;
                    };
                }
                else
                {
                    WindowUtils.Show(formScreenShutcut.Handle);
                    WindowUtils.SetForeground(formScreenShutcut.Handle);
                }
            }
        }
        //3个控制按钮
        //开始录制按钮
        private void btn_record_BtnClick(object sender, EventArgs e)
        {
            if (Recorder.State == Recorder.Record_State.NOT_BEGIN)
            {
                if (pl_rec_finish.Visible)
                    pl_rec_finish.Visible = false;

                //读取设置
                if (Settings.fullscreen) Recorder.SetCaptureRect(0, 0, 0, 0);
                else
                {
                    int x, y, w, h;
                    x = formRect.Left; y = formRect.Top;
                    w = formRect.Width; h = formRect.Height;
                    Recorder.SetCaptureRect(x, y, w, h);

                    formRectRecing.Left = x - 1;
                    formRectRecing.Top = y - 1;
                    formRectRecing.Width = w + 2;
                    formRectRecing.Height = h + 2;
                }

                if (Settings.SaveDir != "DEFAULT" && System.IO.Directory.Exists(Settings.SaveDir))
                    Recorder.SetOutFileDir(Settings.SaveDir);

                Recorder.SetCaptureFrameRate(Settings.frame_rate);
                Recorder.RecordMouse = Settings.recmic;
                Recorder.RecordSound = Settings.recsound;
                Recorder.RecordMicDevIndex = Settings.mic_index;

                //开始录制
                if (!Recorder.StartButton())
                {
                    Utils.Utils.PlayTip("error", true);

                    string error = Recorder.LastError;
                    new FormMsg(string.IsNullOrEmpty(error) ? "未知错误" : error, "录制失败", "确定", "", "ScreenRecoder - 错误", Properties.Resources.error_r_o).ShowDialog();
                }
                else recoder_update(Recorder.UpdateCallbackID.OnStart, IntPtr.Zero);
            }
        }
        //停止录制按钮
        public void btn_stop_BtnClick(object sender, EventArgs e)
        {
            if (Recorder.State == Recorder.Record_State.RECORDING
                || Recorder.State == Recorder.Record_State.SUSPENDED)
            {
                Recorder.StopButton();
                recoder_update(Recorder.UpdateCallbackID.OnStop, IntPtr.Zero);
            }
            //结束之后对话框提示
            if (Settings.notify_when_finish && !exit)
            {
                TimeSpan ts = new TimeSpan(0, 0, Recorder.Duration);
                if (!WindowUtils.IsVisible(Handle)) { 
                    if (WindowState == FormWindowState.Minimized)
                        WindowState = FormWindowState.Normal;
                    WindowUtils.Show(Handle);
                }

                lb_reced_info.Text = "视频时长：" + ts.Hours.ToString("0") +
                        ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00") +
                        "\n视频名称：" + System.IO.Path.GetFileName(Recorder.GetLastOutFileName());
                pl_rec_finish.Show();
            }
        }
        //暂停录制按钮
        public void btn_pause_BtnClick(object sender, EventArgs e)
        {
            if (Recorder.State != Recorder.Record_State.NOT_BEGIN)
            {
                if (Recorder.PauseButton())
                {
                    if (Recorder.State == Recorder.Record_State.SUSPENDED)
                        recoder_update(Recorder.UpdateCallbackID.OnPause, IntPtr.Zero);
                    else if (Recorder.State == Recorder.Record_State.RECORDING)
                        recoder_update(Recorder.UpdateCallbackID.OnContinue, IntPtr.Zero);
                }
            }
        }

        private void btn_open_reced_Click(object sender, EventArgs e)
        {
            pl_rec_finish.Hide();
            Utils.Utils.SelectFileInExplorer(Recorder.GetLastOutFileName());
        }
        private void btn_continue_rec_Click(object sender, EventArgs e)
        {
            pl_rec_finish.Hide();
        }

        //底层录制库 状态改变回调
        private void recoder_update(Recorder.UpdateCallbackID id, IntPtr data)
        {
            switch (id)
            {
                case Recorder.UpdateCallbackID.OnStart:
                    {
                        btn_stop.Visible = true;
                        pl_rec.Visible = true;
                        btn_pause_flashing = false;
                        btn_stop_flashing = true;
                        lb_recing_bottom.Visible = true;
                        lb_time.Text = "0:00:00";
                        timerUpdateTime.Start();
                        if (Settings.playsound) Utils.Utils.PlayTip("play", false);
                        if (!Settings.fullscreen)
                        {
                            WindowUtils.Hide(formRect.Handle);
                            WindowUtils.Show(formRectRecing.Handle);
                        }
                        if (Settings.hide_wnd_when_rec) WindowUtils.Hide(Handle);
                        else if (Settings.show_mini_recing)
                        {
                            if (Settings.fullscreen)
                                WindowUtils.Hide(Handle);
                            else if (new Rectangle(formRect.Location, formRect.Size).IntersectsWith(new Rectangle(Location, Size)))
                                WindowUtils.Hide(Handle);
                            
                            WindowUtils.Show(formRecMini.Handle);
                            if (Settings.window_top) WindowUtils.SetForeground(formRecMini.Handle);
                            if (!Settings.fullscreen)
                            {
                                int x, y, w, h;
                                x = formRect.Left; y = formRect.Top;
                                w = formRect.Width; h = formRect.Height;
                                if (x > 0) formRecMini.Left = x + 5;
                                else formRecMini.Left = 5;
                                if (Screen.PrimaryScreen.Bounds.Height - (y + h) >= 36)
                                    formRecMini.Top = y + h + 2;
                                else if (y > 39)
                                    formRecMini.Top = y - 38;
                                else
                                    formRecMini.Top = Screen.PrimaryScreen.Bounds.Height - 36;
                            }
                            else
                            {
                                formRecMini.Left = 5;
                                formRecMini.Top = 5;
                            }
                            formRecMini.SetPlayPause(true);
                        }
                        else
                        {
                            if (Settings.fullscreen)
                                WindowUtils.Hide(Handle);
                            else if (new Rectangle(formRect.Location, formRect.Size).IntersectsWith(new Rectangle(Location, Size)))
                                WindowUtils.Hide(Handle);
                        }
                        dur = 0;
                        break;
                    }
                case Recorder.UpdateCallbackID.OnPause:
                    {
                        btn_pause_flashing = true;
                        btn_stop_flashing = false;
                        timerUpdateTime.Stop();
                        toolTip1.SetToolTip(btn_pause, "继续录像");
                        if (Settings.playsound) Utils.Utils.PlayTip("pause", true);
                        if (Settings.show_mini_recing)
                            formRecMini.SetPlayPause(false);
                        break;
                    }
                case Recorder.UpdateCallbackID.OnContinue:
                    {
                        timerUpdateTime.Start();
                        btn_pause_flashing = false;
                        btn_stop_flashing = true;
                        toolTip1.SetToolTip(btn_pause, "暂停录像");
                        if (Settings.playsound) Utils.Utils.PlayTip("play", false);
                        if (Settings.show_mini_recing)
                            formRecMini.SetPlayPause(true);
                        break;
                    }
                case Recorder.UpdateCallbackID.OnStop:
                    {
                        timerUpdateTime.Stop();
                        lb_recing_bottom.Visible = false;
                        lb_time.Text = "0:00:00";
                        btn_pause_flashing = false;
                        btn_stop_flashing = false;
                        //last_file = Recorder.GetLastOutFileName();
                        //pl_reced.Show();
                        if (Settings.playsound) Utils.Utils.PlayTip("stop", true);
                        if (Settings.show_mini_recing) formRecMini.SetPlayPause(false);
                        pl_rec.Visible = false;
                        if (!Settings.fullscreen || WindowUtils.IsVisible(formRectRecing.Handle))
                        {
                            if (!Settings.fullscreen) WindowUtils.Show(formRect.Handle);
                            WindowUtils.Hide(formRectRecing.Handle);
                        }
                        if (Settings.hide_wnd_when_rec) WindowUtils.Show(Handle);
                        else if (Settings.show_mini_recing || WindowUtils.IsVisible(formRecMini.Handle) || !WindowUtils.IsVisible(Handle))
                        {
                            WindowUtils.Show(Handle);
                            WindowUtils.Hide(formRecMini.Handle);
                        }
                        WindowUtils.SetForeground(Handle);
                        break;
                    }
            }
        }
        //刷新时间定时器
        private int dur = 0;
        private void timerUpdateTime_Tick(object sender, EventArgs e)
        {
            dur++;
               TimeSpan ts = new TimeSpan(0, 0, dur);
            lb_time.Text = ts.Hours.ToString("0") +
                ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            formRecMini.SetTime(lb_time.Text);
        }
        //预览小窗刷新
        private void timerPreview_Tick(object sender, EventArgs e)
        {
            pb_preview.Invalidate();
        }
        private void pb_preview_Paint(object sender, PaintEventArgs e)
        {
            if (Settings.show_preview)
            {
                if (Recorder.State == Recorder.Record_State.NOT_BEGIN && Settings.fullscreen)
                    e.Graphics.DrawString(screenSize.Width + " x " + screenSize.Height, Font, Brushes.White, 10, 6);
                if (Settings.fullscreen)
                    Recorder.DrawPreviewRect(e.Graphics.GetHdc(), 0, 0, screenSize.Width, screenSize.Height, pb_preview.Width, pb_preview.Height);
                else
                    Recorder.DrawPreviewRect(e.Graphics.GetHdc(), formRect.Left, formRect.Top, formRect.Width, formRect.Height, pb_preview.Width, pb_preview.Height);
                e.Graphics.ReleaseHdc();
            }
            else
            {
                if (Recorder.State == Recorder.Record_State.NOT_BEGIN)
                    e.Graphics.DrawString("未开始录制", screenPreviewFont, Brushes.White, new Rectangle(0, 0, pb_preview.Width, pb_preview.Height), centerStringFormat);
                else if (Recorder.State == Recorder.Record_State.RECORDING)
                    e.Graphics.DrawString("正在录制", screenPreviewFont, Brushes.White, new Rectangle(0, 0, pb_preview.Width, pb_preview.Height), centerStringFormat);
                else if (Recorder.State == Recorder.Record_State.SUSPENDED)
                    e.Graphics.DrawString("已暂停录制", screenPreviewFont, Brushes.White, new Rectangle(0, 0, pb_preview.Width, pb_preview.Height), centerStringFormat);
            }
        }
        private void UpdateScreenSize()
        {
            screenSize = new Size(API.GetSystemMetrics(API.SM_CXSCREEN), API.GetSystemMetrics(API.SM_CYSCREEN));
        }

        //托盘菜单事件
        private void 显示主窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowUtils.Show(Handle);
            WindowUtils.SetForeground(Handle);
        }
        private void 退出软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!WindowUtils.IsVisible(Handle))
            {
                WindowUtils.Show(Handle);
                if (WindowState == FormWindowState.Minimized)
                    WindowState = FormWindowState.Normal;
                WindowUtils.SetForeground(Handle);
            }
            else
            {
                WindowUtils.Hide(Handle);
            }
        }

        public void Exit()
        {
            exit = true;
            if (close_arsk()) return;
            Close();
        }
        public void Restart()
        {
            exit = true;
            if (close_arsk()) return;
            Close();
            AppUtils.StartNewWhenExit();
        }

        //全屏切换
        private void toggle_fullscreen_CheckedChanged(object sender, EventArgs e)
        {
            Settings.fullscreen = toggle_fullscreen.Checked;
            SwitchFullScreen();
            if (Settings.fullscreen)
            {
                lb_fullscreen.ForeColor = Color.FromArgb(18, 150, 219);
                lb_rectrec.ForeColor = Color.White;
            }
            else
            {
                lb_rectrec.ForeColor = Color.FromArgb(18, 150, 219);
                lb_fullscreen.ForeColor = Color.White;
            }
        }
        private void btn_rec_sound_Click(object sender, EventArgs e)
        {
            Settings.recsound = !Settings.recsound;
            btn_rec_sound.Image = Settings.recsound ? Properties.Resources.ico_sound_on : Properties.Resources.ico_sound_off;
            if (formSettings != null && formSettings.check_recsound.Checked != Settings.recsound)
                formSettings.check_recsound.Checked = Settings.recsound;
        }
        private void btn_rec_mic_Click(object sender, EventArgs e)
        {
            Settings.recmic = !Settings.recmic;
            btn_rec_mic.Image = Settings.recmic ? Properties.Resources.ico_mic_on : Properties.Resources.ico_mic_off;
            if (formSettings != null && formSettings.check_recmic.Checked != Settings.recmic)
                formSettings.check_recmic.Checked = Settings.recmic;
        }
        private void btn_top_Click(object sender, EventArgs e)
        {
            Settings.window_top = !Settings.window_top;
            if (formSettings != null && formSettings.check_top.Checked != Settings.window_top)
                formSettings.check_top.Checked = Settings.window_top;
            else SwitchTop();
        }

        //全屏录制状态修改
        public void SwitchFullScreen()
        {
            if (toggle_fullscreen.Checked != Settings.fullscreen) 
                toggle_fullscreen.Checked = Settings.fullscreen; 
            if(formSettings != null && formSettings.check_fullscreen.Checked != Settings.fullscreen)
            {
                if (Settings.fullscreen)
                {
                    formSettings.check_fullscreen.Checked = true;
                    formSettings.radio_rect_rec.Checked = false;
                }
                else
                {
                    formSettings.check_fullscreen.Checked = false;
                    formSettings.radio_rect_rec.Checked = true;
                }
            }

            formRecMini.TopMost = !Settings.fullscreen;
            if (Recorder.State != Recorder.Record_State.RECORDING && Recorder.State != Recorder.Record_State.SUSPENDED)
            {
                if (!Settings.fullscreen)
                {
                    WindowUtils.Show(formRect.Handle);
                    formRect.Invalidate();
                }
                else WindowUtils.Hide(formRect.Handle);
            }
        }
        public void SwitchTop()
        {
            btn_top.Image = Settings.window_top ? Properties.Resources.ico_top_on : Properties.Resources.ico_top_off;
            if (Settings.window_top)
            {
                TopMost = true;
                formRecMini.TopMost = true;
            }
            else
            {
                TopMost = false;
                formRecMini.TopMost = false;
            }
        }
        public void SetFullScreen()
        {
            Settings.fullscreen = true;
            SwitchFullScreen();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == API.WM_HOTKEY)
            {
                //热键
                int id = m.WParam.ToInt32();
                switch (id)
                {
                    case 0x8886:
                        btn_record_BtnClick(this, EventArgs.Empty);
                        break;
                    case 0x8887:
                        btn_pause_BtnClick(this, EventArgs.Empty);
                        break;
                    case 0x8888:
                        btn_stop_BtnClick(this, EventArgs.Empty);
                        break;
                    case 0x8889:
                        //显示窗口热键
                        if (Settings.show_mini_recing && Settings.fullscreen && Recorder.State == Recorder.Record_State.RECORDING)
                        {
                            if (WindowUtils.IsTop(formRecMini.Handle))
                                WindowUtils.Bottom(formRecMini.Handle);
                            else
                                WindowUtils.SetForeground(formRecMini.Handle);
                        }
                        else
                        {
                            if (WindowUtils.IsVisible(Handle))
                                WindowUtils.Hide(Handle);
                            else
                            {
                                WindowUtils.Show(Handle);
                                WindowUtils.SetForeground(Handle);
                            }
                        }
                        break;
                    case 0x8890:
                        btn_screen_shotcut_BtnClick(this, EventArgs.Empty);
                        break;
                }
            }
            else if (m.Msg == API.WM_DISPLAYCHANGE)
            {
                //屏幕大小更改
                UpdateScreenSize();
            }
            base.WndProc(ref m);
        }

        //窗口背景和边框绘画 
        private Pen borderPen = new Pen(Color.FromArgb(56,56,56));
        private StringFormat centerStringFormat;

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, Width - 1, Height - 1));
        }


    }
}
