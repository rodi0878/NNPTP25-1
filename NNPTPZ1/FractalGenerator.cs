using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
   public static class FractalGenerator
    {
        public static Bitmap DrawFractal(
            Polynomial polynomial,
            Polynomial polynomialDerived,
            FractalConfig fractalConfig
        )
        {
            var bmp = new Bitmap(fractalConfig.Width, fractalConfig.Height);
            
            var roots = new List<ComplexNumber>();

            // for every pixel in image...
            for (var i = 0; i < fractalConfig.Width; i++)
            {
                for (var j = 0; j < fractalConfig.Height; j++)
                {
                    var y = fractalConfig.Ymin + j * fractalConfig.Ystep;
                    var x = fractalConfig.Xmin + i * fractalConfig.Xstep;

                    var z = new ComplexNumber()
                    {
                        Real = x,
                        Imaginary = (float)(y)
                    };

                    if (z.Real == 0)
                        z.Real = 0.0001;
                    if (z.Imaginary == 0)
                        z.Imaginary = 0.0001f;


                    float it = 0;
                    for (var q = 0; q < fractalConfig.Iterations; q++)
                    {
                        var diff = polynomial.Evaluate(z).Divide(polynomialDerived.Evaluate(z));
                        z = z.Subtract(diff);

                        if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                        {
                            q--;
                        }

                        it++;
                    }

                    var known = false;
                    var id = 0;
                    for (var w = 0; w < roots.Count; w++)
                    {
                        if (Math.Pow(z.Real - roots[w].Real, 2) + Math.Pow(z.Imaginary - roots[w].Imaginary, 2) <=
                            0.01)
                        {
                            known = true;
                            id = w;
                        }
                    }

                    if (!known)
                    {
                        roots.Add(z);
                        id = roots.Count;
                    }

                    var calculatedColorValue = fractalConfig.Colors[id % fractalConfig.Colors.Length];
                    calculatedColorValue = Color.FromArgb(
                        calculatedColorValue.R,
                        calculatedColorValue.G,
                        calculatedColorValue.B
                    );
                    calculatedColorValue = Color.FromArgb(
                        Math.Min(Math.Max(0, calculatedColorValue.R - (int)it * fractalConfig.ShadingSpeed), 255),
                        Math.Min(Math.Max(0, calculatedColorValue.G - (int)it * fractalConfig.ShadingSpeed), 255),
                        Math.Min(Math.Max(0, calculatedColorValue.B - (int)it * fractalConfig.ShadingSpeed), 255)
                    );
                    bmp.SetPixel(i, j, calculatedColorValue);
                }
            }

            return bmp;
        }
    }
}