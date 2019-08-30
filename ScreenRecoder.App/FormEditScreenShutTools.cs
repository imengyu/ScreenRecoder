using ScreenRecoder.App.Api;
using ScreenRecoder.App.Controls;
using ScreenRecoder.App.PaintBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    public partial class FormEditScreenShutTools : Form
    {
        public FormEditScreenShutTools(FormScreenShutcut formScreenShutcut)
        {
            InitializeComponent();
            this.formScreenShutcut = formScreenShutcut;
            cur_pen = new Cursor(new MemoryStream(Properties.Resources.cur_pen));
            cur_brush_small = new Cursor(new MemoryStream(Properties.Resources.cur_brush_small));
            cur_brush_normal = new Cursor(new MemoryStream(Properties.Resources.cur_brush_normal));
            cur_brush_big = new Cursor(new MemoryStream(Properties.Resources.cur_brush_big));
            cur_default = Cursors.Arrow;//new Cursor(new MemoryStream(Properties.Resources.cur_default));
        }

        internal Cursor cur_current = null;
        internal Cursor cur_default = null;
        private Cursor cur_pen = null;
        private Cursor cur_brush_small = null;
        private Cursor cur_brush_normal = null;
        private Cursor cur_brush_big = null;
        private FormScreenShutcut formScreenShutcut = null;
        private Pen borderPen = new Pen(Color.FromArgb(56, 56, 56));
        private Brush bgBrush = new SolidBrush(Color.FromArgb(31, 31, 31));

        private CheckGroup checkGroupPenWidth = new CheckGroup();
        private CheckGroup checkGroupTools = new CheckGroup();

        public PaintTools CurrentTools { get; set; } = PaintTools.None;
        public Color CurrentColor { get; set; } = Color.Red;
        public int CurrentMosaicLevel { get; set; } = 3;
        public Font CurrentFont { get; set; } = new Font("微软雅黑", 9);
        public PaintPenWidth CurrentPenWidth { get; set; } = PaintPenWidth.Thing;

        private void pl_base_tools_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(bgBrush, new Rectangle(0, 0, Width, Height));
            e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, Width - 1, Height - 1));
        }

        private void FormEditScreenShutTools_Load(object sender, EventArgs e)
        {
            Height = 57;

            colorToolbar1.Colors.Add(Color.Red);
            colorToolbar1.Colors.Add(Color.FromArgb(231, 120, 16));
            colorToolbar1.Colors.Add(Color.FromArgb(248, 220, 22));
            colorToolbar1.Colors.Add(Color.FromArgb(167, 204, 65));
            colorToolbar1.Colors.Add(Color.FromArgb(76, 55, 14));
            colorToolbar1.Colors.Add(Color.FromArgb(61, 203, 255));
            colorToolbar1.Colors.Add(Color.FromArgb(15, 108, 228));
            colorToolbar1.Colors.Add(Color.FromArgb(69, 62, 181));
            colorToolbar1.Colors.Add(Color.FromArgb(216, 64, 196));
            colorToolbar1.Colors.Add(Color.Black);
            colorToolbar1.Colors.Add(Color.Silver);
            colorToolbar1.Colors.Add(Color.White);
            colorToolbar2.Colors.Add(Color.Red);
            colorToolbar2.Colors.Add(Color.FromArgb(231, 120, 16));
            colorToolbar2.Colors.Add(Color.FromArgb(248, 220, 22));
            colorToolbar2.Colors.Add(Color.FromArgb(167, 204, 65));
            colorToolbar2.Colors.Add(Color.FromArgb(76, 55, 14));
            colorToolbar2.Colors.Add(Color.FromArgb(61, 203, 255));
            colorToolbar2.Colors.Add(Color.FromArgb(15, 108, 228));
            colorToolbar2.Colors.Add(Color.FromArgb(69, 62, 181));
            colorToolbar2.Colors.Add(Color.FromArgb(216, 64, 196));
            colorToolbar2.Colors.Add(Color.FromArgb(1, 1, 1));
            colorToolbar2.Colors.Add(Color.Silver);
            colorToolbar2.Colors.Add(Color.White);

            checkGroupPenWidth.Add(btn_draw_width_thing);
            checkGroupPenWidth.Add(btn_draw_width_normal);
            checkGroupPenWidth.Add(btn_draw_width_bold);
            checkGroupPenWidth.Check(btn_draw_width_normal);

            checkGroupTools.Add(btn_rect);
            checkGroupTools.Add(btn_ellipse);
            checkGroupTools.Add(btn_mosaic);
            checkGroupTools.Add(btn_pen);
            checkGroupTools.Add(btn_arrwo);

            combo_font_size.SelectedIndex = 0;
            combo_font.SelectedIndex = 0;

            track_mosaic_level.Value = CurrentMosaicLevel;
        }


        private void colorToolbar2_ChoosedColorChanged(object sender, EventArgs e)
        {
            CurrentColor = colorToolbar2.ChoosedColor;
        }
        private void colorToolbar1_ChoosedColorChanged(object sender, EventArgs e)
        {
            CurrentColor = colorToolbar1.ChoosedColor;
        }

        private void btn_draw_width_thing_Click(object sender, EventArgs e)
        {
            CurrentPenWidth = PaintPenWidth.Thing;
            checkGroupPenWidth.Check((TabButton)sender);
            if (CurrentTools == PaintTools.Mosaic)
                swBrushCur();
        }
        private void btn_draw_width_normal_Click(object sender, EventArgs e)
        {
            CurrentPenWidth = PaintPenWidth.Normal;
            checkGroupPenWidth.Check((TabButton)sender);
            if(CurrentTools == PaintTools.Mosaic)
                swBrushCur();
        }
        private void btn_draw_width_bold_Click(object sender, EventArgs e)
        {
            CurrentPenWidth = PaintPenWidth.Bold;
            checkGroupPenWidth.Check((TabButton)sender);
            if(CurrentTools == PaintTools.Mosaic)
                swBrushCur();
        }

        private void swFontTool()
        {
            pl_font_tool.Visible = true;
            pl_pen_tool.Visible = false;
            Height = 110;
            formScreenShutcut.ResetToolPosition();
        }
        private void swPenTool(bool isMosaic = false)
        {
            pl_font_tool.Visible = false;
            pl_pen_tool.Visible = true;

            colorToolbar1.Visible = !isMosaic;
            lb_mosaic_level.Visible = isMosaic;
            track_mosaic_level.Visible = isMosaic;

            Height = 110;
            formScreenShutcut.ResetToolPosition();
        }
        private void swNoneTool()
        {
            pl_font_tool.Visible = false;
            pl_pen_tool.Visible = false;
            Height = 57;
            formScreenShutcut.ResetToolPosition();
        }

        private void swDefCur()
        {
            cur_current = cur_default;
            formScreenShutcut.Cursor = cur_default;
        }
        private void swPenCur()
        {
            cur_current = cur_pen;
            formScreenShutcut.Cursor = cur_pen;
        }
        private void swBrushCur()
        {
            switch (CurrentPenWidth)
            {
                case PaintPenWidth.Thing: formScreenShutcut.Cursor = cur_brush_small; cur_current = cur_brush_small; break;
                case PaintPenWidth.Normal: formScreenShutcut.Cursor = cur_brush_normal; cur_current = cur_brush_normal; break;
                case PaintPenWidth.Bold: formScreenShutcut.Cursor = cur_brush_big; cur_current = cur_brush_big; break;
            }
        }

        private void btn_rect_Click(object sender, EventArgs e)
        {
          
            if (checkGroupTools.Checked((TabButton)sender))
            {
                checkGroupTools.UnCheck((TabButton)sender);
                CurrentTools = PaintTools.None;
                swDefCur();
                swNoneTool();
            }
            else
            {
                CurrentTools = PaintTools.Rectangle;
                checkGroupTools.Check((TabButton)sender);
                swPenCur();
                swPenTool();
            }
        }
        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            
            if (checkGroupTools.Checked((TabButton)sender))
            {
                checkGroupTools.UnCheck((TabButton)sender);
                CurrentTools = PaintTools.None;
                swDefCur();
                swNoneTool();
            }
            else
            {
                CurrentTools = PaintTools.Ellipse;
                checkGroupTools.Check((TabButton)sender);
                swPenCur();
                swPenTool();
            }
        }
        private void btn_pen_Click(object sender, EventArgs e)
        {
         
            if (checkGroupTools.Checked((TabButton)sender))
            {
                checkGroupTools.UnCheck((TabButton)sender);
                CurrentTools = PaintTools.None;
                swDefCur();
                swNoneTool();
            }
            else
            {
                CurrentTools = PaintTools.Pen;
                checkGroupTools.Check((TabButton)sender);
                swPenCur();
                swPenTool();
            }
        }
        private void btn_arrwo_Click(object sender, EventArgs e)
        {
            
            if (checkGroupTools.Checked((TabButton)sender))
            {
                checkGroupTools.UnCheck((TabButton)sender);
                CurrentTools = PaintTools.None;
                swDefCur();
                swNoneTool();
            }
            else
            {
                CurrentTools = PaintTools.Arrow;
                checkGroupTools.Check((TabButton)sender);
                swPenCur();
                swNoneTool();
            }
        }
        private void btn_mosaic_Click(object sender, EventArgs e)
        {
            
            if (checkGroupTools.Checked((TabButton)sender))
            {
                checkGroupTools.UnCheck((TabButton)sender);
                CurrentTools = PaintTools.None;
                swDefCur();
                swNoneTool();
            }
            else
            {
                CurrentTools = PaintTools.Mosaic;
                checkGroupTools.Check((TabButton)sender);
                swBrushCur();
                swPenTool(true);
            }
        }
        private void btn_text_Click(object sender, EventArgs e)
        {
            
            if (checkGroupTools.Checked((TabButton)sender))
            {
                checkGroupTools.UnCheck((TabButton)sender);
                CurrentTools = PaintTools.None;
                swDefCur();
                swNoneTool();
            }
            else
            {
                CurrentTools = PaintTools.Text;
                checkGroupTools.Check((TabButton)sender);
                swPenCur();
                swFontTool();
            }
        }

        private void btn_revoke_Click(object sender, EventArgs e)
        {
            formScreenShutcut.EditRollback();
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            formScreenShutcut.SaveAndQuit();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            WindowUtils.SetForeground(formScreenShutcut.Handle);
            formScreenShutcut.Quit();
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            formScreenShutcut.CopyAndQuit();
        }

        private void combo_font_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentFont = new Font(combo_font.SelectedItem is string? (string)combo_font.SelectedItem:"微软雅黑", combo_font_size.SelectedItem is int?(int)combo_font_size.SelectedItem:9);
        }
        private void combo_font_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_font_size_SelectedIndexChanged(sender, e);
        }

        private void track_mosaic_level_Scroll(object sender, EventArgs e)
        {
      
        }
        private void track_mosaic_level_ValueChanged(object sender, EventArgs e)
        {
            CurrentMosaicLevel = track_mosaic_level.Value;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= API.WS_EX_TRANSPARENT;
                cp.ExStyle |= API.WS_EX_TOOLWINDOW;
                return cp;
            }
        }
    }
}
