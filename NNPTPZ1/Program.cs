using System;
using System.Collections.Generic;
using System.Drawing;
using Mathematics;

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
            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string outputPath = args[6];

            Size imageSize = new Size(width, height);

            // TODO: add parameters from args?
            Bitmap output = new Bitmap(imageSize.Width, imageSize.Height);

            double xstep = (xmax - xmin) / imageSize.Width;
            double ystep = (ymax - ymin) / imageSize.Height;

            List<Complex> koreny = new List<Complex>();
            // TODO: poly should be parameterised?
            Polynomial polynomial = new Polynomial(new List<Complex>
            {
                new Complex() { Real = 1 },
                Complex.Zero,
                Complex.Zero,
                new Complex() { Real = 1 }
            });

            Polynomial polyTmp = polynomial;
            Polynomial polyDerivative = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(polyDerivative);

            Color[] colorPalete = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };


            for (int pixelY = 0; pixelY < imageSize.Height; pixelY++)
            {
                for (int pixelX = 0; pixelX < imageSize.Width; pixelX++)
                {
                    // find "world" coordinates of pixel
                    double y = ymin + pixelY * ystep;
                    double x = xmin + pixelX * xstep;

                    Complex complexCoords = new Complex()
                    {
                        Real = x,
                        Imaginary = y
                    };

                    complexCoords.Real = complexCoords.Real == 0 ? 0.0001 : complexCoords.Real;
                    complexCoords.Imaginary = complexCoords.Imaginary == 0 ? 0.0001 : complexCoords.Imaginary;

                    for (int q = 0; q < 30;)
                    {
                        Complex diff = polynomial.Eval(complexCoords).Divide(polyDerivative.Eval(complexCoords));
                        complexCoords = complexCoords.Subtract(diff);

                        if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) < 0.5)
                        {
                            q++;
                        }
                    }

                    // find solution root number
                    bool known = false;
                    int pocetKorenu = 0;
                    for (int w = 0; w < koreny.Count; w++)
                    {
                        if (Math.Pow(complexCoords.Real - koreny[w].Real, 2) + Math.Pow(complexCoords.Imaginary - koreny[w].Imaginary, 2) <= 0.01)
                        {
                            known = true;
                            pocetKorenu = w;
                        }
                    }
                    if (!known)
                    {
                        koreny.Add(complexCoords);
                        pocetKorenu = koreny.Count;
                    }

                    // colorize pixel according to root number
                    Color pixelColor = colorPalete[pocetKorenu % colorPalete.Length];
                    output.SetPixel(pixelX, pixelY, pixelColor);
                }
            }

            output.Save(outputPath ?? "../../../out.png");
        }
    }
}
