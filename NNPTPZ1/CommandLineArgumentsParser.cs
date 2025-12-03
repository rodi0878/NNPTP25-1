using System;

namespace NNPTPZ1
{

    
    /// <summary>
    /// Parses command line arguments into NewtonFractalParameters
    /// </summary>
    public class CommandLineArgumentsParser
    {
        /// <summary>
        /// Parses command line arguments and returns parameters
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>Parsed parameters</returns>
        public static NewtonFractalParameters Parse(string[] args)
        {
            int[] integerArguments = new int[2];
            for (int i = 0; i < integerArguments.Length; i++)
            {
                integerArguments[i] = int.Parse(args[i]);
            }
            double[] doubleArguments = new double[4];
            for (int i = 0; i < doubleArguments.Length; i++)
            {
                doubleArguments[i] = double.Parse(args[i + 2]);
            }
            string output = args[6];

            return new NewtonFractalParameters
            {
                Width = integerArguments[0],
                Height = integerArguments[1],
                XMin = doubleArguments[0],
                XMax = doubleArguments[1],
                YMin = doubleArguments[2],
                YMax = doubleArguments[3],
                OutputPath = output
            };
        }
    }
}

