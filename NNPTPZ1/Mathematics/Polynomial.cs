using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<Complex> Coefficients { get; set; }

        public Polynomial() => Coefficients = new List<Complex>();

        public void Add(Complex coe) =>
            Coefficients.Add(coe);

        public Polynomial Derive()
        {
            Polynomial derivative = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivative.Coefficients.Add(Coefficients[i].Multiply(new Complex() { Real = i }));
            }

            return derivative;
        }

        public Complex Evaluate(double x)
        {
            return Evaluate(new Complex() { Real = x, Imaginary = 0 });
        }

        public Complex Evaluate(Complex x)
        {
            Complex result = Complex.Zero;

            for (int i = Coefficients.Count - 1; i >= 0; i--)
            {
                result = result.Multiply(x).Add(Coefficients[i]);
            }

            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Coefficients.Count; i++)
            {
                sb.Append($"({Coefficients[i]})");
                if (i > 0)
                    sb.Append('x', i);
                if (i < Coefficients.Count - 1)
                    sb.Append(" + ");
            }

            return sb.ToString();
        }
    }
}
