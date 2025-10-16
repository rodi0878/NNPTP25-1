using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Poly
    {
        /// <summary>
        /// Coefficients list: a0, a1, a2, ... (index = power).
        /// </summary>
        public List<Cplx> CoefficientsList { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Poly() => CoefficientsList = new List<Cplx>();

        public void Add(Cplx coe) =>
            CoefficientsList.Add(coe);

        /// <summary>
        /// Derives this polynomial and creates a new one
        /// </summary>
        /// <returns>Derived polynomial</returns>
        public Poly Derive()
        {
            Poly derived = new Poly();
            for (int i = 1; i < CoefficientsList.Count; i++)
            {
                derived.CoefficientsList.Add(CoefficientsList[i].Multiply(new Cplx() { Re = i }));
            }

            return derived;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public Cplx Eval(double x)
        {
            var y = Eval(new Cplx() { Re = x, Imaginari = 0 });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public Cplx Eval(Cplx x)
        {
            Cplx sum = Cplx.Zero;
            for (int power = 0; power < CoefficientsList.Count; power++)
            {
                Cplx term = CoefficientsList[power];
                Cplx xPow = x;

                if (power > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        xPow = xPow.Multiply(x);

                    term = term.Multiply(xPow);
                }

                sum = sum.Add(term);
            }

            return sum;
        }

        /// <summary>
        /// String representation of polynomial
        /// </summary>
        public override string ToString()
        {
            string s = "";
            int i = 0;
            for (; i < CoefficientsList.Count; i++)
            {
                s += CoefficientsList[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i + 1 < CoefficientsList.Count)
                    s += " + ";
            }
            return s;
        }
    }
}
