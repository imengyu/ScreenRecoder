namespace ScreenRecoder.App
{
    partial class FormEditScreenShutTools
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
            this.pl_base_tools = new System.Windows.Forms.Panel();
            this.btn_text = new ScreenRecoder.App.Controls.TabButton();
            this.btn_save = new ScreenRecoder.App.Controls.TabButton();
            this.btn_ok = new ScreenRecoder.App.Controls.TabButton();
            this.btn_cancel = new ScreenRecoder.App.Controls.TabButton();
            this.btn_revoke = new ScreenRecoder.App.Controls.TabButton();
            this.btn_mosaic = new ScreenRecoder.App.Controls.TabButton();
            this.btn_arrwo = new ScreenRecoder.App.Controls.TabButton();
            this.btn_pen = new ScreenRecoder.App.Controls.TabButton();
            this.btn_ellipse = new ScreenRecoder.App.Controls.TabButton();
            this.btn_rect = new ScreenRecoder.App.Controls.TabButton();
            this.pl_pen_tool = new System.Windows.Forms.Panel();
            this.lb_mosaic_level = new System.Windows.Forms.Label();
            this.track_mosaic_level = new System.Windows.Forms.TrackBar();
            this.colorToolbar1 = new ScreenRecoder.App.Controls.ColorToolbar();
            this.btn_draw_width_bold = new ScreenRecoder.App.Controls.TabButton();
            this.btn_draw_width_normal = new ScreenRecoder.App.Controls.TabButton();
            this.btn_draw_width_thing = new ScreenRecoder.App.Controls.TabButton();
            this.pl_font_tool = new System.Windows.Forms.Panel();
            this.combo_font = new System.Windows.Forms.ComboBox();
            this.combo_font_size = new System.Windows.Forms.ComboBox();
            this.colorToolbar2 = new ScreenRecoder.App.Controls.ColorToolbar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pl_base_tools.SuspendLayout();
            this.pl_pen_tool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_mosaic_level)).BeginInit();
            this.pl_font_tool.SuspendLayout();
            this.SuspendLayout();
            // 
            // pl_base_tools
            // 
            this.pl_base_tools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.pl_base_tools.Controls.Add(this.btn_text);
            this.pl_base_tools.Controls.Add(this.btn_save);
            this.pl_base_tools.Controls.Add(this.btn_ok);
            this.pl_base_tools.Controls.Add(this.btn_cancel);
            this.pl_base_tools.Controls.Add(this.btn_revoke);
            this.pl_base_tools.Controls.Add(this.btn_mosaic);
            this.pl_base_tools.Controls.Add(this.btn_arrwo);
            this.pl_base_tools.Controls.Add(this.btn_pen);
            this.pl_base_tools.Controls.Add(this.btn_ellipse);
            this.pl_base_tools.Controls.Add(this.btn_rect);
            this.pl_base_tools.Location = new System.Drawing.Point(151, 12);
            this.pl_base_tools.Name = "pl_base_tools";
            this.pl_base_tools.Size = new System.Drawing.Size(373, 38);
            this.pl_base_tools.TabIndex = 0;
            this.pl_base_tools.Paint += new System.Windows.Forms.PaintEventHandler(this.pl_base_tools_Paint);
            // 
            // btn_text
            // 
            this.btn_text.Active = false;
            this.btn_text.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_text.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_text;
            this.btn_text.ImageSize = new System.Drawing.Size(22, 22);
            this.btn_text.Location = new System.Drawing.Point(183, 1);
            this.btn_text.Name = "btn_text";
            this.btn_text.Size = new System.Drawing.Size(36, 36);
            this.btn_text.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btn_text, "绘制马赛克");
            this.btn_text.Click += new System.EventHandler(this.btn_text_Click);
            // 
            // btn_save
            // 
            this.btn_save.Active = false;
            this.btn_save.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_save.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_save;
            this.btn_save.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_save.Location = new System.Drawing.Point(261, 1);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(36, 36);
            this.btn_save.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btn_save, "保存截图至文件");
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Active = false;
            this.btn_ok.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_ok.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_ok;
            this.btn_ok.ImageSize = new System.Drawing.Size(26, 20);
            this.btn_ok.Location = new System.Drawing.Point(333, 1);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(36, 36);
            this.btn_ok.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btn_ok, "完成截图并复制到剪贴板");
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Active = false;
            this.btn_cancel.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_cancel.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_cancel;
            this.btn_cancel.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_cancel.Location = new System.Drawing.Point(297, 1);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(36, 36);
            this.btn_cancel.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btn_cancel, "取消截图");
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_revoke
            // 
            this.btn_revoke.Active = false;
            this.btn_revoke.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_revoke.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_redo;
            this.btn_revoke.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_revoke.Location = new System.Drawing.Point(225, 1);
            this.btn_revoke.Name = "btn_revoke";
            this.btn_revoke.Size = new System.Drawing.Size(36, 36);
            this.btn_revoke.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btn_revoke, "撤销绘画");
            this.btn_revoke.Click += new System.EventHandler(this.btn_revoke_Click);
            // 
            // btn_mosaic
            // 
            this.btn_mosaic.Active = false;
            this.btn_mosaic.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_mosaic.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_mosaic;
            this.btn_mosaic.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_mosaic.Location = new System.Drawing.Point(146, 1);
            this.btn_mosaic.Name = "btn_mosaic";
            this.btn_mosaic.Size = new System.Drawing.Size(36, 36);
            this.btn_mosaic.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btn_mosaic, "绘制马赛克");
            this.btn_mosaic.Click += new System.EventHandler(this.btn_mosaic_Click);
            // 
            // btn_arrwo
            // 
            this.btn_arrwo.Active = false;
            this.btn_arrwo.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_arrwo.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_arrow;
            this.btn_arrwo.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_arrwo.Location = new System.Drawing.Point(110, 1);
            this.btn_arrwo.Name = "btn_arrwo";
            this.btn_arrwo.Size = new System.Drawing.Size(36, 36);
            this.btn_arrwo.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btn_arrwo, "绘制箭头");
            this.btn_arrwo.Click += new System.EventHandler(this.btn_arrwo_Click);
            // 
            // btn_pen
            // 
            this.btn_pen.Active = false;
            this.btn_pen.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_pen.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_pen;
            this.btn_pen.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_pen.Location = new System.Drawing.Point(74, 1);
            this.btn_pen.Name = "btn_pen";
            this.btn_pen.Size = new System.Drawing.Size(36, 36);
            this.btn_pen.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btn_pen, "画笔");
            this.btn_pen.Click += new System.EventHandler(this.btn_pen_Click);
            // 
            // btn_ellipse
            // 
            this.btn_ellipse.Active = false;
            this.btn_ellipse.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_ellipse.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_ellipse;
            this.btn_ellipse.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_ellipse.Location = new System.Drawing.Point(37, 1);
            this.btn_ellipse.Name = "btn_ellipse";
            this.btn_ellipse.Size = new System.Drawing.Size(36, 36);
            this.btn_ellipse.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btn_ellipse, "绘制椭圆");
            this.btn_ellipse.Click += new System.EventHandler(this.btn_ellipse_Click);
            // 
            // btn_rect
            // 
            this.btn_rect.Active = false;
            this.btn_rect.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_rect.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_box;
            this.btn_rect.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_rect.Location = new System.Drawing.Point(1, 1);
            this.btn_rect.Name = "btn_rect";
            this.btn_rect.Size = new System.Drawing.Size(36, 36);
            this.btn_rect.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btn_rect, "绘制方框");
            this.btn_rect.Click += new System.EventHandler(this.btn_rect_Click);
            // 
            // pl_pen_tool
            // 
            this.pl_pen_tool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.pl_pen_tool.Controls.Add(this.lb_mosaic_level);
            this.pl_pen_tool.Controls.Add(this.track_mosaic_level);
            this.pl_pen_tool.Controls.Add(this.colorToolbar1);
            this.pl_pen_tool.Controls.Add(this.btn_draw_width_bold);
            this.pl_pen_tool.Controls.Add(this.btn_draw_width_normal);
            this.pl_pen_tool.Controls.Add(this.btn_draw_width_thing);
            this.pl_pen_tool.Location = new System.Drawing.Point(12, 57);
            this.pl_pen_tool.Name = "pl_pen_tool";
            this.pl_pen_tool.Size = new System.Drawing.Size(512, 38);
            this.pl_pen_tool.TabIndex = 1;
            this.pl_pen_tool.Visible = false;
            this.pl_pen_tool.Paint += new System.Windows.Forms.PaintEventHandler(this.pl_base_tools_Paint);
            // 
            // lb_mosaic_level
            // 
            this.lb_mosaic_level.AutoSize = true;
            this.lb_mosaic_level.Location = new System.Drawing.Point(163, 10);
            this.lb_mosaic_level.Name = "lb_mosaic_level";
            this.lb_mosaic_level.Size = new System.Drawing.Size(56, 17);
            this.lb_mosaic_level.TabIndex = 14;
            this.lb_mosaic_level.Text = "模糊强度";
            // 
            // track_mosaic_level
            // 
            this.track_mosaic_level.AutoSize = false;
            this.track_mosaic_level.Location = new System.Drawing.Point(234, 6);
            this.track_mosaic_level.Name = "track_mosaic_level";
            this.track_mosaic_level.Size = new System.Drawing.Size(138, 25);
            this.track_mosaic_level.TabIndex = 13;
            this.track_mosaic_level.TickFrequency = 0;
            this.track_mosaic_level.TickStyle = System.Windows.Forms.TickStyle.None;
            this.track_mosaic_level.Scroll += new System.EventHandler(this.track_mosaic_level_Scroll);
            this.track_mosaic_level.ValueChanged += new System.EventHandler(this.track_mosaic_level_ValueChanged);
            // 
            // colorToolbar1
            // 
            this.colorToolbar1.ChoosedColor = System.Drawing.Color.Red;
            this.colorToolbar1.ColorBlockSize = new System.Drawing.Size(30, 30);
            this.colorToolbar1.Location = new System.Drawing.Point(139, 1);
            this.colorToolbar1.Name = "colorToolbar1";
            this.colorToolbar1.Size = new System.Drawing.Size(373, 36);
            this.colorToolbar1.TabIndex = 12;
            this.colorToolbar1.Text = "colorToolbar1";
            this.colorToolbar1.ChoosedColorChanged += new System.EventHandler(this.colorToolbar1_ChoosedColorChanged);
            // 
            // btn_draw_width_bold
            // 
            this.btn_draw_width_bold.Active = false;
            this.btn_draw_width_bold.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_draw_width_bold.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_width_bold;
            this.btn_draw_width_bold.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_draw_width_bold.Location = new System.Drawing.Point(78, 1);
            this.btn_draw_width_bold.Name = "btn_draw_width_bold";
            this.btn_draw_width_bold.Size = new System.Drawing.Size(36, 36);
            this.btn_draw_width_bold.TabIndex = 11;
            this.toolTip1.SetToolTip(this.btn_draw_width_bold, "绘制方框");
            this.btn_draw_width_bold.Click += new System.EventHandler(this.btn_draw_width_bold_Click);
            // 
            // btn_draw_width_normal
            // 
            this.btn_draw_width_normal.Active = false;
            this.btn_draw_width_normal.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_draw_width_normal.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_width_normal;
            this.btn_draw_width_normal.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_draw_width_normal.Location = new System.Drawing.Point(42, 1);
            this.btn_draw_width_normal.Name = "btn_draw_width_normal";
            this.btn_draw_width_normal.Size = new System.Drawing.Size(36, 36);
            this.btn_draw_width_normal.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btn_draw_width_normal, "绘制方框");
            this.btn_draw_width_normal.Click += new System.EventHandler(this.btn_draw_width_normal_Click);
            // 
            // btn_draw_width_thing
            // 
            this.btn_draw_width_thing.Active = false;
            this.btn_draw_width_thing.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btn_draw_width_thing.Image = global::ScreenRecoder.App.Properties.Resources.ico_tools_width_thing;
            this.btn_draw_width_thing.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_draw_width_thing.Location = new System.Drawing.Point(6, 1);
            this.btn_draw_width_thing.Name = "btn_draw_width_thing";
            this.btn_draw_width_thing.Size = new System.Drawing.Size(36, 36);
            this.btn_draw_width_thing.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btn_draw_width_thing, "绘制方框");
            this.btn_draw_width_thing.Click += new System.EventHandler(this.btn_draw_width_thing_Click);
            // 
            // pl_font_tool
            // 
            this.pl_font_tool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.pl_font_tool.Controls.Add(this.combo_font);
            this.pl_font_tool.Controls.Add(this.combo_font_size);
            this.pl_font_tool.Controls.Add(this.colorToolbar2);
            this.pl_font_tool.Location = new System.Drawing.Point(12, 57);
            this.pl_font_tool.Name = "pl_font_tool";
            this.pl_font_tool.Size = new System.Drawing.Size(512, 38);
            this.pl_font_tool.TabIndex = 2;
            this.pl_font_tool.Visible = false;
            this.pl_font_tool.Paint += new System.Windows.Forms.PaintEventHandler(this.pl_base_tools_Paint);
            // 
            // combo_font
            // 
            this.combo_font.FormattingEnabled = true;
            this.combo_font.Items.AddRange(new object[] {
            "微软雅黑",
            "宋体",
            "隶书",
            "楷体",
            "黑体",
            "Arial",
            "Century Gothic",
            "Calibri",
            "Helvetica",
            "Tahoma",
            "Times New",
            "Verdana"});
            this.combo_font.Location = new System.Drawing.Point(59, 7);
            this.combo_font.Name = "combo_font";
            this.combo_font.Size = new System.Drawing.Size(74, 25);
            this.combo_font.TabIndex = 15;
            this.combo_font.SelectedIndexChanged += new System.EventHandler(this.combo_font_SelectedIndexChanged);
            // 
            // combo_font_size
            // 
            this.combo_font_size.FormattingEnabled = true;
            this.combo_font_size.Items.AddRange(new object[] {
            "9",
            "12",
            "14",
            "16",
            "18",
            "22",
            "30",
            "52",
            "72"});
            this.combo_font_size.Location = new System.Drawing.Point(6, 7);
            this.combo_font_size.Name = "combo_font_size";
            this.combo_font_size.Size = new System.Drawing.Size(47, 25);
            this.combo_font_size.TabIndex = 14;
            this.combo_font_size.SelectedIndexChanged += new System.EventHandler(this.combo_font_size_SelectedIndexChanged);
            // 
            // colorToolbar2
            // 
            this.colorToolbar2.ChoosedColor = System.Drawing.Color.Red;
            this.colorToolbar2.ColorBlockSize = new System.Drawing.Size(30, 30);
            this.colorToolbar2.Location = new System.Drawing.Point(139, 1);
            this.colorToolbar2.Name = "colorToolbar2";
            this.colorToolbar2.Size = new System.Drawing.Size(369, 36);
            this.colorToolbar2.TabIndex = 13;
            this.colorToolbar2.Text = "colorToolbar2";
            this.colorToolbar2.ChoosedColorChanged += new System.EventHandler(this.colorToolbar2_ChoosedColorChanged);
            // 
            // FormEditScreenShutTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(528, 110);
            this.Controls.Add(this.pl_base_tools);
            this.Controls.Add(this.pl_font_tool);
            this.Controls.Add(this.pl_pen_tool);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormEditScreenShutTools";
            this.ShowInTaskbar = false;
            this.Text = "FormEditScreenShutTools";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.FormEditScreenShutTools_Load);
            this.pl_base_tools.ResumeLayout(false);
            this.pl_pen_tool.ResumeLayout(false);
            this.pl_pen_tool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_mosaic_level)).EndInit();
            this.pl_font_tool.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_base_tools;
        private System.Windows.Forms.Panel pl_pen_tool;
        private System.Windows.Forms.Panel pl_font_tool;
        private Controls.TabButton btn_rect;
        private System.Windows.Forms.ToolTip toolTip1;
        private Controls.TabButton btn_arrwo;
        private Controls.TabButton btn_pen;
        private Controls.TabButton btn_ellipse;
        private Controls.TabButton btn_ok;
        private Controls.TabButton btn_cancel;
        private Controls.TabButton btn_revoke;
        private Controls.TabButton btn_mosaic;
        private Controls.TabButton btn_save;
        private Controls.TabButton btn_draw_width_bold;
        private Controls.TabButton btn_draw_width_normal;
        private Controls.TabButton btn_draw_width_thing;
        private Controls.ColorToolbar colorToolbar1;
        private Controls.ColorToolbar colorToolbar2;
        private System.Windows.Forms.ComboBox combo_font;
        private System.Windows.Forms.ComboBox combo_font_size;
        private Controls.TabButton btn_text;
        private System.Windows.Forms.TrackBar track_mosaic_level;
        private System.Windows.Forms.Label lb_mosaic_level;
    }
}