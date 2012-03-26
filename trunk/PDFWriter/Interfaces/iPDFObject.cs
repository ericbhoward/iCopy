using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public interface iPDFObject
    {
         void WriteToStream(PDFFileStream stream, XRefTable table);
    }
}