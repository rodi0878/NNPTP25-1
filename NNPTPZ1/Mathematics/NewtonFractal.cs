using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace NNPTPZ1.Mathematics
{

    public class NewtonFractal : IDisposable
    {
        private const double MinimumCoordinate = 0.0001;
        private const int MaxNumberOfNewtonIterations = 30;
        private const double ConvergenceThreshold = 0.5;
        private const double RootEqualityThreshold = 0.1;

        private static readonly Polynomial DefaultInitialPolynomial
            = new Polynomial(ComplexNumber.One, ComplexNumber.Zero, ComplexNumber.Zero, ComplexNumber.One);

        private const string DefaultOutputFileInput = "../../../out.png";

        private static readonly Color[] DefaultColorPalette = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private readonly double _minX;
        private readonly double _minY;
        private readonly double _stepX;
        private readonly double _stepY;

        private readonly Bitmap _newtonFractalBitmap;
        private readonly Color[] _colorPalette;

        public bool IsDisposed { get; private set; }

        public NewtonFractal(int bitmapWidth, int bitmapHeight,
            double minX, double maxX, double minY, double maxY,
            Color[] palette = null)
        {
            if (bitmapWidth <= 0) throw new ArgumentOutOfRangeException(nameof(bitmapWidth));
            if (bitmapHeight <= 0) throw new ArgumentOutOfRangeException(nameof(bitmapHeight));
            if (maxX <= minX) throw new ArgumentException("maxX must be greater than minX");
            if (maxY <= minY) throw new ArgumentException("maxY must be greater than minY");

            _minX = minX;
            _minY = minY;
            _stepX = (maxX - minX) / bitmapWidth;
            _stepY = (maxY - minY) / bitmapHeight;

            _newtonFractalBitmap = new Bitmap(bitmapWidth, bitmapHeight);
            _colorPalette = (palette == null || palette.Length == 0) ? DefaultColorPalette : (Color[])palette.Clone();
        }

        public void Generate(Polynomial initialPolynomial = null)
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();

            Polynomial polynomial = initialPolynomial ?? DefaultInitialPolynomial;
            Console.WriteLine($"Initial polynomial: {polynomial}");

            Polynomial polynomialDerivative = polynomial.Differentiate();
            Console.WriteLine($"Initial polynomial derivative: {polynomialDerivative}");

            // for each pixel of bitmap
            for (int i = 0; i < _newtonFractalBitmap.Width; i++)
            {
                for (int j = 0; j < _newtonFractalBitmap.Height; j++)
                {
                    ComplexNumber complexCoordinatesOfPixel = EnsureNonZeroCoordinates(GetComplexCoordinatesOfPixel(i, j));

                    int iterationCount = PerformNewtonIteration(polynomial, polynomialDerivative, ref complexCoordinatesOfPixel);

                    int solutionRootNumber = FindSolutionRootNumberOrAdd(roots, complexCoordinatesOfPixel);

                    Color pixelColor = GetPixelColor(solutionRootNumber, iterationCount);
                    _newtonFractalBitmap.SetPixel(i, j, pixelColor);
                }
            }
        }

        public void Save(string outputFilePath = null)
        {
            outputFilePath = outputFilePath ?? DefaultOutputFileInput;
            string outputFileDir = Path.GetDirectoryName(outputFilePath);
            if (string.IsNullOrEmpty(outputFileDir))
            {
                throw new ArgumentException($"Invalid output file path: {outputFilePath}");
            }
            if (!Directory.Exists(outputFileDir))
            {
                Directory.CreateDirectory(outputFileDir);
            }

            _newtonFractalBitmap.Save(outputFilePath);
        }

        public void GenerateAndSave(string outputFilePath = null, Polynomial initialPolynomial = null)
        {
            Generate(initialPolynomial);
            Save(outputFilePath);
        }

        private ComplexNumber GetComplexCoordinatesOfPixel(int column, int row)
        {
            return new ComplexNumber(_minX + column * _stepX, _minY + row * _stepY);
        }

        private ComplexNumber EnsureNonZeroCoordinates(ComplexNumber complexCoordinates)
        {
            if (complexCoordinates.RealPart != 0 || complexCoordinates.ImaginaryPart != 0)
            {
                return complexCoordinates;
            }

            return new ComplexNumber(
                (complexCoordinates.RealPart == 0) ? MinimumCoordinate : complexCoordinates.RealPart,
                (complexCoordinates.ImaginaryPart == 0) ? MinimumCoordinate : complexCoordinates.ImaginaryPart
            );
        }

        private int PerformNewtonIteration(Polynomial polynomial, Polynomial polynomialDerived, ref ComplexNumber complexCoordinates)
        {
            int iterationCount = 0;

            for (int i = 0; i < MaxNumberOfNewtonIterations; i++)
            {
                ComplexNumber derivativeValue = polynomialDerived.EvaluateAt(complexCoordinates);
                double denom = derivativeValue.GetMagnitudeSquared();
                if (denom == 0)
                {
                    return iterationCount;
                }

                ComplexNumber step = polynomial.EvaluateAt(complexCoordinates).Divide(derivativeValue);
                complexCoordinates = complexCoordinates.Subtract(step);

                if (step.GetMagnitudeSquared() >= ConvergenceThreshold)
                {
                    i--;
                }

                iterationCount++;
            }

            return iterationCount;
        }

        private int FindSolutionRootNumberOrAdd(List<ComplexNumber> roots, ComplexNumber rootCandidate)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (rootCandidate.GetEuclideanDistanceTo(roots[i]) <= RootEqualityThreshold)
                {
                    return i;
                }
            }

            roots.Add(rootCandidate);
            return roots.Count - 1;
        }

        private Color GetPixelColor(int solutionRootNumber, int iterationCount)
        {
            Color basePixelColor = _colorPalette[solutionRootNumber % _colorPalette.Length];
            int factor = iterationCount * 2;
            return Color.FromArgb(
                ClampToByte(basePixelColor.R - factor),
                ClampToByte(basePixelColor.G - factor),
                ClampToByte(basePixelColor.B - factor)
            );
        }

        private static int ClampToByte(int value)
        {
            if (value < 0)
            {
                return 0;
            }
            if (value > 255)
            {
                return 255;
            }
            return value;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~NewtonFractal()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                _newtonFractalBitmap.Dispose();
            }
        }

    }
}
