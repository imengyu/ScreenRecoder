using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenRecoder.App.PaintBox
{
    class PaintPen : PaintObject
    {
        private List<Point> points = new List<Point>();

        public override void Paint(Graphics g, Bitmap thisBitmap, Bitmap baseBitmap)
        {
            using (Pen pen = new Pen(Color, Thickness))
                g.DrawLines(pen, points.ToArray());
            base.Paint(g, thisBitmap, baseBitmap);
        }
        public override void Draw(Point p)
        {
            points.Add(p);
            base.Draw(p);
        }
        public override void Start(Point p)
        {
            points.Add(p);
            base.Start(p);
        }
        public override void End(Point p)
        {
            points.Add(p);
            base.End(p);
        }
    }
}
