using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;


namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        public static void Main(string[] args)
        {
            //example of arguments: 800 600 -1.5 1.5 -1.0 1.0 fractals.bmp 40 1 3 0 6 1 5
            //width, height, xmin, xmax, ymin, ymax, filename, iterations, shading speed, and Polynomial argumetns
            var fractalConfig = new FractalConfig(args);
            //Create Polynomial from arguments
            var polynomial = new Polynomial(args);
            Console.WriteLine(polynomial);
            //Derives Polynomial
            var polynomialDerived = polynomial.Derive();
            Console.WriteLine(polynomialDerived);
            // Draws Fractals onto bitmap
            var bmp = FractalGenerator.DrawFractal(polynomial, polynomialDerived, fractalConfig);
            // saves bitmap to file
            bmp.Save(fractalConfig.FileName);
        }
    }
}