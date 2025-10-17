using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathematics
{
    /// <summary>
    /// Represents an immutable polynomial with complex coefficients.
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// Coefficients of the polynomial, starting from the zero-degree term (a_0).
        /// </summary>
        public IReadOnlyList<ComplexNumber> Coefficients { get; }

        /// <summary>
        /// Creates a new polynomial from the given coefficients.
        /// </summary>
        /// <param name="coefficients">Coefficients starting from a_0, a_1, ...</param>
        public Polynomial(params ComplexNumber[] coefficients)
        {
            Coefficients = coefficients?.Length > 0 ? new List<ComplexNumber>(coefficients) : new List<ComplexNumber> { ComplexNumber.Zero };
        }

        /// <summary>
        /// Creates the derivative of this polynomial.
        /// </summary>
        /// <returns>A new Polynomial instance representing the derivative.</returns>
        public Polynomial Derive()
        {
            if (Coefficients.Count < 2)
            {
                return new Polynomial(ComplexNumber.Zero);
            }

            // Přeskočíme první koeficient (konstantu) a každý další vynásobíme jeho původním stupněm.
            var derivativeCoeffs = Coefficients
                .Select((c, i) => c * new ComplexNumber(i, 0))
                .Skip(1)
                .ToArray();

            return new Polynomial(derivativeCoeffs);
        }

        /// <summary>
        /// Evaluates the polynomial at a given point using Horner's method for efficiency.
        /// </summary>
        /// <param name="x">The complex point at which to evaluate the polynomial.</param>
        /// <returns>The result of the evaluation.</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            // Aplikace Hornerova schématu
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = Coefficients.Count - 1; i >= 0; i--)
            {
                result = result * x + Coefficients[i];
            }
            return result;
        }

        /// <summary>
        /// Provides a clean, readable string representation of the polynomial.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = Coefficients.Count - 1; i >= 0; i--)
            {
                var coeff = Coefficients[i];
                // Ignorujeme členy s nulovým koeficientem
                if (coeff.RealPart == 0 && coeff.ImaginaryPart == 0) continue;

                if (sb.Length > 0)
                {
                    sb.Append(" + ");
                }

                sb.Append(coeff);

                if (i > 0)
                {
                    sb.Append($"x^{i}");
                }
            }
            return sb.ToString();
        }
    }
}
