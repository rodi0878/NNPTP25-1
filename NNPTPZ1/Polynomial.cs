using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Threading;

namespace Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coefficient
        /// </summary>
        public List<ComplexNumber> Coefficient { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficient = new List<ComplexNumber>();

        ///Overloaded constructor for parametrization
        public Polynomial(ComplexNumber arg1, ComplexNumber arg2, ComplexNumber arg3, ComplexNumber arg4)
        {
            Coefficient = new List<ComplexNumber>();

            this.Coefficient.Add(arg1);
            this.Coefficient.Add(arg2);
            this.Coefficient.Add(arg3);
            this.Coefficient.Add(arg4);
        }

        public void Add(ComplexNumber coe) =>
            Coefficient.Add(coe);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial p = new Polynomial();

            for (int q = 1; q < Coefficient.Count; ++q)
            {
                p.Coefficient.Add(Coefficient[q].Multiply(new ComplexNumber() { Re = q }));
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

            for (int i = 0; i < Coefficient.Count; ++i)
            {
                ComplexNumber coef = Coefficient[i];
                ComplexNumber bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; ++j)
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

            for (var i = 0; i < Coefficient.Count; ++i)
            {
                s += Coefficient[i];

                if (i > 0)
                {
                    for (var j = 0; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i + 1 < Coefficient.Count)
                    s += " + ";
            }

            return s;
        }
    }
}
