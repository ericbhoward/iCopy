using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    enum PDFDELIM : byte
    {
        /// <summary>
        /// Null
        /// </summary>
        NUL = 0,
        /// <summary>
        /// Horizontal tab
        /// </summary>
        HT = 9,
        /// <summary>
        /// Line Feed
        /// </summary>
        LF = 10,
        /// <summary>
        /// Form Feed
        /// </summary>
        FF = 12,
        /// <summary>
        /// Carriage return
        /// </summary>
        CR = 13,
        /// <summary>
        /// Space
        /// </summary>
        SP = 32
    }
}
