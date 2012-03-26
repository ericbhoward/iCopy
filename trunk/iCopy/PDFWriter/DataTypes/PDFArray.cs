using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class PDFArray : List<iPDFObject>, iPDFObject 
    {

        public PDFArray()
        {        }

        public PDFArray(List<iPDFObject> lst) : base(lst)
        {        }
        
        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            stream.Write("[");
            foreach (iPDFObject obj in this)
            {
                obj.WriteToStream(stream, table);
                stream.WriteByte(32); // Space char
            }
            stream.Seek(stream.Length-1, System.IO.SeekOrigin.Begin);
            stream.Write("]");
        }
    }
}
