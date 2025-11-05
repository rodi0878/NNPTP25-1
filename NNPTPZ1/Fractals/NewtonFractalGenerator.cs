using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Fractals
{
    /// <summary>
    /// Generates a Newton fractal visualization for a complex polynomial.
    /// </summary>
    public class NewtonFractalGenerator
    {
        private readonly int width;
        private readonly int height;
        private readonly double xmin, xmax, ymin, ymax;
        private readonly Poly polynomial;
        private readonly Poly derivative;

        private readonly Color[] colors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow,
            Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        public NewtonFractalGenerator(int width, int height, double xmin, double xmax, double ymin, double ymax)
        {
            this.width = width;
            this.height = height;
            this.xmin = xmin;
            this.xmax = xmax;
            this.ymin = ymin;
            this.ymax = ymax;

            polynomial = new Poly();
            polynomial.Add(new Cplx(1, 0)); 
            polynomial.Add(Cplx.Zero);
            polynomial.Add(Cplx.Zero);
            polynomial.Add(new Cplx(1, 0));

            derivative = polynomial.Derive();
        }

        public Bitmap Generate()
        {
            var bmp = new Bitmap(width, height);
            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            var roots = new List<Cplx>();

            for (int px = 0; px < width; px++)
            {
                for (int py = 0; py < height; py++)
                {
                    double x = xmin + px * xstep;
                    double y = ymin + py * ystep;

                    var z = new Cplx(x, y);
                    var final = IterateNewton(z);
                    int rootIndex = GetRootIndex(final, roots);
                    Color pixelColor = GetColorForRoot(rootIndex, final);
                    bmp.SetPixel(px, py, pixelColor);
                }
            }

            return bmp;
        }

        private Cplx IterateNewton(Cplx z)
        {
            for (int i = 0; i < 30; i++)
            {
                var numerator = polynomial.Eval(z);
                var denominator = derivative.Eval(z);
                if (denominator.Abs() < 1e-12) break;

                var diff = numerator.Divide(denominator);
                z = z.Subtract(diff);

                if (diff.Abs() < 1e-6)
                    break;
            }
            return z;
        }

        private int GetRootIndex(Cplx root, List<Cplx> roots)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (root.DistanceTo(roots[i]) < 0.01)
                    return i;
            }
            roots.Add(root);
            return roots.Count - 1;
        }

        private Color GetColorForRoot(int rootIndex, Cplx z)
        {
            Color baseColor = colors[rootIndex % colors.Length];
            int brightness = Math.Max(0, 255 - (int)(z.Abs() * 20));
            return Color.FromArgb(
                brightness * baseColor.R / 255,
                brightness * baseColor.G / 255,
                brightness * baseColor.B / 255
            );
        }
    }
}
