using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore
{
    /// <summary>
    /// Simple polynomial: p(z) = sum_i Coefficients[i] * z^i
    /// Coefficients are ComplexNumber values, index = power.
    /// </summary>
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; } = new();

        /// <summary>Compute derivative p'(z).</summary>
        public Polynomial Derivative()
        {
            var d = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
                d.Coefficients.Add(Coefficients[i] * i); // coefficient * power
            return d;
        }

        /// <summary>Evaluate polynomial at complex z (Horner's method).</summary>
        public ComplexNumber Evaluate(ComplexNumber z)
        {
            if (Coefficients.Count == 0) return ComplexNumber.Zero;

            var acc = Coefficients[^1]; // highest power
            for (int i = Coefficients.Count - 2; i >= 0; i--)
                acc = acc * z + Coefficients[i];
            return acc;
        }

        /// <summary>Evaluate at real x (as z = x + 0i).</summary>
        public ComplexNumber Evaluate(double x) => Evaluate(new ComplexNumber(x, 0));

        public override string ToString()
        {
            if (Coefficients.Count == 0) return "0";
            var parts = new List<string>();
            for (int i = 0; i < Coefficients.Count; i++)
            {
                var sb = new StringBuilder(Coefficients[i].ToString());
                sb.Append(new string('x', i));
                parts.Add(sb.ToString());
            }
            return string.Join(" + ", parts);
        }
        
         // Small helper so Program stays self-contained and clear
        public static Polynomial BuildPolynomialZ3Plus1()
        {
            // p(z) = z^3 + 1 (coefficients are in ascending power order)
            var p = new Polynomial();
            p.Coefficients.Add(new ComplexNumber(1, 0)); // c0 = 1
            p.Coefficients.Add(new ComplexNumber(0, 0)); // c1 = 0
            p.Coefficients.Add(new ComplexNumber(0, 0)); // c2 = 0
            p.Coefficients.Add(new ComplexNumber(1, 0)); // c3 = 1
            return p;
        }
    }
}