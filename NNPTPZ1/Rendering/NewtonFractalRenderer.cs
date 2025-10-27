using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Rendering
{
    /// <summary>Renderer of Newton fractal (behavior preserved).</summary>
    public sealed class NewtonFractalRenderer
    {
        public Bitmap Render(Polynomial poly, FractalOptions opts)
        {
            Validate(opts);

            Bitmap bmp = new Bitmap(opts.Width, opts.Height);
            var stepX = (opts.XMax - opts.XMin) / opts.Width;
            var stepY = (opts.YMax - opts.YMin) / opts.Height;

            var roots = new List<ComplexNumber>();
            var derivative = poly.Derive();

            Console.WriteLine(poly);
            Console.WriteLine(derivative);

            var palette = new[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow,
                Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            for (int yPix = 0; yPix < opts.Height; yPix++)
            {
                for (int xPix = 0; xPix < opts.Width; xPix++)
                {
                    var z0 = MapPixelToComplex(opts, stepX, stepY, xPix, yPix);
                    var (z, iter) = IterateNewton(poly, derivative, z0);

                    int rootId = ClassifyRoot(roots, z);
                    var color = ShadeColor(palette[rootId % palette.Length], iter);

                    bmp.SetPixel(xPix, yPix, color);
                }
            }

            return bmp;
        }

        private static void Validate(FractalOptions opts)
        {
            if (opts.Width <= 0 || opts.Height <= 0)
                throw new ArgumentOutOfRangeException(nameof(opts.Height), "Width and Height must be positive.");
            if (opts.XMax <= opts.XMin || opts.YMax <= opts.YMin)
                throw new ArgumentOutOfRangeException(nameof(opts.XMax), "XMax > XMin and YMax > YMin required.");
        }

        private static ComplexNumber MapPixelToComplex(FractalOptions opts, double stepX, double stepY, int xPix, int yPix)
        {
            double x = opts.XMin + xPix * stepX;
            double y = opts.YMin + yPix * stepY;

            if (x == 0) x = 0.0001;
            if (y == 0) y = 0.0001;

            return new ComplexNumber(x, y);
        }

        private static (ComplexNumber z, int iterations) IterateNewton(Polynomial p, Polynomial pd, ComplexNumber start)
        {
            var z = start;
            int it = 0;
            for (int k = 0; k < 30; k++)
            {
                var diff = p.Eval(z).Divide(pd.Eval(z));
                z = z.Subtract(diff);

                if (diff.Real * diff.Real + diff.Imag * diff.Imag >= 0.5)
                    k--;

                it++;
            }
            return (z, it);
        }

        private static int ClassifyRoot(List<ComplexNumber> knownRoots, ComplexNumber z)
        {
            for (int idx = 0; idx < knownRoots.Count; idx++)
            {
                var r = knownRoots[idx];
                var dx = z.Real - r.Real;
                var dy = z.Imag - r.Imag;
                if (dx * dx + dy * dy <= 0.01) return idx;
            }
            knownRoots.Add(z);
            return knownRoots.Count;
        }

        private static Color ShadeColor(Color baseColor, int iterations)
        {
            int r = Clamp(baseColor.R - iterations * 2);
            int g = Clamp(baseColor.G - iterations * 2);
            int b = Clamp(baseColor.B - iterations * 2);
            return Color.FromArgb(r, g, b);
        }

        private static int Clamp(int v) => Math.Min(Math.Max(0, v), 255);
    }
}
