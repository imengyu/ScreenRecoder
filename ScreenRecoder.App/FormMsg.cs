using System;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    //一个自制对话框
    public partial class FormMsg : Form
    {
        public FormMsg()
        {
            InitializeComponent();
        }

        public FormMsg(string text, string title, string btnoktext = "确定", string btncantext = "", string wndtitle = "")
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
            if (!string.IsNullOrEmpty(wndtitle))
                Text = wndtitle;
        }
        private void FormMsg_Load(object sender, EventArgs e)
        {

        }
    }
}
