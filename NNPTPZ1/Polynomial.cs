using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; }

        public Polynomial()
        {
            Coefficients = new List<ComplexNumber>();
        }

        public void Add(ComplexNumber coefficient)
        {
            Coefficients.Add(coefficient);
        }

        /// <summary>
        /// Derives this polynomial and returns new one.
        /// </summary>
        public Polynomial Derive()
        {
            var derivative = new Polynomial();

            for (int i = 1; i < Coefficients.Count; i++)
            {
                var coef = Coefficients[i];
                derivative.Coefficients.Add(
                    coef.Multiply(new ComplexNumber(i, 0)));
            }

            return derivative;
        }

        public ComplexNumber Eval(double x)
        {
            return Eval(new ComplexNumber(x, 0));
        }

        public ComplexNumber Eval(ComplexNumber x)
        {
            var sum = ComplexNumber.Zero;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                var coef = Coefficients[i];

                if (i == 0)
                {
                    sum = sum.Add(coef);
                    continue;
                }

                var term = coef;

                for (int power = 0; power < i; power++)
                {
                    term = term.Multiply(x);
                }

                sum = sum.Add(term);
            }

            return sum;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Coefficients.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" + ");
                }

                sb.Append(Coefficients[i]);

                for (int xPower = 0; xPower < i; xPower++)
                {
                    sb.Append("x");
                }
            }

            return sb.ToString();
        }
    }
}
