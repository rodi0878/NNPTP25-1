using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Rendering
{
    /// <summary>
    /// renderer Newton původním Program.Main.
    /// </summary>
    public sealed class NewtonFractalRenderer
    {
        public Bitmap Render(Poly p, FractalOptions opts)
        {
            Bitmap bmp = new Bitmap(opts.Width, opts.Height);

            double xstep = (opts.XMax - opts.XMin) / opts.Width;
            double ystep = (opts.YMax - opts.YMin) / opts.Height;

            List<Cplx> koreny = new List<Cplx>();
            Poly pd = p.Derive();

            // Jen informativní výpisy
            Console.WriteLine(p);
            Console.WriteLine(pd);

            var clrs = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow,
                Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            var maxid = 0;

            for (int i = 0; i < opts.Height; i++)      // i = y (řádek)
            {
                for (int j = 0; j < opts.Width; j++)   // j = x (sloupec)
                {
                    double y = opts.YMin + i * ystep;
                    double x = opts.XMin + j * xstep;

                    Cplx ox = new Cplx { Re = x, Imaginari = (float)y };

                    if (ox.Re == 0) ox.Re = 0.0001;
                    if (ox.Imaginari == 0) ox.Imaginari = 0.0001f;

                    float it = 0;
                    for (int q = 0; q < 30; q++)
                    {
                        var diff = p.Eval(ox).Divide(pd.Eval(ox));
                        ox = ox.Subtract(diff);

                        if (Math.Pow(diff.Re, 2) + Math.Pow(diff.Imaginari, 2) >= 0.5)
                            q--;

                        it++;
                    }

                    var known = false;
                    var id = 0;
                    for (int w = 0; w < koreny.Count; w++)
                    {
                        if (Math.Pow(ox.Re - koreny[w].Re, 2) +
                            Math.Pow(ox.Imaginari - koreny[w].Imaginari, 2) <= 0.01)
                        {
                            known = true;
                            id = w;
                        }
                    }
                    if (!known)
                    {
                        koreny.Add(ox);
                        id = koreny.Count;
                        maxid = id + 1;
                    }

                    var vv = clrs[id % clrs.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    vv = Color.FromArgb(
                        Math.Min(Math.Max(0, vv.R - (int)it * 2), 255),
                        Math.Min(Math.Max(0, vv.G - (int)it * 2), 255),
                        Math.Min(Math.Max(0, vv.B - (int)it * 2), 255)
                    );

                    bmp.SetPixel(j, i, vv);
                }
            }

            return bmp;
        }
    }
}
