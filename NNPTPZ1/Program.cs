using System;
using System.Collections.Generic;

using System.Drawing;


namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {

        public static Color[] colourPallete = { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta };
        public static List<ComplexNumber> roots = null;

        static void Main(string[] args)
        {
            // TODO: x y Coordinates
            // TODO: argument parser
            // TODO: hide poly adding coefieients
            // TODO: 
            Tuple<int, int> sizeInpixels;
            Tuple<double, double> min;
            Tuple<double, double> max;
            string fileName;

            (sizeInpixels, min, max, fileName) = ParseArgs(args);
            // TODO: add parameters from args?
            Bitmap bmp = new Bitmap(sizeInpixels.Item1, sizeInpixels.Item2);

            Tuple<double, double> stepSize = Tuple.Create((max.Item1 - min.Item1) / sizeInpixels.Item1, (max.Item2 - min.Item2) / sizeInpixels.Item2);

            roots = new List<ComplexNumber>();
            // TODO: poly should be parameterised?
            (Polynomial polynom, Polynomial derivatedPolynom) = InitiatePolynoms();
            // for every pixel in image...
            for (int y = 0; y < sizeInpixels.Item1; y++)
            {
                for (int x = 0; x < sizeInpixels.Item2; x++)
                {
                    Color color = determinePixelCollour(polynom, derivatedPolynom, min, Tuple.Create(x, y), stepSize);
                    bmp.SetPixel(x, y, color);
                }
            }
            bmp.Save(fileName ?? "../../../out.png");
        }
        
        public static (Tuple<int, int> sizesInPixels, Tuple<double, double> min, Tuple<double, double> max, string fileName) ParseArgs(string[] args)
        {
            int xPixelSize = int.Parse(args[0]);
            int yPixelSize = int.Parse(args[1]);

            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);

            string output = args[6];
            return (sizesInPixels: Tuple.Create(xPixelSize, yPixelSize), min: Tuple.Create(xmin, ymin), max: Tuple.Create(xmax, ymax), fileName: output);
        }

        public static (Polynomial polyno, Polynomial derivated) InitiatePolynoms()
        {
            Polynomial polynom = new Polynomial();
            polynom.Coeficients.Add(new ComplexNumber() { RealPart = 1 });
            polynom.Coeficients.Add(ComplexNumber.Zero);
            polynom.Coeficients.Add(ComplexNumber.Zero);
            polynom.Coeficients.Add(new ComplexNumber() { RealPart = 1 });
            Polynomial derivatedPolynom = polynom.Derive();

            Console.WriteLine(polynom);
            Console.WriteLine(derivatedPolynom);
            return (polyno: polynom, derivated: derivatedPolynom);
        }

        public static Color determinePixelCollour(Polynomial polynom, Polynomial derivatedPolynom, Tuple<double, double> min, Tuple<int, int> position, Tuple<double, double> stepSize)
        {
            double argumentRealPart = min.Item1 + position.Item1 * stepSize.Item1;
            double argumentImaginariPart = min.Item2 + position.Item2 * stepSize.Item2;
            // TODO: decide if foloving part is stil necesarry or can be deleted
            if (argumentRealPart == 0)
                argumentRealPart = 0.0001;
            if (argumentImaginariPart == 0)
                argumentImaginariPart = 0.0001;
            return getPixelColour(
                ref polynom,
                ref derivatedPolynom,
                new ComplexNumber() { RealPart = argumentRealPart, ImaginariPart = argumentImaginariPart }
            );
        }
        public static Color getPixelColour(ref Polynomial polynom, ref Polynomial derivatedPolynom, ComplexNumber argument)
        {

            // find solution of equation using newton's iteration
            int iterationMethodStepCounter = newtonMethod(ref polynom, ref derivatedPolynom, ref argument);

            // find solution root number
            int rootIdentityDescriptor = rootSelector(ref argument);

            // colorize pixel according to root number
            Color color = colourPallete[rootIdentityDescriptor % colourPallete.Length];
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iterationMethodStepCounter * 2), 255), Math.Min(Math.Max(0, color.G - iterationMethodStepCounter * 2), 255), Math.Min(Math.Max(0, color.B - iterationMethodStepCounter * 2), 255));
            return color;
        }

        public static int newtonMethod(ref Polynomial polynom, ref Polynomial derivatedPolynom, ref ComplexNumber argument)
        {
            int iterationMethodStepCounter = 0;
            for (int i = 0; i < 30; i++)
            {
                var derivate = polynom.Eval(argument).Divide(derivatedPolynom.Eval(argument));
                argument = argument.Subtract(derivate);

                if (Math.Pow(derivate.RealPart, 2) + Math.Pow(derivate.ImaginariPart, 2) >= 0.5)
                    i--;
                iterationMethodStepCounter++;
            }
            return iterationMethodStepCounter;
        }

        public static int rootSelector(ref ComplexNumber argument)
        {
            for (int rootNumber = 0; rootNumber < roots.Count; rootNumber++)
            {
                if (Math.Pow(argument.RealPart - roots[rootNumber].RealPart, 2) + Math.Pow(argument.ImaginariPart - roots[rootNumber].ImaginariPart, 2) <= 0.01)
                {
                    return rootNumber;
                }
            }
            roots.Add(argument);
            return roots.Count;
            
        }

    }
}
