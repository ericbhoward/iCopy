using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class PDFIndirectObject : iPDFObject
    {
        protected PDFNumber _ObjID;
        public static int objectCount = 1;
        protected PDFNumber _incremental = 0;
        protected iPDFObject _content;
        protected PDFDictionary _objectDict = null;

        /// <summary>
        /// Optional Dictionary to embed in the object
        /// </summary>
        public PDFDictionary ObjectDict
        {
            get { return _objectDict; }
            set { _objectDict = value; }
        }

        public PDFReference Reference
        {
            get { return new PDFReference(_ObjID, _incremental); }
        }

        public iPDFObject Content
        {
            get { return _content; }
            set { _content = value; }
        }
        
        public PDFIndirectObject()
        {
            _ObjID = objectCount;
            objectCount++;
            _content = null;
        }

        public PDFIndirectObject(iPDFObject obj)
        {
            _ObjID = objectCount;
            objectCount++;
            _content = obj;
        }

        public PDFNumber ObjID
        {
            get { return _ObjID; }
        }

        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            XRef xref = new XRef(stream.Length);
            table.Add((ushort)(int)_ObjID, xref);
            _ObjID.WriteToStream(stream, table);
            stream.Space();
            _incremental.WriteToStream(stream, table);
            stream.Space();
            stream.WriteLine("obj");
            if (_objectDict != null)
                _objectDict.WriteToStream(stream, table);

            _content.WriteToStream(stream, table);
            stream.NewLine();
            stream.WriteLine("endobj");
        }

        public static void ResetObjectCount()
        {
            objectCount = 1;
        }
    }
}
