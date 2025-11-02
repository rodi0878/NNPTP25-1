using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coe
        /// </summary>
        public List<Complex> Coefficient { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficient = new List<Complex>();

        public void Add(Complex coe) =>
            Coefficient.Add(coe);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int q = 1; q < Coefficient.Count; q++)
            {
                polynomial.Coefficient.Add(Coefficient[q].Multiply(new Complex(q, 0)));
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
            for (int i = 0; i < Coefficient.Count; i++)
            {
                Complex coef = Coefficient[i];
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
            for (; i < Coefficient.Count; i++)
            {
                s += Coefficient[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i + 1 < Coefficient.Count)
                    s += " + ";
            }
            return s;
        }
    }
}
