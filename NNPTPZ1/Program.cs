using NNPTPZ1.Mathematics;
using NNPTPZ1.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            if (args.Length < 7)
            {
                Console.WriteLine("Použití: NNPTPZ1 <width> <height> <xmin> <xmax> <ymin> <ymax> <outputPath>");
                Console.WriteLine("Příklad: NNPTPZ1 800 600 -2 2 -2 2 out.png");
                return;
            }
            int[] intargs = new int[2];
            for (int i = 0; i < intargs.Length; i++)
            {
                intargs[i] = int.Parse(args[i]);
            }
            double[] doubleargs = new double[4];
            for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }
            string output = args[6];


            Poly p = new Poly();
            p.Coe.Add(new Cplx { Re = 1 });
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(new Cplx { Re = 1 });

            // Nastavení výpočtu
            var opts = new FractalOptions
            {
                Width = intargs[0],
                Height = intargs[1],
                XMin = doubleargs[0],
                XMax = doubleargs[1],
                YMin = doubleargs[2],
                YMax = doubleargs[3],
                OutputPath = output
            };

            var renderer = new NewtonFractalRenderer();
            using (Bitmap bmp = renderer.Render(p, opts))
            {
                bmp.Save(opts.OutputPath ?? "../../../out.png");
            }
        }
    }
}
