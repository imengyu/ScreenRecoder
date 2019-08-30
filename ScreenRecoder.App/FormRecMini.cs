using ScreenRecoder.App.Api;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    //迷你录制窗口
    public partial class FormRecMini : Form
    {
        public FormRecMini()
        {
            InitializeComponent();
        }

        public void SetTime(string s)
        {
            lb_time.Text = s;
        }
        public void SetPlayPause(bool p)
        {
            if(p)
            {
                toolTip1.SetToolTip(btn_pause, "暂停录像");
            }
            else
            {
                toolTip1.SetToolTip(btn_pause, "继续录像");
            }
        }
        public void SetPauseIcon(Image i)
        {
            btn_pause.Icon = i;
            btn_pause.Invalidate();
        }
        public void SetStopIcon(Image i)
        {
            btn_stop.Icon = i;
            btn_stop.Invalidate();
        }

        private void btn_stop_BtnClick(object sender, EventArgs e)
        {
            FormMain.formMain.btn_stop_BtnClick(sender, e);
        }
        private void btn_pause_BtnClick(object sender, EventArgs e)
        {
            FormMain.formMain.btn_pause_BtnClick(sender, e);
        }
        private void btn_huge_BtnClick(object sender, EventArgs e)
        {
            WindowUtils.Hide(Handle);
            WindowUtils.Show(FormMain.formMain.Handle);
        }

        private void lb_time_MouseDown(object sender, MouseEventArgs e)
        {
            WindowUtils.Move(Handle);
        }

        private void FormRecMini_Load(object sender, EventArgs e)
        {
            Height = 36;
            lb_time.Height = 36;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= API.WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        //窗口背景和边框绘画 
        private Pen borderPen = new Pen(Color.FromArgb(66, 66, 66));

        private void FormRecMini_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, Width - 1, Height - 1));
        }
    }
}
