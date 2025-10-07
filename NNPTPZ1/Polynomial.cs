using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Gets or sets the coefficients of the polynomial, ordered by increasing power.
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Computes the first derivative of this polynomial and returns it as a new Polynomial.
        /// Coefficients are ordered by increasing power.
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial GetDerivative()
        {
            Polynomial derivativePolynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivativePolynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
            }

            return derivativePolynomial;
        }

        /// <summary>
        /// Evaluates the polynomial at a real value and returns the result as a ComplexNumber.
        /// </summary>
        /// <param name="realValue">point of evaluation</param>
        /// <returns>
        /// The value of the polynomial evaluated at the given real number, returned as a ComplexNumber with zero imaginary part.
        /// </returns>
        public ComplexNumber Evaluate(double realValue)
        {
            return Evaluate(new ComplexNumber() { Real = realValue, Imaginary = 0 });
        }

        /// <summary>
        /// Evaluates the polynomial at a complex point and returns the result as a ComplexNumber.
        /// </summary>
        /// <param name="point">point of evaluation</param>
        /// <returns>
        /// The value of the polynomial evaluated at the given complex point, returned as a ComplexNumber.
        /// </returns>

        public ComplexNumber Evaluate(ComplexNumber point)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber xPower = point;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        xPower = xPower.Multiply(point);

                    coefficient = coefficient.Multiply(xPower);
                }

                result = result.Add(coefficient);
            }
            return result;
        }

        /// <summary>
        /// Returns a string representation of the polynomial in the form of a sum of terms.
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
