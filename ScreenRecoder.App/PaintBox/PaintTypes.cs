using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenRecoder.App.PaintBox
{
    public enum PaintPenWidth
    {
        Thing  = 2,
        Normal = 4,
        Bold = 8
    }
    public enum PaintTools
    {
        None,
        Rectangle,
        Ellipse,
        Pen,
        Arrow,
        Mosaic,
        Text
    }
}
