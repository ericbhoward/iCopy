using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PDFWriter
{
    public class PDFNumber : iPDFObject
    {
        double _value;

        public PDFNumber(double f)
        {
            _value = f;
        }

        public static implicit operator PDFNumber(double f)
        {
            PDFNumber temp = new PDFNumber(f);
            return temp;
        }

        public static explicit operator PDFNumber(int i)
        {
            PDFNumber temp = new PDFNumber(i);
            return temp;
        }

        public static implicit operator double(PDFNumber b)
        {
            double temp = b._value;
            return temp;
        }

        public static PDFNumber operator + (PDFNumber a, PDFNumber b) {
            PDFNumber temp = new PDFNumber(a._value);
            temp._value += b._value;
            return temp;
        }

        public static explicit operator int(PDFNumber b)
        {
            int temp = Convert.ToInt32(b._value);
            return temp;
        }


        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture.NumberFormat);
        }

        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            stream.Write(_value.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }
    }
}
