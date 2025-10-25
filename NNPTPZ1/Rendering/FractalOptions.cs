namespace NNPTPZ1.Rendering
{
    /// <summary>Configuration for rendering.</summary>
    public sealed class FractalOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }

        public string OutputPath { get; set; }
    }
}
