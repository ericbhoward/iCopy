using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
   public class PDFReference : iPDFObject
    {
        PDFNumber _ObjectID;
        PDFNumber _incremental;

        public PDFReference()
        {
            _ObjectID = 0;
            _incremental = 0;
        }

        public PDFReference(PDFNumber ObjID)
        {
            _ObjectID = ObjID;
            _incremental = 0;
        }

        public PDFReference(PDFNumber ObjID, PDFNumber incremental)
        {
            _ObjectID = ObjID;
            _incremental = incremental;
        }

        public PDFNumber Incremental
        {
            get { return _incremental; }
            set { _incremental = value; }
        }

        public PDFNumber ObjectID
        {
            get { return _ObjectID; }
            set { _ObjectID = value; }
        }

        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            _ObjectID.WriteToStream(stream, table);
            stream.Space();
            _incremental.WriteToStream(stream, table);
            stream.Space();
            stream.Write("R");
        }
    }
}
