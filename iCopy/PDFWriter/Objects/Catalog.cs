using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class Catalog : PDFIndirectObject
    {
        PDFDictionary _dict = new PDFDictionary();
        PageTreeNode _root;

        public Catalog(PageTreeNode Root)
        {
            _root = Root;
            Content = _dict;
        }

        public PageTreeNode PageTreeRoot
        {
            get { return _root; }
            set { _root = value; }
        }

        public new void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            _dict.Add("Type", (PDFName)"Catalog");
            _dict.Add("Pages", _root.Reference);
            _root.WriteToStream(stream, table);
            base.WriteToStream(stream, table);
        }
    }
}
