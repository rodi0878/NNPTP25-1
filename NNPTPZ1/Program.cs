using System;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal static class Program
    {
        private static void Main(string[] arguments)
        {
            FractalArguments fractalArguments = ParseArguments(arguments);
            Polynomial polynomial = CreateDefaultPolynomial();
            NewtonFractalGenerator generator = new NewtonFractalGenerator(polynomial);
            Bitmap fractalImage = generator.GenerateFractalImage(fractalArguments);
            try
            {
                fractalImage.Save(fractalArguments.OutputPath);
            }
            finally
            {
                fractalImage.Dispose();
            }
        }

        private static FractalArguments ParseArguments(string[] arguments)
        {
            if (arguments == null || arguments.Length < 7)
            {
                throw new ArgumentException("Expected 7 arguments: width height minX maxX minY maxY outputPath.");
            }

            return new FractalArguments
            (
                width: int.Parse(arguments[0]),
                height: int.Parse(arguments[1]),
                minX: double.Parse(arguments[2]),
                maxX: double.Parse(arguments[3]),
                minY: double.Parse(arguments[4]),
                maxY: double.Parse(arguments[5]),
                outputPath: arguments[6]
            );
        }

        private static Polynomial CreateDefaultPolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.AddCoefficient(new ComplexNumber(1, 0));
            polynomial.AddCoefficient(ComplexNumber.Zero());
            polynomial.AddCoefficient(ComplexNumber.Zero());
            polynomial.AddCoefficient(new ComplexNumber(1, 0));
            return polynomial;
        }
    }
}
