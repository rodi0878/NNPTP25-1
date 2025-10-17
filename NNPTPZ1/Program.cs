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
        private static int width, height;
        private static double xMin, xMax, yMin, yMax, xStep, yStep;
        private static string outputFileName;
        private static Bitmap image;
        private static List<ComplexNumber> roots = new List<ComplexNumber>();
        private static Polynomal function;
        private static Polynomal derivative;
        private static readonly Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        static void Main(string[] args)
        {
            try
            {
                Initialise(args);
                PrintFunctions();
                CreateImage();
                SaveImage();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private static void Initialise(string[] args)
        {
            if (args.Length < 7)
            {
                throw new Exception("Zadej následující argumenty: <šířka> <výška> <xMin> <xMax> <yMin> <yMax> <výstupníSoubor>");
            }

            // Loading arguments
            if (!int.TryParse(args[0], out width) ||
                !int.TryParse(args[1], out height))
            {
                throw new Exception("Argumenty 1-2 musí být int (šířka, výška).");
            }

            if (!double.TryParse(args[2], out xMin) ||
                !double.TryParse(args[3], out xMax) ||
                !double.TryParse(args[4], out yMin) ||
                !double.TryParse(args[5], out yMax))
            {
                throw new Exception("Argumenty 3–6 musí být double (xMin, xMax, yMin, yMax).");
            }

            outputFileName = args[6];

            image = new Bitmap(width, height);
            xStep = (xMax - xMin) / width;
            yStep = (yMax - yMin) / height;

            roots = new List<ComplexNumber>();
            function = new Polynomal(
                new ComplexNumber { Real = 1 },
                ComplexNumber.Zero,
                ComplexNumber.Zero,
                new ComplexNumber { Real = 1 }
            );
            derivative = function.Derive();
        }

        private static void PrintFunctions()
        {
            Console.WriteLine(function);
            Console.WriteLine(derivative);
        }

        private static void CreateImage()
        {
            // Iterate over all pixels in the image
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var complex = GetComplexForPixel(i, j);
                    float iterations = ApplyNewtonIteration(ref complex);

                    int rootId = GetRootId(complex);
                    var color = GetPixelColor(rootId, iterations);

                    image.SetPixel(j, i, color);
                }
            }
        }

        private static void SaveImage()
        {
            image.Save(outputFileName ?? "../../../out.png");
        }

        private static ComplexNumber GetComplexForPixel(int i, int j)
        {
            // Maps a pixel position to its corresponding point in the complex plane
            double y = yMin + i * yStep;
            double x = xMin + j * xStep;

            var complex = new ComplexNumber
            {
                Real = x == 0 ? 0.0001 : x,
                Imaginary = (float)(y == 0 ? 0.0001f : y)
            };

            return complex;
        }

        private static float ApplyNewtonIteration(ref ComplexNumber complex)
        {
            float iterations = 0;

            for (int step = 0; step < 30; step++)
            {
                var diff = function.Evaluate(complex) / derivative.Evaluate(complex);
                complex -= diff;

                if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                    step--;

                iterations++;
            }

            return iterations;
        }

        private static int GetRootId(ComplexNumber complex)
        {
            for (int rootIndex = 0; rootIndex < roots.Count; rootIndex++)
            {
                double distanceSquared =
                    Math.Pow(complex.Real - roots[rootIndex].Real, 2) +
                    Math.Pow(complex.Imaginary - roots[rootIndex].Imaginary, 2);

                if (distanceSquared <= 0.01)
                    return rootIndex;
            }

            // New Root
            roots.Add(complex);
            return roots.Count - 1;
        }

        private static Color GetPixelColor(int rootId, float iterations)
        {
            var baseColor = colors[rootId % colors.Length];

            int r = Clamp(baseColor.R - (int)iterations * 2);
            int g = Clamp(baseColor.G - (int)iterations * 2);
            int b = Clamp(baseColor.B - (int)iterations * 2);

            return Color.FromArgb(r, g, b);
        }

        private static int Clamp(int value)
        {
            return Math.Min(Math.Max(0, value), 255);
        }
    }
}
