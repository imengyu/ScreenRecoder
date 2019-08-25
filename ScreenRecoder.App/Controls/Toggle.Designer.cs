namespace ScreenRecoder.App.Controls
{
    partial class Toggle
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.imageButtonChecked = new bells.app.ImageButton();
            this.imageButtonUnChecked = new bells.app.ImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageButtonChecked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButtonUnChecked)).BeginInit();
            this.SuspendLayout();
            // 
            // imageButtonChecked
            // 
            this.imageButtonChecked.BackColor = System.Drawing.Color.Transparent;
            this.imageButtonChecked.DialogResult = System.Windows.Forms.DialogResult.None;
            this.imageButtonChecked.DownImage = global::ScreenRecoder.App.Properties.Resources.toggle_on_p;
            this.imageButtonChecked.HoverImage = global::ScreenRecoder.App.Properties.Resources.toggle_on_h;
            this.imageButtonChecked.Location = new System.Drawing.Point(0, 0);
            this.imageButtonChecked.Name = "imageButtonChecked";
            this.imageButtonChecked.NormalImage = global::ScreenRecoder.App.Properties.Resources.toggle_on_n;
            this.imageButtonChecked.Size = new System.Drawing.Size(32, 32);
            this.imageButtonChecked.TabIndex = 0;
            this.imageButtonChecked.TabStop = false;
            this.imageButtonChecked.ToolTipText = null;
            this.imageButtonChecked.Visible = false;
            this.imageButtonChecked.Click += new System.EventHandler(this.imageButtonChecked_Click);
            // 
            // imageButtonUnChecked
            // 
            this.imageButtonUnChecked.BackColor = System.Drawing.Color.Transparent;
            this.imageButtonUnChecked.DialogResult = System.Windows.Forms.DialogResult.None;
            this.imageButtonUnChecked.DownImage = global::ScreenRecoder.App.Properties.Resources.toggle_off_p;
            this.imageButtonUnChecked.HoverImage = global::ScreenRecoder.App.Properties.Resources.toggle_off_h;
            this.imageButtonUnChecked.Location = new System.Drawing.Point(0, 0);
            this.imageButtonUnChecked.Name = "imageButtonUnChecked";
            this.imageButtonUnChecked.NormalImage = global::ScreenRecoder.App.Properties.Resources.toggle_off_n;
            this.imageButtonUnChecked.Size = new System.Drawing.Size(32, 32);
            this.imageButtonUnChecked.TabIndex = 1;
            this.imageButtonUnChecked.TabStop = false;
            this.imageButtonUnChecked.ToolTipText = null;
            this.imageButtonUnChecked.Click += new System.EventHandler(this.imageButtonUnChecked_Click);
            // 
            // Toggle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageButtonUnChecked);
            this.Controls.Add(this.imageButtonChecked);
            this.Name = "Toggle";
            this.Size = new System.Drawing.Size(32, 32);
            this.Click += new System.EventHandler(this.Toggle_Click);
            ((System.ComponentModel.ISupportInitialize)(this.imageButtonChecked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButtonUnChecked)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private bells.app.ImageButton imageButtonChecked;
        private bells.app.ImageButton imageButtonUnChecked;
    }
}
