using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class Poly
    {
        public List<Cplx> Coefficients { get; set; }

        public Poly() => Coefficients = new List<Cplx>();

        public void Add(Cplx coe) =>
            Coefficients.Add(coe);

        public Poly Derive()
        {
            Poly derivative = new Poly();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivative.Coefficients.Add(Coefficients[i].Multiply(new Cplx() { Real = i }));
            }

            return derivative;
        }

        public Cplx Evaluate(double x)
        {
            return Evaluate(new Cplx() { Real = x, Imaginary = 0 });
        }

        public Cplx Evaluate(Cplx x)
        {
            Cplx result = Cplx.Zero;

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
