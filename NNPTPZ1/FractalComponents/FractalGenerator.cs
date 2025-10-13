using System;
using System.Collections.Generic;
using NNPTPZ1.Mathematics;
using static NNPTPZ1.FractalComponents.FractalParameters;

namespace NNPTPZ1.FractalComponents
{
    public class FractalGenerator
    {
        private const double DoubleEpsilon = 0.0001;
        private const float FloatEpsilon = 0.0001f;

        private readonly FractalParameters _parameters;
        private readonly PixelResult[,] _pixels;
        private readonly List<ComplexNumber> _roots;
        
        private Polynomial _derivative;
        private Polynomial _polynomial;

        public FractalGenerator(FractalParameters parameters)
        {
            _parameters = parameters;
            _pixels = new PixelResult[parameters.Width, parameters.Height];
            _roots = new List<ComplexNumber>();
            CreatePolynomialAndItsDerivative();
        }

        private void CreatePolynomialAndItsDerivative()
        {
            _polynomial = new Polynomial();
            _polynomial.Coefficients.AddRange(_parameters.Coefficients);

            _derivative = _polynomial.Derive();
        }

        public PixelResult[,] Generate()
        {
            for (int x = 0; x < _parameters.Width; x++)
            {
                for (int y = 0; y < _parameters.Height; y++)
                {
                    ProcessPixelCalculation(x, y);
                }
            }

            return _pixels;
        }

        private void ProcessPixelCalculation(int x, int y)
        {
            ComplexNumber currentPoint = new ComplexNumber
            {
                Real = _parameters.XMin + y * _parameters.XStep,
                Imaginary = (float)(_parameters.YMin + x * _parameters.YStep)
            };

            ProtectAgainstDivisionByZero(currentPoint);

            int iterationCount = DoNewtonIteration(ref currentPoint);

            int rootIndex = FindRootIndex(currentPoint);

            _pixels[x, y] = new PixelResult
            {
                IterationCount = iterationCount,
                RootIndex = rootIndex
            };
        }

        private static void ProtectAgainstDivisionByZero(ComplexNumber currentPoint)
        {
            if (currentPoint.Real == 0)
                currentPoint.Real = DoubleEpsilon;
            if (currentPoint.Imaginary == 0)
                currentPoint.Imaginary = FloatEpsilon;
        }

        private int FindRootIndex(ComplexNumber currentPoint)
        {
            bool isKnown = false;
            int rootIndex = 0;

            for (int i = 0; i < _roots.Count; i++)
            {
                if (currentPoint.SquaredDistanceTo(_roots[i]) <= RootTolerance)
                {
                    isKnown = true;
                    rootIndex = i;
                }
            }

            if (!isKnown)
            {
                _roots.Add(currentPoint);
                rootIndex = _roots.Count;
            }

            return rootIndex;
        }

        private int DoNewtonIteration(ref ComplexNumber currentPoint)
        {
            int iterationCount = 0;

            for (int i = 0; i < MaxIterations; i++)
            {
                ComplexNumber diff = _polynomial
                    .Evaluate(currentPoint)
                    .Divide(_derivative.Evaluate(currentPoint));

                currentPoint = currentPoint.Subtract(diff);

                if (IsStepAboveDivergenceThreshold(diff))
                {
                    i--;
                }

                iterationCount++;
            }

            return iterationCount;
        }

        private static bool IsStepAboveDivergenceThreshold(ComplexNumber diff)
        {
            return Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= DivergenceThreshold;
        }
    }
}