using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace NNPTPZ1
{
    public class FractalConfig
    {
        public FractalConfig(string[] args)
        {
            var intArguments = new int[2];


            for (var i = 0; i < intArguments.Length; i++)
            {
                intArguments[i] = int.Parse(args[i]);
            }

            Width = intArguments[0];
            Height = intArguments[1];

            var doubleArguments = new double[4];

            for (var i = 0; i < doubleArguments.Length; i++)
            {
                doubleArguments[i] = double.Parse(args[i + 2], CultureInfo.InvariantCulture);
            }

            Xmin = doubleArguments[0];
            Xmax = doubleArguments[1];
            Ymin = doubleArguments[2];
            Ymax = doubleArguments[3];
            Xstep = (Xmax - Xmin) / Width;
            Ystep = (Ymax - Ymin) / Height;

            FileName = args.Length > 6 ? args[6] : "../../../out.png";

            Iterations = args.Length > 7 ? int.Parse(args[7]) : 30;
            ShadingSpeed = args.Length > 8 ? int.Parse(args[8]) : 2;
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public double Xmin { get; set; }

        public double Xmax { get; set; }

        public double Ymin { get; set; }

        public double Ymax { get; set; }

        public double Xstep { get; set; }

        public double Ystep { get; set; }

        public string FileName { get; set; }

        public int Iterations { get; set; }

        public int ShadingSpeed { get; set; }

        public Color[] Colors { get; } =
        {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Orange,
            Color.Fuchsia,
            Color.Gold,
            Color.Cyan,
            Color.Magenta
        };
    }
}