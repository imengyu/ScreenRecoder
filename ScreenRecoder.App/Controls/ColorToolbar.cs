using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRecoder.App.Controls
{
    [DefaultEvent("ChoosedColorChanged")]
    class ColorToolbar : Control
    {
        private Color _ChoosedColor = Color.Red;

        public List<Color> Colors { get; private set; } = new List<Color>();
        public Color ChoosedColor
        {
            get { return _ChoosedColor; }
            set {
                _ChoosedColor = value;
                ChoosedColorChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }
        public Size ColorBlockSize { get; set; } = new Size(32, 32);

        public event EventHandler ChoosedColorChanged;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int selectedIndex = e.X / ColorBlockSize.Width;
                if (selectedIndex < Colors.Count)
                    ChoosedColor = Colors[selectedIndex];
            }
            base.OnMouseClick(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            int selectedIndex = -1;
            for(int i =0; i< Colors.Count; i++)
            {
                if (Colors[i] == ChoosedColor)
                    selectedIndex = i;
                using (SolidBrush brush = new SolidBrush(Colors[i]))
                    e.Graphics.FillRectangle(brush, new Rectangle(i * ColorBlockSize.Width + 2, (Height - ColorBlockSize.Height) / 2, ColorBlockSize.Width, ColorBlockSize.Height));
            }
            if (selectedIndex != -1)
            {
                int x = selectedIndex * ColorBlockSize.Width;
                e.Graphics.DrawRectangle(Pens.White, x + 1, (Height - ColorBlockSize.Height + 2) / 2 - 2, ColorBlockSize.Width + 1, ColorBlockSize.Height + 1);
            }
            base.OnPaint(e);
        }

    }
}
