using System;
using System.Drawing;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {

        private static readonly string outputPath = "../../../out.png";

        static void Main(string[] args)
        {
            NewtonFractalParameters parameters = CommandLineArgumentsParser.Parse(args);
            NewtonFractalGenerator generator = new NewtonFractalGenerator(parameters);
            Bitmap bitmap = generator.Generate();
            bitmap.Save(parameters.OutputPath ?? outputPath);
        }
    }
}
