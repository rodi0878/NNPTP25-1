using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coefficients
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial GetDerivative()
        {
            Polynomial p = new Polynomial();
            for (int q = 1; q < Coefficients.Count; q++)
            {
                p.Coefficients.Add(Coefficients[q].Multiply(new ComplexNumber() { Real = q }));
            }

            return p;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(double x)
        {
            var y = Evaluate(new ComplexNumber() { Real = x, Imaginary = 0 });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber s = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx.Multiply(x);

                    coefficient = coefficient.Multiply(bx);
                }

                s = s.Add(coefficient);
            }
            return s;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                output += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        output += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    output += " + ";
            }
            return output;
        }
    }
}
