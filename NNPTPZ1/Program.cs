using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

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
            var (width, height, xmin, xmax, ymin, ymax, output) = ParseArguments(args);

            Bitmap fractalImage = new Bitmap(width, height);

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            List<ComplexNumber> koreny = new List<ComplexNumber>();
            var (polynomial, derivativePolynomial) = CreatePolynomial();

            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    Color pixelColor = ComputePixelColor(x, y, polynomial, derivativePolynomial, koreny, colors);
                    fractalImage.SetPixel(j, i, pixelColor);
                }
            }
            fractalImage.Save(output ?? "../../../out.png");
        }

        /// <summary>
        /// Parses command-line arguments into named variables.
        /// </summary>
        private static (int width, int height, double xmin, double xmax, double ymin, double ymax, string output) ParseArguments(string[] args)
        {
            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string output = args[6];

            return (width, height, xmin, xmax, ymin, ymax, output);
        }
        private static (Polynomial p, Polynomial pd) CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });

            Polynomial derivativePolynomial = polynomial.GetDerivative();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivativePolynomial);

            return (polynomial, derivativePolynomial);
        }
        private static Color ComputePixelColor(double x, double y, Polynomial polynomial, Polynomial derivativePolynomial, List<ComplexNumber> roots,
                                                Color[] colors)
        {
            ComplexNumber currentEstimate = new ComplexNumber { Real = x, Imaginary = y };

            if (currentEstimate.Real == 0) currentEstimate.Real = 0.0001;
            if (currentEstimate.Imaginary == 0) currentEstimate.Imaginary = 0.0001f;

            // Newton’s iteration
            int iterations = 0;
            for (int i = 0; i < 30; i++)
            {
                var diff = polynomial.Evaluate(currentEstimate).Divide(derivativePolynomial.Evaluate(currentEstimate));
                currentEstimate = currentEstimate.Subtract(diff);

                if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                    i--;

                iterations++;
            }

            // Determine root index
            bool known = false;
            int id = 0;
            for (int j = 0; j < roots.Count; j++)
            {
                if (Math.Pow(currentEstimate.Real - roots[j].Real, 2) + Math.Pow(currentEstimate.Imaginary - roots[j].Imaginary, 2) <= 0.01)
                {
                    known = true;
                    id = j;
                    break;
                }
            }
            if (!known)
            {
                roots.Add(currentEstimate);
                id = roots.Count;
            }

            // Compute pixel color
            var baseColor = colors[id % colors.Length];
            return Color.FromArgb(
                Math.Min(Math.Max(0, baseColor.R - iterations * 2), 255),
                Math.Min(Math.Max(0, baseColor.G - iterations * 2), 255),
                Math.Min(Math.Max(0, baseColor.B - iterations * 2), 255)
            );
        }
    }
}
