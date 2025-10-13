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

        public void Add(ComplexNumber coefficients) =>
            Coefficients.Add(coefficients);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial result = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                result.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber { Real = i }));
            }

            return result;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="point">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(double point)
        {
            ComplexNumber result = Evaluate(new ComplexNumber { Real = point, Imaginary = 0 });
            return result;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="point">point of evaluation</param>
        /// <returns>result</returns>
        public ComplexNumber Evaluate(ComplexNumber point)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber bx = point;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx.Multiply(point);

                    coefficient = coefficient.Multiply(bx);
                }

                result = result.Add(coefficient);
            }

            return result;
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