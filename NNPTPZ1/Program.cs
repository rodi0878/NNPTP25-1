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
        const string DEFAULT_OUTPUT_PATH = "../../../out.png";
        static int imageWidth, imageHeight;
        static double xMin, xMax, yMin, yMax;
        static string outputPath;

        static void Main(string[] args)
        {
            ParseArguments(args);

            Bitmap bmp = new Bitmap(imageWidth, imageHeight);

            double xstep = (xMax - xMin) / imageWidth;
            double ystep = (yMax - yMin) / imageHeight;

            List<ComplexNumber> koreny = new List<ComplexNumber>();
            Polynome p = new Polynome();
            p.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            Polynome ptmp = p;
            Polynome pd = p.Derive();

            Console.WriteLine(p);
            Console.WriteLine(pd);

            var clrs = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            var maxid = 0;

            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    double y = yMin + i * ystep;
                    double x = xMin + j * xstep;

                    ComplexNumber ox = new ComplexNumber()
                    {
                        RealPart = x,
                        ImaginaryPart = (float)(y)
                    };

                    if (ox.RealPart == 0)
                        ox.RealPart = 0.0001;
                    if (ox.ImaginaryPart == 0)
                        ox.ImaginaryPart = 0.0001f;

                    float it = 0;
                    for (int q = 0; q < 30; q++)
                    {
                        var diff = p.Eval(ox).Divide(pd.Eval(ox));
                        ox = ox.Subtract(diff);

                        if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                        {
                            q--;
                        }
                        it++;
                    }

                    var known = false;
                    var id = 0;
                    for (int w = 0; w < koreny.Count; w++)
                    {
                        if (Math.Pow(ox.RealPart - koreny[w].RealPart, 2) + Math.Pow(ox.ImaginaryPart - koreny[w].ImaginaryPart, 2) <= 0.01)
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
                    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R - (int)it * 2), 255), Math.Min(Math.Max(0, vv.G - (int)it * 2), 255), Math.Min(Math.Max(0, vv.B - (int)it * 2), 255));
                    bmp.SetPixel(j, i, vv);
                }
            }

            bmp.Save(outputPath ?? DEFAULT_OUTPUT_PATH);
        }

        static void ParseArguments(string[] arguments)
        {
            imageWidth = int.Parse(arguments[0]);
            imageHeight = int.Parse(arguments[1]);
            xMin = int.Parse(arguments[2]);
            xMax = int.Parse(arguments[3]);
            yMin = int.Parse(arguments[4]);
            yMax = int.Parse(arguments[5]);
            outputPath = arguments[6];
        }
    }
}
