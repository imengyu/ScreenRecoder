using ScreenRecoder.App.Api;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    public partial class FormDebug : Form
    {
        public FormDebug()
        {
            InitializeComponent();
            formDebug = this;
        }

        private static FormDebug formDebug = null;

        public static void HideShowedWindow()
        {
            if (formDebug != null)
                WindowUtils.Hide(formDebug.Handle);
        }

        private Pen borderPen = new Pen(Color.FromArgb(56, 56, 56));

        private void FormDebug_Load(object sender, EventArgs e)
        {
           
        }
        private void FormDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        private void FormDebug_Shown(object sender, EventArgs e)
        {
            
        }
        private void FormDebug_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, Width - 1, Height - 1));
        }
        private void FormDebug_MouseDown(object sender, MouseEventArgs e)
        {
            WindowUtils.Move(Handle);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
