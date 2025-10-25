using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Polynomial over complex coefficients.
    /// Coefficients are stored low-to-high degree in <see cref="Coe"/>.
    /// </summary>
    public sealed class Poly
    {
        /// <summary>Coefficients list (a0, a1, a2, ...).</summary>
        public List<Cplx> Coe { get; set; }

        public Poly() => Coe = new List<Cplx>();

        public void Add(Cplx coe) => Coe.Add(coe);

        /// <summary>Derivative polynomial (kept identical to original).</summary>
        public Poly Derive()
        {
            Poly p = new Poly();
            for (int q = 1; q < Coe.Count; q++)
                p.Coe.Add(Coe[q].Multiply(new Cplx { Re = q }));
            return p;
        }

        public Cplx Eval(double x) => Eval(new Cplx { Re = x, Imaginari = 0 });

        public Cplx Eval(Cplx x)
        {
            Cplx s = Cplx.Zero;
            for (int i = 0; i < Coe.Count; i++)
            {
                Cplx coef = Coe[i];
                if (i > 0)
                {
                    Cplx bx = x;
                    for (int j = 0; j < i - 1; j++)
                        bx = bx.Multiply(x);

                    coef = coef.Multiply(bx);
                }
                s = s.Add(coef);
            }
            return s;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Coe.Count; i++)
            {
                s += Coe[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                        s += "x";
                }
                if (i + 1 < Coe.Count)
                    s += " + ";
            }
            return s;
        }
    }
}
