using System;
using System.Drawing;
using System.IO;
using NNPTPZ1.Fractals;

namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 7)
                {
                    Console.WriteLine("Usage: <width> <height> <xmin> <xmax> <ymin> <ymax> <outputfile>");
                    Console.WriteLine("Example: 800 800 -2 2 -2 2 out.png");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }

                int width = int.Parse(args[0]);
                int height = int.Parse(args[1]);
                double xmin = double.Parse(args[2]);
                double xmax = double.Parse(args[3]);
                double ymin = double.Parse(args[4]);
                double ymax = double.Parse(args[5]);
                string outputPath = args[6];

                var generator = new NewtonFractalGenerator(width, height, xmin, xmax, ymin, ymax);

                Console.WriteLine("Generating Newton fractal...");
                using (Bitmap bmp = generator.Generate())
                {
                    string fullPath = Path.GetFullPath(outputPath);
                    Console.WriteLine($"Saving image to: {fullPath}");
                    BitmapExporter.SaveBitmap(bmp, fullPath);
                }

                Console.WriteLine($"Fractal image saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + ex.Message);
                Console.ResetColor();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
