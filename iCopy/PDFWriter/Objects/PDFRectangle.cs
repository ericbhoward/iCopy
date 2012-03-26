using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class PDFRectangle :  iPDFObject
    {
        PDFNumber _X, _Y, _Width, _Height;

        public PDFNumber X
        {
          get { return _X; }
          set { _X = value; }
        }

        public PDFNumber Y
        {
          get { return _Y; }
          set { _Y = value; }
        }

        public PDFNumber Width
        {
          get { return _Width; }
          set { _Width = value; }
        }

        public PDFNumber Height
        {
          get { return _Height; }
          set { _Height = value; }
        }

        public PDFRectangle(RectangleF rect) {
            _X = rect.X;
            _Y = rect.Y;
            _Width = rect.Width;
            _Height = rect.Height;
        }

        public PDFRectangle(PDFNumber X, PDFNumber Y, PDFNumber Width, PDFNumber Height)
        {
            _X = X;
            _Y = Y;
            _Width = Width;
            _Height = Height;
        }

        public static implicit operator RectangleF(PDFRectangle rect) {
            RectangleF prect = new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
            return prect;
        }

        public static implicit operator PDFRectangle(RectangleF rect) {
            PDFRectangle prect = new PDFRectangle(rect);
            return prect;
        }

        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            List<iPDFObject> arr = new List<iPDFObject>();
            arr.Add(_X);
            arr.Add(_Y);
            arr.Add(_X + _Width);
            arr.Add(_Y + _Height);

            PDFArray array = new PDFArray(arr);
            array.WriteToStream(stream, table);
        }
    }
}
