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
        }
        public void SetStopIcon(Image i)
        {
            btn_stop.Icon = i;
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
            API.WindowHide(Handle);
            API.WindowShow(FormMain.formMain.Handle);
        }

        private void lb_time_MouseDown(object sender, MouseEventArgs e)
        {
            API.WindowMove(Handle);
        }

        private void FormRecMini_Load(object sender, EventArgs e)
        {
            Height = 36;
            lb_time.Height = 36;
        }
    }
}
