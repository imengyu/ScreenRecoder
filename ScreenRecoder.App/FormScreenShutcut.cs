using System;
using System.Drawing;
using System.Windows.Forms;
using ScreenRecoder.App.Api;
using ScreenRecoder.App.PaintBox;

namespace ScreenRecoder.App
{
    //截图窗口
    public partial class FormScreenShutcut : Form
    {
        public FormScreenShutcut()
        {
            InitializeComponent();
        }
        private const int SIZE_BORDER = 10;
        private const int SIZE_BORDER_2 = 20;
        //调整大小的类型
        private enum SizeType
        {
            None = 0,
            Move = 1,
            Top = 2,
            Right = 4,
            Buttom = 8,
            Left = 16,

            TopLeft = Top | Left,
            TopRight = Top | Right,
            ButtomRight = Buttom | Right,
            ButtomLeft = Buttom | Left,
        }
        private SizeType currentSizeType = SizeType.None;
        private API.RECT lastFindedWndRect = new API.RECT();
        private IntPtr lastFindedWnd = IntPtr.Zero;
        internal Bitmap bitmapBg = null;
        internal Bitmap bitmapDrawingBoard = null;
        private bool canfind = true;
        private Pen rectPen = null;
        private StringFormat stringFormatCenter = null;
        private SolidBrush brushBg = null;
        private FormColorPick formColorPick = null;
        private FormEditScreenShutTools formEditScreenShutTools = null;
        private bool _rectChoosed = false;
        private bool rectChoosed
        {
            get { return _rectChoosed; }
            set
            {
                _rectChoosed = value;
                if (_rectChoosed)
                {
                    if (formColorPick != null)
                        WindowUtils.Hide(formColorPick.Handle);

                    if (formEditScreenShutTools != null)
                    {
                        ResetToolPosition();
                        WindowUtils.Show(formEditScreenShutTools.Handle);
                        WindowUtils.Top(formEditScreenShutTools.Handle);
                    }
                }
                else
                {
                    canfind = true;
                    Cursor = Cursors.Cross;
                    lastFindedWndRect = new API.RECT();
                    if (formColorPick != null)
                        WindowUtils.Show(formColorPick.Handle);
                    if (formEditScreenShutTools != null)
                        WindowUtils.Hide(formEditScreenShutTools.Handle);
                    if (paintBox != null) paintBox.Clear();
                     //重新查找一次窗口
                     IntPtr thisfind = API.ChildWindowFromPointEx(API.GetDesktopWindow(), new API.POINT(MousePosition.X, MousePosition.Y), 0x0004 | 0x0001 | 0x0002);
                   lastFindedWnd = thisfind;
                   API.GetWindowRect(lastFindedWnd, out lastFindedWndRect);
                    formColorPick.RectSizeStr = lastFindedWndRect.Width + " x " + lastFindedWndRect.Height;
                    Invalidate();
                }
            }
        }
        private bool mouseDowned = false;
        private Image imagePointDrag = Properties.Resources.drag_point;
        private int imagePointDragSize = 6;
        private PaintBox.PaintBox paintBox = null;

        private int screenWidth = 0;
        private int screenHeight = 0;

        private Point pointStart = new Point();
        private Point pointStartRefRect = new Point();
        private Point pointEnd = new Point();

        //已经选择了的窗口和矩形
        private API.RECT RECT { get { return lastFindedWndRect; } }
        private IntPtr Window { get { return lastFindedWnd; } }

