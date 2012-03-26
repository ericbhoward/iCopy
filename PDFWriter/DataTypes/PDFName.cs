using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class PDFName : iPDFObject
    {
        string _value;

        public PDFName() { _value = ""; }

        public PDFName(string s) 
        {
            if (s.Contains(" "))
                throw new ArgumentException("Can't contain spaces");
            _value = s;
        }

        public static implicit operator PDFName(string s)
        {
            PDFName temp = new PDFName(s);
            return temp;
        }

        public static implicit operator string(PDFName n)
        {
            return n._value;
        }

        public override string ToString()
        {
            return _value;
        }

        public static bool operator ==(PDFName a, PDFName b)
        {
            return (a._value == b._value);
        }

        public static bool operator !=(PDFName a, PDFName b)
        {
            return (a._value != b._value);
        }


        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            stream.Write("/" + _value);
        }
    }
}
