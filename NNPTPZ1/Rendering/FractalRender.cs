using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Rendering
{
    public class FractalRenderer
    {
        private readonly Poly polynomial;
        private readonly Poly derivative;
        private readonly Color[] colors = {
            Color.Red, Color.Blue, Color.Green, Color.Yellow,
            Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        public FractalRenderer(Poly p)
        {
            polynomial = p;
            derivative = p.Derive();
        }

        // Renders Newton fractal into bitmap
        public Bitmap Render(int width, int height, double xmin, double xmax, double ymin, double ymax)
        {
            var bmp = new Bitmap(width, height);
            var roots = new List<Cplx>();

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    double x = xmin + j * xstep;
                    double y = ymin + i * ystep;
                    var c = IterateNewton(new Cplx { Re = x, Imaginari = (float)y });

                    int rootId = GetRootIndex(roots, c);
                    if (rootId == -1)
                    {
                        roots.Add(c);
                        rootId = roots.Count - 1;
                    }

                    var color = colors[rootId % colors.Length];
                    bmp.SetPixel(j, i, color);
                }
            }

            return bmp;
        }

        private Cplx IterateNewton(Cplx z)
        {
            for (int q = 0; q < 30; q++)
            {
                var diff = polynomial.Eval(z).Divide(derivative.Eval(z));
                z = z.Subtract(diff);
            }
            return z;
        }

        private int GetRootIndex(List<Cplx> roots, Cplx candidate)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(candidate.Re - roots[i].Re, 2) + Math.Pow(candidate.Imaginari - roots[i].Imaginari, 2) <= 0.01)
                    return i;
            }
            return -1;
        }
    }
}
