using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class PDFDictionary : Dictionary<PDFName, iPDFObject>, iPDFObject
    {
        public PDFDictionary() { }
        public PDFDictionary(Dictionary<PDFName, iPDFObject> dictionary) : base(dictionary) { }

        public void Add(PDFName name, PDFNumber val) {
            base.Add(name, val);
        }

        public void Add(PDFName name, PDFName val)
        {
            base.Add(name, val);
        }

        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            stream.WriteLine("<<");
            foreach (KeyValuePair<PDFName, iPDFObject> pair in this)
            {
                pair.Key.WriteToStream(stream, table);
                stream.Space();
                pair.Value.WriteToStream(stream, table);
                stream.NewLine();
            }
            stream.Write(">>");
        }

    }
}
