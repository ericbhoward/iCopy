using System;
using System.Collections.Generic;
using System.Text;

namespace PDFWriter
{
    public class XRef
    {
        long _Position;
        ushort _increment;
        bool _free;

        public XRef(long Position)
        {
            _increment = 0;
            _Position = Position;
            _free = false;
        }

        /// <summary>
        /// Creates XRef object specifying if it represents a free or non free object
        /// </summary>
        /// <param name="Position">If NON-FREE, the offset of the object in the file stream. If FREE, the number of the next free object</param>
        /// <param name="free"></param>
        public XRef(long Position, bool free)
        {
            _free = free;
            _Position = Position;
            if (free && _Position == 0)
                _increment = 65535;
            else
                _increment = 0;
        }

        /// <summary>
        /// Creates XRef object specifying its increment, and if it represents a free or non free object
        /// </summary>
        /// <param name="Position">If NON-FREE, the offset of the object in the file stream. If FREE, the number of the next free object</param>
        /// <param name="increment">Increment of the version of the object. Should be 0 usually. 65535 if this is the leading xref</param>
        /// <param name="free"></param>
        public XRef(long Position, ushort increment, bool free)
        {
            _free = free;
            _Position = Position;
            _increment = 0;
        }

        public override string ToString()
        {
            string result = _Position.ToString("D10");
            result += (char)PDFDELIM.SP;
            result += _increment.ToString("D5");
            result += (char)PDFDELIM.SP;
            if (_free)
                result += "f";
            else
                result += "n";
            result += (char)PDFDELIM.CR;
            result += (char)PDFDELIM.LF;
            return result;
        }
    }

    public class XRefTable : SortedList<ushort,XRef>
    {
        long _position = 0;

        public long Position
        {
            get { return _position; }
            set { _position = value; }
        }


        public XRefTable() : base() {
            this.Add(0, new XRef(0, true));
        }

        public void WriteToStream(PDFFileStream stream)
        {
            _position = stream.Length;
            stream.WriteLine("xref");
            ushort i = 0;
            ushort n = 0;
            string buffer;
            while (n < this.Count)
            {

                buffer = "";
                i = n;
                while (this.Keys.Contains(i) && i < this.Count)
                {
                    buffer += this[i].ToString();
                    i++;
                }

                stream.WriteLine(string.Format("{0} {1}", n, i - n));
                stream.Write(buffer);
                n = i;

                while(n < this.Count && !this.ContainsKey(n)) {
                    n++;
                }
            }
        }
    }
}
