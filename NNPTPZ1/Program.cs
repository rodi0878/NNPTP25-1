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

            List<Cplx> koreny = new List<Cplx>();

            // TODO: poly should be parameterised?
            Poly p = new Poly();
            p.Coe.Add(new Cplx() { Re = 1 });
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(new Cplx() { Re = 1 });
            Poly pd = p.Derive();



            var clrs = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            // for every pixel in image...
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // find "world" coordinates of pixel
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    Cplx ox = new Cplx()
                    {
                        Re = x,
                        Imaginari = (float)(y)
                    };

                    if (ox.Re == 0)
                        ox.Re = Epsilon;
                    if (ox.Imaginari == 0)
                        ox.Imaginari = Epsilon;

                    // find solution of equation using newton's iteration
                    float it = 0;
                    for (int q = 0; q< MaxIterations; q++)
                    {
                        var diff = p.Eval(ox).Divide(pd.Eval(ox));
                        ox = ox.Subtract(diff);

                        if (Math.Pow(diff.Re, 2) + Math.Pow(diff.Imaginari, 2) >= ConvergenceThreshold)
                        {
                            q--;
                        }
                        it++;
                    }

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w <koreny.Count;w++)
                    {
                        if (Math.Pow(ox.Re- koreny[w].Re, 2) + Math.Pow(ox.Imaginari - koreny[w].Imaginari, 2) <= RootTolerance)
                        {
                            known = true;
                            id = w;
                        }
                    }
                    if (!known)
                    {
                        koreny.Add(ox);
                        id = koreny.Count;
                    }

                    // colorize pixel according to root number
                    var baseColor = clrs[id % clrs.Length];
                    var shaded = Color.FromArgb(
                        Math.Min(Math.Max(0, baseColor.R - (int)it * 2), 255),
                        Math.Min(Math.Max(0, baseColor.G - (int)it * 2), 255),
                        Math.Min(Math.Max(0, baseColor.B - (int)it * 2), 255)
                        );
                    bmp.SetPixel(j, i, shaded);
                }
            }

                    bmp.Save(output ?? "../../../out.png");
        }
    }

}
