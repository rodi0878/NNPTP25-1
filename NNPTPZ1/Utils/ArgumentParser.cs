namespace NNPTPZ1.Utils
{
    public class Parameters
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public string OutputPath { get; set; }
    }

    public static class ArgumentParser
    {
        // Parses CLI arguments
        public static Parameters Parse(string[] args)
        {
            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string output = args[6];

            return new Parameters
            {
                Width = width,
                Height = height,
                XMin = xmin,
                XMax = xmax,
                YMin = ymin,
                YMax = ymax,
                OutputPath = output
            };
        }
    }
}