using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    public static class FractalGenerator
    {
        public static Bitmap DrawFractal(
            Polynomial polynomial,
            Polynomial polynomialDerived,
            FractalConfig fractalConfig)
        {
            var bmp = new Bitmap(fractalConfig.Width, fractalConfig.Height);
            var roots = new List<ComplexNumber>();

            for (var i = 0; i < fractalConfig.Width; i++)
            {
                for (var j = 0; j < fractalConfig.Height; j++)
                {
                    ProcessPixel(i, j, polynomial, polynomialDerived, fractalConfig, bmp, roots);
                }
            }

            return bmp;
        }

        private static void ProcessPixel(
            int i, int j,
            Polynomial polynomial,
            Polynomial polynomialDerived,
            FractalConfig fractalConfig,
            Bitmap bmp,
            List<ComplexNumber> roots)
        {
            var z = MapPixelToComplexNumber(i, j, fractalConfig);
            var (root, iterations) = FindRoot(z, polynomial, polynomialDerived, fractalConfig.Iterations);
            var rootId = GetOrCreateRootId(root, roots);
            var color = CalculatePixelColor(rootId, iterations, fractalConfig);
            bmp.SetPixel(i, j, color);
        }

        private static ComplexNumber MapPixelToComplexNumber(int i, int j, FractalConfig config)
        {
            double x = config.Xmin + i * config.Xstep;
            double y = config.Ymin + j * config.Ystep;

            return new ComplexNumber(x, y);
        }

        private static (ComplexNumber root, int iterations) FindRoot(
            ComplexNumber z,
            Polynomial polynomial,
            Polynomial polynomialDerived,
            int maxIterations)
        {
            if (z.Real == 0) z.Real = 0.0001;
            if (z.Imaginary == 0) z.Imaginary = 0.0001f;

            var iterations = 0;
            for (var q = 0; q < maxIterations; q++)
            {
                var diff = polynomial.Evaluate(z).Divide(polynomialDerived.Evaluate(z));
                z = z.Subtract(diff);

                if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                {
                    q--;
                }

                iterations++;
            }

            return (z, iterations);
        }

        private static int GetOrCreateRootId(ComplexNumber root, List<ComplexNumber> roots)
        {
            for (var i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(root.Real - roots[i].Real, 2) + Math.Pow(root.Imaginary - roots[i].Imaginary, 2) <= 0.01)
                {
                    return i;
                }
            }

            roots.Add(root);
            return roots.Count - 1;
        }

        private static Color CalculatePixelColor(int rootId, int iterations, FractalConfig config)
        {
            var baseColor = config.Colors[rootId % config.Colors.Length];
            var shading = iterations * config.ShadingSpeed;

            var r = Math.Min(Math.Max(baseColor.R - shading, 0), 255);
            var g = Math.Min(Math.Max(baseColor.G - shading, 0), 255);
            var b = Math.Min(Math.Max(baseColor.B - shading, 0), 255);

            return Color.FromArgb(r, g, b);
        }
    }
}