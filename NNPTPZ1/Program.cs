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
        static int imageWidth, imageHeight;
        static double xMin, xMax, yMin, yMax;
        static string outPath; 

        static List<Complex> roots = new List<Complex>();
        static Polynome polynomial = new Polynome();
        static Polynome polynomialDerived;
        static Color[] colors =
        {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] args)
        {
            ParseArguments(args);
            InitPolynomial();
            PrintPolynomial();
            DrawPolynomialToImage();
        }

        static void ParseArguments(string[] args)
        {
            imageWidth = int.Parse(args[0]);
            imageHeight = int.Parse(args[1]);
            xMin = double.Parse(args[2]);
            xMax = double.Parse(args[3]);
            yMin = double.Parse(args[4]);
            yMax = double.Parse(args[5]);
            outPath = args[6];
        }

        static void InitPolynomial()
        {
            polynomial.Coefficients.Add(new Complex() { RealPart = 1 });
            polynomial.Coefficients.Add(Complex.Zero);
            polynomial.Coefficients.Add(Complex.Zero);
            polynomial.Coefficients.Add(new Complex() { RealPart = 1 });

            polynomialDerived = polynomial.Derive();
        }

        static void PrintPolynomial()
        {
            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerived);
        }

        static void DrawPolynomialToImage()
        {
            Bitmap resultImage = new Bitmap(imageWidth, imageHeight);

            double xStep = (xMax - xMin) / imageWidth;
            double yStep = (yMax - yMin) / imageHeight;

            for (int pixelX = 0; pixelX < imageWidth; pixelX++)
            {
                for (int pixelY = 0; pixelY < imageHeight; pixelY++)
                {
                    Complex current = FindCoordsOfPixel(xStep, yStep, pixelX, pixelY);
                    float iterationCount = PerformCalculation(ref current);
                    int rootIndex = FindSolutionRootNumber(current);
                    Colorize(resultImage, pixelX, pixelY, iterationCount, rootIndex);
                }
            }

            resultImage.Save(outPath);
        }

        static Complex FindCoordsOfPixel(double xStep, double yStep, int pixelX, int pixelY)
        {
            double y = yMin + pixelX * yStep;
            double x = xMin + pixelY * xStep;

            Complex current = new Complex()
            {
                RealPart = x == 0 ? 0.0001 : x,
                ImaginaryPart = (float)(y == 0 ? 0.0001 : y)
            };

            return current;
        }

        static float PerformCalculation(ref Complex current)
        {
            float iterationCount = 0;
            for (int i = 0; i < 30; i++)
            {
                var difference = polynomial.Eval(current).Divide(polynomialDerived.Eval(current));
                current = current.Subtract(difference);

                double differenceMagnitude = Math.Pow(difference.RealPart, 2) 
                    + Math.Pow(difference.ImaginaryPart, 2);
                if (differenceMagnitude >= 0.5)
                    i--;

                iterationCount++;
            }

            return iterationCount;
        }

        static int FindSolutionRootNumber(Complex current)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                double distance = Math.Pow(current.RealPart - roots[i].RealPart, 2)
                    + Math.Pow(current.ImaginaryPart - roots[i].ImaginaryPart, 2);
                if (distance <= 0.01)
                    return i;
            }

            roots.Add(current);
            return roots.Count - 1;
        }

        static void Colorize(Bitmap resultImage, int i, int j, float iterationCount, int id)
        {
            var baseColor = colors[id % colors.Length];
            baseColor = Color.FromArgb(baseColor.R, baseColor.G, baseColor.B);
            baseColor = Color.FromArgb(
                Math.Min(Math.Max(0, baseColor.R - (int)iterationCount * 2), 255), 
                Math.Min(Math.Max(0, baseColor.G - (int)iterationCount * 2), 255), 
                Math.Min(Math.Max(0, baseColor.B - (int)iterationCount * 2), 255));
            resultImage.SetPixel(j, i, baseColor);
        }
    }
}
