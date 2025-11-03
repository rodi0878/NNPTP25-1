using System;
using System.Drawing;
using System.IO;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var config = FractalConfig.FromArgs(args);
            if (!config.IsValid)
            {
                Console.WriteLine("Usage: NNPTPZ1 <width> <height> <xmin> <xmax> <ymin> <ymax> [outputPath] [iterations] [shadingSpeed] [coeff0 coeff1 ...]");
                return;
            }

            var polynomial = Mathematics.Polynomial.FromArgs(args);
            var derivative = polynomial.Derive();

            Bitmap bmp = FractalEngine.Generate(config, polynomial, derivative);
            var outPath = config.OutputPath ?? "../../../out.png";
            var fullPath = Path.GetFullPath(outPath);
            bmp.Save(fullPath);
            Console.WriteLine($"Fraktál uložen do: {fullPath}");
        }

    }

}
