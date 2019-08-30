using System.Drawing;

namespace ScreenRecoder.App.PaintBox
{
    class PaintObject
    {
        protected Point _Location = new Point();
        protected Size _Size = new Size();

        protected Point _StartPos = new Point();
        protected Point _EndPos = new Point();

        public Point Location { get { return _Location;  } set { _Location = value; } }
        public Size Size { get { return _Size; } set { _Size = value; } }
        public Color Color { get; set; } = Color.Red;
        public float Thickness { get; set; } = 1;
        public Rectangle Rectangle { get { return new Rectangle(Location, Size); } }

        private void MouseResize(Point p)
        {
            if (p.X > _StartPos.X)
                _Size.Width = p.X - _StartPos.X;
            else
            {
                _Size.Width = _StartPos.X - p.X;
                _Location.X = p.X;
            }

            if (p.Y > _StartPos.Y)
                _Size.Height = p.Y - _StartPos.Y;
            else
            {
                _Size.Height = _StartPos.Y - p.Y;
                _Location.Y = p.Y;
            }
        }

        public virtual void Paint(Graphics g, Bitmap thisBitmap, Bitmap baseBitmap)
        {

        }
        public virtual void Start(Point p)
        {
            _StartPos = p;
            Location = p;
        }
        public virtual void Draw(Point p)
        {
            MouseResize(p);
        }
        public virtual void End(Point p)
        {
            _EndPos = p;
            MouseResize(p);
        }
    }
}
