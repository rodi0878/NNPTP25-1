using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    internal class BitmapExporter
    {
        public static void SaveBitmap(Bitmap bmp, string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");
            bmp.Save(path);
        }
    }
}
