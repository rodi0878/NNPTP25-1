using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public ComplexNumber()
        {
        }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public double Real { get; set; }
        public double Imaginary { get; set; }

        public static ComplexNumber Zero()
        {
            return new ComplexNumber(0, 0);
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber(Real + other.Real, Imaginary + other.Imaginary);
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber(Real - other.Real, Imaginary - other.Imaginary);
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            double realPart = Real * other.Real - Imaginary * other.Imaginary;
            double imaginaryPart = Real * other.Imaginary + Imaginary * other.Real;
            return new ComplexNumber(realPart, imaginaryPart);
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;
            ComplexNumber conjugate = new ComplexNumber(other.Real, -other.Imaginary);
            ComplexNumber numerator = Multiply(conjugate);
            return new ComplexNumber(numerator.Real / denominator, numerator.Imaginary / denominator);
        }

        public double GetMagnitudeSquared()
        {
            return Real * Real + Imaginary * Imaginary;
        }

        public double GetDistanceSquared(ComplexNumber other)
        {
            double realDifference = Real - other.Real;
            double imaginaryDifference = Imaginary - other.Imaginary;
            return realDifference * realDifference + imaginaryDifference * imaginaryDifference;
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

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
            return Real.GetHashCode() ^ Imaginary.GetHashCode();
        }
    }
}
