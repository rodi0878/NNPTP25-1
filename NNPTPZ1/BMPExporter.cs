using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System;
using Mathematics;

namespace NNPTPZ1
{
    public class BMPExporter
    {
        private readonly ArgumentProcessor argumentProcessor;

        public BMPExporter(ArgumentProcessor argumentProcessor)
        {
            this.argumentProcessor = argumentProcessor;
        }

        /// Exports the given bitmap
        /// Console.WriteLine(string) is used so that user gets some feedback as to what happened
        public void ExportBitmap(Bitmap bmp)
        {
            bmp.Save(argumentProcessor.Output ?? "out.png");
            Console.WriteLine("Exported the bitmap as {0}", argumentProcessor.Output ?? "Default path was used");
        }
    }
}
