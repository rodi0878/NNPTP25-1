using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// Generates Newton fractals
    /// </summary>
    public class NewtonFractalGenerator
    {
        private readonly NewtonFractalParameters parameters;
        private readonly Polynomial polynomial;
        private readonly Polynomial polynomialDerivative;
        private readonly Color[] colors;

        public NewtonFractalGenerator(NewtonFractalParameters parameters)
        {
            this.parameters = parameters;
            this.polynomial = CreatePolynomial();
            this.polynomialDerivative = polynomial.Derive();
            this.colors = InitializeColorPalette();
        }

        /// <summary>
        /// Creates the polynomial used for fractal generation
        /// </summary>
        private Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new Complex() { Real = 1 });
            polynomial.Coefficients.Add(Complex.Zero);
            polynomial.Coefficients.Add(Complex.Zero);
            polynomial.Coefficients.Add(new Complex() { Real = 1 });
            return polynomial;
        }

        /// <summary>
        /// Initializes the color palette
        /// </summary>
        private Color[] InitializeColorPalette()
        {
            return new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
        }

        /// <summary>
        /// Generates the fractal and returns the bitmap
        /// </summary>
        public Bitmap Generate()
        {
            Bitmap bitmap = new Bitmap(parameters.Width, parameters.Height);
            double xStep = (parameters.XMax - parameters.XMin) / parameters.Width;
            double yStep = (parameters.YMax - parameters.YMin) / parameters.Height;

            List<Complex> roots = new List<Complex>();
            int maxId = 0;

            for (int i = 0; i < parameters.Width; i++)
            {
                for (int j = 0; j < parameters.Height; j++)
                {
                    Complex currentPoint = CalculateWorldCoordinates(i, j, xStep, yStep);
                    currentPoint = NormalizePoint(currentPoint);

                    float iterationCount = PerformNewtonIteration(ref currentPoint);
                    int rootId = FindOrAddRoot(currentPoint, roots, ref maxId);
                    Color pixelColor = CalculatePixelColor(rootId, iterationCount);

                    bitmap.SetPixel(j, i, pixelColor);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Calculates world coordinates for a given pixel position
        /// Note: i maps to y coordinate, j maps to x coordinate (preserving original behavior)
        /// </summary>
        private Complex CalculateWorldCoordinates(int i, int j, double xStep, double yStep)
        {
            double y = parameters.YMin + i * yStep;
            double x = parameters.XMin + j * xStep;

            return new Complex()
            {
                Real = x,
                Imaginary = (float)y
            };
        }

        /// <summary>
        /// Normalizes point by replacing zero values with epsilon
        /// </summary>
        private Complex NormalizePoint(Complex point)
        {
            if (point.Real == 0)
                point.Real = parameters.Epsilon;
            if (point.Imaginary == 0)
                point.Imaginary = (float)parameters.Epsilon;
            return point;
        }

        /// <summary>
        /// Performs Newton's method iteration
        /// </summary>
        private float PerformNewtonIteration(ref Complex currentPoint)
        {
            float iterationCount = 0;
            for (int iterationIndex = 0; iterationIndex < parameters.MaxIterations; iterationIndex++)
            {
                Complex difference = polynomial.Evaluate(currentPoint).Divide(polynomialDerivative.Evaluate(currentPoint));
                currentPoint = currentPoint.Subtract(difference);

                double differenceMagnitude = CalculateDifferenceMagnitude(difference);
                if (differenceMagnitude >= parameters.ConvergenceThreshold)
                {
                    iterationIndex--;
                }
                iterationCount++;
            }
            return iterationCount;
        }

        /// <summary>
        /// Calculates the magnitude of the difference vector
        /// </summary>
        private double CalculateDifferenceMagnitude(Complex difference)
        {
            return Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2);
        }

        /// <summary>
        /// Finds existing root or adds new one to the collection
        /// </summary>
        private int FindOrAddRoot(Complex currentPoint, List<Complex> roots, ref int maxId)
        {
            for (int rootIndex = 0; rootIndex < roots.Count; rootIndex++)
            {
                double distance = CalculateDistance(currentPoint, roots[rootIndex]);
                if (distance <= parameters.RootMatchingTolerance)
                {
                    return rootIndex;
                }
            }

            roots.Add(currentPoint);
            int rootId = roots.Count;
            maxId = rootId + 1;
            return rootId;
        }

        /// <summary>
        /// Calculates squared distance between two complex points
        /// </summary>
        private double CalculateDistance(Complex a, Complex b)
        {
            return Math.Pow(a.Real - b.Real, 2) + Math.Pow(a.Imaginary - b.Imaginary, 2);
        }

        /// <summary>
        /// Calculates the pixel color based on root ID and iteration count
        /// </summary>
        private Color CalculatePixelColor(int rootId, float iterationCount)
        {
            Color baseColor = colors[rootId % colors.Length];
            int darkening = (int)iterationCount * parameters.ColorDarkeningFactor;

            int red = ClampColorComponent(baseColor.R - darkening);
            int green = ClampColorComponent(baseColor.G - darkening);
            int blue = ClampColorComponent(baseColor.B - darkening);

            return Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// Clamps color component value to valid range [0, 255]
        /// </summary>
        private int ClampColorComponent(int component)
        {
            return Math.Min(Math.Max(0, component), 255);
        }
    }
}


