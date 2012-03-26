using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class PDFBoolean : iPDFObject
    {
        bool value;
        public PDFBoolean() { value = false; }
        public PDFBoolean(bool b) { value = b; }

        public static implicit operator PDFBoolean(bool b)
        {
            PDFBoolean temp = new PDFBoolean(b);
            return temp;
        }

        public static implicit operator bool(PDFBoolean b)
        {
            bool temp = b.value;
            return temp;
        }

        public override string ToString()
        {
            return value.ToString();
        }


        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            if (value)
                stream.Write("true");
            else
                stream.Write("false");
        }
    }
}
