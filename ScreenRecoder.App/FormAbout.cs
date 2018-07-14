using System;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void btn_about_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new FormMsg("2018.6.28-2099.12.28", "许可证到期时间 2099.12.28").ShowDialog();
        }
    }
}
