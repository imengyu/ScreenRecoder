using System;
using System.Drawing;
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
        public FormMsg(string text, string title, string btnoktext = "确定", string btncantext = "", string wndtitle = "", Image icon = null)
        {
            InitializeComponent();
            lb_text.Text = text;
            lb_title.Text = title;

            if (icon != null)
                pb_ico.Image = icon;

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

        private Pen borderPen = new Pen(Color.FromArgb(56, 56, 56));

        private void FormMsg_Load(object sender, EventArgs e)
        {
            lb_window_title.Text = Text;
        }
        private void FormMsg_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, Width - 1, Height - 1));
        }
        private void FormMsg_TextChanged(object sender, EventArgs e)
        {
            lb_window_title.Text = Text;
        }

        private void pl_title_MouseDown(object sender, MouseEventArgs e)
        {
            API.WindowMove(Handle);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
