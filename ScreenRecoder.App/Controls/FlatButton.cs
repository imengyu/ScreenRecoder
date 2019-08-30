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
    /// 扁平按钮
    /// </summary>
    class FlatButton : Control
    {
        private bool _MouseEntered = false;
        private bool _MouseDowned = false;
        private StringFormat _StringFormat = null;

        public FlatButton()
        {
            _StringFormat = new StringFormat();
            _StringFormat.Alignment = StringAlignment.Center;
            _StringFormat.LineAlignment = StringAlignment.Center;
        }

        public Image Image { get; set; }
        public Size ImageSize { get; set; } = new Size(20, 20);
        public Color BorderColor { get; set; } = Color.FromArgb(31, 31, 31);
        public Color HoverColor { get; set; } = Color.FromArgb(66, 66, 66);
        public Color PressColor { get; set; } = Color.FromArgb(36, 36, 36);

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_MouseEntered)
            {
                using (Pen drawPen = new Pen(BorderColor))
                    e.Graphics.DrawRectangle(drawPen, new Rectangle(0, 0, Width - 1, Height - 1));
            }
            if (_MouseDowned)
            {
                using (SolidBrush bgBrush = new SolidBrush(PressColor))
                    e.Graphics.FillRectangle(bgBrush, new Rectangle(0, 0, Width, Height));
            }
            else if (_MouseEntered)
            {
                using (SolidBrush bgBrush = new SolidBrush(HoverColor))
                    e.Graphics.FillRectangle(bgBrush, new Rectangle(0, 0, Width, Height));
            }
            if (Image != null)
            {
                e.Graphics.DrawImage(Image, Width / 2 - ImageSize.Width / 2,
                 Height / 2 - ImageSize.Height / 2, ImageSize.Width, ImageSize.Height);
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
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _MouseDowned = true;
            base.OnMouseDown(e);
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _MouseDowned = false;
            base.OnMouseUp(e);
            Invalidate();
        }
    }
}
