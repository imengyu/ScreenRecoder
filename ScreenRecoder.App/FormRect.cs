using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    //选择录制区域窗口
    public partial class FormRect : Form
    {
        public FormRect()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //绘制四个角
            if (sizePaint)
                sizePaint = false;
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            SolidBrush b = new SolidBrush(Color.Red);
            g.FillRectangle(b, new Rectangle(0, 0, 50, 10));
            g.FillRectangle(b, new Rectangle(0, 10, 10, 40));
            g.FillRectangle(b, new Rectangle(Width - 50, 0, 50, 10));
            g.FillRectangle(b, new Rectangle(Width - 10, 0, 10, 50));
            g.FillRectangle(b, new Rectangle(Width - 10, Height - 50, 10, 50));
            g.FillRectangle(b, new Rectangle(Width - 50, Height - 10, 50, 10));
            g.FillRectangle(b, new Rectangle(0, Height - 10, 50, 10));
            g.FillRectangle(b, new Rectangle(0, Height - 50, 10, 50));
            g.FillRectangle(b, new Rectangle(Width - 20, Height - 20, 10, 10));
            g.DrawImage(Properties.Resources.ico_drag, Width - 20, Height - 20, 20, 20);

            //释放
            b.Dispose();
            b = null;

            base.OnPaint(e);
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            API.WindowMove(Handle);
        }

        private void FormRect_SizeChanged(object sender, EventArgs e)
        {
            //显示大小到label1
            label1.Text = Width + " x " + Height;
        }
        private void FormRect_LocationChanged(object sender, EventArgs e)
        {
            //显示位置到label2
            label2.Text = Left + " , " + Top;
        }

        //调整大小的类型
        private enum SizeType
        {
            None,
            TopLeft,
            Top,
            TopRight,
            Right,
            ButtomRight,
            Buttom,
            ButtomLeft,
            Left,
        }
        bool sizePaint = true;
        //是否正在调整大小
        bool Sizeing = false;
        int oldSizeRight = 0;
        int oldSizeButtom = 0;
        Point resizeTargetPos = new Point();
        Size resizeTargetSize = new Size();
        SizeType SizeDre = SizeType.None;
        //调整大小的边距
        //鼠标距离边框<SIZE_BORDER大小就认为可以调节大小
        const int SIZE_BORDER = 10;
        const int SIZE_BORDER_2 = 20;

        private void pl_bg_MouseDown(object sender, MouseEventArgs e)
        {            
            //按下获取鼠标位置并确定调整大小的类型
            if (e.Button == MouseButtons.Left)
            {
                timerSizePaint.Start();
                if (e.X > 0 && e.X < SIZE_BORDER && e.Y > SIZE_BORDER && e.Y < Height - SIZE_BORDER)
                {
                    Sizeing = true;
                    SizeDre = SizeType.Left;
                    oldSizeRight = Right;
                    oldSizeButtom = Bottom;
                }
                else if (e.Y > 0 && e.Y < SIZE_BORDER && e.X > SIZE_BORDER && e.X < Width - SIZE_BORDER)
                {
                    Sizeing = true;
                    SizeDre = SizeType.Top;
                    oldSizeRight = Right;
                    oldSizeButtom = Bottom;
                }
                else if (e.Y > Height - SIZE_BORDER && e.Y < Height && e.X > SIZE_BORDER && e.X < Width - SIZE_BORDER)
                {
                    Sizeing = true;
                    SizeDre = SizeType.Buttom;
                }
                else if (e.X > Width - SIZE_BORDER && e.X < Width && e.Y > SIZE_BORDER && e.Y < Height - SIZE_BORDER)
                {
                    Sizeing = true;
                    SizeDre = SizeType.Right;
                }
                else if (e.X > 0 && e.X < SIZE_BORDER && e.Y < SIZE_BORDER && e.Y > 0)
                {
                    Sizeing = true;
                    SizeDre = SizeType.TopLeft;
                    oldSizeRight = Right;
                    oldSizeButtom = Bottom;
                }
                else if (e.X > Width - SIZE_BORDER && e.X < Width && e.Y < SIZE_BORDER && e.Y > 0)
                {
                    Sizeing = true;
                    SizeDre = SizeType.TopRight;
                    oldSizeRight = Right;
                    oldSizeButtom = Bottom;
                }
                else if (e.Y > Height - SIZE_BORDER_2 && e.Y < Height && e.X > Width - SIZE_BORDER_2 && e.X < Width)
                {
                    Sizeing = true;
                    SizeDre = SizeType.ButtomRight;
                }
                else if (e.X < SIZE_BORDER_2 && e.X > 0 && e.Y > Height - SIZE_BORDER_2 && e.Y < Height)
                {
                    Sizeing = true;
                    SizeDre = SizeType.ButtomLeft;
                    oldSizeRight = Right;
                    oldSizeButtom = Bottom;
                }
                else
                {
                    SizeDre = SizeType.None;
                    Sizeing = false;
                    timerSizePaint.Stop();
                }
            }
        }
        private void pl_bg_MouseUp(object sender, MouseEventArgs e)
        {
            //是正在调整大小
            if (Sizeing)
            {
                //调整大小结束
                Sizeing = false;
                timerSizePaint.Stop();
                //应用调整
                timerSizePaint_Tick(sender, e);
                Invalidate();
            }
        }
        private void pl_bg_MouseMove(object sender, MouseEventArgs e)
        {
            //是正在调整大小
            if (Sizeing)
            {
                //
                resizeTargetPos.X = Left;
                resizeTargetPos.Y = Top;
                resizeTargetSize.Width = Width;
                resizeTargetSize.Height = Height;
                //获取调整大小类型
                switch (SizeDre)
                {
                    case SizeType.Left:
                        resizeTargetPos.X = MousePosition.X;
                        resizeTargetSize.Width = oldSizeRight - Left;
                        break;
                    case SizeType.Top:
                        resizeTargetPos.Y = MousePosition.Y;
                        resizeTargetSize.Height = oldSizeButtom - Top;
                        break;
                    case SizeType.Right:
                        resizeTargetSize.Width = MousePosition.X - Left;
                        break;
                    case SizeType.Buttom:
                        resizeTargetSize.Height = MousePosition.Y - Top;
                        break;
                    case SizeType.TopLeft:
                        resizeTargetPos.X = MousePosition.X;
                        resizeTargetSize.Width = oldSizeRight - Left;
                        resizeTargetPos.Y = MousePosition.Y;
                        resizeTargetSize.Height = oldSizeButtom - Top;
                        break;
                    case SizeType.TopRight:
                        resizeTargetPos.Y = MousePosition.Y;
                        resizeTargetSize.Height = oldSizeButtom - Top;
                        resizeTargetSize.Width = MousePosition.X;
                        break;
                    case SizeType.ButtomRight:
                        resizeTargetSize.Width = MousePosition.X - Left;
                        resizeTargetSize.Height = MousePosition.Y - Top;
                        break;
                    case SizeType.ButtomLeft:
                        resizeTargetSize.Height = MousePosition.Y - Top;
                        resizeTargetPos.X = MousePosition.X;
                        resizeTargetSize.Width = oldSizeRight - Left;
                        break;
                }
            }
            else
            {
                //设置鼠标指针
                if (e.X > 0 && e.X < SIZE_BORDER && e.Y > SIZE_BORDER && e.Y < Height - SIZE_BORDER)
                    Cursor = Cursors.SizeWE;
                else if (e.Y > 0 && e.Y < SIZE_BORDER && e.X > SIZE_BORDER && e.X < Width - SIZE_BORDER)
                    Cursor = Cursors.SizeNS;
                else if (e.Y > Height - SIZE_BORDER && e.Y < Height && e.X > SIZE_BORDER && e.X < Width - SIZE_BORDER)
                    Cursor = Cursors.SizeNS;
                else if (e.X > Width - SIZE_BORDER && e.X < Width && e.Y > SIZE_BORDER && e.Y < Height - SIZE_BORDER)
                    Cursor = Cursors.SizeWE;
                else if (e.X > 0 && e.X < SIZE_BORDER_2 && e.Y < SIZE_BORDER_2 && e.Y > 0)
                    Cursor = Cursors.SizeNWSE;
                else if (e.X > Width - SIZE_BORDER_2 && e.X < Width && e.Y < SIZE_BORDER_2 && e.Y > 0)
                    Cursor.Current = Cursors.SizeNESW;
                else if (e.Y > Height - SIZE_BORDER_2 && e.Y < Height && e.X > Width - SIZE_BORDER_2 && e.X < Width)
                    Cursor = Cursors.SizeNWSE;
                else if (e.X < SIZE_BORDER_2 && e.X > 0 && e.Y > Height - SIZE_BORDER_2 && e.Y < Height)
                    Cursor = Cursors.SizeNESW;
            }
        }

        private void timerSizePaint_Tick(object sender, EventArgs e)
        {
            if (!sizePaint)
            {
                //调整大小应用
                //防止因为调整大小过快消耗太多cpu而使用timer控制
                sizePaint = false;
                if (!resizeTargetPos.IsEmpty)
                    Location = resizeTargetPos;
                if (!resizeTargetSize.IsEmpty)
                {
                    if (resizeTargetSize.Width % 4 != 0) resizeTargetSize.Width -= resizeTargetSize.Width % 4;
                    if (resizeTargetSize.Height % 4 != 0) resizeTargetSize.Height -= resizeTargetSize.Height % 4;
                    Size = resizeTargetSize;
                }
                resizeTargetPos = Point.Empty;
                resizeTargetSize = Size.Empty;
                sizePaint = true;
                //刷新重新绘制边框
                Invalidate();
            }
        }

        //两个按钮
        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.BackColor = Color.Orange;
        }
        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = Color.Red;
        }
        private void label3_Click(object sender, EventArgs e)
        {
            FormMain.formMain.SetFullScreen();
        }
        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.BackColor = Color.Orange;
        }
        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.BackColor = Color.Red;
        }
        private void label4_Click(object sender, EventArgs e)
        {
            bool mainShow = API.WindowIsVisible(FormMain.formMain.Handle);
            if(mainShow) API.WindowHide(FormMain.formMain.Handle);
            API.WindowHide(Handle);
            FormChooseWindow formChooseWindow = new FormChooseWindow();
            if (formChooseWindow.ShowDialog()== DialogResult.OK)
            {
                if(formChooseWindow.Window!=IntPtr.Zero)
                {
                    API.RECT rc = formChooseWindow.RECT;
                    Location = new Point(rc.Left, rc.Top);
                    resizeTargetSize = new Size(rc.Right - rc.Left, rc.Bottom - rc.Top);
                    if (resizeTargetSize.Width % 4 != 0) resizeTargetSize.Width -= resizeTargetSize.Width % 4;
                    if (resizeTargetSize.Height % 4 != 0) resizeTargetSize.Height -= resizeTargetSize.Height % 4;
                    Size = resizeTargetSize;
                    resizeTargetSize = Size.Empty;
                }
            }
            if (mainShow) API.WindowShow(FormMain.formMain.Handle);
            API.WindowShow(Handle);
        }
    }
}
