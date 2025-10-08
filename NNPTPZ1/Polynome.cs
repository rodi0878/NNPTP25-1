using System.Collections.Generic;
namespace Mathematics
{
    public class Polynome
    {
        /// <summary>
        /// Coe
        /// </summary>
        public List<ComplexNumber> Coe { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynome() => Coe = new List<ComplexNumber>();

        public void Add(ComplexNumber coe) =>
            Coe.Add(coe);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynome Derive()
        {
            Polynome p = new Polynome();
            for (int q = 1; q < Coe.Count; q++)
            {
                p.Coe.Add(Coe[q].Multiply(new ComplexNumber() { Re = q }));
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
            var y = Eval(new ComplexNumber() { Re = x, Imaginari = 0 });
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
                        bx = bx.Multiply(x);

                    coef = coef.Multiply(bx);
                }

                s = s.Add(coef);
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
