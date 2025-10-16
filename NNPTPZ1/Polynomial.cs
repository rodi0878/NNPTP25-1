using System;
using System.Collections.Generic;
using System.Globalization;

namespace NNPTPZ1
{
    public class Polynomial
    {
        public Polynomial()
        {
        }
        public Polynomial(string[] args)
        {
            // if parameters are not given draw default
            if (args.Length <= 9)
            {
                Coefficient.Add(new ComplexNumber() { Real = 1 });
                Coefficient.Add(ComplexNumber.Zero);
                Coefficient.Add(ComplexNumber.Zero);
                Coefficient.Add(new ComplexNumber() { Real = 1 });
            }
            else
            {
                for (var i = 9; i < args.Length; i++)
                {
                    Coefficient.Add(
                        new ComplexNumber()
                        {
                            Real = double.Parse(args[i], CultureInfo.InvariantCulture)
                        });
                }
            }

           
        }

        public List<ComplexNumber> Coefficient { get; set; } = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficient.Add(coefficient);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            var p = new Polynomial();
            for (var q = 1; q < Coefficient.Count; q++)
            {
                p.Coefficient.Add(Coefficient[q].Multiply(new ComplexNumber() { Real = q }));
            }

            return p;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(double x)
        {
            var y = Evaluate(new ComplexNumber() { Real = x, Imaginary = 0 });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            var y = ComplexNumber.Zero;
            for (var i = 0; i < Coefficient.Count; i++)
            {
                var coefficient = Coefficient[i];
                var bx = x;
                var power = i;

                if (i > 0)
                {
                    for (var j = 0; j < power - 1; j++)
                        bx = bx.Multiply(x);

                    coefficient = coefficient.Multiply(bx);
                }

                y = y.Add(coefficient);
            }

            return y;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            var s = "";
            var i = 0;
            for (; i < Coefficient.Count; i++)
            {
                s += Coefficient[i];
                if (i > 0)
                {
                    var j = 0;
                    for (; j < i; j++)
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