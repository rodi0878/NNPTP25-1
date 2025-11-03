using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal static class FractalOperations
    {
        public static Complex PixelToWorld(int i, int j, double xmin, double ymin, double xstep, double ystep, double eps)
        {
            double x = xmin + i * ystep;
            double y = ymin + j * xstep;
            var ox = new Complex(x, y);
            if (ox.Real == 0) ox.Real = eps;
            if (ox.Imaginary == 0) ox.Imaginary = eps;
            return ox;
        }

        public static int NewtonIterate(ref Complex ox, Polynomial p, Polynomial pd, int maxIter, double threshold)
        {
            int it = 0;
            for (int q = 0; q < maxIter; q++)
            {
                var diff = p.Eval(ox).Divide(pd.Eval(ox));
                ox = ox.Subtract(diff);
                if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= threshold) q--;
                it++;
            }
            return it;
        }

        public static int FindRootIndex(List<Complex> roots, Complex ox, double rootTolerance)
        {
            for (int w = 0; w < roots.Count; w++)
                if (Math.Pow(ox.Real - roots[w].Real, 2) + Math.Pow(ox.Imaginary - roots[w].Imaginary, 2) <= rootTolerance)
                    return w;
            roots.Add(ox);
            return roots.Count;
        }

        public static Color ShadeByIterations(Color baseColor, int it, int shadingSpeed)
        {
            int shade = it * shadingSpeed;
            return Color.FromArgb(
                Math.Min(Math.Max(0, baseColor.R - shade), 255),
                Math.Min(Math.Max(0, baseColor.G - shade), 255),
                Math.Min(Math.Max(0, baseColor.B - shade), 255)
            );
        }

    }
}
