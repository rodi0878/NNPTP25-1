using NNPTPZ1.FractalComponents;

namespace NNPTPZ1
{
    public static class ArgumentParser
    {
        public static FractalParameters Parse(string[] args)
        {
            // Ošetření chybějících argumentů (to by asi nebyla jen refaktorizace)
            return new FractalParameters
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