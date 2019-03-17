using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    //主窗口
    public partial class FormMain : Form
    {
        public static FormMain formMain;

        private IntPtr recoder_update_callback_ptr = IntPtr.Zero;
        private bool exit = false;

        public FormMain(string[] agrs)
        {
            ReadAgrs(agrs);
            InitializeComponent(); formMain = this;
        }

        //两个选择区域窗口
        FormRect formRect = null;
        FormRectRecing formRectRecing = null;
        //迷你窗口
        FormRecMini formRecMini = null;

        //初始化和退出
        private void FormMain_Load(object sender, EventArgs e)
        {
            //创建
            recoder_update_callback_ptr = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate((Recorder.UpdateCallback)recoder_update);

            //创建录制底层库实例
            Recorder.RecoderCreate();
            Recorder.RecoderSetUpdateCallback(recoder_update_callback_ptr);

            API.IniWriteValue("AppSetting", "LastWindow", Handle.ToInt32().ToString());

            //创建矩形选择框
            formRect = new FormRect();
            formRect.Show();
            formRectRecing = new FormRectRecing();
            formRectRecing.Show();

            formRecMini = new FormRecMini();
            formRecMini.Show();

            //隐藏正在录制框
            API.WindowHide(formRectRecing.Handle);
            //隐藏迷你窗口
            API.WindowHide(formRecMini.Handle);
            

            //加载设置
            Settings.LoadSettings();
            //检查设置是否合法
            if (Settings.frame_rate <= 0 || Settings.frame_rate > 30)
                Settings.frame_rate = 15;
            if (Settings.last_appx <= 0 || Settings.last_appx > Screen.PrimaryScreen.Bounds.Width)
                Settings.last_appx = 0;
            if (Settings.last_appy <= 0 || Settings.last_appy > Screen.PrimaryScreen.Bounds.Height)
                Settings.last_appy = 0;
            if (Settings.last_x <= 0 || Settings.last_x > Screen.PrimaryScreen.Bounds.Width)
                Settings.last_x = 0;
            if (Settings.last_y <= 0 || Settings.last_y > Screen.PrimaryScreen.Bounds.Height)
                Settings.last_y = 0;
            if (Settings.last_w <= 160 || Settings.last_w > Screen.PrimaryScreen.Bounds.Width)
                Settings.last_w = 0;
            if (Settings.last_h <= 92 || Settings.last_h > Screen.PrimaryScreen.Bounds.Height)
                Settings.last_h = 0;
            if (Settings.quality > 2)
                Settings.quality = 1;

            //设置默认保存路径
            if (Settings.SaveDir != "DEFAULT" && !System.IO.Directory.Exists(Settings.SaveDir))
            {
                Settings.SaveDir = API.GetDefExportDir();
                //如果默认目录不存在则创建
                if (!System.IO.Directory.Exists(Settings.SaveDir))
                    System.IO.Directory.CreateDirectory(Settings.SaveDir);
            }

            if (Settings.fullscreen) API.WindowHide(formRect.Handle);

            //设置显示到控件
            check_hide_whenrec.Checked = Settings.hide_wnd_when_rec;
            check_usemini_inrec.Checked = Settings.show_mini_recing;
            check_recmic.Checked = Settings.recmic;
            check_recsound.Checked = Settings.recsound;
            check_exit_min.Checked = !Settings.close_act_exit;
            check_fullscreen.Checked = Settings.fullscreen;
            numeric_frame_rate.Value = Settings.frame_rate;
            combo_quality.SelectedIndex = Settings.quality;
            check_rem_pos.Checked = Settings.rempos;
            textBox_export_dir.Text = Settings.SaveDir;
            combo_format.SelectedItem = Settings.VideoType;
            hotKey_pause.SetKeys(Settings.hotkey_pause);
            hotKey_showhide.SetKeys(Settings.hotkey_showehide);
            hotKey_start.SetKeys(Settings.hotkey_start);
            hotKey_stop.SetKeys(Settings.hotkey_stop);
            if (Settings.VideoType == "DEFAULT") combo_format.SelectedIndex = 0;
            else
            {
                try
                {
                    Recorder.RecordFormat = (Recorder.VIDEO_FORMAT)Enum.Parse(typeof(Recorder.VIDEO_FORMAT), Settings.VideoType);
                    combo_format.SelectedIndex = (int)Recorder.RecordFormat;
                }
                catch
                {

                }
            }

            //注册热键
            bool regsuccess = true;
            if (!hotKey_start.IsEmepty())
                regsuccess = API.RegisterHotKey(Handle, 0, (int)Settings.hotkey_start[0], (int)Settings.hotkey_start[1], (int)Settings.hotkey_start[2]);
            if (!hotKey_pause.IsEmepty())
                regsuccess = API.RegisterHotKey(Handle, 1, (int)Settings.hotkey_pause[0], (int)Settings.hotkey_pause[1], (int)Settings.hotkey_pause[2]);
            if (!hotKey_stop.IsEmepty())
                regsuccess = API.RegisterHotKey(Handle, 2, (int)Settings.hotkey_stop[0], (int)Settings.hotkey_stop[1], (int)Settings.hotkey_stop[2]);
            if (!hotKey_showhide.IsEmepty())
                regsuccess = API.RegisterHotKey(Handle, 3, (int)Settings.hotkey_showehide[0], (int)Settings.hotkey_showehide[1], (int)Settings.hotkey_showehide[2]);

            //注册热键失败提示
            if (!regsuccess)
                new FormMsg("有可能是热键设置错误，或者是热键被其他软件占用。\n您可以尝试重新设置热键", "热键注册失败").ShowDialog();

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

            Height = 55;
            lb_time.Height = 55;
            

        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit || Settings.close_act_exit)
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

                //关闭窗口
                formRect.Close();
                formRectRecing.Close();

                //释放录制库
                Recorder.RecoderDestroy();

                //取消注册所有热键
                API.UnRegisterAllHotKey(Handle);
                API.IniWriteValue("AppSetting", "LastWindow", "0");
            }
            else
            {
                //隐藏
                e.Cancel = true;
                Hide();
            }
        }
        private void FormMain_Shown(object sender, EventArgs e)
        {
            //设置本窗口上次位置
            if (Settings.last_appx != 0)
                Left = Settings.last_appx;
            if (Settings.last_appy != 0)
                Top = Settings.last_appy;
        }

        private void ReadAgrs(string[] agrs)
        {

        }

        //窗体移动
        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            API.WindowMove(Handle);
        }

        //最小化 关闭 按钮
        private void btn_min_Click(object sender, EventArgs e)
        {
            if ((Recorder.State == Recorder.Record_State.RECORDING || Recorder.State == Recorder.Record_State.SUSPENDED) && !Settings.fullscreen)
            {
                API.WindowHide(Handle);
                API.WindowShow(formRecMini.Handle);
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
                if (new FormMsg("您的录像会在关闭时保存。", "正在录像中，您是否要退出？", "退出录像", "继续录像", "疑问").ShowDialog() == DialogResult.Cancel)
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
            if (Height == 55)
            {
                Height = 370;
                //btn_stop_flashing = true;
                //pl_rec.Visible = true;
            }
            else
            {
                Height = 55;
                //btn_stop_flashing = false;
                //pl_rec.Visible = false;
            }
        }
        //选择路径按钮
        private void btn_choosedir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                textBox_export_dir.Text = f.DirectoryPath;
                Settings.SaveDir = f.DirectoryPath;
            }
        }
        //3个控制按钮
        //开始录制按钮
        private void btn_record_BtnClick(object sender, EventArgs e)
        {
            if (Recorder.State == Recorder.Record_State.NOT_BEGIN)
            {
                //读取设置
                if (Settings.fullscreen) Recorder.RecoderSetCaptureRect(0, 0, 0, 0);
                else
                {
                    int x, y, w, h;
                    x = formRect.Left; y = formRect.Top;
                    w = formRect.Width; h = formRect.Height;
                    Recorder.RecoderSetCaptureRect(x, y, w, h);

                    formRectRecing.Left = x - 1;
                    formRectRecing.Top = y - 1;
                    formRectRecing.Width = w + 2;
                    formRectRecing.Height = h + 2;
                }

                if (Settings.SaveDir != "DEFAULT" && System.IO.Directory.Exists(Settings.SaveDir))
                    Recorder.RecoderSetOutFileDir(Settings.SaveDir);

                Recorder.RecoderSetCaptureFrameRate(Settings.frame_rate);
                Recorder.RecordMouse = Settings.recmic;
                Recorder.RecordSound = Settings.recsound;
                //开始录制
                if (!Recorder.RecoderStartButton())
                    new FormMsg(Recorder.LastError, "录制失败").ShowDialog();
            }
        }
        //停止录制按钮
        public void btn_stop_BtnClick(object sender, EventArgs e)
        {
            if (Recorder.State == Recorder.Record_State.RECORDING
                || Recorder.State == Recorder.Record_State.SUSPENDED)
            {
                Recorder.RecoderStopButton();
                recoder_update(Recorder.UpdateCallbackID.OnStop, IntPtr.Zero);
            }
            //结束之后对话框提示
            if (Settings.notify_when_finish && !exit)
            {
                TimeSpan ts = new TimeSpan(0, 0, Recorder.Duration);
                if (API.WindowIsVisible(Handle))
                {
                    if (new FormMsg("文件位置：\n" + Recorder.RecoderGetLastOutFileName() + "\n时长：" + ts.Hours.ToString("0") +
                        ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00"), "录制成功", "打开文件", "继续录像").ShowDialog(this) == DialogResult.OK)
                        API.AppOpenFile(Recorder.RecoderGetLastOutFileName());
                }
                else
                {                 
                    if (new FormMsg("文件位置：\n" + Recorder.RecoderGetLastOutFileName() + "\n时长：" + ts.Hours.ToString("0") +
                        ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00"), "录制成功", "打开文件", "继续录像").ShowDialog(Settings.fullscreen ? (IWin32Window)this : (IWin32Window)formRect) == DialogResult.OK)
                        API.AppOpenFile(Recorder.RecoderGetLastOutFileName());
                }
            }
        }
        //暂停录制按钮
        public void btn_pause_BtnClick(object sender, EventArgs e)
        {
            if (Recorder.State != Recorder.Record_State.NOT_BEGIN)
                Recorder.RecoderPauseButton();
        }

        //底层录制库 状态改变回调
        private void recoder_update(Recorder.UpdateCallbackID id, IntPtr data)
        {
            switch (id)
            {
                case Recorder.UpdateCallbackID.OnStart:
                    btn_stop.Visible = true;
                    pl_rec.Visible = true;
                    btn_pause_flashing = false;
                    btn_stop_flashing = true;

                    timerUpdateTime.Start();
                    if (!Settings.fullscreen)
                    {
                        API.WindowHide(formRect.Handle);
                        API.WindowShow(formRectRecing.Handle);
                    }
                    if (Settings.hide_wnd_when_rec) API.WindowHide(Handle);
                    else if (Settings.show_mini_recing)
                    {
                        API.WindowHide(Handle);
                        API.WindowShow(formRecMini.Handle);
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
                            formRecMini.Left = Width - formRecMini.Width;
                            formRecMini.Top = Height - formRecMini.Height;
                        }
                        formRecMini.SetPlayPause(true);
                    }
                    dur = 0;
                    break;
                case Recorder.UpdateCallbackID.OnPause:
                    btn_pause_flashing = true;
                    btn_stop_flashing = false;
                    timerUpdateTime.Stop();
                    toolTip1.SetToolTip(btn_pause, "继续录像");
                    if (Settings.show_mini_recing)
                        formRecMini.SetPlayPause(false);
                    break;
                case Recorder.UpdateCallbackID.OnContinue:
                    timerUpdateTime.Start();
                    btn_pause_flashing = false;
                    btn_stop_flashing = true;
                    toolTip1.SetToolTip(btn_pause, "暂停录像");
                    if (Settings.show_mini_recing)
                        formRecMini.SetPlayPause(true);
                    break;
                case Recorder.UpdateCallbackID.OnStop:
                    timerUpdateTime.Stop();
                    btn_pause_flashing = false;
                    btn_stop_flashing = false;
                    //last_file = Recorder.RecoderGetLastOutFileName();
                    //pl_reced.Show();
                    if (Settings.show_mini_recing)
                        formRecMini.SetPlayPause(false);
                    pl_rec.Visible = false;
                    if (!Settings.fullscreen || API.WindowIsVisible(formRectRecing.Handle))
                    {
                        if (!Settings.fullscreen) API.WindowShow(formRect.Handle);
                        API.WindowHide(formRectRecing.Handle);
                    }
                    if (Settings.hide_wnd_when_rec) API.WindowShow(Handle);
                    else if (Settings.show_mini_recing || API.WindowIsVisible(formRecMini.Handle) || !API.WindowIsVisible(Handle))
                    {
                        API.WindowShow(Handle);
                        API.WindowHide(formRecMini.Handle);
                    }
                    break;
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

        //用户改变设置事件
        private void check_exit_min_CheckedChanged(object sender, EventArgs e)
        {
            Settings.close_act_exit = !check_exit_min.Checked;
        }
        private void check_fullscreen_CheckedChanged(object sender, EventArgs e)
        {
            Settings.fullscreen = check_fullscreen.Checked;
            if (Recorder.State != Recorder.Record_State.RECORDING && Recorder.State != Recorder.Record_State.SUSPENDED)
            {
                if (!Settings.fullscreen)
                {
                    API.WindowShow(formRect.Handle);
                    formRect.Invalidate();
                }
                else API.WindowHide(formRect.Handle);
            }
            recShowNotifyText();
        }
        private void numeric_frame_rate_ValueChanged(object sender, EventArgs e)
        {
            Settings.frame_rate = Convert.ToInt32(numeric_frame_rate.Value);
            recShowNotifyText();
        }
        private void combo_quality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_quality.SelectedIndex != 0)
                Settings.quality = combo_quality.SelectedIndex;
            recShowNotifyText();
        }
        private void combo_format_SelectedIndexChanged(object sender, EventArgs e)
        {       
            if (combo_format.SelectedIndex == 0)
            {
                Recorder.RecordFormat = Recorder.VIDEO_FORMAT.VIDEO_FORMAT_UNKOWN;
                Settings.VideoType = "DEFAULT";
            }
            else
            {
                Settings.VideoType = Recorder.RecordFormat.ToString();
                Recorder.RecordFormat = (Recorder.VIDEO_FORMAT)combo_format.SelectedIndex;
            }
            recShowNotifyText();
        }
        private void check_rem_pos_CheckedChanged(object sender, EventArgs e)
        {
            Settings.rempos = check_rem_pos.Checked;
        }
        private void check_recmic_CheckedChanged(object sender, EventArgs e)
        {
            Settings.recmic = check_recmic.Checked;
            recShowNotifyText();
        }
        private void check_recsound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.recsound = check_recsound.Checked;
            recShowNotifyText();
        }
        private void btn_defsettings_Click(object sender, EventArgs e)
        {
            if (new FormMsg("", "您是否要恢复默认设置？", "是", "否", "疑问").ShowDialog(this) == DialogResult.OK)
            {
                check_recmic.Checked = false;
                check_recsound.Checked = true;
                check_exit_min.Checked = true;
                check_fullscreen.Checked = true;
                numeric_frame_rate.Value = 15;
                combo_format.SelectedIndex = 0;
                combo_quality.SelectedIndex = 0;
                check_rem_pos.Checked = true;
                textBox_export_dir.Text = API.GetDefExportDir();
                Settings.SaveDir = textBox_export_dir.Text;
                hotKey_start.SetKeys(new Keys[] { Keys.F1 });
                hotKey_pause.SetKeys(new Keys[] { Keys.F2 });
                hotKey_stop.SetKeys(new Keys[] { Keys.F3 });
                hotKey_showhide.SetKeys(new Keys[] { Keys.R, Keys.Shift });
                hotKey_start.GetKeys(Settings.hotkey_start);
                hotKey_pause.GetKeys(Settings.hotkey_pause);
                hotKey_stop.GetKeys(Settings.hotkey_stop);
                hotKey_showhide.GetKeys(Settings.hotkey_showehide);
            }
        }
        private void check_usemini_inrec_CheckedChanged(object sender, EventArgs e)
        {
            Settings.show_mini_recing = check_usemini_inrec.Checked;
        }
        private void check_hide_whenrec_CheckedChanged(object sender, EventArgs e)
        {
            Settings.hide_wnd_when_rec = check_hide_whenrec.Checked;
        }
        private void recShowNotifyText()
        {
            if (Recorder.State == Recorder.Record_State.RECORDING || Recorder.State == Recorder.Record_State.SUSPENDED)
                if (!lb_recset_notify.Visible)
                    lb_keyset_notify.Visible = true;
        }
        //重启软件
        private void link_reboot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            exit = true;
            if (close_arsk()) return;
            Close();
            API.StartNewWhenExit();
        }

        //热键设置
        private void hotKey_start_KeysChanged(object sender, EventArgs e)
        {
            hotKey_start.GetKeys(Settings.hotkey_start);
            hotKeyShowNotifyText();
        }
        private void hotKey_pause_KeysChanged(object sender, EventArgs e)
        {
            hotKey_pause.GetKeys(Settings.hotkey_pause);
            hotKeyShowNotifyText();
        }
        private void hotKey_stop_KeysChanged(object sender, EventArgs e)
        {
            hotKey_stop.GetKeys(Settings.hotkey_stop);
            hotKeyShowNotifyText();
        }
        private void hotKey_showhide_KeysChanged(object sender, EventArgs e)
        {
            hotKey_showhide.GetKeys(Settings.hotkey_showehide);
            hotKeyShowNotifyText();
        }
        private void hotKeyShowNotifyText()
        {
            if (!lb_keyset_notify.Visible)
            {
                lb_keyset_notify.Visible = true;
                link_reboot.Visible = true;
            }
        }

        //托盘菜单事件
        private void 显示主窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            API.WindowShow(Handle);
            API.WindowSetForeground(Handle);
        }
        private void 退出软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exit = true;
            if (close_arsk()) return;
            Close();
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            API.WindowShow(Handle);
            API.WindowSetForeground(Handle);
        }

        public void SetFullScreen()
        {
            check_fullscreen.Checked = true;
        }

        //三个设置页 按钮
        private void btn_softset_Click(object sender, EventArgs e)
        {
            btn_softset.BackColor = Color.OrangeRed;
            btn_hotkeyset.BackColor = Color.Tomato;
            btn_recset.BackColor = Color.Tomato;
            btn_about.BackColor = Color.Tomato;
            pl_hotkeyset.Visible = false;
            pl_recset.Visible = false;
            pl_softset.Visible = true;
            pl_about.Visible = false;
        }
        private void btn_recset_Click(object sender, EventArgs e)
        {
            if (lb_recset_notify.Visible) lb_recset_notify.Visible = false;
            btn_softset.BackColor = Color.Tomato;
            btn_hotkeyset.BackColor = Color.Tomato;
            btn_recset.BackColor = Color.OrangeRed;
            btn_about.BackColor = Color.Tomato;
            pl_hotkeyset.Visible = false;
            pl_recset.Visible = true;
            pl_softset.Visible = false;
            pl_about.Visible = false;
        }
        private void btn_hotkeyset_Click(object sender, EventArgs e)
        {
            btn_softset.BackColor = Color.Tomato;
            btn_hotkeyset.BackColor = Color.OrangeRed;
            btn_recset.BackColor = Color.Tomato;
            btn_about.BackColor = Color.Tomato;
            pl_hotkeyset.Visible = true;
            pl_recset.Visible = false;
            pl_softset.Visible = false;
            pl_about.Visible = false;
        }       
        //关于按钮
        private void btn_about_Click(object sender, EventArgs e)
        {
            btn_softset.BackColor = Color.Tomato;
            btn_hotkeyset.BackColor = Color.Tomato;
            btn_recset.BackColor = Color.Tomato;
            btn_about.BackColor = Color.OrangeRed;
            pl_about.Visible = true;
            pl_recset.Visible = false;
            pl_softset.Visible = false;
            pl_hotkeyset.Visible = false;
        }

        protected override void WndProc(ref Message m)
        {
            //WM_HOTKEY
            if (m.Msg == 0x0312)
            {
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
                        if (API.WindowIsVisible(Handle))
                            API.WindowHide(Handle);
                        else
                        {
                            API.WindowShow(Handle);
                            API.WindowSetForeground(Handle);
                        }
                        break;
                }
            }
            base.WndProc(ref m);
        }

        //打开目录按钮
        private void btn_open_folder_Click(object sender, EventArgs e)
        {
            if(textBox_export_dir.Text!="")
            {
                if (System.IO.Directory.Exists(textBox_export_dir.Text))
                    System.Diagnostics.Process.Start(textBox_export_dir.Text);
            }
        }
    }
}
