using ScreenRecoder.App.Api;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    public partial class FormColorPick : Form
    {
        public FormColorPick(FormScreenShutcut bg)
        {
            InitializeComponent();
            formScreenShutcut = bg;
        }

        private FormScreenShutcut formScreenShutcut;
        private Pen linePen = new Pen(Color.DodgerBlue, 2.5f);
        private SolidBrush brushBg = null;
        private Pen borderPen = new Pen(Color.FromArgb(56, 56, 56));
        private Rectangle bitmapRect = new Rectangle(0, 0, 90, 90);
        private Rectangle bitmapRect2 = new Rectangle(1, 1, 88, 88);
        private Rectangle windowRect = Rectangle.Empty;
        private bool paintLock = false;
        private bool paintInNextUnLock = false;

        public string RectSizeStr { get { return lb_size.Text; } set { lb_size.Text = "尺寸：" + value; } }

        public void InvalidatePreview()
        {
            if(!paintLock)
                Invalidate(bitmapRect2);
        }

        private void FormChooseWindow_Load(object sender, EventArgs e)
        {
            windowRect = new Rectangle(0, 0, Width - 1, Height - 1);
            timer1.Start();
        }
        private void FormChooseWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //释放
            if (brushBg != null)
            {
                brushBg.Dispose();
                brushBg = null;
            }
            if (linePen != null)
            {
                linePen.Dispose();
                linePen = null;
            }
            timer1.Stop();
        }
        private void FormChooseWindow_Paint(object sender, PaintEventArgs e)
        {
            if (paintLock)
            {
                paintInNextUnLock = true;
                return;
            }
            else paintLock = true;
            //绘制蓝色的
            Graphics g = e.Graphics;
            g.DrawRectangle(borderPen, windowRect);
            g.DrawRectangle(borderPen, bitmapRect);
            if (formScreenShutcut.bitmapBg != null)
            {
                //更新颜色值
                Color color = formScreenShutcut.bitmapBg.GetPixel(MousePosition.X - 8, MousePosition.Y - 8);
                lb_color.Text = "颜色：RGB: (" + color.R + "," + color.G + "," + color.B + ")\n          " + ColorTranslator.ToHtml(color);
                //绘制鼠标处图像
                g.DrawImage(formScreenShutcut.bitmapBg, bitmapRect2, new Rectangle(MousePosition.X - 8, MousePosition.Y - 8, 16, 16), GraphicsUnit.Pixel);
            }
            //绘制交叉线
            float x = bitmapRect.Width / 2 - linePen.Width / 2;
            float y = bitmapRect.Height / 2 - linePen.Width / 2;
            g.DrawLine(linePen, x, 1, x, bitmapRect2.Height);
            g.DrawLine(linePen, 1, y, bitmapRect2.Width, y);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            paintLock = false;
            if (paintInNextUnLock)
            {
                paintInNextUnLock = false;
                Invalidate(bitmapRect2);
            }
        }
    }
}
