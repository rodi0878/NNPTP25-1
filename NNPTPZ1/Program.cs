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
        /// <summary>
        /// Predefined colors used to represent different roots in the fractal.
        /// </summary>
        private static readonly Color[] Colors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        /// <summary>
        /// List of discovered roots so far. Shared across the fractal generation.
        /// </summary>
        private static readonly List<ComplexNumber> Roots = new List<ComplexNumber>();

        /// <summary>
        /// Fractal rendering settings parsed from command-line arguments.
        /// </summary>
        public static FractalSettings Settings;

        /// <summary>
        /// Entry point of the program. Parses arguments, generates the Newton fractal, and saves it as an image.
        /// </summary>
        static void Main(string[] args)
        {
            Settings = ParseArguments(args);
            var (polynomial, derivativePolynomial) = CreatePolynomial();

            Bitmap fractalImage = GenerateFractalImage(polynomial, derivativePolynomial);
            fractalImage.Save(Settings.OutputPath ?? "../../../out.png");

            Console.WriteLine($"Fractal saved to: {Settings.OutputPath}");
        }
        /// <summary>
        /// Generates the Newton fractal image based on the given settings and polynomial.
        /// </summary>
        /// <param name="settings">Rendering and coordinate settings.</param>
        /// <param name="polynomial">The polynomial used for Newton iteration.</param>
        /// <param name="derivativePolynomial">The derivative of the polynomial.</param>
        /// <returns>The generated bitmap image of the fractal.</returns>
        private static Bitmap GenerateFractalImage(
            Polynomial polynomial,
            Polynomial derivativePolynomial)
        {
            Bitmap fractalImage = new Bitmap(Settings.Width, Settings.Height);

            double xstep = (Settings.XMax - Settings.XMin) / Settings.Width;
            double ystep = (Settings.YMax - Settings.YMin) / Settings.Height;

            for (int i = 0; i < Settings.Width; i++)
            {
                for (int j = 0; j < Settings.Height; j++)
                {
                    double y = Settings.YMin + i * ystep;
                    double x = Settings.XMin + j * xstep;

                    Color pixelColor = ComputePixelColor(x, y, polynomial, derivativePolynomial);
                    fractalImage.SetPixel(j, i, pixelColor);
                }
            }

            return fractalImage;
        }
        /// <summary>
        /// Parses command-line arguments or uses default values if not provided.
        /// </summary>
        private static FractalSettings ParseArguments(string[] args)
        {
            var settings = new FractalSettings();

            if (args.Length > 0) settings.Width = int.Parse(args[0]);
            if (args.Length > 1) settings.Height = int.Parse(args[1]);
            if (args.Length > 2) settings.XMin = double.Parse(args[2]);
            if (args.Length > 3) settings.XMax = double.Parse(args[3]);
            if (args.Length > 4) settings.YMin = double.Parse(args[4]);
            if (args.Length > 5) settings.YMax = double.Parse(args[5]);
            if (args.Length > 6) settings.OutputPath = args[6];

            return settings;
        }
        /// <summary>
        /// Creates the base polynomial used to generate the Newton fractal and its derivative.
        /// </summary>
        /// <returns>
        /// A tuple containing the original polynomial and its derivative
        /// </returns>
        private static (Polynomial polynom, Polynomial polynomDerivated) CreatePolynomial()
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
        /// <summary>
        /// Computes the color of a single pixel in the Newton fractal image based on its (x, y) position.
        /// </summary>
        /// <param name="x">X coordinate in the fractal plane.</param>
        /// <param name="y">Y coordinate in the fractal plane.</param>
        /// <param name="polynomial">The polynomial used for Newton iteration.</param>
        /// <param name="derivativePolynomial">The derivative of the polynomial.</param>
        /// <returns>The computed color for the given pixel.</returns>
        private static Color ComputePixelColor(double x, double y, Polynomial polynomial, Polynomial derivativePolynomial)
        {
            ComplexNumber currentEstimate = new ComplexNumber { Real = x, Imaginary = y };

            if (currentEstimate.Real == 0) currentEstimate.Real = 0.0001;
            if (currentEstimate.Imaginary == 0) currentEstimate.Imaginary = 0.0001f;

            int iterations = 0;
            currentEstimate = PerformNewtonIteration(currentEstimate, polynomial, derivativePolynomial, out iterations);

            int rootIndex = DetermineRootIndex(currentEstimate);

            Color baseColor = Colors[rootIndex % Colors.Length];
            return AdjustColorForIterations(baseColor, iterations);
        }
        /// <summary>
        /// Determines whether the given complex number corresponds to an already known root.
        /// Adds it to the list if not found.
        /// </summary>
        /// <param name="estimate">The estimated root.</param>
        /// <returns>Index of the corresponding root in the list.</returns>
        private static int DetermineRootIndex(ComplexNumber estimate)
        {
            const double rootTolerance = 0.01;

            for (int i = 0; i < Roots.Count; i++)
            {
                double distanceSquared =
                    Math.Pow(estimate.Real - Roots[i].Real, 2) +
                    Math.Pow(estimate.Imaginary - Roots[i].Imaginary, 2);

                if (distanceSquared <= rootTolerance)
                    return i;
            }

            Roots.Add(estimate);
            return Roots.Count - 1;
        }
        /// <summary>
        /// Performs Newton's iteration to approximate a root of the given polynomial.
        /// </summary>
        /// <param name="initialGuess">Starting complex number.</param>
        /// <param name="polynomial">Polynomial to evaluate.</param>
        /// <param name="derivativePolynomial">Derivative of the polynomial.</param>
        /// <param name="iterations">Outputs the number of performed iterations.</param>
        /// <returns>The approximated complex root.</returns>
        private static ComplexNumber PerformNewtonIteration(
            ComplexNumber initialGuess,
            Polynomial polynomial,
            Polynomial derivativePolynomial,
            out int iterations)
        {
            ComplexNumber estimate = initialGuess;
            iterations = 0;

            const int maxIterations = 30;
            const double divergenceThreshold = 0.5;

            for (int i = 0; i < maxIterations; i++)
            {
                var difference = polynomial.Evaluate(estimate).Divide(derivativePolynomial.Evaluate(estimate));
                estimate = estimate.Subtract(difference);

                double delta = difference.Real * difference.Real + difference.Imaginary * difference.Imaginary;
                if (delta >= divergenceThreshold)
                    i--; // stay in loop longer if not converging fast enough

                iterations++;
            }
            return estimate;
        }
        /// <summary>
        /// Adjusts the base color based on the number of Newton iterations performed.
        /// The higher the iteration count, the darker the resulting color.
        /// </summary>
        /// <param name="baseColor">The base color associated with the root.</param>
        /// <param name="iterations">The number of Newton iterations performed.</param>
        /// <returns>A darker version of the base color based on the iteration count.</returns>
        private static Color AdjustColorForIterations(Color baseColor, int iterations)
        {
            int adjustment = iterations * 2;

            int r = Math.Max(0, Math.Min(255, baseColor.R - adjustment));
            int g = Math.Max(0, Math.Min(255, baseColor.G - adjustment));
            int b = Math.Max(0, Math.Min(255, baseColor.B - adjustment));

            return Color.FromArgb(r, g, b);
        }
    }
}
