using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace NNPTPZ1
{
    class Program
    {
        static Color AdjustColor(Color baseColor, int iterations)
        {
            int r = Math.Min(Math.Max(0, baseColor.R - iterations * 2), 255);
            int g = Math.Min(Math.Max(0, baseColor.G - iterations * 2), 255);
            int b = Math.Min(Math.Max(0, baseColor.B - iterations * 2), 255);
            return Color.FromArgb(r, g, b);
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 7)
                {
                    Console.WriteLine("Invalid arguments.");
                    Console.WriteLine("Usage: <width> <height> <xmin> <xmax> <ymin> <ymax> <output file>");
                    Console.WriteLine("Example: 1000 1000 -2 2 -2 2 output.png");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }

                int width = int.Parse(args[0]);
                int height = int.Parse(args[1]);
                double xmin = double.Parse(args[2]);
                double xmax = double.Parse(args[3]);
                double ymin = double.Parse(args[4]);
                double ymax = double.Parse(args[5]);
                string outputPath = args[6];

                double xstep = (xmax - xmin) / width;
                double ystep = (ymax - ymin) / height;

                Bitmap bmp = new Bitmap(width, height);

                List<Cplx> roots = new List<Cplx>();
                Poly polynomial = new Poly();
                polynomial.Coefficients.Add(new Cplx() { Real = 1 });
                polynomial.Coefficients.Add(Cplx.Zero);
                polynomial.Coefficients.Add(Cplx.Zero);
                polynomial.Coefficients.Add(new Cplx() { Real = 1 });
                Poly derivative = polynomial.Derive();

                var colors = new Color[]
                {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
                };

                const double epsilon = 0.0001;

                Console.WriteLine("Starting Newton fractal generation...");
                int progressStep = Math.Max(width / 10, 1);

                for (int i = 0; i < width; i++)
                {
                    if (i % progressStep == 0)
                    {
                        Console.WriteLine($"Progress: {i * 100 / width}%");
                    }

                    for (int j = 0; j < height; j++)
                    {
                        double y = ymin + i * ystep;
                        double x = xmin + j * xstep;

                        Cplx currentValue = new Cplx()
                        {
                            Real = x,
                            Imaginary = y
                        };

                        if (currentValue.Real == 0)
                            currentValue.Real = epsilon;
                        if (currentValue.Imaginary == 0)
                            currentValue.Imaginary = 0.0001f;

                        const int maxIterations = 30;
                        const double stepTolerance = 0.5;
                        int iterationCount = 0;
                        for (int k = 0; k < maxIterations; k++)
                        {
                            var newtonStep = polynomial.Evaluate(currentValue).Divide(derivative.Evaluate(currentValue));
                            currentValue = currentValue.Subtract(newtonStep);

                            if (Math.Pow(newtonStep.Real, 2) + Math.Pow(newtonStep.Imaginary, 2) >= stepTolerance)
                            {
                                k--;
                            }
                            iterationCount++;
                        }

                        var rootExists = false;
                        var rootIndex = 0;
                        const double rootTolerance = 0.01;
                        for (int k = 0; k < roots.Count; k++)
                        {
                            if (Math.Pow(currentValue.Real - roots[k].Real, 2) + Math.Pow(currentValue.Imaginary - roots[k].Imaginary, 2) <= rootTolerance)
                            {
                                rootExists = true;
                                rootIndex = k;
                            }
                        }
                        if (!rootExists)
                        {
                            roots.Add(currentValue);
                            rootIndex = roots.Count - 1;
                        }

                        Color pixelColor = colors[rootIndex % colors.Length];
                        pixelColor = AdjustColor(pixelColor, iterationCount);
                        bmp.SetPixel(j, i, pixelColor);
                    }
                }

                Console.WriteLine("Generation finished. Saving image...");
                string fullPath = Path.GetFullPath(outputPath);
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? ".");
                bmp.Save(fullPath);
                Console.WriteLine($"Image saved to {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
