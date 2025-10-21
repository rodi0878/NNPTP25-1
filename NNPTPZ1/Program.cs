using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;
using System.Globalization;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Argument validation
            if (args.Length < 7)
            {
                Console.WriteLine("Usage: NNPTPZ1 <width> <height> <xmin> <xmax> <ymin> <ymax> <outputPath>");
                return;
            }

            int width = int.Parse(args[0], CultureInfo.InvariantCulture);
            int height = int.Parse(args[1], CultureInfo.InvariantCulture);
            double xmin = double.Parse(args[2], CultureInfo.InvariantCulture);
            double xmax = double.Parse(args[3], CultureInfo.InvariantCulture);
            double ymin = double.Parse(args[4], CultureInfo.InvariantCulture);
            double ymax = double.Parse(args[5], CultureInfo.InvariantCulture);
            string output = args[6];

            const int MaxIterations = 30;
            const double ConvergenceThreshold = 0.5;
            const float Epsilon = 0.0001f;
            const double RootTolerance = 0.01;

            var bmp = new Bitmap(width, height);

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            List<Cplx> roots = new List<Cplx>();

            // TODO: poly should be parameterised?
            Poly p = new Poly();
            p.Coe.Add(new Cplx() { Re = 1 });
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(new Cplx() { Re = 1 });
            Poly pd = p.Derive();



            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            // for every pixel in image...
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var ox = PixelToWorld(i, j, xmin, ymin, xstep, ystep, Epsilon);

                    int it = NewtonIterate(ref ox, p, pd, MaxIterations, ConvergenceThreshold);

                    var id = FindRootIndex(roots, ox, RootTolerance);

                    var baseColor = colors[id % colors.Length];
                    var shaded = ShadeByIterations(baseColor, it);

                    bmp.SetPixel(j, i, shaded);
                }
            }

                    bmp.Save(output ?? "../../../out.png");
        }

        private static Cplx PixelToWorld(int i, int j, double xmin, double ymin, double xstep, double ystep, float eps)
        {
            double y = ymin + i * ystep;
            double x = xmin + j * xstep;
            var ox = new Cplx { Re = x, Imaginari = (float)y };
            if (ox.Re == 0) ox.Re = eps;
            if (ox.Imaginari == 0) ox.Imaginari = eps;
            return ox;
        }

        private static int NewtonIterate(ref Cplx ox, Poly p, Poly pd, int maxIter, double threshold)
        {
            int it = 0;
            for (int q = 0; q < maxIter; q++)
            {
                var diff = p.Eval(ox).Divide(pd.Eval(ox));
                ox = ox.Subtract(diff);
                if (Math.Pow(diff.Re, 2) + Math.Pow(diff.Imaginari, 2) >= threshold) q--;
                it++;
            }
            return it;
        }

        private static int FindRootIndex(List<Cplx> roots, Cplx ox, double rootTolerance)
        {
            for (int w = 0; w < roots.Count; w++)
                if (Math.Pow(ox.Re - roots[w].Re, 2) + Math.Pow(ox.Imaginari - roots[w].Imaginari, 2) <= rootTolerance)
                    return w;
            roots.Add(ox);
            return roots.Count;
        }

        private static Color ShadeByIterations(Color baseColor, int it)
        {
            return Color.FromArgb(
                Math.Min(Math.Max(0, baseColor.R - it * 2), 255),
                Math.Min(Math.Max(0, baseColor.G - it * 2), 255),
                Math.Min(Math.Max(0, baseColor.B - it * 2), 255)
            );
        }
    }

}
