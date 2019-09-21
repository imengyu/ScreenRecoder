using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenRecoder.App.PaintBox
{
    class PaintArrow : PaintObject
    {
        private Point start = new Point();
        private Point end = new Point();

        public override void Paint(Graphics g, Bitmap thisBitmap, Bitmap baseBitmap)
        {
            using (Pen pen = new Pen(Color, Thickness))
            {
                int wArrowCapSize = 6;
                if (Thickness == 2) wArrowCapSize = 7;
                if (Thickness == 4) wArrowCapSize = 9;
                AdjustableArrowCap lineCap = new AdjustableArrowCap(wArrowCapSize, wArrowCapSize, true);
                pen.StartCap = LineCap.Triangle;
                pen.CustomEndCap = lineCap;
                g.DrawLine(pen, start, end);
            }
            base.Paint(g, thisBitmap, baseBitmap);
        }
        public override void Draw(Point p)
        {
            end = p;
            base.Draw(p);
        }
        public override void Start(Point p)
        {
            start = p;
            base.Start(p);
        }
        public override void End(Point p)
        {
            end = p;
            base.End(p);
        }
    }
}
