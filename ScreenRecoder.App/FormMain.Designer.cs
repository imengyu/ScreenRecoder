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
            this.lb_time = new System.Windows.Forms.Label();
            this.pl_rec = new System.Windows.Forms.Panel();
            this.lb_tip = new System.Windows.Forms.Label();
            this.timerBtnFlashing = new System.Windows.Forms.Timer(this.components);
            this.timerUpdateTime = new System.Windows.Forms.Timer(this.components);
            this.check_exit_min = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示主窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出软件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.check_rem_pos = new System.Windows.Forms.CheckBox();
            this.check_fullscreen = new System.Windows.Forms.CheckBox();
            this.numeric_frame_rate = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_format = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.combo_quality = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_export_dir = new System.Windows.Forms.TextBox();
            this.btn_defsettings = new System.Windows.Forms.Button();
            this.check_recmic = new System.Windows.Forms.CheckBox();
            this.check_recsound = new System.Windows.Forms.CheckBox();
            this.btn_softset = new System.Windows.Forms.Button();
            this.pl_softset = new System.Windows.Forms.Panel();
            this.check_hide_whenrec = new System.Windows.Forms.CheckBox();
            this.check_usemini_inrec = new System.Windows.Forms.CheckBox();
            this.pl_recset = new System.Windows.Forms.Panel();
            this.lb_recset_notify = new System.Windows.Forms.Label();
            this.btn_recset = new System.Windows.Forms.Button();
            this.btn_hotkeyset = new System.Windows.Forms.Button();
            this.pl_hotkeyset = new System.Windows.Forms.Panel();
            this.link_reboot = new System.Windows.Forms.LinkLabel();
            this.lb_keyset_notify = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_about = new System.Windows.Forms.Button();
            this.pl_about = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btn_pause = new ScreenRecoder.App.IconButton();
            this.btn_stop = new ScreenRecoder.App.IconButton();
            this.btn_min = new bells.app.ImageButton();
            this.btn_close = new bells.app.ImageButton();
            this.btn_start = new ScreenRecoder.App.IconButton();
            this.btn_setting = new ScreenRecoder.App.IconButton();
            this.hotKey_showhide = new ScreenRecoder.App.HotKeySelecter();
            this.hotKey_stop = new ScreenRecoder.App.HotKeySelecter();
            this.hotKey_pause = new ScreenRecoder.App.HotKeySelecter();
            this.hotKey_start = new ScreenRecoder.App.HotKeySelecter();
            this.btn_open_folder = new bells.app.ImageButton();
            this.btn_choosedir = new bells.app.ImageButton();
            this.pl_rec.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_frame_rate)).BeginInit();
            this.pl_softset.SuspendLayout();
            this.pl_recset.SuspendLayout();
            this.pl_hotkeyset.SuspendLayout();
            this.pl_about.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_open_folder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_choosedir)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_time
            // 
            this.lb_time.AutoEllipsis = true;
            this.lb_time.BackColor = System.Drawing.Color.DarkRed;
            this.lb_time.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_time.ForeColor = System.Drawing.Color.White;
            this.lb_time.Location = new System.Drawing.Point(110, 0);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(130, 55);
            this.lb_time.TabIndex = 1;
            this.lb_time.Text = "00:00:00";
            this.lb_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_time.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            // 
            // pl_rec
            // 
            this.pl_rec.Controls.Add(this.btn_pause);
            this.pl_rec.Controls.Add(this.btn_stop);
            this.pl_rec.Controls.Add(this.lb_time);
            this.pl_rec.Location = new System.Drawing.Point(0, 0);
            this.pl_rec.Name = "pl_rec";
            this.pl_rec.Size = new System.Drawing.Size(240, 55);
            this.pl_rec.TabIndex = 4;
            this.pl_rec.Visible = false;
            // 
            // lb_tip
            // 
            this.lb_tip.AutoEllipsis = true;
            this.lb_tip.BackColor = System.Drawing.Color.Transparent;
            this.lb_tip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_tip.ForeColor = System.Drawing.Color.White;
            this.lb_tip.Location = new System.Drawing.Point(53, 0);
            this.lb_tip.Name = "lb_tip";
            this.lb_tip.Size = new System.Drawing.Size(179, 55);
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
            // check_exit_min
            // 
            this.check_exit_min.AutoSize = true;
            this.check_exit_min.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_exit_min.ForeColor = System.Drawing.Color.White;
            this.check_exit_min.Location = new System.Drawing.Point(12, 0);
            this.check_exit_min.Name = "check_exit_min";
            this.check_exit_min.Size = new System.Drawing.Size(144, 21);
            this.check_exit_min.TabIndex = 7;
            this.check_exit_min.Text = "点击×时最小化到托盘";
            this.check_exit_min.UseVisualStyleBackColor = true;
            this.check_exit_min.CheckedChanged += new System.EventHandler(this.check_exit_min_CheckedChanged);
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
            // check_rem_pos
            // 
            this.check_rem_pos.AutoSize = true;
            this.check_rem_pos.Checked = true;
            this.check_rem_pos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_rem_pos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_rem_pos.ForeColor = System.Drawing.Color.White;
            this.check_rem_pos.Location = new System.Drawing.Point(12, 17);
            this.check_rem_pos.Name = "check_rem_pos";
            this.check_rem_pos.Size = new System.Drawing.Size(111, 21);
            this.check_rem_pos.TabIndex = 9;
            this.check_rem_pos.Text = "记住本窗口位置";
            this.check_rem_pos.UseVisualStyleBackColor = true;
            this.check_rem_pos.CheckedChanged += new System.EventHandler(this.check_rem_pos_CheckedChanged);
            // 
            // check_fullscreen
            // 
            this.check_fullscreen.AutoSize = true;
            this.check_fullscreen.Checked = true;
            this.check_fullscreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_fullscreen.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_fullscreen.ForeColor = System.Drawing.Color.White;
            this.check_fullscreen.Location = new System.Drawing.Point(10, 3);
            this.check_fullscreen.Name = "check_fullscreen";
            this.check_fullscreen.Size = new System.Drawing.Size(75, 21);
            this.check_fullscreen.TabIndex = 11;
            this.check_fullscreen.Text = "全屏录制";
            this.check_fullscreen.UseVisualStyleBackColor = true;
            this.check_fullscreen.CheckedChanged += new System.EventHandler(this.check_fullscreen_CheckedChanged);
            // 
            // numeric_frame_rate
            // 
            this.numeric_frame_rate.Location = new System.Drawing.Point(9, 83);
            this.numeric_frame_rate.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numeric_frame_rate.Name = "numeric_frame_rate";
            this.numeric_frame_rate.Size = new System.Drawing.Size(49, 21);
            this.numeric_frame_rate.TabIndex = 12;
            this.numeric_frame_rate.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numeric_frame_rate.ValueChanged += new System.EventHandler(this.numeric_frame_rate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(9, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "录制帧率";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(71, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(233, 41);
            this.label4.TabIndex = 14;
            this.label4.Text = "帧率越高画面约流畅，但会加重电脑负担，所以帧率不宜设置太高";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(9, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "录制格式";
            // 
            // combo_format
            // 
            this.combo_format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_format.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_format.FormattingEnabled = true;
            this.combo_format.Items.AddRange(new object[] {
            "默认",
            "MP4 (H264) 默认",
            "MP4 (MPEG4)",
            "AVI (H264)",
            "AVI (MPEG4)",
            "FLV",
            "WMV"});
            this.combo_format.Location = new System.Drawing.Point(74, 107);
            this.combo_format.Name = "combo_format";
            this.combo_format.Size = new System.Drawing.Size(190, 20);
            this.combo_format.TabIndex = 16;
            this.combo_format.SelectedIndexChanged += new System.EventHandler(this.combo_format_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(9, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "录制质量";
            // 
            // combo_quality
            // 
            this.combo_quality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_quality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_quality.FormattingEnabled = true;
            this.combo_quality.Items.AddRange(new object[] {
            "默认",
            "普通",
            "高",
            "最高"});
            this.combo_quality.Location = new System.Drawing.Point(74, 130);
            this.combo_quality.Name = "combo_quality";
            this.combo_quality.Size = new System.Drawing.Size(190, 20);
            this.combo_quality.TabIndex = 18;
            this.combo_quality.SelectedIndexChanged += new System.EventHandler(this.combo_quality_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(9, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "输出目录";
            // 
            // textBox_export_dir
            // 
            this.textBox_export_dir.Location = new System.Drawing.Point(74, 154);
            this.textBox_export_dir.Name = "textBox_export_dir";
            this.textBox_export_dir.Size = new System.Drawing.Size(190, 21);
            this.textBox_export_dir.TabIndex = 20;
            // 
            // btn_defsettings
            // 
            this.btn_defsettings.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_defsettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_defsettings.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_defsettings.ForeColor = System.Drawing.Color.White;
            this.btn_defsettings.Location = new System.Drawing.Point(12, 306);
            this.btn_defsettings.Name = "btn_defsettings";
            this.btn_defsettings.Size = new System.Drawing.Size(75, 26);
            this.btn_defsettings.TabIndex = 22;
            this.btn_defsettings.Text = "默认设置";
            this.btn_defsettings.UseVisualStyleBackColor = false;
            this.btn_defsettings.Click += new System.EventHandler(this.btn_defsettings_Click);
            // 
            // check_recmic
            // 
            this.check_recmic.AutoSize = true;
            this.check_recmic.Checked = true;
            this.check_recmic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_recmic.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_recmic.ForeColor = System.Drawing.Color.White;
            this.check_recmic.Location = new System.Drawing.Point(10, 21);
            this.check_recmic.Name = "check_recmic";
            this.check_recmic.Size = new System.Drawing.Size(75, 21);
            this.check_recmic.TabIndex = 24;
            this.check_recmic.Text = "录制鼠标";
            this.check_recmic.UseVisualStyleBackColor = true;
            this.check_recmic.CheckedChanged += new System.EventHandler(this.check_recmic_CheckedChanged);
            // 
            // check_recsound
            // 
            this.check_recsound.AutoSize = true;
            this.check_recsound.Checked = true;
            this.check_recsound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_recsound.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_recsound.ForeColor = System.Drawing.Color.White;
            this.check_recsound.Location = new System.Drawing.Point(10, 39);
            this.check_recsound.Name = "check_recsound";
            this.check_recsound.Size = new System.Drawing.Size(75, 21);
            this.check_recsound.TabIndex = 25;
            this.check_recsound.Text = "录制声音";
            this.check_recsound.UseVisualStyleBackColor = true;
            this.check_recsound.CheckedChanged += new System.EventHandler(this.check_recsound_CheckedChanged);
            // 
            // btn_softset
            // 
            this.btn_softset.BackColor = System.Drawing.Color.Tomato;
            this.btn_softset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_softset.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_softset.ForeColor = System.Drawing.Color.White;
            this.btn_softset.Location = new System.Drawing.Point(12, 61);
            this.btn_softset.Name = "btn_softset";
            this.btn_softset.Size = new System.Drawing.Size(66, 26);
            this.btn_softset.TabIndex = 26;
            this.btn_softset.Text = "软件设置";
            this.btn_softset.UseVisualStyleBackColor = false;
            this.btn_softset.Click += new System.EventHandler(this.btn_softset_Click);
            // 
            // pl_softset
            // 
            this.pl_softset.Controls.Add(this.check_hide_whenrec);
            this.pl_softset.Controls.Add(this.check_usemini_inrec);
            this.pl_softset.Controls.Add(this.check_rem_pos);
            this.pl_softset.Controls.Add(this.check_exit_min);
            this.pl_softset.Location = new System.Drawing.Point(0, 93);
            this.pl_softset.Name = "pl_softset";
            this.pl_softset.Size = new System.Drawing.Size(309, 207);
            this.pl_softset.TabIndex = 27;
            // 
            // check_hide_whenrec
            // 
            this.check_hide_whenrec.AutoSize = true;
            this.check_hide_whenrec.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_hide_whenrec.ForeColor = System.Drawing.Color.White;
            this.check_hide_whenrec.Location = new System.Drawing.Point(12, 55);
            this.check_hide_whenrec.Name = "check_hide_whenrec";
            this.check_hide_whenrec.Size = new System.Drawing.Size(135, 21);
            this.check_hide_whenrec.TabIndex = 11;
            this.check_hide_whenrec.Text = "录制时隐藏软件窗口";
            this.check_hide_whenrec.UseVisualStyleBackColor = true;
            this.check_hide_whenrec.CheckedChanged += new System.EventHandler(this.check_hide_whenrec_CheckedChanged);
            // 
            // check_usemini_inrec
            // 
            this.check_usemini_inrec.AutoSize = true;
            this.check_usemini_inrec.Checked = true;
            this.check_usemini_inrec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_usemini_inrec.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_usemini_inrec.ForeColor = System.Drawing.Color.White;
            this.check_usemini_inrec.Location = new System.Drawing.Point(12, 36);
            this.check_usemini_inrec.Name = "check_usemini_inrec";
            this.check_usemini_inrec.Size = new System.Drawing.Size(153, 21);
            this.check_usemini_inrec.TabIndex = 10;
            this.check_usemini_inrec.Text = "录制时使用MINI小窗口";
            this.check_usemini_inrec.UseVisualStyleBackColor = true;
            this.check_usemini_inrec.CheckedChanged += new System.EventHandler(this.check_usemini_inrec_CheckedChanged);
            // 
            // pl_recset
            // 
            this.pl_recset.Controls.Add(this.btn_open_folder);
            this.pl_recset.Controls.Add(this.lb_recset_notify);
            this.pl_recset.Controls.Add(this.check_recsound);
            this.pl_recset.Controls.Add(this.check_recmic);
            this.pl_recset.Controls.Add(this.check_fullscreen);
            this.pl_recset.Controls.Add(this.btn_choosedir);
            this.pl_recset.Controls.Add(this.numeric_frame_rate);
            this.pl_recset.Controls.Add(this.textBox_export_dir);
            this.pl_recset.Controls.Add(this.label3);
            this.pl_recset.Controls.Add(this.label7);
            this.pl_recset.Controls.Add(this.label4);
            this.pl_recset.Controls.Add(this.combo_quality);
            this.pl_recset.Controls.Add(this.label5);
            this.pl_recset.Controls.Add(this.label6);
            this.pl_recset.Controls.Add(this.combo_format);
            this.pl_recset.Location = new System.Drawing.Point(0, 93);
            this.pl_recset.Name = "pl_recset";
            this.pl_recset.Size = new System.Drawing.Size(309, 207);
            this.pl_recset.TabIndex = 28;
            this.pl_recset.Visible = false;
            // 
            // lb_recset_notify
            // 
            this.lb_recset_notify.AutoEllipsis = true;
            this.lb_recset_notify.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_recset_notify.ForeColor = System.Drawing.Color.White;
            this.lb_recset_notify.Image = global::ScreenRecoder.App.Properties.Resources.ico_msg;
            this.lb_recset_notify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_recset_notify.Location = new System.Drawing.Point(10, 181);
            this.lb_recset_notify.Name = "lb_recset_notify";
            this.lb_recset_notify.Size = new System.Drawing.Size(254, 21);
            this.lb_recset_notify.TabIndex = 33;
            this.lb_recset_notify.Text = "您所更改的设置将在下次录像时应用";
            this.lb_recset_notify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_recset_notify.Visible = false;
            // 
            // btn_recset
            // 
            this.btn_recset.BackColor = System.Drawing.Color.Tomato;
            this.btn_recset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_recset.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_recset.ForeColor = System.Drawing.Color.White;
            this.btn_recset.Location = new System.Drawing.Point(84, 61);
            this.btn_recset.Name = "btn_recset";
            this.btn_recset.Size = new System.Drawing.Size(66, 26);
            this.btn_recset.TabIndex = 29;
            this.btn_recset.Text = "录制设置";
            this.btn_recset.UseVisualStyleBackColor = false;
            this.btn_recset.Click += new System.EventHandler(this.btn_recset_Click);
            // 
            // btn_hotkeyset
            // 
            this.btn_hotkeyset.BackColor = System.Drawing.Color.Tomato;
            this.btn_hotkeyset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_hotkeyset.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_hotkeyset.ForeColor = System.Drawing.Color.White;
            this.btn_hotkeyset.Location = new System.Drawing.Point(156, 61);
            this.btn_hotkeyset.Name = "btn_hotkeyset";
            this.btn_hotkeyset.Size = new System.Drawing.Size(66, 26);
            this.btn_hotkeyset.TabIndex = 30;
            this.btn_hotkeyset.Text = "热键设置";
            this.btn_hotkeyset.UseVisualStyleBackColor = false;
            this.btn_hotkeyset.Click += new System.EventHandler(this.btn_hotkeyset_Click);
            // 
            // pl_hotkeyset
            // 
            this.pl_hotkeyset.Controls.Add(this.link_reboot);
            this.pl_hotkeyset.Controls.Add(this.lb_keyset_notify);
            this.pl_hotkeyset.Controls.Add(this.hotKey_showhide);
            this.pl_hotkeyset.Controls.Add(this.hotKey_stop);
            this.pl_hotkeyset.Controls.Add(this.hotKey_pause);
            this.pl_hotkeyset.Controls.Add(this.hotKey_start);
            this.pl_hotkeyset.Controls.Add(this.label9);
            this.pl_hotkeyset.Controls.Add(this.label8);
            this.pl_hotkeyset.Controls.Add(this.label2);
            this.pl_hotkeyset.Controls.Add(this.label1);
            this.pl_hotkeyset.Location = new System.Drawing.Point(0, 93);
            this.pl_hotkeyset.Name = "pl_hotkeyset";
            this.pl_hotkeyset.Size = new System.Drawing.Size(309, 207);
            this.pl_hotkeyset.TabIndex = 31;
            this.pl_hotkeyset.Visible = false;
            // 
            // link_reboot
            // 
            this.link_reboot.AutoSize = true;
            this.link_reboot.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.link_reboot.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.link_reboot.LinkColor = System.Drawing.Color.DeepSkyBlue;
            this.link_reboot.Location = new System.Drawing.Point(246, 179);
            this.link_reboot.Name = "link_reboot";
            this.link_reboot.Size = new System.Drawing.Size(56, 17);
            this.link_reboot.TabIndex = 33;
            this.link_reboot.TabStop = true;
            this.link_reboot.Text = "立即重启";
            this.link_reboot.Visible = false;
            this.link_reboot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_reboot_LinkClicked);
            // 
            // lb_keyset_notify
            // 
            this.lb_keyset_notify.AutoEllipsis = true;
            this.lb_keyset_notify.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_keyset_notify.ForeColor = System.Drawing.Color.White;
            this.lb_keyset_notify.Image = global::ScreenRecoder.App.Properties.Resources.ico_msg;
            this.lb_keyset_notify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_keyset_notify.Location = new System.Drawing.Point(12, 177);
            this.lb_keyset_notify.Name = "lb_keyset_notify";
            this.lb_keyset_notify.Size = new System.Drawing.Size(262, 21);
            this.lb_keyset_notify.TabIndex = 32;
            this.lb_keyset_notify.Text = "热键设置只有在重启软件以后才会生效";
            this.lb_keyset_notify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_keyset_notify.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(10, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 17);
            this.label9.TabIndex = 27;
            this.label9.Text = "显示/隐藏主窗口";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(9, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "结束录制";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "暂停/继续录制";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "开始录制";
            // 
            // btn_about
            // 
            this.btn_about.BackColor = System.Drawing.Color.Tomato;
            this.btn_about.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_about.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_about.ForeColor = System.Drawing.Color.White;
            this.btn_about.Location = new System.Drawing.Point(228, 61);
            this.btn_about.Name = "btn_about";
            this.btn_about.Size = new System.Drawing.Size(66, 26);
            this.btn_about.TabIndex = 32;
            this.btn_about.Text = "关于软件";
            this.btn_about.UseVisualStyleBackColor = false;
            this.btn_about.Click += new System.EventHandler(this.btn_about_Click);
            // 
            // pl_about
            // 
            this.pl_about.Controls.Add(this.label18);
            this.pl_about.Controls.Add(this.label17);
            this.pl_about.Controls.Add(this.label16);
            this.pl_about.Controls.Add(this.label15);
            this.pl_about.Controls.Add(this.label10);
            this.pl_about.Controls.Add(this.label11);
            this.pl_about.Controls.Add(this.label12);
            this.pl_about.Controls.Add(this.label13);
            this.pl_about.Controls.Add(this.label14);
            this.pl_about.Location = new System.Drawing.Point(0, 93);
            this.pl_about.Name = "pl_about";
            this.pl_about.Size = new System.Drawing.Size(309, 248);
            this.pl_about.TabIndex = 34;
            this.pl_about.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoEllipsis = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(9, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(285, 21);
            this.label10.TabIndex = 32;
            this.label10.Text = "一个非常简单的屏幕录像软件";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(16, 160);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 17);
            this.label11.TabIndex = 27;
            this.label11.Text = "制作者：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(16, 137);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 17);
            this.label12.TabIndex = 24;
            this.label12.Text = "制作日期：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(16, 116);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 17);
            this.label13.TabIndex = 23;
            this.label13.Text = "版本：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Image = global::ScreenRecoder.App.Properties.Resources.question_mark_r_o;
            this.label14.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label14.Location = new System.Drawing.Point(10, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(284, 64);
            this.label14.TabIndex = 21;
            this.label14.Text = "关于 Screen Recoder";
            this.label14.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(112, 116);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 17);
            this.label15.TabIndex = 33;
            this.label15.Text = "3.1.0.627";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(112, 137);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 17);
            this.label16.TabIndex = 34;
            this.label16.Text = "2019/3/31";
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(13, 218);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(281, 17);
            this.label17.TabIndex = 35;
            this.label17.Text = "© 2019 MagicalSoft™";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(112, 160);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(111, 17);
            this.label18.TabIndex = 36;
            this.label18.Text = "DreamFish（yzc）";
            // 
            // btn_pause
            // 
            this.btn_pause.BackColor = System.Drawing.Color.Maroon;
            this.btn_pause.HoverColor = System.Drawing.Color.IndianRed;
            this.btn_pause.Icon = global::ScreenRecoder.App.Properties.Resources.ico_pause;
            this.btn_pause.IconSize = new System.Drawing.Size(26, 26);
            this.btn_pause.Light = false;
            this.btn_pause.Location = new System.Drawing.Point(55, 0);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.PressedColor = System.Drawing.Color.Black;
            this.btn_pause.Size = new System.Drawing.Size(55, 55);
            this.btn_pause.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btn_pause, "暂停录制");
            this.btn_pause.BtnClick += new System.EventHandler(this.btn_pause_BtnClick);
            // 
            // btn_stop
            // 
            this.btn_stop.BackColor = System.Drawing.Color.Transparent;
            this.btn_stop.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(90)))));
            this.btn_stop.Icon = global::ScreenRecoder.App.Properties.Resources.ico_stop;
            this.btn_stop.IconSize = new System.Drawing.Size(26, 26);
            this.btn_stop.Light = false;
            this.btn_stop.Location = new System.Drawing.Point(0, 0);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.PressedColor = System.Drawing.Color.Black;
            this.btn_stop.Size = new System.Drawing.Size(55, 55);
            this.btn_stop.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btn_stop, "结束录制");
            this.btn_stop.BtnClick += new System.EventHandler(this.btn_stop_BtnClick);
            // 
            // btn_min
            // 
            this.btn_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_min.BackColor = System.Drawing.Color.Transparent;
            this.btn_min.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_min.DownImage = global::ScreenRecoder.App.Properties.Resources.btn_min_p;
            this.btn_min.HoverImage = global::ScreenRecoder.App.Properties.Resources.btn_min_h;
            this.btn_min.Location = new System.Drawing.Point(295, 26);
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
            this.btn_close.Location = new System.Drawing.Point(295, 8);
            this.btn_close.Name = "btn_close";
            this.btn_close.NormalImage = global::ScreenRecoder.App.Properties.Resources.btn_close_n;
            this.btn_close.Size = new System.Drawing.Size(12, 12);
            this.btn_close.TabIndex = 5;
            this.btn_close.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_close, "关闭");
            this.btn_close.ToolTipText = null;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.Transparent;
            this.btn_start.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(90)))));
            this.btn_start.Icon = global::ScreenRecoder.App.Properties.Resources.ico_record;
            this.btn_start.IconSize = new System.Drawing.Size(26, 26);
            this.btn_start.Light = false;
            this.btn_start.Location = new System.Drawing.Point(0, 0);
            this.btn_start.Name = "btn_start";
            this.btn_start.PressedColor = System.Drawing.Color.Black;
            this.btn_start.Size = new System.Drawing.Size(55, 55);
            this.btn_start.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btn_start, "开始录制");
            this.btn_start.BtnClick += new System.EventHandler(this.btn_record_BtnClick);
            // 
            // btn_setting
            // 
            this.btn_setting.BackColor = System.Drawing.Color.Transparent;
            this.btn_setting.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(90)))));
            this.btn_setting.Icon = global::ScreenRecoder.App.Properties.Resources.ico_settings;
            this.btn_setting.IconSize = new System.Drawing.Size(32, 32);
            this.btn_setting.Light = false;
            this.btn_setting.Location = new System.Drawing.Point(241, 0);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.PressedColor = System.Drawing.Color.Black;
            this.btn_setting.Size = new System.Drawing.Size(55, 55);
            this.btn_setting.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btn_setting, "设置");
            this.btn_setting.BtnClick += new System.EventHandler(this.btn_settings_BtnClick);
            // 
            // hotKey_showhide
            // 
            this.hotKey_showhide.Location = new System.Drawing.Point(174, 96);
            this.hotKey_showhide.Name = "hotKey_showhide";
            this.hotKey_showhide.Size = new System.Drawing.Size(100, 21);
            this.hotKey_showhide.TabIndex = 31;
            this.hotKey_showhide.KeysChanged += new System.EventHandler(this.hotKey_showhide_KeysChanged);
            // 
            // hotKey_stop
            // 
            this.hotKey_stop.Location = new System.Drawing.Point(174, 69);
            this.hotKey_stop.Name = "hotKey_stop";
            this.hotKey_stop.Size = new System.Drawing.Size(100, 21);
            this.hotKey_stop.TabIndex = 30;
            this.hotKey_stop.KeysChanged += new System.EventHandler(this.hotKey_stop_KeysChanged);
            // 
            // hotKey_pause
            // 
            this.hotKey_pause.Location = new System.Drawing.Point(174, 41);
            this.hotKey_pause.Name = "hotKey_pause";
            this.hotKey_pause.Size = new System.Drawing.Size(100, 21);
            this.hotKey_pause.TabIndex = 29;
            this.hotKey_pause.KeysChanged += new System.EventHandler(this.hotKey_pause_KeysChanged);
            // 
            // hotKey_start
            // 
            this.hotKey_start.Location = new System.Drawing.Point(174, 13);
            this.hotKey_start.Name = "hotKey_start";
            this.hotKey_start.Size = new System.Drawing.Size(100, 21);
            this.hotKey_start.TabIndex = 28;
            this.hotKey_start.KeysChanged += new System.EventHandler(this.hotKey_start_KeysChanged);
            // 
            // btn_open_folder
            // 
            this.btn_open_folder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_open_folder.BackColor = System.Drawing.Color.Transparent;
            this.btn_open_folder.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_open_folder.DownImage = ((System.Drawing.Image)(resources.GetObject("btn_open_folder.DownImage")));
            this.btn_open_folder.HoverImage = ((System.Drawing.Image)(resources.GetObject("btn_open_folder.HoverImage")));
            this.btn_open_folder.Location = new System.Drawing.Point(290, 156);
            this.btn_open_folder.Name = "btn_open_folder";
            this.btn_open_folder.NormalImage = ((System.Drawing.Image)(resources.GetObject("btn_open_folder.NormalImage")));
            this.btn_open_folder.Size = new System.Drawing.Size(16, 16);
            this.btn_open_folder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_open_folder.TabIndex = 34;
            this.btn_open_folder.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_open_folder, "打开目录");
            this.btn_open_folder.ToolTipText = null;
            this.btn_open_folder.Click += new System.EventHandler(this.btn_open_folder_Click);
            // 
            // btn_choosedir
            // 
            this.btn_choosedir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_choosedir.BackColor = System.Drawing.Color.Transparent;
            this.btn_choosedir.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_choosedir.DownImage = global::ScreenRecoder.App.Properties.Resources.btn_folder_p;
            this.btn_choosedir.HoverImage = global::ScreenRecoder.App.Properties.Resources.btn_folder_h;
            this.btn_choosedir.Location = new System.Drawing.Point(270, 156);
            this.btn_choosedir.Name = "btn_choosedir";
            this.btn_choosedir.NormalImage = global::ScreenRecoder.App.Properties.Resources.btn_folder_n;
            this.btn_choosedir.Size = new System.Drawing.Size(16, 16);
            this.btn_choosedir.TabIndex = 21;
            this.btn_choosedir.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_choosedir, "选择目录");
            this.btn_choosedir.ToolTipText = null;
            this.btn_choosedir.Click += new System.EventHandler(this.btn_choosedir_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(314, 353);
            this.Controls.Add(this.pl_about);
            this.Controls.Add(this.btn_about);
            this.Controls.Add(this.btn_hotkeyset);
            this.Controls.Add(this.btn_recset);
            this.Controls.Add(this.btn_softset);
            this.Controls.Add(this.btn_defsettings);
            this.Controls.Add(this.pl_rec);
            this.Controls.Add(this.btn_min);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_setting);
            this.Controls.Add(this.lb_tip);
            this.Controls.Add(this.pl_hotkeyset);
            this.Controls.Add(this.pl_recset);
            this.Controls.Add(this.pl_softset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "屏幕录像";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.pl_rec.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numeric_frame_rate)).EndInit();
            this.pl_softset.ResumeLayout(false);
            this.pl_softset.PerformLayout();
            this.pl_recset.ResumeLayout(false);
            this.pl_recset.PerformLayout();
            this.pl_hotkeyset.ResumeLayout(false);
            this.pl_hotkeyset.PerformLayout();
            this.pl_about.ResumeLayout(false);
            this.pl_about.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_open_folder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_choosedir)).EndInit();
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
        private System.Windows.Forms.CheckBox check_exit_min;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox check_rem_pos;
        private System.Windows.Forms.CheckBox check_fullscreen;
        private System.Windows.Forms.NumericUpDown numeric_frame_rate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combo_format;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox combo_quality;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_export_dir;
        private bells.app.ImageButton btn_choosedir;
        private System.Windows.Forms.Button btn_defsettings;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示主窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出软件ToolStripMenuItem;
        private System.Windows.Forms.CheckBox check_recmic;
        private System.Windows.Forms.CheckBox check_recsound;
        private System.Windows.Forms.Button btn_softset;
        private System.Windows.Forms.Panel pl_softset;
        private System.Windows.Forms.Panel pl_recset;
        private System.Windows.Forms.Button btn_recset;
        private System.Windows.Forms.Button btn_hotkeyset;
        private System.Windows.Forms.Panel pl_hotkeyset;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private HotKeySelecter hotKey_showhide;
        private HotKeySelecter hotKey_stop;
        private HotKeySelecter hotKey_pause;
        private HotKeySelecter hotKey_start;
        private System.Windows.Forms.Label lb_keyset_notify;
        private System.Windows.Forms.Label lb_recset_notify;
        private System.Windows.Forms.LinkLabel link_reboot;
        private System.Windows.Forms.Button btn_about;
        private System.Windows.Forms.CheckBox check_hide_whenrec;
        private System.Windows.Forms.CheckBox check_usemini_inrec;
        private bells.app.ImageButton btn_open_folder;
        private System.Windows.Forms.Panel pl_about;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
    }
}