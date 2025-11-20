using System.Drawing;
using System.IO;

namespace NNPTPZ1
{
    public static class ImageSaver
    {
        public static void SaveImage(string path, Bitmap bitmap)
        {
            string fullPath = Path.GetFullPath(path);
            string directory = Path.GetDirectoryName(fullPath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            bitmap.Save(fullPath);
        }
    }
}
