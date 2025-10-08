using System;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program generates Newton fractals from given arguments and saves them into a file.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private sealed class ProgramArguments
        {
            public int BitmapWidth { get; set; }
            public int BitmapHeight { get; set; }
            public double MinX { get; set; }
            public double MaxX { get; set; }
            public double MinY { get; set; }
            public double MaxY { get; set; }
            public string OutputFilePath { get; set; }
        }

        static int Main(string[] args)
        {
            try
            {
                ProgramArguments programArguments = ParseProgramArguments(args);

                using (NewtonFractal newtonFractal = new NewtonFractal(
                    programArguments.BitmapWidth, programArguments.BitmapHeight,
                    programArguments.MinX, programArguments.MaxX, programArguments.MinY, programArguments.MaxY
                ))
                {
                    newtonFractal.GenerateAndSave(programArguments.OutputFilePath);
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("Program failed:");
                Console.Error.WriteLine(exception.ToString());
                Console.ReadKey();
                return 1;
            }

            return 0;
        }

        private static ProgramArguments ParseProgramArguments(string[] args)
        {
            if (args == null || args.Length < 6)
            {
                throw new ArgumentException("Too few arguments. Expected at least 6 numeric parameters.");
            }

            try
            {
                return new ProgramArguments
                {
                    BitmapWidth = int.Parse(args[0]),
                    BitmapHeight = int.Parse(args[1]),
                    MinX = double.Parse(args[2]),
                    MaxX = double.Parse(args[3]),
                    MinY = double.Parse(args[4]),
                    MaxY = double.Parse(args[5]),
                    OutputFilePath = (args.Length == 6) ? null : args[6]
                };
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid numeric format in arguments.");
            }
        }
    }
}
