using System.Drawing;
using System.Globalization;

namespace NNPTPZ1
{
    internal class FractalConfig
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double XMin { get; private set; }
        public double XMax { get; private set; }
        public double YMin { get; private set; }
        public double YMax { get; private set; }
        public string OutputPath { get; private set; }


        public double XStep => (XMax - XMin) / Width;
        public double YStep => (YMax - YMin) / Height;


        public int MaxIterations { get; private set; } = 30;
        public int ShadingSpeed { get; private set; } = 2;
        public double ConvergenceThreshold { get; } = 0.5;
        public double Epsilon { get; } = 0.0001;
        public double RootTolerance { get; } = 0.01;


        public Color[] Colors { get; } =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow,
            Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        public bool IsValid { get; private set; }

        public static FractalConfig FromArgs(string[] args)
        {
            var config = new FractalConfig();

            if (args == null || args.Length < 6)
            {
                config.IsValid = false;
                return config;
            }

            try
            {
                config.Width = int.Parse(args[0], CultureInfo.InvariantCulture);
                config.Height = int.Parse(args[1], CultureInfo.InvariantCulture);
                config.XMin = double.Parse(args[2], CultureInfo.InvariantCulture);
                config.XMax = double.Parse(args[3], CultureInfo.InvariantCulture);
                config.YMin = double.Parse(args[4], CultureInfo.InvariantCulture);
                config.YMax = double.Parse(args[5], CultureInfo.InvariantCulture);

                config.OutputPath = args.Length > 6 ? args[6] : "../../../out.png";
                config.MaxIterations = args.Length > 7 ? int.Parse(args[7], CultureInfo.InvariantCulture) : 30;
                config.ShadingSpeed = args.Length > 8 ? int.Parse(args[8], CultureInfo.InvariantCulture) : 2;
                config.IsValid = true;
            }
            catch
            {
                config.IsValid = false;
            }

            return config;
        }
    }
}
