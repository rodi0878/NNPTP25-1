using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class Polynomal
    {
        /// <summary>
        /// List of Coefficients
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomal() => Coefficients = new List<ComplexNumber>();

        /// <summary>
        /// Parametrised constructor
        /// </summary>
        public Polynomal(params ComplexNumber[] coefficients)
        {
            Coefficients = coefficients.ToList();
        }

        /// <summary>
        /// Adds coefficient at the end of list
        /// </summary>
        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomal Derive()
        {
            Polynomal function = new Polynomal();
            for (int exponent = 1; exponent < Coefficients.Count; exponent++)
            {
                function.Coefficients.Add(Coefficients[exponent] * new ComplexNumber() { Real = exponent });
            }

            return function;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="input">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(double input)
        {
            return Evaluate(new ComplexNumber() { Real = input, Imaginary = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="input">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber input)
        {
            // Horner: result = a_n; for i=n-1..0: result = result * x + a_i
            int degree = Coefficients.Count;
            if (degree == 0) return ComplexNumber.Zero;

            ComplexNumber result = Coefficients[degree - 1];
            for (int coefficientIndex = degree - 2; coefficientIndex >= 0; coefficientIndex--)
            {
                result = result * input + Coefficients[coefficientIndex];
            }

            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < Coefficients.Count; i++)
            {
                builder.Append(Coefficients[i]);

                if (i > 0)
                {
                    builder.Append('x', i);
                }

                if (i + 1 < Coefficients.Count)
                    builder.Append(" + ");
            }

            return builder.ToString();
        }
    }
}
