using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenRecoder.App
{
    [DefaultEvent("BtnClick")]
    public partial class IconButton : UserControl
    {
        public IconButton()
        {
            InitializeComponent();
            IconSize = new Size(32, 32);
        }

        public Size IconSize { get; set; }
        public Color PressedColor { get; set; }
        public Color HoverColor { get; set; }
        public Image Icon { get; set; }
        public bool Light { get; set; }

        public event EventHandler BtnClick;

        private void ico_MouseEnter(object sender, EventArgs e)
        {
            BackColor = HoverColor;
        }
        private void ico_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.Transparent;
        }
        private void ico_MouseDown(object sender, MouseEventArgs e)
        {
            BackColor = PressedColor;
        }
        private void ico_MouseUp(object sender, MouseEventArgs e)
        {
            BackColor = HoverColor;
        }
        private void IconButton_MouseClick(object sender, MouseEventArgs e)
        {
            BtnClick?.Invoke(sender, e);
        }
        private void IconButton_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Icon, Width / 2 - IconSize.Width / 2, Height / 2 - IconSize.Height / 2, IconSize.Width, IconSize.Height);
        }
    }
}
