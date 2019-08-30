namespace ScreenRecoder.App
{
    partial class FormColorPick
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
            this.lb_color = new System.Windows.Forms.Label();
            this.lb_tip = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lb_size = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_color
            // 
            this.lb_color.AutoEllipsis = true;
            this.lb_color.Location = new System.Drawing.Point(103, 47);
            this.lb_color.Name = "lb_color";
            this.lb_color.Size = new System.Drawing.Size(212, 35);
            this.lb_color.TabIndex = 0;
            this.lb_color.Text = "颜色：";
            // 
            // lb_tip
            // 
            this.lb_tip.AutoSize = true;
            this.lb_tip.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lb_tip.Location = new System.Drawing.Point(103, 9);
            this.lb_tip.Name = "lb_tip";
            this.lb_tip.Size = new System.Drawing.Size(212, 17);
            this.lb_tip.TabIndex = 1;
            this.lb_tip.Text = "拖动选择需要截取的区域，或右键退出";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lb_size
            // 
            this.lb_size.AutoEllipsis = true;
            this.lb_size.Location = new System.Drawing.Point(103, 29);
            this.lb_size.Name = "lb_size";
            this.lb_size.Size = new System.Drawing.Size(212, 18);
            this.lb_size.TabIndex = 2;
            this.lb_size.Text = "尺寸：";
            // 
            // FormColorPick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(327, 90);
            this.Controls.Add(this.lb_size);
            this.Controls.Add(this.lb_tip);
            this.Controls.Add(this.lb_color);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormColorPick";
            this.ShowInTaskbar = false;
            this.Text = "选择区域";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormChooseWindow_FormClosing);
            this.Load += new System.EventHandler(this.FormChooseWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormChooseWindow_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_color;
        private System.Windows.Forms.Label lb_tip;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_size;
    }
}