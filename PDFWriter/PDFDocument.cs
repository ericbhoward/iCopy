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
        PageTreeNode _root;     //Contains the children pages. This is the root obj of a PDF document
        Catalog _cat;           //PDF Document catalog
        XRefTable _table;       //PDF Reference table
        FileTrailer _trailer;   //PDF Trailer
        
        public int PageCount
        {
            get { return _root.Kids.Count; }
        }

        /// <summary>
        /// Adds a page with <code>img</code> in the center of the page.
        /// </summary>
        /// <param name="img">A System.Drawing.Image</param>
        /// <param name="size">Paper size of the page</param>
        /// <param name="landscape">Orientation (portrait by default)</param>
        /// <param name="center">Tells if the image has to be centered or put at (0,0)</param>
        /// <param name="scaling">Scaling (in %)</param>
        public void AddPage(Image img, PaperSize size, int scaling = 100, bool center = true)
        {
            if (scaling < 1) throw new ArgumentException("Scaling can't be less than 1%");
            ImageObject imgObj = new ImageObject(img);
            Page page = new Page(_root, size, center, scaling);
            page.Image = imgObj;
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

        /// <summary>
        /// Closes the document and releases the memory (eg images)
        /// </summary>
        public void Close()
        {
            foreach (Page pag in _root.Kids)
            {
                pag.Dispose();
            }
            _root.Kids.Clear();
        }

        ~PDFDocument()
        {
            Close();
        }

        public PDFDocument()
        {
            PDFIndirectObject.ResetObjectCount();
            _root = new PageTreeNode();
            _cat = new Catalog(_root);
            _table = new XRefTable();
        }

    }
}
