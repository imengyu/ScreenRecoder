using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRecoder.App.PaintBox
{
    class PaintText : PaintObject
    {
        public string Text { get; set; }
        public Font Font { get; set; }
        public bool Editing { get; set; } = true;
        public TextBox TextBox { get; set; }

        public PaintText()
        {
            brushEditing = new Pen(Color.DodgerBlue, 1.5f);
            brushEditing.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        private Pen brushEditing = null;

        public override void Paint(Graphics g, Bitmap thisBitmap, Bitmap baseBitmap)
        {
            if (Editing)
                g.DrawRectangle(brushEditing, Rectangle);
            else
            {
                using (SolidBrush brush = new SolidBrush(Color))
                    g.DrawString(Text, Font, brush, Rectangle);
            }
            base.Paint(g, thisBitmap, baseBitmap);
        }
        public override void Start(Point p)
        {
            TextBox.Location = p;
            base.Start(p);
        }
        public override void End(Point p)
        {
            base.End(p);
            if (Size.Width < 60) _Size.Width = 60;
            if (Size.Height < 30) _Size.Width = 30;

            TextBox.Location = Location;
            TextBox.Size = Size;
            TextBox.Show();
            TextBox.LostFocus += TextBox_LostFocus;
            TextBox.Focus();
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            Editing = false;
            Text = TextBox.Text;
        }
    }
}
