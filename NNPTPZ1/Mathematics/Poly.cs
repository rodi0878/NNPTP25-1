using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class Poly
    {
        public List<Cplx> Coefficients { get; }

        public Poly()
        {
            Coefficients = new List<Cplx>();
        }

        public void Add(Cplx c) => Coefficients.Add(c);

        public Poly Derive()
        {
            var p = new Poly();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                var derived = Coefficients[i].Multiply(new Cplx(i, 0));
                p.Add(derived);
            }
            return p;
        }

        public Cplx Eval(Cplx x)
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
                sb.Append(Coefficients[i]);
                if (i > 0)
                {
                    sb.Append(new string('x', i));
                }
                if (i < Coefficients.Count - 1)
                    sb.Append(" + ");
            }
            return sb.ToString();
        }
    }
}
