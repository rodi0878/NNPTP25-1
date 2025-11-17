using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class ComplexNumber
    {
        public double Real { get; }
        public double Imaginary { get; }

        public static readonly ComplexNumber Zero = new ComplexNumber(0, 0);

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            double real = Real * other.Real - Imaginary * other.Imaginary;
            double imaginary = Real * other.Imaginary + Imaginary * other.Real;
            return new ComplexNumber(real, imaginary);
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber(Real + other.Real, Imaginary + other.Imaginary);
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber(Real - other.Real, Imaginary - other.Imaginary);
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

            var conjugate = new ComplexNumber(other.Real, -other.Imaginary);
            var numerator = this.Multiply(conjugate);

            return new ComplexNumber(
                numerator.Real / denominator,
                numerator.Imaginary / denominator);
        }

        public double Magnitude => Math.Sqrt(Real * Real + Imaginary * Imaginary);

        public double MagnitudeSquared => Real * Real + Imaginary * Imaginary;

        public double GetAngleInRadians() => Math.Atan2(Imaginary, Real);

        public override string ToString() => $"({Real} + {Imaginary}i)";

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary);
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Real.GetHashCode() * 397) ^ Imaginary.GetHashCode();
            }
        }
    }
}
