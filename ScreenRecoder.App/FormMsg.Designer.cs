namespace ScreenRecoder.App
{
    partial class FormMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMsg));
            this.lb_title = new System.Windows.Forms.Label();
            this.lb_text = new System.Windows.Forms.Label();
            this.pl_title = new System.Windows.Forms.Panel();
            this.lb_window_title = new System.Windows.Forms.Label();
            this.btn_cancel = new ScreenRecoder.App.Controls.FlatButton();
            this.btn_ok = new ScreenRecoder.App.Controls.FlatButton();
            this.pb_ico = new System.Windows.Forms.PictureBox();
            this.pl_title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ico)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_title.Location = new System.Drawing.Point(67, 46);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(414, 32);
            this.lb_title.TabIndex = 2;
            this.lb_title.Text = "label1";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_text
            // 
            this.lb_text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_text.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lb_text.Location = new System.Drawing.Point(68, 84);
            this.lb_text.Name = "lb_text";
            this.lb_text.Size = new System.Drawing.Size(414, 64);
            this.lb_text.TabIndex = 3;
            this.lb_text.Text = "label1";
            // 
            // pl_title
            // 
            this.pl_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pl_title.BackColor = System.Drawing.Color.Black;
            this.pl_title.Controls.Add(this.lb_window_title);
            this.pl_title.Location = new System.Drawing.Point(1, 1);
            this.pl_title.Name = "pl_title";
            this.pl_title.Size = new System.Drawing.Size(492, 30);
            this.pl_title.TabIndex = 9;
            this.pl_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pl_title_MouseDown);
            // 
            // lb_window_title
            // 
            this.lb_window_title.AutoSize = true;
            this.lb_window_title.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_window_title.Location = new System.Drawing.Point(13, 6);
            this.lb_window_title.Name = "lb_window_title";
            this.lb_window_title.Size = new System.Drawing.Size(43, 17);
            this.lb_window_title.TabIndex = 0;
            this.lb_window_title.Text = "label1";
            this.lb_window_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pl_title_MouseDown);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_cancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btn_cancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_cancel.Image = null;
            this.btn_cancel.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_cancel.Location = new System.Drawing.Point(303, 163);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btn_cancel.Size = new System.Drawing.Size(86, 26);
            this.btn_cancel.TabIndex = 8;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_ok.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btn_ok.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ok.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_ok.Image = null;
            this.btn_ok.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_ok.Location = new System.Drawing.Point(395, 163);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btn_ok.Size = new System.Drawing.Size(86, 26);
            this.btn_ok.TabIndex = 7;
            this.btn_ok.Text = "确定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // pb_ico
            // 
            this.pb_ico.Image = global::ScreenRecoder.App.Properties.Resources.complete_o;
            this.pb_ico.Location = new System.Drawing.Point(17, 47);
            this.pb_ico.Name = "pb_ico";
            this.pb_ico.Size = new System.Drawing.Size(32, 32);
            this.pb_ico.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_ico.TabIndex = 10;
            this.pb_ico.TabStop = false;
            // 
            // FormMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(494, 201);
            this.Controls.Add(this.pb_ico);
            this.Controls.Add(this.pl_title);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.lb_text);
            this.Controls.Add(this.lb_title);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "屏幕录制消息";
            this.Load += new System.EventHandler(this.FormMsg_Load);
            this.TextChanged += new System.EventHandler(this.FormMsg_TextChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMsg_Paint);
            this.pl_title.ResumeLayout(false);
            this.pl_title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lb_title;
        private System.Windows.Forms.Label lb_text;
        private System.Windows.Forms.Panel pl_title;
        private System.Windows.Forms.Label lb_window_title;
        private Controls.FlatButton btn_cancel;
        private Controls.FlatButton btn_ok;
        private System.Windows.Forms.PictureBox pb_ico;
    }
}