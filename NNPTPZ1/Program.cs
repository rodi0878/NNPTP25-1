using NNPTPZ1.Fractals;
using System;
using System.Drawing;

namespace NNPTPZ1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var settings = FractalSettings.FromArguments(args);
                var generator = new FractalGenerator(settings);

                Console.WriteLine("Generating fractal...");
                Bitmap bitmap = generator.Generate();

                ImageSaver.SaveImage(settings.OutputPath, bitmap);
                Console.WriteLine($"Fractal saved to {settings.OutputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
