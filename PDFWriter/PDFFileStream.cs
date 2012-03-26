using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class PDFFileStream : System.IO.FileStream
    {
        public PDFFileStream(string path, FileMode mode) : base (path, mode) {}

        public void Space()
        {
            WriteByte((byte)PDFDELIM.SP);
        }

        public void NewLine()
        {
            WriteByte((byte)PDFDELIM.LF);
        }
        
        public void Write(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(str);
            Write(bytes, 0, bytes.Length);
        }

        public void WriteLine(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(str);
            Write(bytes,0,bytes.Length);
            WriteByte((byte)PDFDELIM.LF);
        }
    }
}
