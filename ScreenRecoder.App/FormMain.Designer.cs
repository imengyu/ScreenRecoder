namespace ScreenRecoder.App
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btn_top = new System.Windows.Forms.PictureBox();
            this.btn_rec_mic = new System.Windows.Forms.PictureBox();
            this.btn_rec_sound = new System.Windows.Forms.PictureBox();
            this.lb_time = new System.Windows.Forms.Label();
            this.pl_rec = new System.Windows.Forms.Panel();
            this.lb_tip = new System.Windows.Forms.Label();
            this.timerBtnFlashing = new System.Windows.Forms.Timer(this.components);
            this.timerUpdateTime = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示主窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出软件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lb_fullscreen = new System.Windows.Forms.Label();
            this.lb_rectrec = new System.Windows.Forms.Label();
            this.timerPreview = new System.Windows.Forms.Timer(this.components);
            this.lb_recing_bottom = new System.Windows.Forms.Label();
            this.pl_footer = new System.Windows.Forms.Panel();
            this.pl_rec_finish = new System.Windows.Forms.Panel();
            this.lb_reced_info = new System.Windows.Forms.Label();
            this.lb_reced_title = new System.Windows.Forms.Label();
            this.pb_reced = new System.Windows.Forms.PictureBox();
            this.pb_preview = new System.Windows.Forms.PictureBox();
            this.btn_screen_shotcut = new ScreenRecoder.App.IconButton();
            this.toggle_fullscreen = new ScreenRecoder.App.Controls.Toggle();
            this.btn_min = new bells.app.ImageButton();
            this.btn_close = new bells.app.ImageButton();
            this.btn_setting = new ScreenRecoder.App.IconButton();
            this.btn_stop = new ScreenRecoder.App.IconButton();
            this.btn_pause = new ScreenRecoder.App.IconButton();
            this.btn_start = new ScreenRecoder.App.IconButton();
            this.btn_open_reced = new ScreenRecoder.App.Controls.FlatButton();
            this.btn_continue_rec = new ScreenRecoder.App.Controls.FlatButton();
            ((System.ComponentModel.ISupportInitialize)(this.btn_top)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_rec_mic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_rec_sound)).BeginInit();
            this.pl_rec.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.pl_footer.SuspendLayout();
            this.pl_rec_finish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_reced)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_top
            // 
            this.btn_top.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_top.Image = global::ScreenRecoder.App.Properties.Resources.ico_top_off;
            this.btn_top.Location = new System.Drawing.Point(295, 14);
            this.btn_top.Name = "btn_top";
            this.btn_top.Size = new System.Drawing.Size(20, 20);
            this.btn_top.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_top.TabIndex = 14;
            this.btn_top.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_top, "窗口置顶");
            this.btn_top.Click += new System.EventHandler(this.btn_top_Click);
            // 
            // btn_rec_mic
            // 
            this.btn_rec_mic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_rec_mic.Image = global::ScreenRecoder.App.Properties.Resources.ico_mic_off;
            this.btn_rec_mic.Location = new System.Drawing.Point(266, 16);
            this.btn_rec_mic.Name = "btn_rec_mic";
            this.btn_rec_mic.Size = new System.Drawing.Size(18, 18);
            this.btn_rec_mic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_rec_mic.TabIndex = 11;
            this.btn_rec_mic.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_rec_mic, "录制麦克风");
            this.btn_rec_mic.Click += new System.EventHandler(this.btn_rec_mic_Click);
            // 
            // btn_rec_sound
            // 
            this.btn_rec_sound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_rec_sound.Image = global::ScreenRecoder.App.Properties.Resources.ico_sound_off;
            this.btn_rec_sound.Location = new System.Drawing.Point(233, 13);
            this.btn_rec_sound.Name = "btn_rec_sound";
            this.btn_rec_sound.Size = new System.Drawing.Size(23, 23);
            this.btn_rec_sound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_rec_sound.TabIndex = 12;
            this.btn_rec_sound.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_rec_sound, "录制声音");
            this.btn_rec_sound.Click += new System.EventHandler(this.btn_rec_sound_Click);
            // 
            // lb_time
            // 
            this.lb_time.AutoEllipsis = true;
            this.lb_time.BackColor = System.Drawing.Color.DarkRed;
            this.lb_time.Font = new System.Drawing.Font("宋体", 9F);
            this.lb_time.ForeColor = System.Drawing.Color.White;
            this.lb_time.Location = new System.Drawing.Point(110, 1);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(97, 60);
            this.lb_time.TabIndex = 1;
            this.lb_time.Text = "0:00:00";
            this.lb_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_time.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            // 
            // pl_rec
            // 
            this.pl_rec.Controls.Add(this.btn_stop);
            this.pl_rec.Controls.Add(this.lb_time);
            this.pl_rec.Controls.Add(this.btn_pause);
            this.pl_rec.Location = new System.Drawing.Point(1, 1);
            this.pl_rec.Name = "pl_rec";
            this.pl_rec.Size = new System.Drawing.Size(207, 60);
            this.pl_rec.TabIndex = 4;
            this.pl_rec.Visible = false;
            // 
            // lb_tip
            // 
            this.lb_tip.AutoEllipsis = true;
            this.lb_tip.BackColor = System.Drawing.Color.Transparent;
            this.lb_tip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_tip.ForeColor = System.Drawing.Color.White;
            this.lb_tip.Location = new System.Drawing.Point(56, 1);
            this.lb_tip.Name = "lb_tip";
            this.lb_tip.Size = new System.Drawing.Size(147, 58);
            this.lb_tip.TabIndex = 5;
            this.lb_tip.Text = "点击红点开始录像";
            this.lb_tip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_tip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            // 
            // timerBtnFlashing
            // 
            this.timerBtnFlashing.Interval = 1000;
            this.timerBtnFlashing.Tick += new System.EventHandler(this.timerBtnFlashing_Tick);
            // 
            // timerUpdateTime
            // 
            this.timerUpdateTime.Interval = 1000;
            this.timerUpdateTime.Tick += new System.EventHandler(this.timerUpdateTime_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "屏幕录像";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示主窗口ToolStripMenuItem,
            this.退出软件ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // 显示主窗口ToolStripMenuItem
            // 
            this.显示主窗口ToolStripMenuItem.Name = "显示主窗口ToolStripMenuItem";
            this.显示主窗口ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.显示主窗口ToolStripMenuItem.Text = "显示主窗口";
            this.显示主窗口ToolStripMenuItem.Click += new System.EventHandler(this.显示主窗口ToolStripMenuItem_Click);
            // 
            // 退出软件ToolStripMenuItem
            // 
            this.退出软件ToolStripMenuItem.Name = "退出软件ToolStripMenuItem";
            this.退出软件ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出软件ToolStripMenuItem.Text = "退出软件";
            this.退出软件ToolStripMenuItem.Click += new System.EventHandler(this.退出软件ToolStripMenuItem_Click);
            // 
            // lb_fullscreen
            // 
            this.lb_fullscreen.AutoSize = true;
            this.lb_fullscreen.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lb_fullscreen.ForeColor = System.Drawing.Color.White;
            this.lb_fullscreen.Location = new System.Drawing.Point(125, 16);
            this.lb_fullscreen.Name = "lb_fullscreen";
            this.lb_fullscreen.Size = new System.Drawing.Size(56, 17);
            this.lb_fullscreen.TabIndex = 9;
            this.lb_fullscreen.Text = "全屏录制";
            // 
            // lb_rectrec
            // 
            this.lb_rectrec.AutoSize = true;
            this.lb_rectrec.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lb_rectrec.ForeColor = System.Drawing.Color.White;
            this.lb_rectrec.Location = new System.Drawing.Point(20, 16);
            this.lb_rectrec.Name = "lb_rectrec";
            this.lb_rectrec.Size = new System.Drawing.Size(56, 17);
            this.lb_rectrec.TabIndex = 10;
            this.lb_rectrec.Text = "区域录制";
            // 
            // timerPreview
            // 
            this.timerPreview.Interval = 200;
            this.timerPreview.Tick += new System.EventHandler(this.timerPreview_Tick);
            // 
            // lb_recing_bottom
            // 
            this.lb_recing_bottom.AutoEllipsis = true;
            this.lb_recing_bottom.BackColor = System.Drawing.Color.Transparent;
            this.lb_recing_bottom.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_recing_bottom.ForeColor = System.Drawing.Color.White;
            this.lb_recing_bottom.Location = new System.Drawing.Point(23, 1);
            this.lb_recing_bottom.Name = "lb_recing_bottom";
            this.lb_recing_bottom.Size = new System.Drawing.Size(268, 40);
            this.lb_recing_bottom.TabIndex = 13;
            this.lb_recing_bottom.Text = "正在录制...";
            this.lb_recing_bottom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_recing_bottom.Visible = false;
            // 
            // pl_footer
            // 
            this.pl_footer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pl_footer.Controls.Add(this.btn_top);
            this.pl_footer.Controls.Add(this.btn_rec_mic);
            this.pl_footer.Controls.Add(this.toggle_fullscreen);
            this.pl_footer.Controls.Add(this.btn_rec_sound);
            this.pl_footer.Controls.Add(this.lb_fullscreen);
            this.pl_footer.Controls.Add(this.lb_rectrec);
            this.pl_footer.Controls.Add(this.lb_recing_bottom);
            this.pl_footer.Location = new System.Drawing.Point(1, 321);
            this.pl_footer.Name = "pl_footer";
            this.pl_footer.Size = new System.Drawing.Size(328, 35);
            this.pl_footer.TabIndex = 14;
            // 
            // pl_rec_finish
            // 
            this.pl_rec_finish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.pl_rec_finish.Controls.Add(this.btn_open_reced);
            this.pl_rec_finish.Controls.Add(this.btn_continue_rec);
            this.pl_rec_finish.Controls.Add(this.lb_reced_info);
            this.pl_rec_finish.Controls.Add(this.lb_reced_title);
            this.pl_rec_finish.Controls.Add(this.pb_reced);
            this.pl_rec_finish.Location = new System.Drawing.Point(1, 60);
            this.pl_rec_finish.Name = "pl_rec_finish";
            this.pl_rec_finish.Size = new System.Drawing.Size(328, 262);
            this.pl_rec_finish.TabIndex = 15;
            this.pl_rec_finish.Visible = false;
            // 
            // lb_reced_info
            // 
            this.lb_reced_info.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lb_reced_info.ForeColor = System.Drawing.Color.White;
            this.lb_reced_info.Location = new System.Drawing.Point(0, 133);
            this.lb_reced_info.Name = "lb_reced_info";
            this.lb_reced_info.Size = new System.Drawing.Size(328, 45);
            this.lb_reced_info.TabIndex = 2;
            this.lb_reced_info.Text = "视频时长 00:00";
            this.lb_reced_info.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lb_reced_title
            // 
            this.lb_reced_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_reced_title.ForeColor = System.Drawing.Color.White;
            this.lb_reced_title.Location = new System.Drawing.Point(0, 110);
            this.lb_reced_title.Name = "lb_reced_title";
            this.lb_reced_title.Size = new System.Drawing.Size(328, 23);
            this.lb_reced_title.TabIndex = 1;
            this.lb_reced_title.Text = "录制成功！视频已保存";
            this.lb_reced_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pb_reced
            // 
            this.pb_reced.Image = global::ScreenRecoder.App.Properties.Resources.ico_rec_success;
            this.pb_reced.Location = new System.Drawing.Point(131, 40);
            this.pb_reced.Name = "pb_reced";
            this.pb_reced.Size = new System.Drawing.Size(64, 64);
            this.pb_reced.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_reced.TabIndex = 0;
            this.pb_reced.TabStop = false;
            // 
            // pb_preview
            // 
            this.pb_preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.pb_preview.Location = new System.Drawing.Point(1, 60);
            this.pb_preview.Name = "pb_preview";
            this.pb_preview.Size = new System.Drawing.Size(328, 262);
            this.pb_preview.TabIndex = 7;
            this.pb_preview.TabStop = false;
            this.pb_preview.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_preview_Paint);
            // 
            // btn_screen_shotcut
            // 
            this.btn_screen_shotcut.BackColor = System.Drawing.Color.Transparent;
            this.btn_screen_shotcut.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_screen_shotcut.Icon = global::ScreenRecoder.App.Properties.Resources.ico_screenshotcct;
            this.btn_screen_shotcut.IconSize = new System.Drawing.Size(32, 32);
            this.btn_screen_shotcut.Light = false;
            this.btn_screen_shotcut.Location = new System.Drawing.Point(205, 2);
            this.btn_screen_shotcut.Name = "btn_screen_shotcut";
            this.btn_screen_shotcut.PressedColor = System.Drawing.Color.Black;
            this.btn_screen_shotcut.Size = new System.Drawing.Size(52, 60);
            this.btn_screen_shotcut.TabIndex = 16;
            this.toolTip1.SetToolTip(this.btn_screen_shotcut, "截图");
            this.btn_screen_shotcut.BtnClick += new System.EventHandler(this.btn_screen_shotcut_BtnClick);
            // 
            // toggle_fullscreen
            // 
            this.toggle_fullscreen.Checked = false;
            this.toggle_fullscreen.Location = new System.Drawing.Point(85, 9);
            this.toggle_fullscreen.Name = "toggle_fullscreen";
            this.toggle_fullscreen.Size = new System.Drawing.Size(32, 32);
            this.toggle_fullscreen.TabIndex = 8;
            this.toggle_fullscreen.CheckedChanged += new System.EventHandler(this.toggle_fullscreen_CheckedChanged);
            // 
            // btn_min
            // 
            this.btn_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_min.BackColor = System.Drawing.Color.Transparent;
            this.btn_min.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_min.DownImage = global::ScreenRecoder.App.Properties.Resources.btn_min_p;
            this.btn_min.HoverImage = global::ScreenRecoder.App.Properties.Resources.btn_min_h;
            this.btn_min.Location = new System.Drawing.Point(311, 29);
            this.btn_min.Name = "btn_min";
            this.btn_min.NormalImage = global::ScreenRecoder.App.Properties.Resources.btn_min_n;
            this.btn_min.Size = new System.Drawing.Size(12, 12);
            this.btn_min.TabIndex = 6;
            this.btn_min.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_min, "最小化");
            this.btn_min.ToolTipText = null;
            this.btn_min.Click += new System.EventHandler(this.btn_min_Click);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_close.DownImage = global::ScreenRecoder.App.Properties.Resources.btn_close_p;
            this.btn_close.HoverImage = global::ScreenRecoder.App.Properties.Resources.btn_close_h;
            this.btn_close.Location = new System.Drawing.Point(311, 13);
            this.btn_close.Name = "btn_close";
            this.btn_close.NormalImage = global::ScreenRecoder.App.Properties.Resources.btn_close_n;
            this.btn_close.Size = new System.Drawing.Size(12, 12);
            this.btn_close.TabIndex = 5;
            this.btn_close.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_close, "关闭");
            this.btn_close.ToolTipText = null;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_setting
            // 
            this.btn_setting.BackColor = System.Drawing.Color.Transparent;
            this.btn_setting.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_setting.Icon = global::ScreenRecoder.App.Properties.Resources.ico_settings;
            this.btn_setting.IconSize = new System.Drawing.Size(32, 32);
            this.btn_setting.Light = false;
            this.btn_setting.Location = new System.Drawing.Point(254, 2);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.PressedColor = System.Drawing.Color.Black;
            this.btn_setting.Size = new System.Drawing.Size(52, 60);
            this.btn_setting.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btn_setting, "设置");
            this.btn_setting.BtnClick += new System.EventHandler(this.btn_settings_BtnClick);
            // 
            // btn_stop
            // 
            this.btn_stop.BackColor = System.Drawing.Color.Transparent;
            this.btn_stop.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_stop.Icon = global::ScreenRecoder.App.Properties.Resources.ico_stop;
            this.btn_stop.IconSize = new System.Drawing.Size(26, 26);
            this.btn_stop.Light = false;
            this.btn_stop.Location = new System.Drawing.Point(1, 1);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.PressedColor = System.Drawing.Color.Black;
            this.btn_stop.Size = new System.Drawing.Size(55, 60);
            this.btn_stop.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btn_stop, "结束录制");
            this.btn_stop.BtnClick += new System.EventHandler(this.btn_stop_BtnClick);
            // 
            // btn_pause
            // 
            this.btn_pause.BackColor = System.Drawing.Color.Maroon;
            this.btn_pause.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_pause.Icon = global::ScreenRecoder.App.Properties.Resources.ico_pause;
            this.btn_pause.IconSize = new System.Drawing.Size(26, 26);
            this.btn_pause.Light = false;
            this.btn_pause.Location = new System.Drawing.Point(55, 1);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btn_pause.Size = new System.Drawing.Size(55, 60);
            this.btn_pause.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btn_pause, "暂停录制");
            this.btn_pause.BtnClick += new System.EventHandler(this.btn_pause_BtnClick);
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.Transparent;
            this.btn_start.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_start.Icon = global::ScreenRecoder.App.Properties.Resources.ico_record;
            this.btn_start.IconSize = new System.Drawing.Size(26, 26);
            this.btn_start.Light = false;
            this.btn_start.Location = new System.Drawing.Point(2, 2);
            this.btn_start.Name = "btn_start";
            this.btn_start.PressedColor = System.Drawing.Color.Black;
            this.btn_start.Size = new System.Drawing.Size(55, 60);
            this.btn_start.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btn_start, "开始录制");
            this.btn_start.BtnClick += new System.EventHandler(this.btn_record_BtnClick);
            // 
            // btn_open_reced
            // 
            this.btn_open_reced.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.btn_open_reced.BorderColor = System.Drawing.Color.Empty;
            this.btn_open_reced.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_open_reced.ForeColor = System.Drawing.Color.White;
            this.btn_open_reced.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btn_open_reced.Image = null;
            this.btn_open_reced.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_open_reced.Location = new System.Drawing.Point(74, 197);
            this.btn_open_reced.Name = "btn_open_reced";
            this.btn_open_reced.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btn_open_reced.Size = new System.Drawing.Size(86, 26);
            this.btn_open_reced.TabIndex = 43;
            this.btn_open_reced.Text = "浏览录像";
            this.btn_open_reced.Click += new System.EventHandler(this.btn_open_reced_Click);
            // 
            // btn_continue_rec
            // 
            this.btn_continue_rec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.btn_continue_rec.BorderColor = System.Drawing.Color.Empty;
            this.btn_continue_rec.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_continue_rec.ForeColor = System.Drawing.Color.White;
            this.btn_continue_rec.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btn_continue_rec.Image = null;
            this.btn_continue_rec.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_continue_rec.Location = new System.Drawing.Point(166, 197);
            this.btn_continue_rec.Name = "btn_continue_rec";
            this.btn_continue_rec.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btn_continue_rec.Size = new System.Drawing.Size(86, 26);
            this.btn_continue_rec.TabIndex = 42;
            this.btn_continue_rec.Text = "继续录像";
            this.btn_continue_rec.Click += new System.EventHandler(this.btn_continue_rec_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(330, 368);
            this.Controls.Add(this.btn_screen_shotcut);
            this.Controls.Add(this.pl_footer);
            this.Controls.Add(this.btn_min);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_setting);
            this.Controls.Add(this.pl_rec);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.lb_tip);
            this.Controls.Add(this.pl_rec_finish);
            this.Controls.Add(this.pb_preview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "屏幕录像";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMain_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.btn_top)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_rec_mic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_rec_sound)).EndInit();
            this.pl_rec.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pl_footer.ResumeLayout(false);
            this.pl_footer.PerformLayout();
            this.pl_rec_finish.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_reced)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private IconButton btn_start;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lb_time;
        private IconButton btn_setting;
        private IconButton btn_stop;
        private System.Windows.Forms.Panel pl_rec;
        private IconButton btn_pause;
        private bells.app.ImageButton btn_close;
        private bells.app.ImageButton btn_min;
        private System.Windows.Forms.Label lb_tip;
        private System.Windows.Forms.Timer timerBtnFlashing;
        private System.Windows.Forms.Timer timerUpdateTime;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示主窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出软件ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pb_preview;
        private Controls.Toggle toggle_fullscreen;
        private System.Windows.Forms.Label lb_fullscreen;
        private System.Windows.Forms.Label lb_rectrec;
        private System.Windows.Forms.Timer timerPreview;
        private System.Windows.Forms.Label lb_recing_bottom;
        private System.Windows.Forms.Panel pl_footer;
        public System.Windows.Forms.PictureBox btn_rec_mic;
        public System.Windows.Forms.PictureBox btn_rec_sound;
        private System.Windows.Forms.Panel pl_rec_finish;
        private System.Windows.Forms.PictureBox pb_reced;
        private System.Windows.Forms.Label lb_reced_info;
        private System.Windows.Forms.Label lb_reced_title;
        private Controls.FlatButton btn_open_reced;
        private Controls.FlatButton btn_continue_rec;
        private IconButton btn_screen_shotcut;
        public System.Windows.Forms.PictureBox btn_top;
    }
}