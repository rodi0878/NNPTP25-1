using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public class Poly
    {
        /// <summary>
        /// Coe
        /// </summary>
        public List<ComplexNumber> Coe { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Poly() => Coe = new List<ComplexNumber>();

        public void Add(ComplexNumber coe) =>
            Coe.Add(coe);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Poly Derive()
        {
            Poly p = new Poly();
            for (int q = 1; q < Coe.Count; q++)
            {
                p.Coe.Add(Coe[q] * (new ComplexNumber(q, 0)));
            }

            return p;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double x)
        {
            var y = Eval(new ComplexNumber(x,0));
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(ComplexNumber x)
        {
            ComplexNumber s = ComplexNumber.Zero;
            for (int i = 0; i < Coe.Count; i++)
            {
                ComplexNumber coef = Coe[i];
                ComplexNumber bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx * x;

                    coef = coef * bx;
                }

                s = s + coef;
            }

            return s;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string s = "";
            int i = 0;
            for (; i < Coe.Count; i++)
            {
                s += Coe[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i + 1 < Coe.Count)
                    s += " + ";
            }
            return s;
        }
    }
}
