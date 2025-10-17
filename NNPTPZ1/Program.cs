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
        private const int MaxIterations = 30;
        private const double ConvergenceThreshold = 0.5;
        private const double RootMatchThreshold = 0.01;
        private const double ZeroValueReplacement = 0.0001;
        private const int ColorDimmingFactor = 2;

        private static readonly Color[] RootColors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow,
            Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] args)
        {
            var config = ParseArguments(args);
            var polynomial = CreatePolynomial();
            var derivative = polynomial.Derive();

            Console.WriteLine($"Polynomial: {polynomial}");
            Console.WriteLine($"Derivative: {derivative}");

            var fractal = GenerateFractal(config, polynomial, derivative);
            fractal.Save(config.OutputPath);
        }

        private static FractalConfig ParseArguments(string[] args)
        {
            if (args.Length < 7)
                throw new ArgumentException("Not enough arguments.");

            return new FractalConfig
            {
                Width = int.Parse(args[0]),
                Height = int.Parse(args[1]),
                XMin = double.Parse(args[2]),
                XMax = double.Parse(args[3]),
                YMin = double.Parse(args[4]),
                YMax = double.Parse(args[5]),
                OutputPath = args[6]
            };
        }

        private static Polynomial CreatePolynomial()
        {
            var coefficients = new ComplexNumber[]
            {
                new ComplexNumber(1, 0),  // z^3
                ComplexNumber.Zero,        // z^2
                ComplexNumber.Zero,        // z^1
                new ComplexNumber(-1, 0)   // konstanta
            };

            return new Polynomial(coefficients);
        }

        private static Bitmap GenerateFractal(FractalConfig config, Polynomial polynomial, Polynomial derivative)
        {
            var bitmap = new Bitmap(config.Width, config.Height);
            var roots = new List<ComplexNumber>();

            double xStep = (config.XMax - config.XMin) / config.Width;
            double yStep = (config.YMax - config.YMin) / config.Height;

            for (int i = 0; i < config.Height; i++)
            {
                for (int j = 0; j < config.Width; j++)
                {
                    var worldCoord = PixelToWorld(j, i, config, xStep, yStep);
                    var result = FindRoot(worldCoord, polynomial, derivative);
                    var rootId = GetOrAddRoot(result.Root, roots);
                    var color = CalculateColor(rootId, result.Iterations);

                    bitmap.SetPixel(j, i, color);
                }
            }

            return bitmap;
        }

        private static ComplexNumber PixelToWorld(int pixelX, int pixelY, FractalConfig config, double xStep, double yStep)
        {
            double x = config.XMin + pixelX * xStep;
            double y = config.YMin + pixelY * yStep;

            if (x == 0) x = ZeroValueReplacement;
            if (y == 0) y = ZeroValueReplacement;

            return new ComplexNumber(x, y);
        }

        private static NewtonResult FindRoot(ComplexNumber initialGuess, Polynomial polynomial, Polynomial derivative)
        {
            var z = initialGuess;
            int iterations = 0;

            for (int i = 0; i < MaxIterations; i++)
            {
                var diff = polynomial.Evaluate(z) / derivative.Evaluate(z);
                z = z - diff;
                iterations++;

                if (GetMagnitudeSquared(diff) >= ConvergenceThreshold)
                {
                    i--;
                }
            }

            return new NewtonResult { Root = z, Iterations = iterations };
        }

        private static int GetOrAddRoot(ComplexNumber root, List<ComplexNumber> knownRoots)
        {
            for (int i = 0; i < knownRoots.Count; i++)
            {
                if (GetDistanceSquared(root, knownRoots[i]) <= RootMatchThreshold)
                {
                    return i;
                }
            }

            knownRoots.Add(root);
            return knownRoots.Count - 1;
        }

        private static Color CalculateColor(int rootId, int iterations)
        {
            var baseColor = RootColors[rootId % RootColors.Length];

            int dimming = iterations * ColorDimmingFactor;

            return Color.FromArgb(
                ClampColorValue(baseColor.R - dimming),
                ClampColorValue(baseColor.G - dimming),
                ClampColorValue(baseColor.B - dimming)
            );
        }

        private static double GetMagnitudeSquared(ComplexNumber z)
        {
            return z.RealPart * z.RealPart + z.ImaginaryPart * z.ImaginaryPart;
        }

        private static double GetDistanceSquared(ComplexNumber z1, ComplexNumber z2)
        {
            double realDiff = z1.RealPart - z2.RealPart;
            double imagDiff = z1.ImaginaryPart - z2.ImaginaryPart;
            return realDiff * realDiff + imagDiff * imagDiff;
        }

        private static int ClampColorValue(int value)
        {
            return Math.Min(Math.Max(0, value), 255);
        }
    }

    class FractalConfig
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public string OutputPath { get; set; }
    }

    struct NewtonResult
    {
        public ComplexNumber Root { get; set; }
        public int Iterations { get; set; }
    }
}