using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; private set; }

        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void AddCoefficient(ComplexNumber newCoefficient) => Coefficients.Add(newCoefficient);

        public Polynomial Differentiate()
        {
            Polynomial derivative = new Polynomial();
            for (int q = 1; q < this.Coefficients.Count; q++)
            {
                derivative.AddCoefficient(Coefficients[q].Multiply(q));
            }

            return derivative;
        }

        public ComplexNumber EvaluateAt(double evaluationPoint) => EvaluateAt(ComplexNumber.FromDouble(evaluationPoint));

        public ComplexNumber EvaluateAt(ComplexNumber evaluationPoint)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                result = result.Add(Coefficients[i].Multiply(evaluationPoint.Power(i)));
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = Coefficients.Count; i > 0; i--)
            {
                sb.Append(Coefficients[i]).Append("x^").Append(i).Append(" + ");
            }
            sb.Append(Coefficients[0]);

            return sb.ToString();
        }
    }
}
