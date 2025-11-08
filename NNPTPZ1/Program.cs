using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Mathematics;
using static NNPTPZ1.ArgumentParser;
using static NNPTPZ1.Newton;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        public static int XSize;
        public static int YSize;
        public static double XMin;
        public static double XMax;
        public static double YMin;
        public static double YMax;
        public static string? OutputFile;

        private static readonly Rgba32[] Colors =
        [
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        ];

        static void Main(string[] args)
        {
            ParseArguments(args);
            
            double xStep = (XMax - XMin) / XSize;
            double yStep = (YMax - YMin) / YSize;
            
            Image<Rgba32> bitmapImage = new Image<Rgba32>(XSize, YSize);
            
            List<ComplexNumber> roots = new List<ComplexNumber>();
            
            var coefficients = new List<ComplexNumber>
            {
                new() { Real = 1 },
                ComplexNumber.Zero,
                ComplexNumber.Zero,
                new() { Real = 1 }
            };
            Polynomial polynome = new Polynomial(coefficients);
            Polynomial polynomeDerived = polynome.Derive();

            Console.WriteLine(polynome);
            Console.WriteLine(polynomeDerived);
            
            
            // for every pixel in image...
            for (int row = 0; row < XSize; row++)
            {
                for (int column = 0; column < YSize; column++)
                {
                    // find "world" coordinates of pixel
                    double y = YMin + row * yStep;
                    double x = XMin + column * xStep;

                    ComplexNumber complexPosition = new ComplexNumber
                    {
                        Real = x,
                        Imaginary = y
                    };

                    if (complexPosition.Real == 0)
                        complexPosition.Real = 0.0001;
                    if (complexPosition.Imaginary == 0)
                        complexPosition.Imaginary = 0.0001f;
                    
                    var iterations = FindRoot(polynome, polynomeDerived, ref complexPosition);

                    var index = AddUniqueRoot(roots, complexPosition);

                    DrawPixel(index, iterations, bitmapImage, column, row);
                }
            }
            bitmapImage.Save(OutputFile ?? "../../../out.png");
        }

        private static void DrawPixel(int index, float iterations, Image<Rgba32> bitmapImage, int row, int column)
        {
            // Colorize pixel according to root number
            var baseColor = Colors[index % Colors.Length];
        
            // Apply darkness based on iteration count
            var darkenAmount = iterations * 2;
            
            var r = (byte)Math.Max(baseColor.R - darkenAmount, 0);
            var g = (byte)Math.Max(baseColor.G - darkenAmount, 0);
            var b = (byte)Math.Max(baseColor.B - darkenAmount, 0);
        
            bitmapImage[row, column] = new Rgba32(r, g, b, 255);
        }
    }
}