        public void ResetToolPosition()
        {
            if (screenHeight - lastFindedWndRect.Bottom > formEditScreenShutTools.Height)
                formEditScreenShutTools.Top = lastFindedWndRect.Bottom;
            else if (lastFindedWndRect.Top > formEditScreenShutTools.Height)
                formEditScreenShutTools.Top = lastFindedWndRect.Top - formEditScreenShutTools.Height;
            else formEditScreenShutTools.Top = lastFindedWndRect.Top;

            if (lastFindedWndRect.Right <  formEditScreenShutTools.Width)
                formEditScreenShutTools.Left = 0;
            else formEditScreenShutTools.Left = lastFindedWndRect.Right - formEditScreenShutTools.Width;
        }
        public void Quit()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        public void SaveAndQuit()
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next().ToString("00");
            string savePath = DialogUtils.ShowSaveImageFileDialog(Handle, "保存截图", fileName);
            if (savePath != null)
            {
                Bitmap bitmapTarget = new Bitmap(lastFindedWndRect.Width, lastFindedWndRect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Bitmap bitmapSave = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                if (bitmapBg != null)
                {
                    Graphics g = Graphics.FromImage(bitmapSave);
                    g.DrawImage(bitmapBg, 0, 0, Width, Height);
                    if (paintBox != null)
                    {
                        Graphics gB = Graphics.FromImage(bitmapDrawingBoard);
                        paintBox.OnPaint(new PaintEventArgs(gB, ClientRectangle));
                        gB.Dispose();
                    }
                    if (bitmapDrawingBoard != null)
                        g.DrawImage(bitmapDrawingBoard, 0, 0, Width, Height);
                    g.Dispose();
                    g = null;
                    g = Graphics.FromImage(bitmapTarget);
                    g.DrawImage(bitmapSave, new Rectangle(0, 0, lastFindedWndRect.Width, lastFindedWndRect.Height),
                        new Rectangle(lastFindedWndRect.Left, lastFindedWndRect.Top, lastFindedWndRect.Width, lastFindedWndRect.Height), GraphicsUnit.Pixel);
                    g.Dispose();
                    g = null;

                    string ext = savePath.Substring(savePath.LastIndexOf('.'));
                    try
                    {
                        if (ext == ".bmp")                
                            bitmapTarget.Save(savePath, System.Drawing.Imaging.ImageFormat.Bmp);
                        else if (ext == ".png")
                            bitmapTarget.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);
                        else if (ext == ".jpg" || ext == ".jpeg")
                            bitmapTarget.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        else if (ext == ".gif")
                            bitmapTarget.Save(savePath, System.Drawing.Imaging.ImageFormat.Gif);
                    }
                    catch (Exception e)
                    {
                        Utils.Utils.WriteDebugString(e.ToString());
                        FormMsg m = new FormMsg("无法写入图片：" + savePath + "\n错误信息：" + e.ToString(), "保存图片失败", "确定", "", "ScreenRecoder - 程序错误", Properties.Resources.error_r_o);
                        m.TopMost = true;
                        m.ShowDialog(this);
                    }
                }

                bitmapSave.Dispose();
                bitmapSave = null;
                bitmapTarget.Dispose();
                bitmapTarget = null;

            
                Quit();
            }
        }
        public void CopyAndQuit()
        {
            Bitmap bitmapTarget = new Bitmap(lastFindedWndRect.Width, lastFindedWndRect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap bitmapSave = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            if (bitmapBg != null)
            {
                Graphics g = Graphics.FromImage(bitmapSave);
                g.DrawImage(bitmapBg, 0, 0, Width, Height);
                if (paintBox != null)
                {
                    Graphics gB = Graphics.FromImage(bitmapDrawingBoard);
                    paintBox.OnPaint(new PaintEventArgs(gB, ClientRectangle));
                    gB.Dispose();
                }
                if (bitmapDrawingBoard != null)
                    g.DrawImage(bitmapDrawingBoard, 0, 0, Width, Height);
                g.Dispose();
                g = null;
                g = Graphics.FromImage(bitmapTarget);
                g.DrawImage(bitmapSave, new Rectangle(0, 0, lastFindedWndRect.Width, lastFindedWndRect.Height),
                    new Rectangle(lastFindedWndRect.Left, lastFindedWndRect.Top, lastFindedWndRect.Width, lastFindedWndRect.Height), GraphicsUnit.Pixel);
                g.Dispose();
                g = null;

                Clipboard.SetImage(bitmapTarget);
            }

            bitmapSave.Dispose();
            bitmapSave = null;
            bitmapTarget.Dispose();
            bitmapTarget = null;

            Quit();
        }
        public void EditRollback()
        {
            paintBox.RollBack();
            Invalidate();
        }

        private void FormChooseWindow_Load(object sender, EventArgs e)
        {
            //设置本窗口全屏
            screenWidth = API.GetSystemMetrics(API.SM_CXSCREEN);
            screenHeight = API.GetSystemMetrics(API.SM_CYSCREEN);
            Location = Point.Empty;
            Size = new Size(screenWidth, screenHeight);
            stringFormatCenter = new StringFormat();
            stringFormatCenter.Alignment = StringAlignment.Center;
            stringFormatCenter.LineAlignment = StringAlignment.Center;
            bitmapBg = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            bitmapDrawingBoard = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //截下桌面的图，然后作为背景进行绘画
            using (Graphics g = Graphics.FromImage(bitmapBg))
                g.CopyFromScreen(Point.Empty, Point.Empty, new Size(screenWidth, screenHeight));
            timer1.Start();
            rectPen = new Pen(Color.DodgerBlue, 5);
            brushBg = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
            formColorPick = new FormColorPick(this);
            formColorPick.Show();
            formColorPick.KeyDown += FormScreenShutcut_KeyDown;
            formEditScreenShutTools = new FormEditScreenShutTools(this);
            formEditScreenShutTools.Show();
            Cursor = formEditScreenShutTools.cur_default;
            paintBox = new PaintBox.PaintBox(this, formEditScreenShutTools);
            WindowUtils.Top(Handle);
            WindowUtils.Hide(formEditScreenShutTools.Handle);
            WindowUtils.Top(formColorPick.Handle);

        }
        private void FormChooseWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //释放
            if (formColorPick != null)
            {
                formColorPick.Close();
                formColorPick = null;
            }
            if (formEditScreenShutTools != null)
            {
                formEditScreenShutTools.Close();
                formEditScreenShutTools = null;
            }
            if (stringFormatCenter != null)
            {
                stringFormatCenter.Dispose();
                stringFormatCenter = null;
            }
            if (brushBg != null)
            {
                brushBg.Dispose();
                brushBg = null;
            }
            if (rectPen != null)
            {
                rectPen.Dispose();
                rectPen = null;
            }
            if (bitmapBg != null)
            {
                bitmapBg.Dispose();
                bitmapBg = null;
            }
            if (bitmapDrawingBoard != null)
            {
                bitmapDrawingBoard.Dispose();
                bitmapDrawingBoard = null;
            }
            if (paintBox != null)
            {
                paintBox.Dispose();
                paintBox = null;
            }
            
            timer1.Stop();
        }
        private void FormChooseWindow_MouseClick(object sender, MouseEventArgs e)
        {
            //if (!rectChoosed && formColorPick != null) WindowUtils.Top(formColorPick.Handle);
            //用户点击鼠标
            if (e.Button == MouseButtons.Right)
            {
                //右键认为取消
                if (rectChoosed)
                {
                    rectChoosed = false;
                    Invalidate();
                }
                else Quit();
            }
        }
        private void FormChooseWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (canfind && !rectChoosed)
            {
                canfind = false;
                //移动鼠标根据鼠标位置找窗口
                //百度来的 ChildWindowFromPointEx
                IntPtr thisfind = API.ChildWindowFromPointEx(API.GetDesktopWindow(),new API.POINT(e.X, e.Y), 
                    0x0004 | 0x0001 | 0x0002/*（CWP_SKIPTRANSPARENT ）表示忽略透明窗口，而本窗口设置  标识则可忽略本窗口*/);
                //上一个找到的不等于这个找到的
                if (lastFindedWnd != thisfind)
                {
                    //重绘框
                    lastFindedWnd = thisfind;
                    API.GetWindowRect(lastFindedWnd, out lastFindedWndRect);
                    formColorPick.RectSizeStr = lastFindedWndRect.Width + " x " + lastFindedWndRect.Height;
                    Invalidate();
                }
            }
            if (!rectChoosed || mouseDowned)
            {
                Point p = MousePosition;
                Point tp = new Point();
                if (p.X > screenWidth - formColorPick.Width - 25)
                    tp.X = p.X - formColorPick.Width - 25;
                else tp.X = p.X + 25; 
                if (p.Y > screenHeight - formColorPick.Height - 25)
                    tp.Y = p.Y - formColorPick.Height - 25;
                else tp.Y = p.Y + 25;

                formColorPick.Location = tp;
                formColorPick.InvalidatePreview();
                WindowUtils.Top(formColorPick.Handle);
            }
            if (mouseDowned)
            {
                //拖动框
                if (!rectChoosed) //拉出一个框
                {
                    pointEnd = MousePosition;
                    //重置绘画的矩形框
                    if (pointStart.X > pointEnd.X)
                    {
                        lastFindedWndRect.Left = pointEnd.X;
                        lastFindedWndRect.Right = pointStart.X;
                    }
                    else
                    {
                        lastFindedWndRect.Left = pointStart.X;
                        lastFindedWndRect.Right = pointEnd.X;
                    }
                    if (pointStart.Y > pointEnd.Y)
                    {
                        lastFindedWndRect.Top = pointEnd.Y;
                        lastFindedWndRect.Bottom = pointStart.Y;
                    }
                    else
                    {
                        lastFindedWndRect.Top = pointStart.Y;
                        lastFindedWndRect.Bottom = pointEnd.Y;
                    }
                    Invalidate();
                }
                else if (formEditScreenShutTools.CurrentTools == PaintTools.None) //拖动调整框大小
                {
                    if (currentSizeType == SizeType.Move)
                    {
                        int w = lastFindedWndRect.Width;
                        int h = lastFindedWndRect.Height;
                        Point tp = new Point(MousePosition.X - pointStartRefRect.X,
                            MousePosition.Y - pointStartRefRect.Y);
                        if (tp.X < 0) tp.X = 0; if (tp.Y < 0) tp.Y = 0;
                        if (tp.X + w > screenWidth) tp.X = screenWidth - w;
                        if (tp.Y + h > screenHeight) tp.Y = screenHeight - h;
                        lastFindedWndRect.Left = tp.X;
                        lastFindedWndRect.Top = tp.Y;
                        lastFindedWndRect.Right = tp.X + w;
                        lastFindedWndRect.Bottom = tp.Y + h;
                        ResetToolPosition();
                        Invalidate();
                    }
                    else
                    {
                        bool resized = false;

                        if ((currentSizeType & SizeType.Top) == SizeType.Top)
                        {
                            Point tp = MousePosition;
                            if (tp.Y < 0) tp.Y = 0;
                            else if (tp.Y > lastFindedWndRect.Bottom)
                            {
                                currentSizeType ^= SizeType.Top;
                                currentSizeType |= SizeType.Buttom;
                            }
                            lastFindedWndRect.Top = tp.Y;
                            resized = true;
                        }
                        if ((currentSizeType & SizeType.Buttom) == SizeType.Buttom)
                        {
                            Point tp = MousePosition;
                            if (tp.Y > screenHeight) tp.Y = screenHeight;
                            else if (tp.Y < lastFindedWndRect.Top)
                            {
                                currentSizeType ^= SizeType.Buttom;
                                currentSizeType |= SizeType.Top;
                            }
                            lastFindedWndRect.Bottom = tp.Y;
                            resized = true;
                        }

                        if ((currentSizeType & SizeType.Left) == SizeType.Left)
                        {
                            Point tp = MousePosition;
                            if (tp.X < 0) tp.X = 0;
                            else if (tp.X > lastFindedWndRect.Right)
                            {
                                currentSizeType ^= SizeType.Left;
                                currentSizeType |= SizeType.Right;
                            }
                            lastFindedWndRect.Left = tp.X;
                            resized = true;
                        }
                        if ((currentSizeType & SizeType.Right) == SizeType.Right)
                        {
                            Point tp = MousePosition;
                            if (tp.X > screenWidth) tp.X = screenWidth;
                            else if (tp.X < lastFindedWndRect.Left)
                            {
                                currentSizeType ^= SizeType.Right;
                                currentSizeType |= SizeType.Left;
                            }
                            lastFindedWndRect.Right = tp.X;
                            resized = true;
                        }

                        if (resized)
                        {
                            ResetToolPosition();
                            formColorPick.InvalidatePreview();
                            formColorPick.RectSizeStr = lastFindedWndRect.Width + " x " + lastFindedWndRect.Height;
                            Invalidate();
                        }
                    }
                }
                else //画板
                {
                    //绘制工具
                    paintBox.OnMouseMove(e);
                }
            }
            else if (rectChoosed)
            {

                //检测鼠标是否在框的四角
                Rectangle rc = new Rectangle(lastFindedWndRect.Left - 10, lastFindedWndRect.Top - 10,
                 lastFindedWndRect.Right - lastFindedWndRect.Left + 20, lastFindedWndRect.Bottom - lastFindedWndRect.Top + 20);
                Point pt2 = new Point(e.X - rc.Left, e.Y - rc.Top);
                //设置鼠标指针
                if (pt2.X > 0 && pt2.X < SIZE_BORDER && pt2.Y > SIZE_BORDER && pt2.Y < rc.Height - SIZE_BORDER)
                {
                    Cursor = Cursors.SizeWE;
                    currentSizeType = SizeType.Left;
                }
                else if (pt2.Y > 0 && pt2.Y < SIZE_BORDER && pt2.X > SIZE_BORDER && pt2.X < rc.Width - SIZE_BORDER)
                {
                    Cursor = Cursors.SizeNS;
                    currentSizeType = SizeType.Top;
                }
                else if (pt2.Y > rc.Height - SIZE_BORDER && pt2.Y < rc.Height && pt2.X > SIZE_BORDER && pt2.X < rc.Width - SIZE_BORDER)
                {
                    Cursor = Cursors.SizeNS;
                    currentSizeType = SizeType.Buttom;
                }
                else if (pt2.X > rc.Width - SIZE_BORDER && pt2.X < rc.Width && pt2.Y > SIZE_BORDER && pt2.Y < rc.Height - SIZE_BORDER)
                {
                    Cursor = Cursors.SizeWE;
                    currentSizeType = SizeType.Right;
                }
                else if (pt2.X > 0 && pt2.X < SIZE_BORDER_2 && pt2.Y < SIZE_BORDER_2 && pt2.Y > 0)
                {
                    Cursor = Cursors.SizeNWSE;
                    currentSizeType = SizeType.TopLeft;
                }
                else if (pt2.X > rc.Width - SIZE_BORDER_2 && pt2.X < rc.Width && pt2.Y < SIZE_BORDER_2 && pt2.Y > 0)
                {
                    Cursor.Current = Cursors.SizeNESW;
                    currentSizeType = SizeType.TopRight;
                }
                else if (pt2.Y > rc.Height - SIZE_BORDER_2 && pt2.Y < rc.Height && pt2.X > rc.Width - SIZE_BORDER_2 && pt2.X < rc.Width)
                {
                    Cursor = Cursors.SizeNWSE;
                    currentSizeType = SizeType.ButtomRight;
                }
                else if (pt2.X < SIZE_BORDER_2 && pt2.X > 0 && pt2.Y > rc.Height - SIZE_BORDER_2 && pt2.Y < rc.Height)
                {
                    Cursor = Cursors.SizeNESW;
                    currentSizeType = SizeType.ButtomLeft;
                }
                else if (pt2.X > SIZE_BORDER_2 && pt2.Y > SIZE_BORDER_2 &&
                    pt2.X < rc.Width - SIZE_BORDER_2 && pt2.Y < rc.Height - SIZE_BORDER_2)
                {
                    if (formEditScreenShutTools.CurrentTools == PaintTools.None)
                        Cursor = Cursors.SizeAll;
                    else
                        Cursor = formEditScreenShutTools.cur_current;
                    currentSizeType = SizeType.Move;
                }
                else
                {
                    Cursor = formEditScreenShutTools.cur_default;
                    currentSizeType = SizeType.None;
                }
            }
        }
        private void FormScreenShutcut_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                //右键认为取消
                if (rectChoosed)
                {
                    rectChoosed = false;
                    Invalidate();
                }
                else Quit();
            }
        }
        private void FormScreenShutcut_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pointStart = MousePosition;
                mouseDowned = true;

                if (currentSizeType > SizeType.Move)
                {
                    WindowUtils.Show(formColorPick.Handle);
                    WindowUtils.Top(formColorPick.Handle);
                }
                else if (currentSizeType == SizeType.Move && formEditScreenShutTools.CurrentTools != PaintTools.None)
                {
                    //工具初始化
                    paintBox.OnMouseDown(e);
                }

                if (rectChoosed)
                pointStartRefRect = new Point(MousePosition.X - lastFindedWndRect.Left, MousePosition.Y - lastFindedWndRect.Top);
            }
        }
        private void FormScreenShutcut_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pointEnd = MousePosition;
                pointStartRefRect = Point.Empty;
                mouseDowned = false;
                //选择框
                if (!rectChoosed)
                {
                    //普通选择
                    if (pointStart.X != pointEnd.X || pointStart.Y != pointEnd.Y)
                    {
                        rectChoosed = true;
                        Invalidate();
                    }
                    else if (lastFindedWnd != IntPtr.Zero)
                    {
                        if (lastFindedWndRect.Left == 0 && lastFindedWndRect.Top == 0
                            && lastFindedWndRect.Left == lastFindedWndRect.Right && lastFindedWndRect.Top == lastFindedWndRect.Bottom)
                            return;
                        rectChoosed = true;
                        Invalidate();
                    }
                }
                else
                {
                    if (formColorPick.Visible)
                        WindowUtils.Hide(formColorPick.Handle);
                    WindowUtils.Top(formEditScreenShutTools.Handle);
                    if (currentSizeType == SizeType.Move && formEditScreenShutTools.CurrentTools != PaintTools.None)
                    {
                        //工具结束
                        paintBox.OnMouseUp(e);
                    }
                }
            }
        }
        private void FormChooseWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (bitmapBg != null)
                g.DrawImage(bitmapBg, 0, 0, Width, Height);

            if (paintBox != null)
            {
                Graphics gB = Graphics.FromImage(bitmapDrawingBoard);
                paintBox.OnPaint(new PaintEventArgs(gB, e.ClipRectangle));
                gB.Dispose();
            }

            if (bitmapDrawingBoard != null)
                g.DrawImage(bitmapDrawingBoard, 0, 0, Width, Height);

            if (lastFindedWndRect.Left < 0) lastFindedWndRect.Left = 0;
            if (lastFindedWndRect.Top < 0) lastFindedWndRect.Top = 0;
            if (lastFindedWndRect.Bottom > screenHeight) lastFindedWndRect.Bottom = screenHeight;
            if (lastFindedWndRect.Right > screenWidth) lastFindedWndRect.Left = screenWidth;

            if (rectChoosed || mouseDowned)
            {
                //绘制选择框
                Rectangle r = new Rectangle(lastFindedWndRect.Left + 1, lastFindedWndRect.Top + 1,
                        lastFindedWndRect.Right - lastFindedWndRect.Left - 3,
                        lastFindedWndRect.Bottom - lastFindedWndRect.Top - 3);
                Rectangle rr = new Rectangle(lastFindedWndRect.Left, lastFindedWndRect.Top,
                        lastFindedWndRect.Right - lastFindedWndRect.Left, lastFindedWndRect.Bottom - lastFindedWndRect.Top);

                if (rr.Width == 0 && rr.Height == 0)
                    return;

                g.FillRectangle(brushBg, 0, 0, rr.Left, Height);
                g.FillRectangle(brushBg, rr.Right, 0, Width - rr.Right, Height);
                g.FillRectangle(brushBg, rr.Left, 0, lastFindedWndRect.Right - rr.Left, rr.Top);
                g.FillRectangle(brushBg, rr.Left, rr.Bottom, rr.Right - rr.Left, Height - rr.Bottom);
                g.DrawRectangle(rectPen, rr);

                //绘制上下左右八个圆圈
                g.DrawImage(imagePointDrag, r.Left - imagePointDragSize, r.Top - imagePointDragSize);//上左
                g.DrawImage(imagePointDrag, r.Left - imagePointDragSize, r.Top + (r.Height / 2 - imagePointDragSize / 2));//中左
                g.DrawImage(imagePointDrag, r.Left - imagePointDragSize, r.Top + imagePointDragSize / 2 + r.Height);//下左

                g.DrawImage(imagePointDrag, r.Left + (r.Width / 2), r.Top - imagePointDragSize); //上中
                g.DrawImage(imagePointDrag, r.Left + (r.Width / 2), r.Top + imagePointDragSize / 2 + r.Height);//下中

                g.DrawImage(imagePointDrag, r.Left + r.Width + imagePointDragSize / 2, r.Top - imagePointDragSize);//上右
                g.DrawImage(imagePointDrag, r.Left + r.Width + imagePointDragSize / 2, r.Top + ( r.Height / 2 - imagePointDragSize / 2));//中右
                g.DrawImage(imagePointDrag, r.Left + r.Width + imagePointDragSize / 2, r.Top + imagePointDragSize / 2 + r.Height);//下右

                //矩形区域
                Rectangle rectText = new Rectangle(r.X, r.Top > 23 ? r.Top - 30 : r.Top + 3, 130, 26);
                g.FillRectangle(Brushes.Black, rectText);
                g.DrawString(rr.Left + "," + rr.Top + "  " + rr.Width + " x " + rr.Height, Font, Brushes.White, rectText, stringFormatCenter);
            }
            else
            {  
                //绘制蓝色的框
                if (lastFindedWnd != IntPtr.Zero)
                {
                    Rectangle r = new Rectangle(lastFindedWndRect.Left + 1, lastFindedWndRect.Top + 1,
                        lastFindedWndRect.Right - lastFindedWndRect.Left - 3,
                        lastFindedWndRect.Bottom - lastFindedWndRect.Top - 3);
                    
                    g.DrawRectangle(rectPen, r);

                    g.FillRectangle(brushBg, 0, 0, lastFindedWndRect.Left, Height);
                    g.FillRectangle(brushBg, lastFindedWndRect.Right, 0, Width - lastFindedWndRect.Right, Height);

                    g.FillRectangle(brushBg, lastFindedWndRect.Left, 0, lastFindedWndRect.Right - lastFindedWndRect.Left, lastFindedWndRect.Top);
                    g.FillRectangle(brushBg, lastFindedWndRect.Left, lastFindedWndRect.Bottom, lastFindedWndRect.Right - lastFindedWndRect.Left, Height - lastFindedWndRect.Bottom);
                }
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
                //加上 WS_EX_TRANSPARENT 可以让ChildWindowFromPointEx函数忽略本窗口
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if(m.Msg == API.WM_DISPLAYCHANGE)
            {
                screenWidth = API.GetSystemMetrics(API.SM_CXSCREEN);
                screenHeight = API.GetSystemMetrics(API.SM_CYSCREEN);
                Location = Point.Empty;
                Size = new Size(screenWidth, screenHeight);
            }
        }

    }
}
