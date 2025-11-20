using System;

namespace NNPTPZ1.Fractals
{
    public class FractalSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public string OutputPath { get; set; }

        public static FractalSettings FromArguments(string[] args)
        {
            if (args.Length < 7)
            {
                Console.WriteLine("Invalid arguments.");
                Console.WriteLine("Usage: <width> <height> <xmin> <xmax> <ymin> <ymax> <output file>");
                Console.WriteLine("Example: 1000 1000 -2 2 -2 2 output.png");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return null;
            }

            return new FractalSettings
            {
                Width = int.Parse(args[0]),
                Height = int.Parse(args[1]),
                XMin = double.Parse(args[2]),
                XMax = double.Parse(args[3]),
                YMin = double.Parse(args[4]),
                YMax = double.Parse(args[5]),
                OutputPath = args[6]
            };
        }
    }
}
