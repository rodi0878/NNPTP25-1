using System;
using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coefficients of the polynomial
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
            Polynomial derivative = new Polynomial();
            for (int index = 1; index < Coefficients.Count; index++)
            {
                derivative.Coefficients.Add(Coefficients[index].Multiply(new Complex() { Real = index }));
            }

            return derivative;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public Complex Evaluate(double x)
        {
            Complex result = Evaluate(new Complex() { Real = x, Imaginary = 0 });
            return result;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public Complex Evaluate(Complex x)
        {
            Complex sum = Complex.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                Complex coefficient = Coefficients[i];
                Complex xPower = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        xPower = xPower.Multiply(x);

                    coefficient = coefficient.Multiply(xPower);
                }

                sum = sum.Add(coefficient);
            }

            return sum;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string result = "";
            int i = 0;
            for (; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        result += "x";
                    }
                }
                if (i+1 < Coefficients.Count)
                result += " + ";
            }
            return result;
        }
    }
}

