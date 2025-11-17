using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class NewtonFractalGenerator
    {
        private readonly Polynomial polynomial;
        private readonly Polynomial derivative;
        private readonly Color[] palette;

        public NewtonFractalGenerator(Polynomial polynomial)
        {
            this.polynomial = polynomial ?? throw new ArgumentNullException(nameof(polynomial));
            this.derivative = polynomial.Derive();

            palette = new[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow,
                Color.Orange, Color.Fuchsia, Color.Gold,
                Color.Cyan, Color.Magenta
            };
        }

        public Bitmap Generate(FractalConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            var bitmap = new Bitmap(config.Width, config.Height);

            double xStep = (config.XMax - config.XMin) / config.Width;
            double yStep = (config.YMax - config.YMin) / config.Height;

            var roots = new List<ComplexNumber>();

            for (int yPixel = 0; yPixel < config.Height; yPixel++)
            {
                for (int xPixel = 0; xPixel < config.Width; xPixel++)
                {
                    // world coordinates
                    double y = config.YMin + yPixel * yStep;
                    double x = config.XMin + xPixel * xStep;

                    var z = new ComplexNumber(x, y);
                    int iterations;

                    z = FindRoot(z, out iterations);

                    int rootIndex = GetRootIndex(roots, z);
                    Color color = GetColorForRoot(rootIndex, iterations);

                    bitmap.SetPixel(xPixel, yPixel, color);
                }
            }

            return bitmap;
        }

        private ComplexNumber FindRoot(ComplexNumber start, out int iterations)
        {
            var z = start;
            iterations = 0;

            const int maxIterations = 30;
            const double tolerance = 0.5;

            // guard against exact zero
            if (Math.Abs(z.Real) < double.Epsilon)
                z = new ComplexNumber(0.0001, z.Imaginary);
            if (Math.Abs(z.Imaginary) < double.Epsilon)
                z = new ComplexNumber(z.Real, 0.0001);

            for (int i = 0; i < maxIterations; i++)
            {
                var diff = polynomial.Eval(z).Divide(derivative.Eval(z));
                z = z.Subtract(diff);

                if (diff.MagnitudeSquared >= tolerance)
                {
                    i--;
                }

                iterations++;
            }

            return z;
        }

        private int GetRootIndex(List<ComplexNumber> roots, ComplexNumber root)
        {
            const double rootTolerance = 0.01;

            for (int i = 0; i < roots.Count; i++)
            {
                var known = roots[i];

                double distance =
                    Math.Pow(root.Real - known.Real, 2) +
                    Math.Pow(root.Imaginary - known.Imaginary, 2);

                if (distance <= rootTolerance)
                {
                    return i;
                }
            }

            roots.Add(root);
            return roots.Count - 1;
        }

        private Color GetColorForRoot(int rootIndex, int iterations)
        {
            var baseColor = palette[rootIndex % palette.Length];

            int r = Clamp(baseColor.R - iterations * 2);
            int g = Clamp(baseColor.G - iterations * 2);
            int b = Clamp(baseColor.B - iterations * 2);

            return Color.FromArgb(r, g, b);
        }

        private static int Clamp(int value) =>
            Math.Min(Math.Max(0, value), 255);
    }
}
