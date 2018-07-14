using System;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    public partial class FormMsgSmall : Form
    {
        public FormMsgSmall()
        {
            InitializeComponent();
        }

        public FormMsgSmall(string text, string title, string btnoktext = "确定", string btncantext = "", int showtime = 0)
        {
            InitializeComponent();
            lb_text.Text = text;
            lb_title.Text = title;

            if (string.IsNullOrEmpty(btnoktext))
                btn_ok.Text = "确定";
            else btn_ok.Text = btnoktext;

            if (btncantext == "")
                btn_cancel.Visible = false;
            else
            {
                btn_cancel.Text = btncantext;
                btn_cancel.Visible = true;
            }
            if(showtime!=0)
            {
                timer1.Interval = showtime;
                timer1.Start();
            }
        }

        private void FormMsg_Load(object sender, EventArgs e)
        {
            Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 10;
            Top = Screen.PrimaryScreen.WorkingArea.Height - Height - 10;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Close();
        }
    }
}
