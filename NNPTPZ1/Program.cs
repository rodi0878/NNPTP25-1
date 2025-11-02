using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;
using System.Globalization;

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

            int width = int.Parse(args[0], CultureInfo.InvariantCulture);
            int height = int.Parse(args[1], CultureInfo.InvariantCulture);
            double xmin = double.Parse(args[2], CultureInfo.InvariantCulture);
            double xmax = double.Parse(args[3], CultureInfo.InvariantCulture);
            double ymin = double.Parse(args[4], CultureInfo.InvariantCulture);
            double ymax = double.Parse(args[5], CultureInfo.InvariantCulture);
            string output = args[6];

            const int MaxIterations = 30;
            const double ConvergenceThreshold = 0.5;
            const float Epsilon = 0.0001f;
            const double RootTolerance = 0.01;

            var bmp = new Bitmap(width, height);

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            List<Complex> roots = new List<Complex>();

            // TODO: poly should be parameterised?
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficient.Add(new Complex(1, 0));
            polynomial.Coefficient.Add(Complex.Zero);
            polynomial.Coefficient.Add(Complex.Zero);
            polynomial.Coefficient.Add(new Complex(1, 0));
            Polynomial derivative = polynomial.Derive();



            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            // for every pixel in image...
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var ox = FractalOperations.PixelToWorld(i, j, xmin, ymin, xstep, ystep, Epsilon);

                    int it = FractalOperations.NewtonIterate(ref ox, polynomial, derivative, MaxIterations, ConvergenceThreshold);

                    var id = FractalOperations.FindRootIndex(roots, ox, RootTolerance);

                    var baseColor = colors[id % colors.Length];
                    var shaded = FractalOperations.ShadeByIterations(baseColor, it);

                    bmp.SetPixel(j, i, shaded);
                }
            }

                    bmp.Save(output ?? "../../../out.png");
        }

    }

}
