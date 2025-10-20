using System;

namespace NNPTPZ1
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public ComplexNumber(double real)
        {
            Real = real;
        }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public override bool Equals(object obj)
        {
            return obj is ComplexNumber other && Equals(other);
        }

        public bool Equals(ComplexNumber other)
        {
            if (other is null)
            {
                return false;
            }

            return Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Real.GetHashCode() * 397) ^ Imaginary.GetHashCode();
            }
        }

        public static readonly ComplexNumber Zero = new ComplexNumber(0, 0);

        public ComplexNumber Multiply(ComplexNumber b)
        {
            var a = this;
            return new ComplexNumber(
                a.Real * b.Real - a.Imaginary * b.Imaginary,
                a.Real * b.Imaginary + a.Imaginary * b.Real
            );
        }

        public double CalculateAbsoluteValue()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            var a = this;
            return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan2(Imaginary, Real);
        }

        public double GetAngleInDegrees()
        {
            return GetAngleInRadians() * (180.0 / Math.PI);
        }

        public ComplexNumber Subtract(ComplexNumber b)
        {
            var a = this;
            return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        internal ComplexNumber Divide(ComplexNumber b)
        {
            var tmp = Multiply(new ComplexNumber(b.Real, -b.Imaginary));
            var tmp2 = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new ComplexNumber(tmp.Real / tmp2, tmp.Imaginary / tmp2);
        }
    }
}