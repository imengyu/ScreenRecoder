namespace ScreenRecoder.App
{
    partial class FormRecMini
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecMini));
            this.pl_rec = new System.Windows.Forms.Panel();
            this.btn_huge = new ScreenRecoder.App.IconButton();
            this.btn_pause = new ScreenRecoder.App.IconButton();
            this.btn_stop = new ScreenRecoder.App.IconButton();
            this.lb_time = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pl_rec.SuspendLayout();
            this.SuspendLayout();
            // 
            // pl_rec
            // 
            this.pl_rec.Controls.Add(this.btn_huge);
            this.pl_rec.Controls.Add(this.btn_pause);
            this.pl_rec.Controls.Add(this.btn_stop);
            this.pl_rec.Controls.Add(this.lb_time);
            this.pl_rec.Location = new System.Drawing.Point(1, 1);
            this.pl_rec.Name = "pl_rec";
            this.pl_rec.Size = new System.Drawing.Size(177, 36);
            this.pl_rec.TabIndex = 5;
            // 
            // btn_huge
            // 
            this.btn_huge.BackColor = System.Drawing.Color.Transparent;
            this.btn_huge.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_huge.Icon = global::ScreenRecoder.App.Properties.Resources.ico_huge;
            this.btn_huge.IconSize = new System.Drawing.Size(26, 26);
            this.btn_huge.Light = false;
            this.btn_huge.Location = new System.Drawing.Point(141, 0);
            this.btn_huge.Name = "btn_huge";
            this.btn_huge.PressedColor = System.Drawing.Color.Black;
            this.btn_huge.Size = new System.Drawing.Size(36, 36);
            this.btn_huge.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btn_huge, "返回主界面");
            this.btn_huge.BtnClick += new System.EventHandler(this.btn_huge_BtnClick);
            // 
            // btn_pause
            // 
            this.btn_pause.BackColor = System.Drawing.Color.Transparent;
            this.btn_pause.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_pause.Icon = global::ScreenRecoder.App.Properties.Resources.ico_pause_small;
            this.btn_pause.IconSize = new System.Drawing.Size(16, 16);
            this.btn_pause.Light = false;
            this.btn_pause.Location = new System.Drawing.Point(36, 0);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.PressedColor = System.Drawing.Color.Black;
            this.btn_pause.Size = new System.Drawing.Size(36, 36);
            this.btn_pause.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btn_pause, "暂停/继续录像");
            this.btn_pause.BtnClick += new System.EventHandler(this.btn_pause_BtnClick);
            // 
            // btn_stop
            // 
            this.btn_stop.BackColor = System.Drawing.Color.Transparent;
            this.btn_stop.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_stop.Icon = global::ScreenRecoder.App.Properties.Resources.ico_stop_small;
            this.btn_stop.IconSize = new System.Drawing.Size(16, 16);
            this.btn_stop.Light = false;
            this.btn_stop.Location = new System.Drawing.Point(0, 0);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.PressedColor = System.Drawing.Color.Black;
            this.btn_stop.Size = new System.Drawing.Size(36, 36);
            this.btn_stop.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btn_stop, "结束录像");
            this.btn_stop.BtnClick += new System.EventHandler(this.btn_stop_BtnClick);
            // 
            // lb_time
            // 
            this.lb_time.AutoEllipsis = true;
            this.lb_time.BackColor = System.Drawing.Color.DarkRed;
            this.lb_time.Font = new System.Drawing.Font("宋体", 9F);
            this.lb_time.ForeColor = System.Drawing.Color.White;
            this.lb_time.Location = new System.Drawing.Point(72, 0);
            this.lb_time.Name = "lb_time";
            this.lb_time.Size = new System.Drawing.Size(73, 36);
            this.lb_time.TabIndex = 1;
            this.lb_time.Text = "0:00:00";
            this.lb_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_time.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lb_time_MouseDown);
            // 
            // FormRecMini
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(179, 38);
            this.Controls.Add(this.pl_rec);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRecMini";
            this.ShowInTaskbar = false;
            this.Text = "迷你录制窗口";
            this.Load += new System.EventHandler(this.FormRecMini_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormRecMini_Paint);
            this.pl_rec.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_rec;
        private IconButton btn_pause;
        private IconButton btn_stop;
        private IconButton btn_huge;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Label lb_time;
    }
}