namespace NNPTPZ1
{
    /// <summary>
    /// Parameters for generating a Newton fractal
    /// </summary>
    public class NewtonFractalParameters
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public string OutputPath { get; set; }
        public int MaxIterations { get; set; } = 30;
        public double ConvergenceThreshold { get; set; } = 0.5;
        public double RootMatchingTolerance { get; set; } = 0.01;
        public double Epsilon { get; set; } = 0.0001;
        public int ColorDarkeningFactor { get; set; } = 2;
    }
}

