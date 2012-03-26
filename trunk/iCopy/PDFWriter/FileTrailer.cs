using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class FileTrailer
    {
        PDFDictionary _dict = new PDFDictionary();
        int _size;
        Catalog _catalog;

        public FileTrailer(int size, Catalog cat)
        {
            _size = size;
            _catalog = cat;
            _dict.Add("Size", (PDFNumber)size);
            _dict.Add("Root", _catalog.Reference);
        }

        public void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            stream.WriteLine("trailer");
            _dict.WriteToStream(stream, table);
            stream.NewLine();
            stream.WriteLine("startxref");
            stream.WriteLine(table.Position.ToString());
            stream.Write("%%EOF");
        }

    }
}
