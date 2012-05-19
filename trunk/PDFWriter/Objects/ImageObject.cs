using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.util.zlib;

namespace PDFWriter
{
    public class ImageObject : PDFIndirectObject, iPDFObject, IDisposable
    {
        PDFDictionary _dict = new PDFDictionary();
        Image _source;

        /// <summary>
        /// Creates an ImageObject (a binary stream object containing all the information about an image). If necessary,
        /// the source image is converted to a suitable format
        /// </summary>
        /// <param name="source">Any valid System.Drawing.Image</param>
        public ImageObject(Image source) 
        {
            _source = source;
            //Adds image information to the dictionary
            _dict.Add("Type", "XObject");
            _dict.Add("Subtype", "Image");
            _dict.Add("Width", source.Width);
            _dict.Add("Height", source.Height);

            // Handles the most common pixel formats. If the image has a differnt
            // format it is converted to 24 bit RGB jpeg
            switch (source.PixelFormat)
            {
                case PixelFormat.Format16bppGrayScale:
                    //TODO: Implement
                    break;
                case PixelFormat.Format1bppIndexed: //This is the black & white image
                    _dict.Add("BitsPerComponent", 1);    
                    _dict.Add("ColorSpace","DeviceGray");
                    break;
                case PixelFormat.Format24bppRgb: //No action needed
                    _dict.Add("Filter", "DCTDecode");
                    _dict.Add("ColorSpace", "DeviceRGB");
                    _dict.Add("BitsPerComponent", 8);      
                    break;
                default: //Converts the image to 24bppRGB in order to threat it
                    Bitmap conv = new Bitmap(source.Size.Width, source.Size.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    conv.SetResolution(source.HorizontalResolution, source.VerticalResolution);
                    Graphics g = Graphics.FromImage(conv);
                    conv.SetResolution(source.HorizontalResolution, source.VerticalResolution);
                    g.DrawImage(source, 0,0,source.Size.Width, source.Size.Height);
                    g.Dispose();
                    _source = conv;
                    _dict.Add("Filter", "DCTDecode");
                    _dict.Add("ColorSpace", "DeviceRGB");
                    _dict.Add("BitsPerComponent", 8);
                    source.Dispose(); //Must be called to release memory
                    break;
            }
        }

        public Image Source
        {
            get { return _source; }
        }
        
        public new void WriteToStream(PDFFileStream stream, XRefTable table)
        {
            //Saves the image to a temporary stream in order to get its length
            MemoryStream _stream = new MemoryStream();

            // 1bpp (BW) images must be 
            if (_source.PixelFormat == PixelFormat.Format1bppIndexed)
            {
                Bitmap bmp = (Bitmap)_source;
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                //Use LockBits in order to access the binary data directly
                System.Drawing.Imaging.BitmapData bmpData =
                    bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmp.PixelFormat);

                IntPtr ptr = bmpData.Scan0; //The pointer to the memory area

                // Declare an array to hold the bytes of the bitmap.
                int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                byte[] imageData = new byte[bytes];

                // Copy the pixel values into the array.
                System.Runtime.InteropServices.Marshal.Copy(ptr, imageData, 0, bytes);

                // Unlock the bits.
                bmp.UnlockBits(bmpData);

                // Invert the pixels if necessary
                if (bmp.Palette.Entries[0] == Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF))                    
                    for (int i = 0; i < bytes; i++)
                        imageData[i] = (byte)(imageData[i] ^ 0xff);

                //This function creates an array of bytes with the correct padding
                imageData = Convert(imageData, bmp.Width, bmp.Height);

                //Apply FlateDecode to the stream in order to save some space. The optimal
                //option would be CCITTFaxDecode, but it is too difficult to handle (for me)
                ZDeflaterOutputStream zip = new ZDeflaterOutputStream(_stream, -1);
                _dict.Add("Filter", "FlateDecode");
                zip.Write(imageData, 0, imageData.Length);
                zip.Finish(); //Writes the compressed output to _stream
                bmp.Dispose();
            }
            else
            {
                // Other image formats can be conveniently compressed with JPEG
                _source.Save(_stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            _dict.Add("Length", _stream.Length); //Now we now the stream length

            //Writes the obj that stores the image stream to the PDF File stream
            XRef xref = new XRef(stream.Length);
            table.Add((ushort)(int)_ObjID, xref);
            _ObjID.WriteToStream(stream, table);
            stream.Space();
            _incremental.WriteToStream(stream, table);
            stream.Space();

            stream.WriteLine("obj");
            _dict.WriteToStream(stream, table);
            stream.NewLine();

            //Write image stream
            stream.WriteLine("stream");
            stream.Write(_stream.ToArray(), 0 , (int)_stream.Length);
            stream.NewLine();
            stream.WriteLine("endstream");
            
            stream.WriteLine("endobj");
        }

        // This code comes from iTextSharp library
        /// <summary>
        /// Converts an array of image data bytes to one that is PDF-Compliant
        /// </summary>
        /// <param name="data">1 BPP Image data</param>
        /// <param name="width">Image width</param>
        /// <param name="height">Image heigth</param>
        /// <returns>A byte array containing image data with the correct padding</returns>
        private byte[] Convert(byte[] data, int width, int height)
        {
            byte[] bdata = new byte[((width + 7) / 8) * height];
            int padding = 0;
            int bytesPerScanline = (int)Math.Ceiling((double)width / 8.0);

            int remainder = bytesPerScanline % 4;
            if (remainder != 0)
            {
                padding = 4 - remainder;
            }

            int imSize = (bytesPerScanline + padding) * height;

            //This if was originally intedend to distinguish between top down and bottom up images. Actually I think
            //that all images produced by the scanner are topdown
            if (false) 
            {
                // Convert the bottom up image to a top down format by copying
                // one scanline from the bottom to the top at a time.

                for (int i = 0; i < height; i++)
                {
                    Array.Copy(data,
                    imSize - (i + 1) * (bytesPerScanline + padding),
                    bdata,
                    i * bytesPerScanline, bytesPerScanline);
                }
            }

            else
            {

                for (int i = 0; i < height; i++)
                {
                    Array.Copy(data,
                    i * (bytesPerScanline + padding),
                    bdata,
                    i * bytesPerScanline,
                    bytesPerScanline);
                }
            }
            return bdata;
        }

        public void Dispose()
        {
            _source.Dispose();
        }
    }
}
