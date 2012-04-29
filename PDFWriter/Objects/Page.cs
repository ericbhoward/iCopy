using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PDFWriter
{
    public class Page : PDFIndirectObject, iPageTreeElement, IDisposable
    {
        PDFDictionary _dict = new PDFDictionary();
        ImageObject _image;
        PDFRectangle _CropBox = null;
        PageTreeNode _parent;
        PDFRectangle _MediaBox = null;
        ContentStream _cont = null;
        
        public ContentStream PageContent
        {
            get { return _cont; }
            set { _cont = value; }
        }

        public Page(PageTreeNode Parent)
        {
            _parent = Parent;
            this.Content = _dict;
        }

        public ImageObject Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public Page(PageTreeNode Parent, PDFRectangle MediaBox)
        {
            _parent = Parent;
            Content = _dict;
            _MediaBox = MediaBox;
        }

        public Page(PageTreeNode Parent, PaperSize size, bool landscape = false)
        {
            _parent = Parent;
            Content = _dict;
            if (landscape)
            switch (size)
                {
                    case PaperSize.A4:
                        _MediaBox = new PDFRectangle(0, 0, 842, 595.22);
                        break;
                    case PaperSize.A3:
                        _MediaBox = new PDFRectangle(0, 0, 1190, 842);
                        break;
                    case PaperSize.Legal:
                        break;
                    case PaperSize.Letter:
                        break;
                    default:
                        break;
                }
            else
                switch (size)
                {
                    case PaperSize.A4:
                        _MediaBox = new PDFRectangle(0, 0, 595.22, 842);
                        break;
                    case PaperSize.A3:
                        _MediaBox = new PDFRectangle(0, 0, 842, 1190);
                        break;
                    case PaperSize.Legal:
                        break;
                    case PaperSize.Letter:
                        break;
                    default:
                        break;
                }
        }

        public PDFRectangle MediaBox
        {
            get { return _MediaBox; }
            set { _MediaBox = value; }
        }

        public PageTreeNode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        
        public new void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            _dict.Add("Parent", _parent.Reference);
            if (_MediaBox != null)
                _dict.Add("MediaBox", _MediaBox);
            if (_image != null)
            {
                PDFDictionary resources = new PDFDictionary();
                if (_image.Source.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale)
                    resources.Add("ProcSet", "ImageB");
                else
                    resources.Add("ProcSet", "ImageC");
                PDFDictionary imgDict = new PDFDictionary();
                //Calculate the position of the lower left corner in order to center the image
                
                double imWidth = (double)_image.Source.Width / ((Bitmap)_image.Source).HorizontalResolution * 72;
                double imHeight = (double)_image.Source.Height / ((Bitmap)_image.Source).VerticalResolution * 72;
                double x = Math.Max((_MediaBox.Width - imWidth) / 2, 0);
                double y = Math.Max((_MediaBox.Height - imHeight) / 2, 0);
                imgDict.Add(String.Format("Im{0}", _image.ObjID), _image.Reference);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture.NumberFormat, "1 0 0 1 {0} {1} cm", x, y));
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture.NumberFormat, "{0} 0 0 {1} 0 0 cm", imWidth, imHeight));
                sb.AppendLine(String.Format("/Im{0} Do", _image.ObjID));
                _cont = new ContentStream(sb.ToString());
                _image.WriteToStream(stream, table);

                resources.Add("XObject", imgDict);
                _dict.Add("Resources", resources);
            }

            if (_cont != null)
            {
                _dict.Add("Contents", _cont.Reference);
                _cont.WriteToStream(stream,table);
            }

            base.WriteToStream(stream, table);
        }

        public void Dispose()
        {
            _image.Dispose();
        }
    }
}
