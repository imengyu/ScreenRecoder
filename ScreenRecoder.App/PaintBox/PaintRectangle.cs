using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenRecoder.App.PaintBox
{
    class PaintRectangle : PaintObject
    {
        public override void Paint(Graphics g, Bitmap thisBitmap, Bitmap baseBitmap)
        {
            base.Paint(g, thisBitmap, baseBitmap);
            using (Pen pen = new Pen(Color, Thickness))
                g.DrawRectangle(pen, new Rectangle(Location, Size));
        }
    }
}
