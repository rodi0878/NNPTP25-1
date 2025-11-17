using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public sealed class FractalConfig
    {
        public int Width { get; }
        public int Height { get; }
        public double XMin { get; }
        public double XMax { get; }
        public double YMin { get; }
        public double YMax { get; }
        public string OutputPath { get; }

        private FractalConfig(
            int width,
            int height,
            double xMin,
            double xMax,
            double yMin,
            double yMax,
            string outputPath)
        {
            Width = width;
            Height = height;
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
            OutputPath = outputPath;
        }

        public static FractalConfig FromArgs(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (args.Length < 7) throw new ArgumentException("Not enough arguments.", nameof(args));

            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            double xMin = double.Parse(args[2]);
            double xMax = double.Parse(args[3]);
            double yMin = double.Parse(args[4]);
            double yMax = double.Parse(args[5]);
            string output = args[6];

            return new FractalConfig(width, height, xMin, xMax, yMin, yMax, output);
        }
    }
}
