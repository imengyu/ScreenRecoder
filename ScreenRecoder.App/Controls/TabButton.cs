using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRecoder.App.Controls
{
    /// <summary>
    /// 标签按钮
    /// </summary>
    class TabButton : Control
    {
        private bool _MouseEntered = false;
        private StringFormat _StringFormat = null;

        public TabButton()
        {
            _StringFormat = new StringFormat();
            _StringFormat.Alignment = StringAlignment.Center;
            _StringFormat.LineAlignment = StringAlignment.Center;
        }

        public Color HoverColor { get; set; } = Color.FromArgb(66, 66, 66);
        public Image Image { get; set; }
        public Size ImageSize { get; set; } = new Size(20,20);
        public bool Active { get; set; }
        public void SetActive(bool b)
        {
            Active = b;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_MouseEntered || Active)
            {
                using (SolidBrush bgBrush = new SolidBrush(HoverColor))
                    e.Graphics.FillRectangle(bgBrush, new Rectangle(0, 0, Width, Height));
            }
            if (Image != null)
            {
                e.Graphics.DrawImage(Image, Width / 2 - Image.Width / 2,
                 Height / 2 - Image.Height / 2, ImageSize.Width, ImageSize.Height);
            }
            using (SolidBrush drawBrush = new SolidBrush(base.ForeColor))
            {
                e.Graphics.DrawString(base.Text, base.Font, drawBrush, new Rectangle(0,0,Width, Height), _StringFormat);
            }
            base.OnPaint(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            _MouseEntered = true;
            base.OnMouseEnter(e);
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            _MouseEntered = false;
            base.OnMouseLeave(e);
            Invalidate();
        }
    }
}
