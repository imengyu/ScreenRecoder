using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenRecoder.App.PaintBox
{
    class PaintBox : IDisposable
    {
        private FormEditScreenShutTools formEditScreenShutTools;
        private FormScreenShutcut formScreenShutcut;
        private bool _MouseDowned = false;
        private TextBox formScreenShutcutAddText;
        private Point _MousePos = new Point();
        private Font _Font = new Font("宋体", 9f);

        public PaintBox(FormScreenShutcut formScreenShutcut, FormEditScreenShutTools formEditScreenShutTools)
        {
            this.formScreenShutcut = formScreenShutcut;
            this.formEditScreenShutTools = formEditScreenShutTools;
            this.PaintObjects = new List<PaintObject>();
        }

        public PaintObject paintObjectAdd = null;
        public List<PaintObject> PaintObjects { get; private set; }

        public void RollBack()
        {
            if (PaintObjects.Count >= 1)
                PaintObjects.RemoveAt(PaintObjects.Count - 1);
        }
        public void Clear()
        {
            PaintObjects.Clear();
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            _MousePos = e.Location;
            if (_MouseDowned)
            {
                if (paintObjectAdd != null)
                    paintObjectAdd.Draw(e.Location);
                formScreenShutcut.Invalidate();
            }
        }
        public void OnMouseUp(MouseEventArgs e)
        {
            _MousePos = e.Location;
            _MouseDowned = false;
            paintObjectAdd = null;
            formScreenShutcut.Invalidate();
        }
        public void OnMouseDown(MouseEventArgs e)
        {
            _MousePos = e.Location;
            _MouseDowned = true;
            switch (formEditScreenShutTools.CurrentTools)
            {
                case PaintTools.None:
                    break;
                case PaintTools.Rectangle:
                    paintObjectAdd = new PaintRectangle();
                    paintObjectAdd.Thickness = (int)formEditScreenShutTools.CurrentPenWidth;
                    paintObjectAdd.Color = formEditScreenShutTools.CurrentColor;
                    break;
                case PaintTools.Ellipse:
                    paintObjectAdd = new PaintEllipse();
                    paintObjectAdd.Thickness = (int)formEditScreenShutTools.CurrentPenWidth;
                    paintObjectAdd.Color = formEditScreenShutTools.CurrentColor;
                    break;
                case PaintTools.Pen:
                    paintObjectAdd = new PaintPen();
                    paintObjectAdd.Thickness = (int)formEditScreenShutTools.CurrentPenWidth;
                    paintObjectAdd.Color = formEditScreenShutTools.CurrentColor;
                    break;
                case PaintTools.Arrow:
                    paintObjectAdd = new PaintArrow();
                    paintObjectAdd.Thickness = (int)formEditScreenShutTools.CurrentPenWidth;
                    paintObjectAdd.Color = formEditScreenShutTools.CurrentColor;
                    break;
                case PaintTools.Mosaic:
                    paintObjectAdd = new PaintMosaic();
                    paintObjectAdd.Thickness = (int)formEditScreenShutTools.CurrentPenWidth;
                    paintObjectAdd.Color = formEditScreenShutTools.CurrentColor;
                    ((PaintMosaic)paintObjectAdd).MosaicLevel = formEditScreenShutTools.CurrentMosaicLevel;
                    break;
                case PaintTools.Text:
                    if (formScreenShutcutAddText == null)
                    {
                        formScreenShutcutAddText = new TextBox();
                        formScreenShutcutAddText.Font = formEditScreenShutTools.CurrentFont;
                        formScreenShutcutAddText.ForeColor = formEditScreenShutTools.CurrentColor;
                        formScreenShutcutAddText.Multiline = true;
                        formScreenShutcutAddText.LostFocus += FormScreenShutcutAddText_LostFocus;
                        formScreenShutcut.Controls.Add(formScreenShutcutAddText);

                        paintObjectAdd = new PaintText();
                        paintObjectAdd.Thickness = (int)formEditScreenShutTools.CurrentPenWidth;
                        paintObjectAdd.Color = formEditScreenShutTools.CurrentColor;
                        ((PaintText)paintObjectAdd).Font = formEditScreenShutTools.CurrentFont;
                        ((PaintText)paintObjectAdd).TextBox = formScreenShutcutAddText;
                    }
                    break;
            }

            PaintObjects.Add(paintObjectAdd);
            if (paintObjectAdd != null)
                paintObjectAdd.Start(e.Location);
        }
        public void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Transparent);
            //if (_MouseDowned)
            //{
            //    e.Graphics.FillRectangle(Brushes.Black, 20, 40, 220, 66);
            //    e.Graphics.DrawString(_MousePos.ToString() + "\n" + (paintObjectAdd != null? paintObjectAdd.Rectangle.ToString():"No PaintObject"), _Font, Brushes.White, 22, 42);
            //}
            foreach (PaintObject o in PaintObjects)
                o.Paint(e.Graphics, formScreenShutcut.bitmapDrawingBoard, formScreenShutcut.bitmapBg);
        }

        private void FormScreenShutcutAddText_LostFocus(object sender, EventArgs e)
        {
            formScreenShutcutAddText.Parent.Controls.Remove(formScreenShutcutAddText);
            formScreenShutcutAddText = null;
            formScreenShutcut.Invalidate();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    PaintObjects.Clear();
                    PaintObjects = null;
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~PaintBox() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
