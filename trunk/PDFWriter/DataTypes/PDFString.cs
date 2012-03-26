using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class PDFString : iPDFObject        
    {
        string _value;

        public PDFString() { _value = ""; }

        public PDFString(string s) 
        {
            _value = s;
            //TODO: Should check for unbalanced brakets or other invalid characters, see § 7.3.4.2
        }

        public static implicit operator PDFString(string s)
        {
            PDFString temp = new PDFString(s);
            return temp;
        }

        public static implicit operator string(PDFString n)
        {
            return n._value;
        }

        public override string ToString()
        {
            return _value;
        }

        public void WriteToStream( PDFFileStream stream, XRefTable table)
        {
            stream.Write("(" + _value + ")");
        }
    }
}
