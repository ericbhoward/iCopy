using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class PDFStreamObject : iPDFObject
    {
        byte[] _content;
        long _length;
        PDFDictionary _streamDictionary;

        public PDFStreamObject(byte[] content)
        {
            _length = content.Length;
            _content = content;
            _streamDictionary = new PDFDictionary();
            _streamDictionary.Add("Length", (PDFNumber)_length);
        }
        public PDFStreamObject(string content)
        {
            _streamDictionary = new PDFDictionary();
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            _content = encoding.GetBytes(content);
            _length = content.Length;
            _streamDictionary.Add("Length", (PDFNumber)_length);
        }
        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            _streamDictionary.WriteToStream(stream, table);
            stream.NewLine();
            stream.WriteLine("stream");
            stream.Write(_content, 0, _content.Length);
            stream.NewLine();
            stream.Write("endstream");
        }
    }
}
