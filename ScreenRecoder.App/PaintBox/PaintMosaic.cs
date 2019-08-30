using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenRecoder.App.PaintBox
{
    class PaintMosaic : PaintObject
    {
        private List<Point> points = new List<Point>();
        public int MosaicLevel { get; set; }

        public override void Paint(Graphics g, Bitmap thisBitmap, Bitmap baseBitmap)
        {
            RectangleF rc = RectangleF.Empty;
            PointF pc = PointF.Empty;
            int divideBlock = 10 - MosaicLevel;
            float width = Thickness * 2 * 2;
            if (divideBlock == 0) divideBlock = 1;
            float divideBlockSize = width / divideBlock;
            foreach (Point p in points)
            {
                for(int i = 0; i < divideBlock; i++)
                {
                    int cr = 0, cg = 0, cb = 0;
                    Color cc;
                    for (int j = 0; j < divideBlock; j++)
                    {
                        pc = new PointF(p.X + i * divideBlockSize, p.Y + j * divideBlockSize);
                        rc = new RectangleF(pc.X - divideBlockSize / 2, pc.Y - divideBlockSize / 2 - 5, divideBlockSize, divideBlockSize);
                       
                        for (int k = 0; k < divideBlock; k++)
                        {
                            cc = baseBitmap.GetPixel((int)rc.X + k * 1, (int)rc.Y + k * 1);
                            cr += cc.R;
                            cg += cc.G;
                            cb += cc.B;
                        }

                        using (SolidBrush b = new SolidBrush(Color.FromArgb(cr / divideBlock, cg / divideBlock, cb / divideBlock)))
                            g.FillRectangle(b, rc);

                        cr = 0; cg = 0; cb = 0;
                    }

                }
            }
            base.Paint(g, thisBitmap, baseBitmap);
        }
        public override void Draw(Point p)
        {
            RectangleF rc = Rectangle.Empty;
            foreach (Point po in points)
            {
                rc = new RectangleF(po.X - Thickness, po.Y - Thickness, Thickness * 2, Thickness * 2);
                if (rc.Contains(p))
                    return;
            }
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
