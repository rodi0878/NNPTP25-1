using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1.Fractals
{
    public class FractalGenerator
    {
        private readonly FractalSettings settings;
        private readonly Polynomial polynomial;
        private readonly Polynomial derivative;
        private readonly List<Complex> roots;

        private readonly Color[] rootColors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow,
            Color.Orange, Color.Fuchsia, Color.Cyan, Color.Magenta
        };

        public FractalGenerator(FractalSettings settings)
        {
            this.settings = settings;
            this.polynomial = CreatePolynomial();
            this.derivative = this.polynomial.Derive();
            this.roots = new List<Complex>();
        }

        public Bitmap Generate()
        {
            Bitmap bitmap = new Bitmap(settings.Width, settings.Height);

            double xStep = (settings.XMax - settings.XMin) / settings.Width;
            double yStep = (settings.YMax - settings.YMin) / settings.Height;

            int progressStep = Math.Max(settings.Width / 10, 1);

            for (int py = 0; py < settings.Height; py++)
            {
                if (py % progressStep == 0)
                    Console.WriteLine($"Progress: {py * 100 / settings.Height}%");

                for (int px = 0; px < settings.Width; px++)
                {
                    Complex startPoint = new Complex
                    {
                        Real = settings.XMin + px * xStep,
                        Imaginary = settings.YMin + py * yStep
                    };

                    var (finalValue, iterations) = ComputeNewtonConvergence(startPoint);
                    int rootIndex = FindOrAddRoot(finalValue);
                    Color color = GetColorForPixel(rootIndex, iterations);

                    bitmap.SetPixel(px, py, color);
                }
            }

            return bitmap;
        }

        private static Polynomial CreatePolynomial()
        {
            var p = new Polynomial();
            p.Coefficients.Add(new Complex { Real = 1 });
            p.Coefficients.Add(Complex.Zero);
            p.Coefficients.Add(Complex.Zero);
            p.Coefficients.Add(new Complex { Real = 1 });
            return p;
        }

        private (Complex result, int iterations) ComputeNewtonConvergence(Complex z)
        {
            const int maxIterations = 30;
            const double divergenceThreshold = 0.5;

            int iterationCount = 0;

            for (int i = 0; i < maxIterations; i++)
            {
                var step = polynomial.Evaluate(z).Divide(derivative.Evaluate(z));
                z = z.Subtract(step);

                if (step.Real * step.Real + step.Imaginary * step.Imaginary >= divergenceThreshold)
                {
                    i--;
                }

                iterationCount++;
            }

            return (z, iterationCount);
        }

        private int FindOrAddRoot(Complex z)
        {
            const double tolerance = 0.01;

            for (int i = 0; i < roots.Count; i++)
            {
                double dx = z.Real - roots[i].Real;
                double dy = z.Imaginary - roots[i].Imaginary;

                if (dx * dx + dy * dy <= tolerance)
                    return i;
            }

            roots.Add(z);
            return roots.Count - 1;
        }

        private Color GetColorForPixel(int rootIndex, int iterations)
        {
            Color baseColor = rootColors[rootIndex % rootColors.Length];

            int r = Math.Max(0, baseColor.R - iterations * 2);
            int g = Math.Max(0, baseColor.G - iterations * 2);
            int b = Math.Max(0, baseColor.B - iterations * 2);

            return Color.FromArgb(r, g, b);
        }
    }
}
