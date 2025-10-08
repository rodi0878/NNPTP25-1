using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        private readonly List<ComplexNumber> _coefficients;

        public Polynomial(params ComplexNumber[] coefficients)
        {
            _coefficients = new List<ComplexNumber>(coefficients);
        }

        public void AddCoefficient(ComplexNumber newCoefficient)
        {
            _coefficients.Add(newCoefficient);
        }

        public ComplexNumber EvaluateAt(double evaluationPoint)
        {
            return EvaluateAt(ComplexNumber.FromDouble(evaluationPoint));
        }

        public ComplexNumber EvaluateAt(ComplexNumber evaluationPoint)
        {
            ComplexNumber result = ComplexNumber.Zero;
            // Horner's method
            for (int i = _coefficients.Count - 1; i >= 0; i--)
            {
                result = result.Multiply(evaluationPoint).Add(_coefficients[i]);
            }

            return result;
        }

        public Polynomial Differentiate()
        {
            Polynomial derivative = new Polynomial();
            for (int i = 1; i < _coefficients.Count; i++)
            {
                derivative.AddCoefficient(_coefficients[i].Multiply(i));
            }

            return derivative;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = _coefficients.Count - 1; i > 0; i--)
            {
                sb.Append(_coefficients[i]).Append("x^").Append(i).Append(" + ");
            }
            sb.Append(_coefficients[0]);

            return sb.ToString();
        }
    }
}
