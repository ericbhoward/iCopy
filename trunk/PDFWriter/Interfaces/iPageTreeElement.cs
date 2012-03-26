using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
   public interface iPageTreeElement : iPDFObject
    {
       PageTreeNode Parent
       {
           get;
           set;
       }
       PDFReference Reference
       {
           get;
       }
    }
}
