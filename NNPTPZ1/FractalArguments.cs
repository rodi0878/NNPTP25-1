namespace NNPTPZ1
{
    public class FractalArguments
    {
        public FractalArguments(int width, int height, double minX, double maxX, double minY, double maxY, string outputPath)
        {
            Width = width;
            Height = height;
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            OutputPath = outputPath;
        }

        public int Width { get; }
        public int Height { get; }
        public double MinX { get; }
        public double MaxX { get; }
        public double MinY { get; }
        public double MaxY { get; }
        public string OutputPath { get; }
    }
}
