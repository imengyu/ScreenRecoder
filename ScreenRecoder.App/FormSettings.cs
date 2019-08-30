using ScreenRecoder.App.Api;
using ScreenRecoder.App.Controls;
using ScreenRecoder.App.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    public partial class FormSettings : Form
    {
        public FormSettings(FormMain m)
        {
            InitializeComponent();
            formMain = m;
        }

        public bool isClose { get; set; }

        private Taber taber = new Taber();
        private FormMain formMain = null;
        private void FormSettings_Load(object sender, EventArgs e)
        {
            //初始化Tab
            taber.AddTab(pl_about, tab_about);
            taber.AddTab(pl_hotkeyset, tab_hotkeys);
            taber.AddTab(pl_recset, tab_recorder);
            taber.AddTab(pl_softset, tab_soft);
            taber.SwitchTab(pl_recset);

            LoadMicrophoneDevices();
            LoadSettings();
        }
        private void LoadMicrophoneDevices()
        {
            string[] devs = Recorder.GetAudioCaptureDevices();
            foreach (string s in devs)
                combo_mic.Items.Add(s);
        }
        public void LoadSettings()
        {
            //设置显示到控件
            check_hide_whenrec.Checked = Settings.hide_wnd_when_rec;
            check_usemini_inrec.Checked = Settings.show_mini_recing;
            check_recmic.Checked = Settings.recmic;
            check_recsound.Checked = Settings.recsound;
            check_exit_min.Checked = !Settings.close_act_exit;
            check_fullscreen.Checked = Settings.fullscreen;
            check_top.Checked = Settings.window_top;
            if (!Settings.fullscreen) radio_rect_rec.Checked = true;
            //帧率设置
            if (Settings.frame_rate == 15)
            {
                combo_framerate.SelectedIndex = 0;
                numeric_frame_rate.Visible = false;
            }
            else if (Settings.frame_rate == 20)
            {
                combo_framerate.SelectedIndex = 1;
                numeric_frame_rate.Visible = false;
            }
            else if (Settings.frame_rate == 30)
            {
                combo_framerate.SelectedIndex = 2;
                numeric_frame_rate.Visible = false;
            }
            else
            {
                combo_framerate.SelectedIndex = 3;
                numeric_frame_rate.Visible = true;
            }
            numeric_frame_rate.Value = Settings.frame_rate;
            combo_mic.SelectedIndex =  Settings.mic_index + 1;

            combo_quality.SelectedIndex = Settings.quality;
            check_rem_pos.Checked = Settings.rempos;
            if (Settings.SaveDir == "DEFAULT") textBox_export_dir.Text = "默认";
            else textBox_export_dir.Text = Settings.SaveDir;
            if (Settings.VideoType == "DEFAULT") combo_format.SelectedIndex = 0;
            else combo_format.SelectedItem = Settings.VideoType;
            hotKey_pause.SetKeys(Settings.hotkey_pause);
            hotKey_showhide.SetKeys(Settings.hotkey_showehide);
            hotKey_start.SetKeys(Settings.hotkey_start);
            hotKey_stop.SetKeys(Settings.hotkey_stop);
            hotKey_screenshutcut.SetKeys(Settings.hotkey_screenshutcut);
        }

        //窗口背景和边框绘画 
        private Pen borderPen = new Pen(Color.FromArgb(56, 56, 56));
        private void FormSettings_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, Width - 1, Height - 1));
        }

        //用户改变设置事件

        private void check_exit_min_CheckedChanged(object sender, EventArgs e)
        {
            Settings.close_act_exit = !check_exit_min.Checked;
        }
        private void check_fullscreen_CheckedChanged(object sender, EventArgs e)
        {
            Settings.fullscreen = check_fullscreen.Checked;
            formMain.SwitchFullScreen();
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
        private void combo_framerate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_framerate.SelectedIndex == 0)
            {
                numeric_frame_rate.Value = 15;
                if (numeric_frame_rate.Visible)
                    numeric_frame_rate.Hide();
            }
            else if (combo_framerate.SelectedIndex == 1)
            {
                numeric_frame_rate.Value = 20;
                if (numeric_frame_rate.Visible)
                    numeric_frame_rate.Hide();
            }
            else if (combo_framerate.SelectedIndex == 2)
            {
                numeric_frame_rate.Value = 30;
                if (numeric_frame_rate.Visible)
                    numeric_frame_rate.Hide();
            }
            else if (combo_framerate.SelectedIndex == 3)
            {
                if (!numeric_frame_rate.Visible)
                    numeric_frame_rate.Show();
            }
            recShowNotifyText();
        }
        private void combo_mic_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.mic_index = combo_mic.SelectedIndex - 1;
            recShowNotifyText();
        }
        private void check_rem_pos_CheckedChanged(object sender, EventArgs e)
        {
            Settings.rempos = check_rem_pos.Checked;
        }
        private void check_recmic_CheckedChanged(object sender, EventArgs e)
        {
            Settings.recmic = check_recmic.Checked;
            formMain.btn_rec_mic.Image = Settings.recmic ? Properties.Resources.ico_mouse_on : Properties.Resources.ico_mouse_off;
            recShowNotifyText();
        }
        private void check_recsound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.recsound = check_recsound.Checked;
            formMain.btn_rec_sound.Image = Settings.recsound ? Properties.Resources.ico_sound_on : Properties.Resources.ico_sound_off;
            recShowNotifyText();
        }
        private void check_usemini_inrec_CheckedChanged(object sender, EventArgs e)
        {
            Settings.show_mini_recing = check_usemini_inrec.Checked;
        }
        private void check_hide_whenrec_CheckedChanged(object sender, EventArgs e)
        {
            Settings.hide_wnd_when_rec = check_hide_whenrec.Checked;
        }
        private void check_use_sound_tip_CheckedChanged(object sender, EventArgs e)
        {
            Settings.playsound = check_use_sound_tip.Checked;
        }
        private void check_show_preview_CheckedChanged(object sender, EventArgs e)
        {
            Settings.show_preview = check_show_preview.Checked;
        }
        private void check_top_CheckedChanged(object sender, EventArgs e)
        {
            Settings.window_top = check_top.Checked;
            formMain.SwitchTop();
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
            formMain.Restart();
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
        private void hotKey_screenshutcut_KeysChanged(object sender, EventArgs e)
        {
            hotKey_screenshutcut.GetKeys(Settings.hotkey_screenshutcut);
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

        //默认设置
        private void btn_defsettings_Click(object sender, EventArgs e)
        {
            if (new FormMsg("", "您是否要恢复默认设置？", "是", "否", "ScreenRecoder - 疑问", Properties.Resources.question_mark_r_o).ShowDialog(this) == DialogResult.OK)
            {
                check_recmic.Checked = false;
                check_recsound.Checked = true;
                check_exit_min.Checked = true;
                check_fullscreen.Checked = true;
                numeric_frame_rate.Value = 15;
                combo_format.SelectedIndex = 0;
                combo_quality.SelectedIndex = 0;
                check_rem_pos.Checked = true;
                textBox_export_dir.Text = AppUtils.GetDefExportDir();
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
        //打开目录按钮
        private void btn_open_folder_Click(object sender, EventArgs e)
        {
            if (textBox_export_dir.Text != "")
            {
                if (textBox_export_dir.Text == "默认" || textBox_export_dir.Text == "DEFAULT")
                    System.Diagnostics.Process.Start(AppUtils.GetDefExportDir());
                else if (System.IO.Directory.Exists(textBox_export_dir.Text))
                    System.Diagnostics.Process.Start(textBox_export_dir.Text);
            }
        }
        //确定
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (textBox_export_dir.Text != Settings.SaveDir)
            {
                if (textBox_export_dir.Text == "默认" || textBox_export_dir.Text == "默认") Settings.SaveDir = "DEFAULT";
                else Settings.SaveDir = textBox_export_dir.Text;
            }
            WindowUtils.Hide(Handle);
        }
        //跳转github
        private void link_github_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(link_github.Text);
        }

  

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClose) e.Cancel = false;
            else
            {
                e.Cancel = true;
                WindowUtils.Hide(Handle);
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if (Settings.close_act_exit) formMain.Exit();
            else
            {
                if (new FormMsg("", "您希望退出软件吗？", "退出", "取消", "ScreenRecoder - 疑问", Properties.Resources.question_mark_r_o).ShowDialog(this) == DialogResult.OK)
                    formMain.Exit();
            }
        }


    }
}
