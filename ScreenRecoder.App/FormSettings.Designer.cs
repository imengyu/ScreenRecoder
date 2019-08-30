namespace ScreenRecoder.App
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.pl_recset = new System.Windows.Forms.Panel();
            this.radio_rect_rec = new System.Windows.Forms.RadioButton();
            this.check_fullscreen = new System.Windows.Forms.RadioButton();
            this.label20 = new System.Windows.Forms.Label();
            this.combo_framerate = new System.Windows.Forms.ComboBox();
            this.lb_recset_notify = new System.Windows.Forms.Label();
            this.check_recsound = new System.Windows.Forms.CheckBox();
            this.check_recmic = new System.Windows.Forms.CheckBox();
            this.numeric_frame_rate = new System.Windows.Forms.NumericUpDown();
            this.textBox_export_dir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.combo_quality = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.combo_format = new System.Windows.Forms.ComboBox();
            this.pl_softset = new System.Windows.Forms.Panel();
            this.check_top = new System.Windows.Forms.CheckBox();
            this.check_show_preview = new System.Windows.Forms.CheckBox();
            this.check_use_sound_tip = new System.Windows.Forms.CheckBox();
            this.check_hide_whenrec = new System.Windows.Forms.CheckBox();
            this.check_usemini_inrec = new System.Windows.Forms.CheckBox();
            this.check_rem_pos = new System.Windows.Forms.CheckBox();
            this.check_exit_min = new System.Windows.Forms.CheckBox();
            this.pl_about = new System.Windows.Forms.Panel();
            this.link_github = new System.Windows.Forms.LinkLabel();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pl_hotkeyset = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.link_reboot = new System.Windows.Forms.LinkLabel();
            this.lb_keyset_notify = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label22 = new System.Windows.Forms.Label();
            this.combo_mic = new System.Windows.Forms.ComboBox();
            this.btn_exit = new ScreenRecoder.App.Controls.FlatButton();
            this.btn_ok = new ScreenRecoder.App.Controls.FlatButton();
            this.btn_reset_default = new ScreenRecoder.App.Controls.FlatButton();
            this.btn_choosedir = new bells.app.ImageButton();
            this.hotKey_screenshutcut = new ScreenRecoder.App.HotKeySelecter();
            this.hotKey_showhide = new ScreenRecoder.App.HotKeySelecter();
            this.hotKey_stop = new ScreenRecoder.App.HotKeySelecter();
            this.hotKey_pause = new ScreenRecoder.App.HotKeySelecter();
            this.hotKey_start = new ScreenRecoder.App.HotKeySelecter();
            this.tab_about = new ScreenRecoder.App.Controls.TabButton();
            this.tab_soft = new ScreenRecoder.App.Controls.TabButton();
            this.tab_hotkeys = new ScreenRecoder.App.Controls.TabButton();
            this.tab_recorder = new ScreenRecoder.App.Controls.TabButton();
            this.btn_open_folder = new System.Windows.Forms.LinkLabel();
            this.pl_recset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_frame_rate)).BeginInit();
            this.pl_softset.SuspendLayout();
            this.pl_about.SuspendLayout();
            this.pl_hotkeyset.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_choosedir)).BeginInit();
            this.SuspendLayout();
            // 
            // pl_recset
            // 
            this.pl_recset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.pl_recset.Controls.Add(this.btn_open_folder);
            this.pl_recset.Controls.Add(this.combo_mic);
            this.pl_recset.Controls.Add(this.label22);
            this.pl_recset.Controls.Add(this.radio_rect_rec);
            this.pl_recset.Controls.Add(this.check_fullscreen);
            this.pl_recset.Controls.Add(this.label20);
            this.pl_recset.Controls.Add(this.combo_framerate);
            this.pl_recset.Controls.Add(this.lb_recset_notify);
            this.pl_recset.Controls.Add(this.check_recsound);
            this.pl_recset.Controls.Add(this.check_recmic);
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
            this.pl_recset.Location = new System.Drawing.Point(0, 0);
            this.pl_recset.Name = "pl_recset";
            this.pl_recset.Size = new System.Drawing.Size(510, 356);
            this.pl_recset.TabIndex = 29;
            this.pl_recset.Visible = false;
            // 
            // radio_rect_rec
            // 
            this.radio_rect_rec.AutoSize = true;
            this.radio_rect_rec.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio_rect_rec.ForeColor = System.Drawing.Color.White;
            this.radio_rect_rec.Location = new System.Drawing.Point(251, 25);
            this.radio_rect_rec.Name = "radio_rect_rec";
            this.radio_rect_rec.Size = new System.Drawing.Size(74, 21);
            this.radio_rect_rec.TabIndex = 39;
            this.radio_rect_rec.Text = "区域录制";
            this.radio_rect_rec.UseVisualStyleBackColor = true;
            // 
            // check_fullscreen
            // 
            this.check_fullscreen.AutoSize = true;
            this.check_fullscreen.Checked = true;
            this.check_fullscreen.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_fullscreen.ForeColor = System.Drawing.Color.White;
            this.check_fullscreen.Location = new System.Drawing.Point(135, 25);
            this.check_fullscreen.Name = "check_fullscreen";
            this.check_fullscreen.Size = new System.Drawing.Size(74, 21);
            this.check_fullscreen.TabIndex = 38;
            this.check_fullscreen.TabStop = true;
            this.check_fullscreen.Text = "全屏录制";
            this.check_fullscreen.UseVisualStyleBackColor = true;
            this.check_fullscreen.CheckedChanged += new System.EventHandler(this.check_fullscreen_CheckedChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(29, 29);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 17);
            this.label20.TabIndex = 36;
            this.label20.Text = "录制设置";
            // 
            // combo_framerate
            // 
            this.combo_framerate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_framerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_framerate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combo_framerate.FormattingEnabled = true;
            this.combo_framerate.Items.AddRange(new object[] {
            "流畅 (15fps)",
            "高速 (20fps)",
            "极速 (30fps)",
            "自定义"});
            this.combo_framerate.Location = new System.Drawing.Point(133, 121);
            this.combo_framerate.Name = "combo_framerate";
            this.combo_framerate.Size = new System.Drawing.Size(191, 25);
            this.combo_framerate.TabIndex = 35;
            this.combo_framerate.SelectedIndexChanged += new System.EventHandler(this.combo_framerate_SelectedIndexChanged);
            // 
            // lb_recset_notify
            // 
            this.lb_recset_notify.AutoEllipsis = true;
            this.lb_recset_notify.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_recset_notify.ForeColor = System.Drawing.Color.White;
            this.lb_recset_notify.Image = global::ScreenRecoder.App.Properties.Resources.ico_msg;
            this.lb_recset_notify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_recset_notify.Location = new System.Drawing.Point(38, 322);
            this.lb_recset_notify.Name = "lb_recset_notify";
            this.lb_recset_notify.Size = new System.Drawing.Size(254, 21);
            this.lb_recset_notify.TabIndex = 33;
            this.lb_recset_notify.Text = "您所更改的设置将在下次录像时应用";
            this.lb_recset_notify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_recset_notify.Visible = false;
            // 
            // check_recsound
            // 
            this.check_recsound.AutoSize = true;
            this.check_recsound.Checked = true;
            this.check_recsound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_recsound.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_recsound.ForeColor = System.Drawing.Color.White;
            this.check_recsound.Location = new System.Drawing.Point(252, 52);
            this.check_recsound.Name = "check_recsound";
            this.check_recsound.Size = new System.Drawing.Size(75, 21);
            this.check_recsound.TabIndex = 25;
            this.check_recsound.Text = "录制声音";
            this.check_recsound.UseVisualStyleBackColor = true;
            this.check_recsound.CheckedChanged += new System.EventHandler(this.check_recsound_CheckedChanged);
            // 
            // check_recmic
            // 
            this.check_recmic.AutoSize = true;
            this.check_recmic.Checked = true;
            this.check_recmic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_recmic.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_recmic.ForeColor = System.Drawing.Color.White;
            this.check_recmic.Location = new System.Drawing.Point(135, 52);
            this.check_recmic.Name = "check_recmic";
            this.check_recmic.Size = new System.Drawing.Size(87, 21);
            this.check_recmic.TabIndex = 24;
            this.check_recmic.Text = "录制麦克风";
            this.check_recmic.UseVisualStyleBackColor = true;
            this.check_recmic.CheckedChanged += new System.EventHandler(this.check_recmic_CheckedChanged);
            // 
            // numeric_frame_rate
            // 
            this.numeric_frame_rate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numeric_frame_rate.Location = new System.Drawing.Point(338, 121);
            this.numeric_frame_rate.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numeric_frame_rate.Name = "numeric_frame_rate";
            this.numeric_frame_rate.Size = new System.Drawing.Size(49, 23);
            this.numeric_frame_rate.TabIndex = 12;
            this.numeric_frame_rate.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numeric_frame_rate.Visible = false;
            this.numeric_frame_rate.ValueChanged += new System.EventHandler(this.numeric_frame_rate_ValueChanged);
            // 
            // textBox_export_dir
            // 
            this.textBox_export_dir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_export_dir.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_export_dir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox_export_dir.Location = new System.Drawing.Point(135, 272);
            this.textBox_export_dir.Name = "textBox_export_dir";
            this.textBox_export_dir.Size = new System.Drawing.Size(313, 23);
            this.textBox_export_dir.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(30, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "录制帧率";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(30, 274);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "输出目录";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Image = global::ScreenRecoder.App.Properties.Resources.ico_msg;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(100, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(360, 21);
            this.label4.TabIndex = 14;
            this.label4.Text = "帧率越高画面约流畅，但会加重电脑负担，所以不宜设置太高";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combo_quality
            // 
            this.combo_quality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_quality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_quality.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combo_quality.FormattingEnabled = true;
            this.combo_quality.Items.AddRange(new object[] {
            "默认",
            "普通",
            "高",
            "最高"});
            this.combo_quality.Location = new System.Drawing.Point(135, 229);
            this.combo_quality.Name = "combo_quality";
            this.combo_quality.Size = new System.Drawing.Size(311, 25);
            this.combo_quality.TabIndex = 18;
            this.combo_quality.SelectedIndexChanged += new System.EventHandler(this.combo_quality_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(29, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "录制格式";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(29, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "视频质量";
            // 
            // combo_format
            // 
            this.combo_format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_format.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_format.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combo_format.FormattingEnabled = true;
            this.combo_format.Items.AddRange(new object[] {
            "默认",
            "MP4 (H264) 默认",
            "MP4 (MPEG4)",
            "AVI (H264)",
            "AVI (MPEG4)",
            "FLV",
            "WMV"});
            this.combo_format.Location = new System.Drawing.Point(135, 185);
            this.combo_format.Name = "combo_format";
            this.combo_format.Size = new System.Drawing.Size(189, 25);
            this.combo_format.TabIndex = 16;
            this.combo_format.SelectedIndexChanged += new System.EventHandler(this.combo_format_SelectedIndexChanged);
            // 
            // pl_softset
            // 
            this.pl_softset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.pl_softset.Controls.Add(this.check_top);
            this.pl_softset.Controls.Add(this.check_show_preview);
            this.pl_softset.Controls.Add(this.check_use_sound_tip);
            this.pl_softset.Controls.Add(this.check_hide_whenrec);
            this.pl_softset.Controls.Add(this.check_usemini_inrec);
            this.pl_softset.Controls.Add(this.check_rem_pos);
            this.pl_softset.Controls.Add(this.check_exit_min);
            this.pl_softset.Location = new System.Drawing.Point(0, 0);
            this.pl_softset.Name = "pl_softset";
            this.pl_softset.Size = new System.Drawing.Size(507, 356);
            this.pl_softset.TabIndex = 34;
            // 
            // check_top
            // 
            this.check_top.AutoSize = true;
            this.check_top.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_top.ForeColor = System.Drawing.Color.White;
            this.check_top.Location = new System.Drawing.Point(56, 245);
            this.check_top.Name = "check_top";
            this.check_top.Size = new System.Drawing.Size(99, 21);
            this.check_top.TabIndex = 14;
            this.check_top.Text = "录制窗口置顶";
            this.check_top.UseVisualStyleBackColor = true;
            this.check_top.CheckedChanged += new System.EventHandler(this.check_top_CheckedChanged);
            // 
            // check_show_preview
            // 
            this.check_show_preview.AutoSize = true;
            this.check_show_preview.Checked = true;
            this.check_show_preview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_show_preview.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_show_preview.ForeColor = System.Drawing.Color.White;
            this.check_show_preview.Location = new System.Drawing.Point(56, 213);
            this.check_show_preview.Name = "check_show_preview";
            this.check_show_preview.Size = new System.Drawing.Size(111, 21);
            this.check_show_preview.TabIndex = 13;
            this.check_show_preview.Text = "使用小窗口预览";
            this.check_show_preview.UseVisualStyleBackColor = true;
            this.check_show_preview.CheckedChanged += new System.EventHandler(this.check_show_preview_CheckedChanged);
            // 
            // check_use_sound_tip
            // 
            this.check_use_sound_tip.AutoSize = true;
            this.check_use_sound_tip.Checked = true;
            this.check_use_sound_tip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_use_sound_tip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_use_sound_tip.ForeColor = System.Drawing.Color.White;
            this.check_use_sound_tip.Location = new System.Drawing.Point(56, 176);
            this.check_use_sound_tip.Name = "check_use_sound_tip";
            this.check_use_sound_tip.Size = new System.Drawing.Size(183, 21);
            this.check_use_sound_tip.TabIndex = 12;
            this.check_use_sound_tip.Text = "录制开始停止时播放声音提示";
            this.check_use_sound_tip.UseVisualStyleBackColor = true;
            this.check_use_sound_tip.CheckedChanged += new System.EventHandler(this.check_use_sound_tip_CheckedChanged);
            // 
            // check_hide_whenrec
            // 
            this.check_hide_whenrec.AutoSize = true;
            this.check_hide_whenrec.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_hide_whenrec.ForeColor = System.Drawing.Color.White;
            this.check_hide_whenrec.Location = new System.Drawing.Point(56, 142);
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
            this.check_usemini_inrec.Location = new System.Drawing.Point(56, 108);
            this.check_usemini_inrec.Name = "check_usemini_inrec";
            this.check_usemini_inrec.Size = new System.Drawing.Size(153, 21);
            this.check_usemini_inrec.TabIndex = 10;
            this.check_usemini_inrec.Text = "录制时使用MINI小窗口";
            this.check_usemini_inrec.UseVisualStyleBackColor = true;
            this.check_usemini_inrec.CheckedChanged += new System.EventHandler(this.check_usemini_inrec_CheckedChanged);
            // 
            // check_rem_pos
            // 
            this.check_rem_pos.AutoSize = true;
            this.check_rem_pos.Checked = true;
            this.check_rem_pos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_rem_pos.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_rem_pos.ForeColor = System.Drawing.Color.White;
            this.check_rem_pos.Location = new System.Drawing.Point(56, 74);
            this.check_rem_pos.Name = "check_rem_pos";
            this.check_rem_pos.Size = new System.Drawing.Size(111, 21);
            this.check_rem_pos.TabIndex = 9;
            this.check_rem_pos.Text = "记住本窗口位置";
            this.check_rem_pos.UseVisualStyleBackColor = true;
            this.check_rem_pos.CheckedChanged += new System.EventHandler(this.check_rem_pos_CheckedChanged);
            // 
            // check_exit_min
            // 
            this.check_exit_min.AutoSize = true;
            this.check_exit_min.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_exit_min.ForeColor = System.Drawing.Color.White;
            this.check_exit_min.Location = new System.Drawing.Point(56, 39);
            this.check_exit_min.Name = "check_exit_min";
            this.check_exit_min.Size = new System.Drawing.Size(236, 21);
            this.check_exit_min.TabIndex = 7;
            this.check_exit_min.Text = "点击 × 时最小化到托盘而不是退出程序";
            this.check_exit_min.UseVisualStyleBackColor = true;
            this.check_exit_min.CheckedChanged += new System.EventHandler(this.check_exit_min_CheckedChanged);
            // 
            // pl_about
            // 
            this.pl_about.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.pl_about.Controls.Add(this.link_github);
            this.pl_about.Controls.Add(this.label19);
            this.pl_about.Controls.Add(this.label18);
            this.pl_about.Controls.Add(this.label17);
            this.pl_about.Controls.Add(this.label16);
            this.pl_about.Controls.Add(this.label15);
            this.pl_about.Controls.Add(this.label10);
            this.pl_about.Controls.Add(this.label11);
            this.pl_about.Controls.Add(this.label12);
            this.pl_about.Controls.Add(this.label13);
            this.pl_about.Controls.Add(this.label14);
            this.pl_about.Location = new System.Drawing.Point(0, 0);
            this.pl_about.Name = "pl_about";
            this.pl_about.Size = new System.Drawing.Size(510, 358);
            this.pl_about.TabIndex = 35;
            this.pl_about.Visible = false;
            // 
            // link_github
            // 
            this.link_github.AutoSize = true;
            this.link_github.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.link_github.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.link_github.LinkColor = System.Drawing.Color.DeepSkyBlue;
            this.link_github.Location = new System.Drawing.Point(164, 288);
            this.link_github.Name = "link_github";
            this.link_github.Size = new System.Drawing.Size(255, 17);
            this.link_github.TabIndex = 38;
            this.link_github.TabStop = true;
            this.link_github.Text = "https://github.com/717021/ScreenRecoder";
            this.link_github.Click += new System.EventHandler(this.link_github_Click);
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(103, 288);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(73, 17);
            this.label19.TabIndex = 37;
            this.label19.Text = "Github ";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(245, 224);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(69, 17);
            this.label18.TabIndex = 36;
            this.label18.Text = "DreamFish";
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(112, 307);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(281, 34);
            this.label17.TabIndex = 35;
            this.label17.Text = "© 2019 MagicalSoft™\r\n本程序遵守 MIT 开源协议";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(245, 201);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 17);
            this.label16.TabIndex = 34;
            this.label16.Text = "2019/8/31";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(245, 180);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 17);
            this.label15.TabIndex = 33;
            this.label15.Text = "3.2.0.860";
            // 
            // label10
            // 
            this.label10.AutoEllipsis = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(-2, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(512, 21);
            this.label10.TabIndex = 32;
            this.label10.Text = "一个非常简单的屏幕录像软件";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(149, 224);
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
            this.label12.Location = new System.Drawing.Point(149, 201);
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
            this.label13.Location = new System.Drawing.Point(149, 180);
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
            this.label14.Location = new System.Drawing.Point(0, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(503, 64);
            this.label14.TabIndex = 21;
            this.label14.Text = "关于 Screen Recoder";
            this.label14.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // pl_hotkeyset
            // 
            this.pl_hotkeyset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.pl_hotkeyset.Controls.Add(this.hotKey_screenshutcut);
            this.pl_hotkeyset.Controls.Add(this.label21);
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
            this.pl_hotkeyset.Location = new System.Drawing.Point(0, 0);
            this.pl_hotkeyset.Name = "pl_hotkeyset";
            this.pl_hotkeyset.Size = new System.Drawing.Size(510, 356);
            this.pl_hotkeyset.TabIndex = 36;
            this.pl_hotkeyset.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(44, 198);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(32, 17);
            this.label21.TabIndex = 34;
            this.label21.Text = "截图";
            // 
            // link_reboot
            // 
            this.link_reboot.AutoSize = true;
            this.link_reboot.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.link_reboot.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.link_reboot.LinkColor = System.Drawing.Color.DeepSkyBlue;
            this.link_reboot.Location = new System.Drawing.Point(257, 322);
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
            this.lb_keyset_notify.Location = new System.Drawing.Point(13, 320);
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
            this.label9.Location = new System.Drawing.Point(44, 158);
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
            this.label8.Location = new System.Drawing.Point(43, 115);
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
            this.label2.Location = new System.Drawing.Point(44, 73);
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
            this.label1.Location = new System.Drawing.Point(44, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "开始录制";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Controls.Add(this.btn_reset_default);
            this.panel1.Controls.Add(this.pl_recset);
            this.panel1.Controls.Add(this.pl_hotkeyset);
            this.panel1.Controls.Add(this.pl_about);
            this.panel1.Controls.Add(this.pl_softset);
            this.panel1.Location = new System.Drawing.Point(119, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 406);
            this.panel1.TabIndex = 43;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(30, 84);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(44, 17);
            this.label22.TabIndex = 40;
            this.label22.Text = "麦克风";
            // 
            // combo_mic
            // 
            this.combo_mic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_mic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_mic.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combo_mic.FormattingEnabled = true;
            this.combo_mic.Items.AddRange(new object[] {
            "默认"});
            this.combo_mic.Location = new System.Drawing.Point(133, 81);
            this.combo_mic.Name = "combo_mic";
            this.combo_mic.Size = new System.Drawing.Size(313, 25);
            this.combo_mic.TabIndex = 41;
            this.combo_mic.SelectedIndexChanged += new System.EventHandler(this.combo_mic_SelectedIndexChanged);
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.btn_exit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btn_exit.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_exit.Image = global::ScreenRecoder.App.Properties.Resources.ico_exit;
            this.btn_exit.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_exit.Location = new System.Drawing.Point(65, 366);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btn_exit.Size = new System.Drawing.Size(24, 24);
            this.btn_exit.TabIndex = 43;
            this.toolTip1.SetToolTip(this.btn_exit, "退出程序");
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btn_ok.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btn_ok.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ok.ForeColor = System.Drawing.Color.White;
            this.btn_ok.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.btn_ok.Image = null;
            this.btn_ok.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_ok.Location = new System.Drawing.Point(409, 366);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btn_ok.Size = new System.Drawing.Size(86, 26);
            this.btn_ok.TabIndex = 41;
            this.btn_ok.Text = "确定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_reset_default
            // 
            this.btn_reset_default.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btn_reset_default.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btn_reset_default.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_reset_default.ForeColor = System.Drawing.Color.White;
            this.btn_reset_default.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.btn_reset_default.Image = null;
            this.btn_reset_default.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_reset_default.Location = new System.Drawing.Point(16, 366);
            this.btn_reset_default.Name = "btn_reset_default";
            this.btn_reset_default.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btn_reset_default.Size = new System.Drawing.Size(112, 26);
            this.btn_reset_default.TabIndex = 42;
            this.btn_reset_default.Text = "恢复默认设置";
            this.btn_reset_default.Click += new System.EventHandler(this.btn_defsettings_Click);
            // 
            // btn_choosedir
            // 
            this.btn_choosedir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_choosedir.BackColor = System.Drawing.Color.Transparent;
            this.btn_choosedir.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_choosedir.DownImage = global::ScreenRecoder.App.Properties.Resources.btn_folder_p;
            this.btn_choosedir.HoverImage = global::ScreenRecoder.App.Properties.Resources.btn_folder_h;
            this.btn_choosedir.Location = new System.Drawing.Point(106, 275);
            this.btn_choosedir.Name = "btn_choosedir";
            this.btn_choosedir.NormalImage = global::ScreenRecoder.App.Properties.Resources.btn_folder_n;
            this.btn_choosedir.Size = new System.Drawing.Size(16, 16);
            this.btn_choosedir.TabIndex = 21;
            this.btn_choosedir.TabStop = false;
            this.toolTip1.SetToolTip(this.btn_choosedir, "选择输出目录");
            this.btn_choosedir.ToolTipText = null;
            this.btn_choosedir.Click += new System.EventHandler(this.btn_choosedir_Click);
            // 
            // hotKey_screenshutcut
            // 
            this.hotKey_screenshutcut.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotKey_screenshutcut.Location = new System.Drawing.Point(208, 198);
            this.hotKey_screenshutcut.Name = "hotKey_screenshutcut";
            this.hotKey_screenshutcut.Size = new System.Drawing.Size(124, 23);
            this.hotKey_screenshutcut.TabIndex = 35;
            this.hotKey_screenshutcut.KeysChanged += new System.EventHandler(this.hotKey_screenshutcut_KeysChanged);
            // 
            // hotKey_showhide
            // 
            this.hotKey_showhide.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotKey_showhide.Location = new System.Drawing.Point(208, 158);
            this.hotKey_showhide.Name = "hotKey_showhide";
            this.hotKey_showhide.Size = new System.Drawing.Size(124, 23);
            this.hotKey_showhide.TabIndex = 31;
            this.hotKey_showhide.KeysChanged += new System.EventHandler(this.hotKey_showhide_KeysChanged);
            // 
            // hotKey_stop
            // 
            this.hotKey_stop.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotKey_stop.Location = new System.Drawing.Point(208, 115);
            this.hotKey_stop.Name = "hotKey_stop";
            this.hotKey_stop.Size = new System.Drawing.Size(124, 23);
            this.hotKey_stop.TabIndex = 30;
            this.hotKey_stop.KeysChanged += new System.EventHandler(this.hotKey_stop_KeysChanged);
            // 
            // hotKey_pause
            // 
            this.hotKey_pause.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotKey_pause.Location = new System.Drawing.Point(208, 73);
            this.hotKey_pause.Name = "hotKey_pause";
            this.hotKey_pause.Size = new System.Drawing.Size(124, 23);
            this.hotKey_pause.TabIndex = 29;
            this.hotKey_pause.KeysChanged += new System.EventHandler(this.hotKey_pause_KeysChanged);
            // 
            // hotKey_start
            // 
            this.hotKey_start.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotKey_start.Location = new System.Drawing.Point(208, 32);
            this.hotKey_start.Name = "hotKey_start";
            this.hotKey_start.Size = new System.Drawing.Size(124, 23);
            this.hotKey_start.TabIndex = 28;
            this.hotKey_start.KeysChanged += new System.EventHandler(this.hotKey_start_KeysChanged);
            // 
            // tab_about
            // 
            this.tab_about.Active = false;
            this.tab_about.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tab_about.ForeColor = System.Drawing.Color.White;
            this.tab_about.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.tab_about.Image = null;
            this.tab_about.ImageSize = new System.Drawing.Size(20, 20);
            this.tab_about.Location = new System.Drawing.Point(0, 108);
            this.tab_about.Name = "tab_about";
            this.tab_about.Size = new System.Drawing.Size(120, 30);
            this.tab_about.TabIndex = 40;
            this.tab_about.Text = "关于软件";
            // 
            // tab_soft
            // 
            this.tab_soft.Active = false;
            this.tab_soft.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tab_soft.ForeColor = System.Drawing.Color.White;
            this.tab_soft.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.tab_soft.Image = null;
            this.tab_soft.ImageSize = new System.Drawing.Size(20, 20);
            this.tab_soft.Location = new System.Drawing.Point(0, 75);
            this.tab_soft.Name = "tab_soft";
            this.tab_soft.Size = new System.Drawing.Size(120, 30);
            this.tab_soft.TabIndex = 39;
            this.tab_soft.Text = "软件设置";
            // 
            // tab_hotkeys
            // 
            this.tab_hotkeys.Active = false;
            this.tab_hotkeys.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tab_hotkeys.ForeColor = System.Drawing.Color.White;
            this.tab_hotkeys.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.tab_hotkeys.Image = null;
            this.tab_hotkeys.ImageSize = new System.Drawing.Size(20, 20);
            this.tab_hotkeys.Location = new System.Drawing.Point(0, 43);
            this.tab_hotkeys.Name = "tab_hotkeys";
            this.tab_hotkeys.Size = new System.Drawing.Size(120, 30);
            this.tab_hotkeys.TabIndex = 38;
            this.tab_hotkeys.Text = "热键设置";
            // 
            // tab_recorder
            // 
            this.tab_recorder.Active = false;
            this.tab_recorder.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tab_recorder.ForeColor = System.Drawing.Color.White;
            this.tab_recorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.tab_recorder.Image = null;
            this.tab_recorder.ImageSize = new System.Drawing.Size(20, 20);
            this.tab_recorder.Location = new System.Drawing.Point(0, 12);
            this.tab_recorder.Name = "tab_recorder";
            this.tab_recorder.Size = new System.Drawing.Size(120, 30);
            this.tab_recorder.TabIndex = 37;
            this.tab_recorder.Text = "录制设置";
            // 
            // btn_open_folder
            // 
            this.btn_open_folder.AutoSize = true;
            this.btn_open_folder.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_open_folder.ForeColor = System.Drawing.Color.White;
            this.btn_open_folder.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.btn_open_folder.LinkColor = System.Drawing.Color.White;
            this.btn_open_folder.Location = new System.Drawing.Point(368, 298);
            this.btn_open_folder.Name = "btn_open_folder";
            this.btn_open_folder.Size = new System.Drawing.Size(80, 17);
            this.btn_open_folder.TabIndex = 42;
            this.btn_open_folder.TabStop = true;
            this.btn_open_folder.Text = "打开输出目录";
            this.btn_open_folder.Click += new System.EventHandler(this.btn_open_folder_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(626, 410);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tab_about);
            this.Controls.Add(this.tab_soft);
            this.Controls.Add(this.tab_hotkeys);
            this.Controls.Add(this.tab_recorder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "屏幕录制设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.pl_recset.ResumeLayout(false);
            this.pl_recset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_frame_rate)).EndInit();
            this.pl_softset.ResumeLayout(false);
            this.pl_softset.PerformLayout();
            this.pl_about.ResumeLayout(false);
            this.pl_about.PerformLayout();
            this.pl_hotkeyset.ResumeLayout(false);
            this.pl_hotkeyset.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_choosedir)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_recset;
        private System.Windows.Forms.Label lb_recset_notify;
        private bells.app.ImageButton btn_choosedir;
        private System.Windows.Forms.TextBox textBox_export_dir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox combo_quality;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox combo_format;
        private System.Windows.Forms.Panel pl_softset;
        private System.Windows.Forms.CheckBox check_hide_whenrec;
        private System.Windows.Forms.CheckBox check_usemini_inrec;
        private System.Windows.Forms.CheckBox check_rem_pos;
        private System.Windows.Forms.CheckBox check_exit_min;
        private System.Windows.Forms.Panel pl_about;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pl_hotkeyset;
        private System.Windows.Forms.LinkLabel link_reboot;
        private System.Windows.Forms.Label lb_keyset_notify;
        private HotKeySelecter hotKey_showhide;
        private HotKeySelecter hotKey_stop;
        private HotKeySelecter hotKey_pause;
        private HotKeySelecter hotKey_start;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel link_github;
        private System.Windows.Forms.Label label19;
        private Controls.TabButton tab_recorder;
        private Controls.TabButton tab_hotkeys;
        private Controls.TabButton tab_soft;
        private Controls.TabButton tab_about;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox combo_framerate;
        private System.Windows.Forms.NumericUpDown numeric_frame_rate;
        private Controls.FlatButton btn_ok;
        private Controls.FlatButton btn_reset_default;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox check_use_sound_tip;
        public System.Windows.Forms.RadioButton radio_rect_rec;
        public System.Windows.Forms.RadioButton check_fullscreen;
        public System.Windows.Forms.CheckBox check_recsound;
        public System.Windows.Forms.CheckBox check_recmic;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox check_show_preview;
        private HotKeySelecter hotKey_screenshutcut;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.CheckBox check_top;
        private Controls.FlatButton btn_exit;
        private System.Windows.Forms.ComboBox combo_mic;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.LinkLabel btn_open_folder;
    }
}