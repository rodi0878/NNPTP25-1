using System;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Represents a complex number with real and imaginary parts.
    /// </summary>
    public class ComplexNumber
    {
        /// <summary>
        /// Real part of the complex number.
        /// </summary>
        public double Real { get; set; }

        /// <summary>
        /// Imaginary part of the complex number.
        /// </summary>
        public double Imaginary { get; set; }
        /// <summary>
        /// Represents the complex number 0 + 0i.
        /// </summary>
        public static readonly ComplexNumber Zero = new ComplexNumber { Real = 0, Imaginary = 0 };

        /// <summary>
        /// Returns the magnitude (absolute value) of the complex number.
        /// </summary>
        public double Magnitude()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }
        /// <summary>
        /// Returns the angle of the complex number in radians.
        /// </summary>
        public double GetAngleInRadians()
        {
            return Math.Atan2(Imaginary, Real);
        }
        /// <summary>
        /// Adds another complex number to this one.
        /// </summary>
        /// <param name="addend">The complex number to add.</param>
        /// <returns>The result of the addition.</returns>
        public ComplexNumber Add(ComplexNumber addend)
        {
            return new ComplexNumber()
            {
                Real = Real + addend.Real,
                Imaginary = Imaginary + addend.Imaginary
            };
        }
        /// <summary>
        /// Subtracts another complex number from this one.
        /// </summary>
        /// <param name="subtrahend">The complex number to subtract.</param>
        /// <returns>The result of the subtraction.</returns>
        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            return new ComplexNumber()
            {
                Real = Real - subtrahend.Real,
                Imaginary = Imaginary - subtrahend.Imaginary
            };
        }
        /// <summary>
        /// Multiplies this complex number with another complex number.
        /// </summary>
        /// <param name="multiplier">The complex number to multiply by.</param>
        /// <returns>The result of the multiplication.</returns>
        public ComplexNumber Multiply(ComplexNumber multiplier)
        {
            return new ComplexNumber()
            {
                Real = Real * multiplier.Real - Imaginary * multiplier.Imaginary,
                Imaginary = Real * multiplier.Imaginary + Imaginary * multiplier.Real
            };
        }
        /// <summary>
        /// Divides this complex number by another complex number.
        /// </summary>
        /// <param name="divisor">The complex number to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="DivideByZeroException">Thrown when divisor is zero.</exception>
        public ComplexNumber Divide(ComplexNumber divisor)
        {
            if (divisor.Equals(Zero))
                throw new DivideByZeroException("Cannot divide by a zero complex number.");
            var numeratorComplex = this.Multiply(new ComplexNumber() { Real = divisor.Real, Imaginary = -divisor.Imaginary });
            var denominator = divisor.Real * divisor.Real + divisor.Imaginary * divisor.Imaginary;

            return new ComplexNumber()
            {
                Real = numeratorComplex.Real / denominator,
                Imaginary = numeratorComplex.Imaginary / denominator
            };
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current complex number.
        /// </summary>
        /// <param name="obj">The object to compare with the current complex number.</param>
        /// <returns>True if the specified object is a ComplexNumber and has the same real and imaginary parts; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = obj as ComplexNumber;
                return x.Real == Real && x.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }
        /// <summary>
        /// Returns a string representation of the complex number in the form "(Real + Imaginaryi)".
        /// </summary>
        /// <returns>A string representing the complex number.</returns>
        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }
    }
}
