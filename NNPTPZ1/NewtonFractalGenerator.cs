using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    public class NewtonFractalGenerator
    {
        private readonly Polynomial polynomial;
        private readonly Polynomial polynomialDerivative;
        private readonly List<ComplexNumber> discoveredRoots = new List<ComplexNumber>();
        private readonly Color[] colorPalette =
        {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Orange,
            Color.Fuchsia,
            Color.Gold,
            Color.Cyan,
            Color.Magenta
        };

        public NewtonFractalGenerator(Polynomial polynomial)
        {
            this.polynomial = polynomial;
            polynomialDerivative = polynomial.Derive();
        }

        public Bitmap GenerateFractalImage(FractalArguments arguments)
        {
            Bitmap image = new Bitmap(arguments.Width, arguments.Height);
            double xStep = CalculateStep(arguments.MinX, arguments.MaxX, arguments.Width);
            double yStep = CalculateStep(arguments.MinY, arguments.MaxY, arguments.Height);

            for (int pixelY = 0; pixelY < arguments.Height; pixelY++)
            {
                for (int pixelX = 0; pixelX < arguments.Width; pixelX++)
                {
                    ComplexNumber initialGuess = CreateInitialGuess(arguments, pixelX, pixelY, xStep, yStep);
                    NewtonIterationResult iterationResult = IterateNewtonMethod(initialGuess);
                    Color pixelColor = DeterminePixelColor(iterationResult);
                    image.SetPixel(pixelX, pixelY, pixelColor);
                }
            }

            return image;
        }

        private static double CalculateStep(double minValue, double maxValue, int pixelCount)
        {
            return (maxValue - minValue) / pixelCount;
        }

        private static ComplexNumber CreateInitialGuess(FractalArguments arguments, int pixelX, int pixelY, double xStep, double yStep)
        {
            double realPart = arguments.MinX + pixelX * xStep;
            double imaginaryPart = arguments.MinY + pixelY * yStep;
            return EnsureNonZero(new ComplexNumber(realPart, imaginaryPart));
        }

        private static ComplexNumber EnsureNonZero(ComplexNumber value)
        {
            double adjustedReal = value.Real == 0 ? 0.0001 : value.Real;
            double adjustedImaginary = value.Imaginary == 0 ? 0.0001 : value.Imaginary;
            return new ComplexNumber(adjustedReal, adjustedImaginary);
        }

        private NewtonIterationResult IterateNewtonMethod(ComplexNumber initialGuess)
        {
            ComplexNumber currentGuess = initialGuess;
            int iterationCount = 0;

            for (int iterationIndex = 0; iterationIndex < 30; iterationIndex++)
            {
                ComplexNumber delta = polynomial.Evaluate(currentGuess).Divide(polynomialDerivative.Evaluate(currentGuess));
                currentGuess = currentGuess.Subtract(delta);
                if (delta.GetMagnitudeSquared() >= 0.5)
                {
                    iterationIndex--;
                }

                iterationCount++;
            }

            return new NewtonIterationResult(currentGuess, iterationCount);
        }

        private Color DeterminePixelColor(NewtonIterationResult iterationResult)
        {
            int rootIndex = GetRootIndex(iterationResult.Root);
            Color baseColor = colorPalette[rootIndex % colorPalette.Length];
            int intensityOffset = System.Math.Min(iterationResult.IterationCount * 2, 255);
            return Color.FromArgb(
                System.Math.Max(0, baseColor.R - intensityOffset),
                System.Math.Max(0, baseColor.G - intensityOffset),
                System.Math.Max(0, baseColor.B - intensityOffset));
        }

        private int GetRootIndex(ComplexNumber root)
        {
            for (int index = 0; index < discoveredRoots.Count; index++)
            {
                if (root.GetDistanceSquared(discoveredRoots[index]) <= 0.01)
                {
                    return index;
                }
            }

            discoveredRoots.Add(root);
            return discoveredRoots.Count - 1;
        }
    }
}
