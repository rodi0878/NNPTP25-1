using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// Program to generate Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            NewtonFractal.ParseArguments(args);
            NewtonFractal.InitializePolynomials();

            Console.WriteLine("Generating Newton fractal image...");
            NewtonFractal.GenerateFractalImage();

            Console.WriteLine("Image saved successfully.");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}