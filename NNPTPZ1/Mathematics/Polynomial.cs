using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coefficients of the polynomial, where each element represents a complex coefficient.
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Default constructor initializes the list of coefficients.
        /// </summary>
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        /// <summary>
        /// Adds a coefficient to the polynomial.
        /// </summary>
        /// <param name="coefficient">Complex coefficient to add</param>
        public void AddCoefficient(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Derives the polynomial and returns a new polynomial representing its derivative.
        /// </summary>
        /// <returns>Derived polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial derivative = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivative.Coefficients.Add(
                    Coefficients[i].Multiply(new ComplexNumber { Real = i })
                );
            }
            return derivative;
        }

        /// <summary>
        /// Evaluates the polynomial at a given real point.
        /// </summary>
        /// <param name="x">Real point of evaluation</param>
        /// <returns>Result of the evaluation as a complex number</returns>
        public ComplexNumber Evaluate(double x)
        {
            return Evaluate(new ComplexNumber { Real = x, Imaginary = 0 });
        }

        /// <summary>
        /// Evaluates the polynomial at a given complex point.
        /// </summary>
        /// <param name="x">Complex point of evaluation</param>
        /// <returns>Result of the evaluation as a complex number</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            var sum = ComplexNumber.Zero;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                var coefficient = Coefficients[i];

                if (i > 0)
                {
                    var baseX = x;
                    for (int j = 0; j < i - 1; j++)
                    {
                        baseX = baseX.Multiply(x);
                    }
                    coefficient = coefficient.Multiply(baseX);
                }

                sum = sum.Add(coefficient);
            }

            return sum;
        }


        /// <summary>
        /// Returns a string representation of the polynomial.
        /// </summary>
        /// <returns>String representation of the polynomial</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Coefficients.Count; i++)
            {
                if (i > 0) result.Append(" + ");
                result.Append(Coefficients[i]);
                for (int j = 0; j < i; j++)
                {
                    result.Append("x");
                }
            }
            return result.ToString();
        }
    }
}