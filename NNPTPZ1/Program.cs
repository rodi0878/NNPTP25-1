using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.FractalComponents;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    public static class Program
    {
        static void Main(string[] args)
        {
            FractalParameters parameters = ArgumentParser.Parse(args);

            FractalGenerator generator = new FractalGenerator(parameters);
            PixelResult[,] output = generator.Generate();

            FractalImageRenderer renderer = new FractalImageRenderer(parameters, output);
            renderer.RenderImage();
            renderer.SaveImage();
        }
    }
}