using System.Collections.Generic;
using System.Globalization;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coe
        /// </summary>
        public List<Complex> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<Complex>();

        public void Add(Complex coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int q = 1; q < Coefficients.Count; q++)
            {
                polynomial.Coefficients.Add(Coefficients[q].Multiply(new Complex(q, 0)));
            }

            return polynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public Complex Eval(double x)
        {
            var y = Eval(new Complex(x, 0));
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public Complex Eval(Complex x)
        {
            Complex s = Complex.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                Complex coef = Coefficients[i];
                Complex bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx.Multiply(x);

                    coef = coef.Multiply(bx);
                }

                s = s.Add(coef);
            }

            return s;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string s = "";
            int i = 0;
            for (; i < Coefficients.Count; i++)
            {
                s += Coefficients[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    s += " + ";
            }
            return s;
        }

        public static Polynomial FromArgs(string[] args)
        {
            var poly = new Polynomial();

            // no coefficients in args => default 1 + x^3
            if (args == null || args.Length <= 9)
            {
                poly.Coefficients.Add(new Complex(1, 0));
                poly.Coefficients.Add(Complex.Zero);
                poly.Coefficients.Add(Complex.Zero);
                poly.Coefficients.Add(new Complex(1, 0));
                return poly;
            }

            for (int i = 9; i < args.Length; i++)
            {
                double real = double.Parse(args[i], CultureInfo.InvariantCulture);
                poly.Coefficients.Add(new Complex(real, 0));
            }

            return poly;
        }
    }
}
