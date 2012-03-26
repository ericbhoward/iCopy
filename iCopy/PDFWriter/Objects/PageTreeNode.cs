using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class PageTreeNode :  PDFIndirectObject, iPageTreeElement
    {
        PDFDictionary _dict;
        PageTreeNode _parent;
        List<iPageTreeElement> _kids;

        public List<iPageTreeElement> Kids
        {
            get { return _kids; }
            set { _kids = value; }
        }

        public PageTreeNode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        void Initialize()
        {
            _dict = new PDFDictionary();
            this.Content = _dict;
            _kids = new List<iPageTreeElement>();
            _dict.Add("Type", (PDFName)"Pages");
        }

        public PageTreeNode()
        {
            Initialize();
        }

        public PageTreeNode(PageTreeNode parent)
        {
            Initialize();
            _parent = parent;
        }

        public PageTreeNode(List<iPageTreeElement> kids)
        {
            Initialize();
            _kids.AddRange(kids);
        }

        public PageTreeNode(PageTreeNode parent, List<iPageTreeElement> kids)
        {
            Initialize();
            _parent = parent;
            _kids.AddRange(kids);
        }

        public new void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            List<iPDFObject> refList = new List<iPDFObject>();
            foreach (iPageTreeElement item in _kids)
            {
                refList.Add(item.Reference);
                item.WriteToStream(stream,table);
            } 
            if (_parent != null)
                _dict.Add("Parent", _parent.Reference);
            if (_kids.Count > 0)
            {
                _dict.Add("Kids", new PDFArray(refList));
                _dict.Add("Count", (PDFNumber)_kids.Count);
            }

            base.WriteToStream(stream,table);
        }
    }
}
