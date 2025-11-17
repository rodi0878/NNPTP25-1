using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    /// <summary>
    /// Produces Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const int DefaultMaxIterations = 30;
        private const double DefaulPolynomialValue = 1.0;
        private const double ZeroValue = 0.0;
        private const double DefaultConvergenceTolerance = 1e-6;
        private const double DefaultRootMergeDistanceSquared = 0.01;
        private const double SmallInitialValue = 1e-4;

        private static readonly Color[] ColorPalette =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] args)
        {
            if (!TryParseArguments(args, out InputArguments inputArguments))
            {
                Console.WriteLine("Usage: <PixelWidth> <PixelHeight> <realNumberMin> <realNumberMax> <imagNumberMin> <imagNumberMax> <outputPath>");
                return;
            }

            using (var bitmap = new Bitmap(inputArguments.PixelWidth, inputArguments.PixelHeight))
            {
                RenderNewtonFractal(bitmap, inputArguments);
                bitmap.Save(inputArguments.OutputPath ?? "../../../out.png");
            }
        }


        /// <summary>
        /// Parses command-line arguments into an InputArguments object.
        /// </summary>
        private static bool TryParseArguments(string[] args, out InputArguments inputArguments)
        {
            inputArguments = null;
            if (args == null || args.Length < 7)
                return false;

            if (!int.TryParse(args[0], out int PixelWidth)) return false;
            if (!int.TryParse(args[1], out int PixelHeight)) return false;
            if (!double.TryParse(args[2], out double RealNumberMinimum)) return false;
            if (!double.TryParse(args[3], out double RealNumberMaximum)) return false;
            if (!double.TryParse(args[4], out double ImaginaryNumberMinimum)) return false;
            if (!double.TryParse(args[5], out double ImaginaryNumberMaximum)) return false;
            string OutputPath = args[6];

            inputArguments = new InputArguments
            {
                PixelWidth = PixelWidth,
                PixelHeight = PixelHeight,
                RealNumberMinimum = RealNumberMinimum,
                RealNumberMaximum = RealNumberMaximum,
                ImaginaryNumberMinimum = ImaginaryNumberMinimum,
                ImaginaryNumberMaximum = ImaginaryNumberMaximum,
                OutputPath = OutputPath,
            };
            return true;
        }


        /// <summary>
        /// Renders a Newton fractal into the provided bitmap using given input arguments.
        /// </summary>
        private static void RenderNewtonFractal(Bitmap bitmap, InputArguments inputArguments)
        {
            double pixelWidth = (inputArguments.RealNumberMaximum - inputArguments.RealNumberMinimum) / inputArguments.PixelWidth;
            double pixelHeight = (inputArguments.ImaginaryNumberMaximum - inputArguments.ImaginaryNumberMinimum) / inputArguments.PixelHeight;

            var polynomial = CreateDefaultPolynomial();
            var derivative = polynomial.Derive();

            var roots = new List<ComplexNumber>();

            for (int row = 0; row < inputArguments.PixelHeight; row++)
            {
                double imaginaryPart = inputArguments.ImaginaryNumberMinimum + row * pixelHeight;

                for (int collumn = 0; collumn < inputArguments.PixelWidth; collumn++)
                {
                    double realPart = inputArguments.RealNumberMinimum + collumn * pixelWidth;
                    var initialValue = CreateInitialComplexValue(realPart, imaginaryPart);

                    int iterationCount = PerformNewtonIteration(initialValue, polynomial, derivative, DefaultMaxIterations, DefaultConvergenceTolerance, out ComplexNumber convergedValue);

                    int rootPosition = FindOrAddRoot(roots, convergedValue, DefaultRootMergeDistanceSquared);

                    var baseColor = ColorPalette[rootPosition % ColorPalette.Length];
                    var pixelColor = ShadeColor(baseColor, iterationCount);
                    bitmap.SetPixel(collumn, row, pixelColor);
                }
            }
        }


        private static Polynomial CreateDefaultPolynomial()
        {
            var polynomial = new Polynomial();
            polynomial.ListOfCoefficients.Add(new ComplexNumber { RealPart = DefaulPolynomialValue });
            polynomial.ListOfCoefficients.Add(ComplexNumber.ZeroValue);
            polynomial.ListOfCoefficients.Add(ComplexNumber.ZeroValue);
            polynomial.ListOfCoefficients.Add(new ComplexNumber { RealPart = DefaulPolynomialValue });
            return polynomial;
        }


        private static ComplexNumber CreateInitialComplexValue(double realPart, double imaginaryPart)
        {
            return new ComplexNumber
            {
                RealPart = (realPart == ZeroValue) ? SmallInitialValue : realPart,
                ImaginaryPart = (imaginaryPart == ZeroValue) ? SmallInitialValue : imaginaryPart
            };
        }


        /// <summary>
        /// Performs Newton-Raphson iteration for a complex polynomial.
        /// </summary>
        private static int PerformNewtonIteration(ComplexNumber startingPoint, Polynomial polynomial, Polynomial polynomialDerivative, int maxIterations, double convergenceTolerance, out ComplexNumber convergedValue)
        {
            var currentEstimate = new ComplexNumber { RealPart = startingPoint.RealPart, ImaginaryPart = startingPoint.ImaginaryPart };


            for (int i = 0; i < maxIterations; i++)
            {
                var functionValue = polynomial.EvaluateAtComplexPoint(currentEstimate);
                var derivativeValue = polynomialDerivative.EvaluateAtComplexPoint(currentEstimate);

                if (derivativeValue.RealPart == 0 && derivativeValue.ImaginaryPart == 0)
                    break;

                var correctionStep = functionValue.Divide(derivativeValue);
                currentEstimate = currentEstimate.Subtract(correctionStep);

                if (correctionStep.GetAbsoluteValue() < convergenceTolerance)
                {
                    convergedValue = currentEstimate;
                    return i + 1;
                }
            }

            convergedValue = currentEstimate;
            return maxIterations;
        }


        /// <summary>
        /// Finds an existing root within a merge distance or adds a new one.
        /// </summary
        private static int FindOrAddRoot(List<ComplexNumber> roots, ComplexNumber candidate, double mergeDistSquared)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                double realDifference = candidate.RealPart - roots[i].RealPart;
                double imaginaryDifference = candidate.ImaginaryPart - roots[i].ImaginaryPart;
                if (realDifference * realDifference + imaginaryDifference * imaginaryDifference <= mergeDistSquared)
                    return i;
            }

            roots.Add(candidate);
            return roots.Count - 1;
        }


        /// <summary>
        /// Shades a base color based on the number of iterations.
        /// </summary>
        private static Color ShadeColor(Color baseColor, int iterationCount)
        {
            int shadeAdjustment = iterationCount * 2;
            int adjustedRed = Math.Min(Math.Max(0, baseColor.R - shadeAdjustment), 255);
            int adjustedGreen = Math.Min(Math.Max(0, baseColor.G - shadeAdjustment), 255);
            int adjustedBlue = Math.Min(Math.Max(0, baseColor.B - shadeAdjustment), 255);
            return Color.FromArgb(adjustedRed, adjustedGreen, adjustedBlue);
        }
    }
}
