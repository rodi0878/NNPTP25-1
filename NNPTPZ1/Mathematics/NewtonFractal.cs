using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    class NewtonFractal
    {
        private const double POLYNOMIAL_TOLERANCE = 0.5;
        private const double ROOT_TOLERANCE = 0.01;
        private const int MAX_ITERATIONS = 30;
        private const double OFFSET = 0.0001;

        private static int imageWidth, imageHeight;
        private static double xMin, yMin, xMax, yMax;
        private static string outputFileName;
        private static List<ComplexNumber> roots = new List<ComplexNumber>();
        private static Polynomial polynomial, polynomialDerivative;
        private static readonly Color[] colors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        public static void ParseArguments(string[] arguments)
        {
            imageWidth = int.Parse(arguments[0]);
            imageHeight = int.Parse(arguments[1]);
            xMin = double.Parse(arguments[2]);
            xMax = double.Parse(arguments[3]);
            yMin = double.Parse(arguments[4]);
            yMax = double.Parse(arguments[5]);
            outputFileName = arguments.Length > 6 ? arguments[6] : "../../../out.png";
        }

        public static void GenerateFractalImage()
        {
            Bitmap bitmap = new Bitmap(imageWidth, imageHeight);

            for (int x = 0; x < imageWidth; x++)
            {
                for (int y = 0; y < imageHeight; y++)
                {
                    ComplexNumber initialPoint = GetComplexCoordinates(x, y);
                    Color pixelColor = DeterminePixelColor(ref initialPoint);
                    bitmap.SetPixel(x, y, pixelColor);
                }
            }

            bitmap.Save(outputFileName);
        }

        private static Polynomial InitializePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.AddCoefficient(new ComplexNumber { Real = 1 });
            polynomial.AddCoefficient(ComplexNumber.Zero);
            polynomial.AddCoefficient(ComplexNumber.Zero);
            polynomial.AddCoefficient(new ComplexNumber { Real = 1 });
            return polynomial;
        }

        public static void InitializePolynomials()
        {
            polynomial = InitializePolynomial();
            Console.WriteLine($"Polynomial: {polynomial}");
            polynomialDerivative = polynomial.Derive();
            Console.WriteLine($"Polynomial derivative: {polynomialDerivative}");
        }

        private static int SolveUsingNewtonMethod(ref ComplexNumber root)
        {
            int iterations = 0;
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                ComplexNumber quotient = polynomial.Evaluate(root).Divide(polynomialDerivative.Evaluate(root));
                root = root.Subtract(quotient);

                if (quotient.GetAbsoluteValue() >= POLYNOMIAL_TOLERANCE)
                {
                    i--;
                }
                iterations++;
            }
            return iterations;
        }

        private static int FindOrAddRoot(ComplexNumber root)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if ((root.Real - roots[i].Real) * (root.Real - roots[i].Real) +
                    (root.Imaginary - roots[i].Imaginary) * (root.Imaginary - roots[i].Imaginary) <= ROOT_TOLERANCE)
                {
                    return i;
                }
            }

            roots.Add(root);
            return roots.Count - 1;
        }

        private static ComplexNumber GetComplexCoordinates(int x, int y)
        {
            double xStep = (xMax - xMin) / imageWidth;
            double yStep = (yMax - yMin) / imageHeight;
            double realPart = xMin + x * xStep;
            double imaginaryPart = yMin + y * yStep;

            return new ComplexNumber
            {
                Real = realPart != 0 ? realPart : OFFSET,
                Imaginary = imaginaryPart != 0 ? imaginaryPart : OFFSET
            };
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private static Color DeterminePixelColor(ref ComplexNumber root)
        {
            int iteration = SolveUsingNewtonMethod(ref root);
            int rootIndex = FindOrAddRoot(root);

            Color baseColor = colors[rootIndex % colors.Length];
            int colorAdjustment = Math.Min(iteration * 2, 255);

            return Color.FromArgb(
                Clamp(baseColor.R - colorAdjustment, 0, 255),
                Clamp(baseColor.G - colorAdjustment, 0, 255),
                Clamp(baseColor.B - colorAdjustment, 0, 255)
            );
        }
    }
}