using System.Drawing;
using System.Collections.Generic;
using Mathematics;
using System;

namespace NNPTPZ1
{
    public class FractalRenderer
    {
        private readonly ArgumentProcessor argumentProcessor;
        private static readonly Color[] colours = new Color[]
        {
        Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };
        private ContextState contextState;

        public FractalRenderer(ArgumentProcessor argumentProcessor)
        {
            this.argumentProcessor = argumentProcessor;
        }

        /// Performs calculations for every pixel in the Bitmap.
        /// ContextState is used for storing intermediate calculations instead of passing up to 4 arguments into the individual methods, which is a bad practice (https://refactoring.guru).
        public Bitmap PopulateBitmap()
        {
            var bmp = new Bitmap(argumentProcessor.X, argumentProcessor.Y);
            var polynomialHelper = new Polynomial(new ComplexNumber() { Re = 1 }, ComplexNumber.Zero, ComplexNumber.Zero, new ComplexNumber() { Re = 1 });
            contextState = new ContextState()
            {
                Xstep = (argumentProcessor.Xmax - argumentProcessor.Xmin) / argumentProcessor.X,
                Ystep = (argumentProcessor.Ymax - argumentProcessor.Ymin) / argumentProcessor.Y,
                Koreny = new List<ComplexNumber>(),
                P = polynomialHelper,
                Pd = polynomialHelper.Derive()
            };

            for (int i = 0; i < argumentProcessor.X; ++i)
            {
                for (int j = 0; j < argumentProcessor.Y; ++j)
                {
                    contextState.Ox = CalculateComplexNumber(i, j);

                    var it = FindSolutionNewtonIteration(30);
                    var id = FindSolutionRootNumber();

                    bmp.SetPixel(i, j, ShadePixel(it, id));
                }
            }

            return bmp;
        }

        /// find "world" coordinates of pixel, and calculate a complex number based on their values
        private ComplexNumber CalculateComplexNumber(int i, int j)
        {
            double y = argumentProcessor.Ymin + i * contextState.Ystep;
            double x = argumentProcessor.Xmin + j * contextState.Xstep;
            var ox = new ComplexNumber()
            {
                Re = x,
                Imaginari = (float)(y)
            };

            if (ox.Re == 0)
                ox.Re = 0.0001;
            if (ox.Imaginari == 0)
                ox.Imaginari = 0.0001f;

            return ox;
        }

        /// colorize pixel according to root number
        private Color ShadePixel(float it, int id)
        {
            var vv = colours[id % colours.Length];
            vv = Color.FromArgb(vv.R, vv.G, vv.B);
            var r = Math.Min(Math.Max(0, vv.R - (int)it * 2), 255);
            var g = Math.Min(Math.Max(0, vv.G - (int)it * 2), 255);
            var b = Math.Min(Math.Max(0, vv.B - (int)it * 2), 255);

            return Color.FromArgb(r, g, b);
        }

        /// find solution of equation using newton's iteration
        /// http://atmos.eas.cornell.edu/~mek236/fractal/fractal.html
        private float FindSolutionNewtonIteration(int maxNewtonIterations)
        {
            float it = 0;

            for (var newtonIteration = 0; newtonIteration < maxNewtonIterations; ++newtonIteration)
            {
                var diff = contextState.P.Eval(contextState.Ox).Divide(contextState.Pd.Eval(contextState.Ox));
                contextState.Ox = contextState.Ox.Subtract(diff);

                if (Math.Pow(diff.Re, 2) + Math.Pow(diff.Imaginari, 2) >= 0.5)
                {
                    --newtonIteration;
                }

                ++it;
            }

            return it;
        }

        /// find solution root number
        private int FindSolutionRootNumber()
        {
            var known = false;
            var id = 0;

            for (int w = 0; w < contextState.Koreny.Count; ++w)
            {
                if (Math.Pow(contextState.Ox.Re - contextState.Koreny[w].Re, 2) + Math.Pow(contextState.Ox.Imaginari - contextState.Koreny[w].Imaginari, 2) <= 0.01)
                {
                    known = true;
                    id = w;
                }
            }

            if (!known)
            {
                contextState.Koreny.Add(contextState.Ox);

                id = contextState.Koreny.Count;
            }

            return id;
        }

        /// This inner class is similar to struct (maybe replace?), and in the context of OOP design suffers from the Data Class issue (https://yegor256.com, https://refactoring.guru)
        /// References:
        /// https://refactoring.guru/smells/data-class
        /// https://www.yegor256.com/2014/12/01/orm-offensive-anti-pattern.html
        /// https://www.yegor256.com/2016/07/06/data-transfer-object.html
        /// https://stackoverflow.com/questions/1440952/why-are-data-transfer-objects-dtos-an-anti-pattern
        /// https://stackoverflow.com/questions/11014501/dto-classes-vs-struct
        private class ContextState
        {
            public double Xstep { set; get; }
            public double Ystep { set; get; }
            public Polynomial P { set; get; }
            public Polynomial Pd { set; get; }
            public List<ComplexNumber> Koreny { set; get; }
            public ComplexNumber Ox { set; get; }
        }
    }
}
