using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Polynomial over complex coefficients (a0 + a1 x + a2 x^2 + ...).
    /// </summary>
    public sealed class Polynomial
    {
        /// <summary>Coefficients stored low-to-high degree: a0, a1, a2, ...</summary>
        public List<ComplexNumber> Coefficients { get; set; } = new List<ComplexNumber>();

        public void Add(ComplexNumber c) => Coefficients.Add(c);

        public Polynomial Derive()
        {
            var p = new Polynomial();
            for (int degree = 1; degree < Coefficients.Count; degree++)
            {
                // derivative: degree * a_degree * x^(degree-1)
                p.Coefficients.Add(Coefficients[degree].Multiply(new ComplexNumber(degree, 0)));
            }
            return p;
        }

        public ComplexNumber Eval(double x) => Eval(new ComplexNumber(x, 0));

        public ComplexNumber Eval(ComplexNumber x)
        {
            var sum = ComplexNumber.Zero;
            for (int degree = 0; degree < Coefficients.Count; degree++)
            {
                var term = Coefficients[degree];
                if (degree > 0)
                {
                    var pow = x;
                    for (int k = 0; k < degree - 1; k++)
                        pow = pow.Multiply(x);
                    term = term.Multiply(pow);
                }
                sum = sum.Add(term);
            }
            return sum;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                s += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                        s += "x";
                }
                if (i + 1 < Coefficients.Count) s += " + ";
            }
            return s;
        }
    }
}
