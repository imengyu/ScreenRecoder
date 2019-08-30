using ScreenRecoder.App.Api;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    //吸附窗口 的窗口选择器
    // 类似截图软件的功能，鼠标移到窗口上显示一个框，即可选择窗口
    public partial class FormChooseWindow : Form
    {
        public FormChooseWindow()
        {
            InitializeComponent();
        }

        private API.RECT lastFindedWndRect = new API.RECT();
        private IntPtr lastFindedWnd = IntPtr.Zero;
        private Bitmap bitmapBg = null;
        private bool canfind = true;
        private Pen rectPen = null;
        private SolidBrush brushBg = null;

        //已经选择了的窗口和矩形
        public API.RECT RECT { get { return lastFindedWndRect; } }
        public IntPtr Window { get { return lastFindedWnd; } }

        private void FormChooseWindow_Load(object sender, EventArgs e)
        {
            //设置本窗口全屏
            Location = Point.Empty;
            Size = Screen.PrimaryScreen.Bounds.Size;
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            bitmapBg = new Bitmap(bounds.Width, bounds.Height);
            //截下桌面的图，然后作为背景进行绘画
            using (Graphics g = Graphics.FromImage(bitmapBg))
                g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            timer1.Start();
            rectPen = new Pen(Color.DodgerBlue, 5);
            brushBg = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
        }
        private void FormChooseWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //释放
            brushBg.Dispose();
            brushBg = null;
            rectPen.Dispose();
            rectPen = null;
            bitmapBg.Dispose();
            bitmapBg = null;
            timer1.Stop();
        }
        private void FormChooseWindow_MouseClick(object sender, MouseEventArgs e)
        {
            //用户点击鼠标
            if (e.Button == MouseButtons.Right)
            {
                //右键认为取消
                DialogResult = DialogResult.Cancel;
                Close();
            }
            else
            {
                //左键认为确定
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        private void FormChooseWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if(canfind)
            {
                canfind = false;
                //移动鼠标根据鼠标位置找窗口
                //百度 ChildWindowFromPointEx
                IntPtr thisfind = API.ChildWindowFromPointEx(API.GetDesktopWindow(),new API.POINT(e.X, e.Y), 
                    0x0004 | 0x0001 | 0x0002/*（CWP_SKIPTRANSPARENT ）表示忽略透明窗口，而本窗口设置  标识则可忽略本窗口*/);
                //上一个找到的不等于这个找到的
                if (lastFindedWnd != thisfind)
                {
                    //重绘框
                    lastFindedWnd = thisfind;
                    API.GetWindowRect(lastFindedWnd, out lastFindedWndRect);
                    Invalidate();
                }
            }
        }
        private void FormChooseWindow_Paint(object sender, PaintEventArgs e)
        {
            //绘制蓝色的框
            // 黑色背景
            Graphics g = e.Graphics;
            if (bitmapBg != null)
                g.DrawImage(bitmapBg, 0, 0, Width, Height);
            if (lastFindedWnd != IntPtr.Zero)
            {
                Rectangle r = new Rectangle(lastFindedWndRect.Left + 1, lastFindedWndRect.Top + 1,
                    lastFindedWndRect.Right - lastFindedWndRect.Left - 3,
                    lastFindedWndRect.Bottom - lastFindedWndRect.Top - 3);
                g.DrawRectangle(rectPen, r);

                g.FillRectangle(brushBg, 0, 0, lastFindedWndRect.Left, Height);
                g.FillRectangle(brushBg, lastFindedWndRect.Right, 0, Width - lastFindedWndRect.Right, Height);

                g.FillRectangle(brushBg, lastFindedWndRect.Left, 0, lastFindedWndRect.Right - lastFindedWndRect.Left, lastFindedWndRect.Top);
                g.FillRectangle(brushBg, lastFindedWndRect.Left, lastFindedWndRect.Bottom, lastFindedWndRect.Right - lastFindedWndRect.Left, Height- lastFindedWndRect.Bottom);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //限制一秒的寻找次数
            if (!canfind) canfind = true;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= API.WS_EX_TRANSPARENT;
                return cp;
            }
        }
    }
}
