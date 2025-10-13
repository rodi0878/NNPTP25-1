using NNPTPZ1.Mathematics;

namespace NNPTPZ1.FractalComponents
{
    public class FractalParameters
    {
        public const int MaxIterations = 30;
        public const double RootTolerance = 0.01;
        public const double DivergenceThreshold = 0.5;

        public ComplexNumber[] Coefficients { get; set; } =
            { ComplexNumber.One, ComplexNumber.Zero, ComplexNumber.Zero, ComplexNumber.One };

        public int Width { get; set; }
        public int Height { get; set; }

        public string OutputPath { get; set; }

        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }

        public double XStep => (XMax - XMin) / Width;
        public double YStep => (YMax - YMin) / Height;
    }
}