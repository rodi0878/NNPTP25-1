using System;
using System.Collections.Generic;
using System.Drawing;
using Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        const string DEFAULT_OUTPUT_PATH = "../../../out.png";
        static int imageWidth, imageHeight;
        static double xMin, xMax, yMin, yMax;
        static string outputPath;
        static readonly Color[] Colors =
        {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        static List<ComplexNumber> Roots = new List<ComplexNumber>();
        static Polynome Polynomial = new Polynome();
        static Polynome PolynomialDerivative;

        static void Main(string[] args)
        {
            ParseArguments(args);

            Polynomial = PreparePolynomial();
            PolynomialDerivative = Polynomial.Derive();

            PrintPolynomial();

            Bitmap fractalImage = GenerateFractalImage();

            fractalImage.Save(outputPath ?? DEFAULT_OUTPUT_PATH);
        }

        static Bitmap GenerateFractalImage()
        {
            Bitmap fractalImage = new Bitmap(imageWidth, imageHeight);

            double xStep = (xMax - xMin) / imageWidth;
            double yStep = (yMax - yMin) / imageHeight;

            for (int row = 0; row < imageWidth; row++)
            {
                for (int column = 0; column < imageHeight; column++)
                {
                    ProcessPixel(fractalImage, row, column, xStep, yStep);
                }
            }

            return fractalImage;
        }

        static void ProcessPixel(Bitmap fractalImage, int row, int column, double xStep, double yStep)
        {
            double yCoordinate = yMin + row * yStep;
            double xCoordinate = xMin + column * xStep;

            ComplexNumber complexPoint = new ComplexNumber()
            {
                RealPart = xCoordinate,
                ImaginaryPart = (float)(yCoordinate)
            };

            if (complexPoint.RealPart == 0)
                complexPoint.RealPart = 0.0001;
            if (complexPoint.ImaginaryPart == 0)
                complexPoint.ImaginaryPart = 0.0001f;

            float iterations = PerformNewtonIterations(ref complexPoint);

            int rootIndex = IdentifyRoot(complexPoint);

            ApplyColor(fractalImage, row, column, rootIndex, iterations);
        }

        static float PerformNewtonIterations(ref ComplexNumber complexPoint)
        {
            float iterations = 0;
            for (int iteration = 0; iteration < 30; iteration++)
            {
                var difference = Polynomial.Eval(complexPoint).Divide(PolynomialDerivative.Eval(complexPoint));
                complexPoint = complexPoint.Subtract(difference);

                if (Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2) >= 0.5)
                {
                    iteration--;
                }
                iterations++;
            }

            return iterations;
        }

        static int IdentifyRoot(ComplexNumber complexPoint)
        {
            bool isKnownRoot = false;
            int rootIndex = 0;

            for (int i = 0; i < Roots.Count; i++)
            {
                if (Math.Pow(complexPoint.RealPart - Roots[i].RealPart, 2) + Math.Pow(complexPoint.ImaginaryPart - Roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    isKnownRoot = true;
                    rootIndex = i;
                }
            }

            if (!isKnownRoot)
            {
                Roots.Add(complexPoint);
                rootIndex = Roots.Count - 1;
            }

            return rootIndex;
        }

        static void ApplyColor(Bitmap fractalImage, int row, int column, int rootIndex, float iterations)
        {
            var baseColor = Colors[rootIndex % Colors.Length];
            baseColor = Color.FromArgb(baseColor.R, baseColor.G, baseColor.B);
            baseColor = Color.FromArgb(
                Math.Min(Math.Max(0, baseColor.R - (int)iterations * 2), 255),
                Math.Min(Math.Max(0, baseColor.G - (int)iterations * 2), 255),
                Math.Min(Math.Max(0, baseColor.B - (int)iterations * 2), 255)
            );
            fractalImage.SetPixel(column, row, baseColor);
        }

        static void ParseArguments(string[] arguments)
        {
            imageWidth = int.Parse(arguments[0]);
            imageHeight = int.Parse(arguments[1]);
            xMin = int.Parse(arguments[2]);
            xMax = int.Parse(arguments[3]);
            yMin = int.Parse(arguments[4]);
            yMax = int.Parse(arguments[5]);

            // We have defined default output path, so this argument is optional
            outputPath = arguments.Length > 6 ? arguments[6] : null;
        }

        static void PrintPolynomial()
        {
            Console.WriteLine(Polynomial);
            Console.WriteLine(PolynomialDerivative);
        }

        static Polynome PreparePolynomial()
        {
            var polynomial = new Polynome();
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            return polynomial;
        }
    }
}
