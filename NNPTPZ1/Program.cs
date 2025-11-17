using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Threading;

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
            if (args.Length < 7)
            {
                Console.WriteLine("No arguments provided, using defaults:");
                Console.WriteLine("NNPTPZ1 800 800 -2 2 -2 2 out.png");

                args = new[]
                {
                    "800",         // width
                    "800",         // height
                    "-2",          // xmin
                    "2",           // xmax
                    "-2",          // ymin
                    "2",           // ymax
                    "out.png"      // outputPath
                };
            }

            var config = FractalConfig.FromArgs(args);

            // Polynomial: x^3 + 1
            var polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber(1, 0));  // constant term
            polynomial.Coefficients.Add(ComplexNumber.Zero);       // x
            polynomial.Coefficients.Add(ComplexNumber.Zero);       // x^2
            polynomial.Coefficients.Add(new ComplexNumber(1, 0));  // x^3

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomial.Derive());

            var generator = new NewtonFractalGenerator(polynomial);

            using (Bitmap bitmap = generator.Generate(config))
            {
                bitmap.Save(config.OutputPath ?? "../../../out.png");
            }

            Console.WriteLine("Hotovo, obrázek uložen do " + (config.OutputPath ?? "../../../out.png"));
            Console.ReadKey(); // aby se okno hned nezavřelo při F5

        }
    }
}


