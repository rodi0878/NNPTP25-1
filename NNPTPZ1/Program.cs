using System;
using System.Drawing;
using NNPTPZ1.Mathematics;
using NNPTPZ1.Rendering;

namespace NNPTPZ1
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 7)
            {
                Console.WriteLine("Usage: NNPTPZ1 <width> <height> <xmin> <xmax> <ymin> <ymax> <outputPath>");
                Console.WriteLine("Example: NNPTPZ1 800 600 -2 2 -2 2 out.png");
                return;
            }

            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string output = args[6];

            // polynomial: 1 + x^3 (kept identical to original)
            var p = new Polynomial();
            p.Coefficients.Add(new ComplexNumber(1, 0));
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(new ComplexNumber(1, 0));

            var opts = new FractalOptions
            {
                Width = width,
                Height = height,
                XMin = xmin,
                XMax = xmax,
                YMin = ymin,
                YMax = ymax,
                OutputPath = output
            };

            var renderer = new NewtonFractalRenderer();
            using (Bitmap bmp = renderer.Render(p, opts))
            {
                bmp.Save(opts.OutputPath ?? "../../../out.png");
            }
        }
    }
}
