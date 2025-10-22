using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        private readonly List<ComplexNumber> coefficients = new List<ComplexNumber>();

        public IReadOnlyList<ComplexNumber> Coefficients => coefficients;

        public void AddCoefficient(ComplexNumber coefficient)
        {
            coefficients.Add(coefficient);
        }

        public Polynomial Derive()
        {
            Polynomial derivative = new Polynomial();
            for (int index = 1; index < coefficients.Count; index++)
            {
                ComplexNumber derivedCoefficient = coefficients[index].Multiply(new ComplexNumber(index, 0));
                derivative.AddCoefficient(derivedCoefficient);
            }

            return derivative;
        }

        public ComplexNumber Evaluate(double value)
        {
            return Evaluate(new ComplexNumber(value, 0));
        }

        public ComplexNumber Evaluate(ComplexNumber value)
        {
            ComplexNumber result = ComplexNumber.Zero();
            for (int index = coefficients.Count - 1; index >= 0; index--)
            {
                result = result.Multiply(value).Add(coefficients[index]);
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < coefficients.Count; index++)
            {
                builder.Append(coefficients[index]);
                builder.Append(new string('x', index));
                if (index + 1 < coefficients.Count)
                {
                    builder.Append(" + ");
                }
            }

            return builder.ToString();
        }
    }
}
