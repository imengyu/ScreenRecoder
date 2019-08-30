using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenRecoder.App.PaintBox
{
    class PaintEllipse : PaintObject
    {
           public override void Paint(Graphics g, Bitmap thisBitmap, Bitmap baseBitmap)
        {
            base.Paint(g, thisBitmap, baseBitmap);
            using (Pen pen = new Pen(Color, Thickness))
                g.DrawEllipse
(pen, new Rectangle(Location, Size));
        }
    }
}
