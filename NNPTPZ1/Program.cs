using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Threading;
using NNPTPZ1.Mathematics;
using NNPTPZ1.Rendering;
using NNPTPZ1.Utils;

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
            var parameters = ArgumentParser.Parse(args);

            Poly p = new Poly();
            p.Coe.Add(new Cplx() { Re = 1 });
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(new Cplx() { Re = 1 });

            var renderer = new FractalRenderer(p);
            var bmp = renderer.Render(
                parameters.Width,
                parameters.Height,
                parameters.XMin,
                parameters.XMax,
                parameters.YMin,
                parameters.YMax
            );

            bmp.Save(parameters.OutputPath ?? "../../../out.png");
        }
    }


}
