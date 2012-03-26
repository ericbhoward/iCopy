using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    /// <summary>
    /// Represents a PDF document. Has methods to add pages and images And to save the output to file
    /// </summary>
    public class PDFDocument
    {
        PDFDictionary dict = new PDFDictionary();
        
        PageTreeNode _root = new PageTreeNode();
        Catalog _cat;
        FileTrailer _trailer;
        XRefTable _table = new XRefTable();

        List<Page> _pages;

        public int PageCount
        {
            get { return _pages.Count; }
        }

        /// <summary>
        /// Adds a page with <code>img</code> in the center of the page.
        /// </summary>
        /// <param name="img">A System.Drawing.Image</param>
        /// <param name="size">Paper size of the page</param>
        /// <param name="landscape">Orientation (portrait by default)</param>
        public void AddPage(Image img, PaperSize size, bool landscape = false)
        {
            ImageObject imgObj = new ImageObject(img);
            Page page = new Page(_root, size, landscape);
            page.Image = imgObj;
            _root.Kids.Add(page);
        }

        public void AddPage( PaperSize size, bool landscape = false)
        {
            Page page = new Page(_root, size, landscape);
            _root.Kids.Add(page);
        }

        /// <summary>
        /// Saves the document to the provided file path
        /// </summary>
        /// <param name="filepath">A valid file path (.pdf extension)</param>
        public void Save(string filepath)
        {
            PDFFileStream stream = new PDFFileStream(filepath, FileMode.Create);
            stream.WriteLine("%PDF-1.4"); //Header
            _cat.WriteToStream(stream, _table);
            _trailer = new FileTrailer( _table.Count, _cat);
            _table.WriteToStream(stream);
            _trailer.WriteToStream(stream, _table);
            stream.Close();
        }

        public PDFDocument()
        {
            _cat = new Catalog(_root);
        }
    }
}
