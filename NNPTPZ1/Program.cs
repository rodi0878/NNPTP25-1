using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static int[] imageSize;
        static string outputPath;
        static Bitmap bitmap;
        static double xMin, xMax, yMin, yMax, xStep, yStep;
        static List<ComplexNumber> roots;
        static Polynome polynome, derivedPolynome;
        static Color[] colorPalette;
        static int maxRootIndex;
        private const int MaxIterations = 30;
        private const double NewtonIterationTolerance = 0.5;
        private const double RootProximityThreshold = 0.01;
        private const string DefaultOutputPath = "../../../out.png";
        private const double MinimalCoordinateValue = 0.0001;


        static void Main(string[] args)
        {
            ParseCommandLineArguments(args);
            PrepareCalculationEnvironment();
            maxRootIndex = ProcessNewtonIterationAlgorithm();
            SaveOutputImage();
        }

        private static void ParseCommandLineArguments(string[] args)
        {
            const int WidthIndex = 0;
            const int HeightIndex = 1;
            const int XMinIndex = 2;
            const int XMaxIndex = 3;
            const int YMinIndex = 4;
            const int YMaxIndex = 5;
            const int OutputPathIndex = 6;

            imageSize = new int[2];
            imageSize[0] = int.Parse(args[WidthIndex]);
            imageSize[1] = int.Parse(args[HeightIndex]);

            double xMinValue = double.Parse(args[XMinIndex]);
            double xMaxValue = double.Parse(args[XMaxIndex]);
            double yMinValue = double.Parse(args[YMinIndex]);
            double yMaxValue = double.Parse(args[YMaxIndex]);

            outputPath = args[OutputPathIndex];
            bitmap = new Bitmap(imageSize[WidthIndex], imageSize[HeightIndex]);

            xMin = xMinValue;
            xMax = xMaxValue;
            yMin = yMinValue;
            yMax = yMaxValue;

            xStep = (xMax - xMin) / imageSize[WidthIndex];
            yStep = (yMax - yMin) / imageSize[HeightIndex];
        }

        private static void PrepareCalculationEnvironment()
        {
            roots = new List<ComplexNumber>();

            polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber { RealPart = 1 });
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(new ComplexNumber { RealPart = 1 });

            derivedPolynome = polynome.Derive();

            colorPalette = new Color[]
            {
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Yellow,
                Color.Orange,
                Color.Fuchsia,
                Color.Gold,
                Color.Cyan,
                Color.Magenta
            };

            maxRootIndex = 0;
        }

        private static void SaveOutputImage()
        {
            bitmap.Save(outputPath ?? DefaultOutputPath);
        }

        private static int ProcessNewtonIterationAlgorithm()
        {
            for (int i = 0; i < imageSize[0]; i++)
            {
                for (int j = 0; j < imageSize[1]; j++)
                {
                    ProcessPixelUsingNewtonIteration(i, j);
                }
            }

            return maxRootIndex;
        }


        private static void ProcessPixelUsingNewtonIteration(int i, int j)
        {
            ComplexNumber point = PrepareComplexNumberAtPoint(i, j);

            int convergenceIterations = ProcessNewtonIterationOnSinglePixel(ref point);
            int rootIndex = FindPolynomeRoot(point);
            ColorizePixel(i, j, convergenceIterations, rootIndex);
        }

        private static ComplexNumber PrepareComplexNumberAtPoint(int i, int j)
        {
            double y = yMin + i * yStep;
            double x = xMin + j * xStep;

            ComplexNumber point = new ComplexNumber()
            {
                RealPart = x,
                ImaginaryPart = y
            };

            if (point.RealPart == 0)
                point.RealPart = MinimalCoordinateValue;
            if (point.ImaginaryPart == 0)
                point.ImaginaryPart = MinimalCoordinateValue;

            return point;
        }

        private static int ProcessNewtonIterationOnSinglePixel(ref ComplexNumber point)
        {
            int iteration = 0;
            for (int i = 0; i < MaxIterations; i++)
            {
                ComplexNumber differential = polynome.Eval(point).Divide(derivedPolynome.Eval(point));
                point = point.Subtract(differential);

                if (differential.GetAbsoluteValue() >= NewtonIterationTolerance)
                {
                    i--;
                }
                iteration++;
            }

            return iteration;
        }

        private static int FindPolynomeRoot(ComplexNumber point)
        {
            bool knownRoot = false;
            int rootIndex = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (point.Subtract(roots[i]).GetAbsoluteValue() <= RootProximityThreshold)
                {
                    knownRoot = true;
                    rootIndex = i;
                }
            }
            if (!knownRoot)
            {
                roots.Add(point);
                rootIndex = roots.Count;
                maxRootIndex = rootIndex + 1;
            }

            return rootIndex;
        }

        private static void ColorizePixel(int i, int j, int convergenceIterations, int rootIndex)
        {
            Color color = colorPalette[rootIndex % colorPalette.Length];
            color = Color.FromArgb(
                Math.Min(Math.Max(0, color.R - convergenceIterations * 2), 255),
                Math.Min(Math.Max(0, color.G - convergenceIterations * 2), 255),
                Math.Min(Math.Max(0, color.B - convergenceIterations * 2), 255)
                );
            bitmap.SetPixel(j, i, color);
        }

    }


}
