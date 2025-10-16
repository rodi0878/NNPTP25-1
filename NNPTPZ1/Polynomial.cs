using System;
using System.Collections.Generic;

namespace Mathematics
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
        public Polynomial(List<Complex> complexes) => Coefficients = new List<Complex>(complexes);

        public Polynomial() => Coefficients = new List<Complex>();
        

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial p = new Polynomial();
            for (int q = 1; q < Coefficients.Count; q++)
            {
                p.Coefficients.Add(Coefficients[q].Multiply(new Complex() { Real = q }));
            }

            return p;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public Complex Eval(Complex x)
        {
            Complex sum = Complex.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                Complex coef = Coefficients[i];
                Complex xToPower = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        xToPower = xToPower.Multiply(x);

                    coef = coef.Multiply(xToPower);
                }

                sum = sum.Add(coef);
            }

            return sum;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                s += Coefficients[i];
                if (i > 0)
                {
                    
                    for (int j = 0; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    s += " + ";
            }
            return s;
        }
    }
}