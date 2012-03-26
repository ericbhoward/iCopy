using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class ContentStream : PDFIndirectObject, iPDFObject
    {
        string _commands;

        public ContentStream(string commands)
        {
            _commands = commands;
        }

        public string Commands
        {
            get { return _commands; }
            set { _commands = value; }
        }

        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            PDFStreamObject _stream = new PDFStreamObject(_commands);
            Content = _stream;
            base.WriteToStream(stream, table);
        }
    }
}
