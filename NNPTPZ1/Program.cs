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
            p.CoefficientsList.Add(new Cplx() { Re = 1 });
            p.CoefficientsList.Add(Cplx.Zero);
            p.CoefficientsList.Add(Cplx.Zero);
            p.CoefficientsList.Add(new Cplx() { Re = 1 });

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
