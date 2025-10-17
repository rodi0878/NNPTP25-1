using System;

namespace Mathematics
{
    /// <summary>
    /// Represents an immutable complex number.
    /// </summary>
    public readonly struct ComplexNumber
    {
        public double RealPart { get;}
        public double ImaginaryPart { get;}
        public static ComplexNumber Zero { get; } = new ComplexNumber(0, 0);
        public ComplexNumber(double real, double imaginary)
        {
            RealPart = real;
            ImaginaryPart = imaginary;
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = (ComplexNumber) obj;
                return x.RealPart == RealPart && x.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + RealPart.GetHashCode();
                hash = hash * 23 + ImaginaryPart.GetHashCode();
                return hash;
            }
        }

        public double Abs()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.RealPart + b.RealPart, a.ImaginaryPart + b.ImaginaryPart);
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.RealPart - b.RealPart, a.ImaginaryPart - b.ImaginaryPart);
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(
                a.RealPart * b.RealPart - a.ImaginaryPart * b.ImaginaryPart,
                a.RealPart * b.ImaginaryPart + a.ImaginaryPart * b.RealPart
            );

        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            double denominator = b.RealPart * b.RealPart + b.ImaginaryPart * b.ImaginaryPart;
            if (denominator == 0)
            {
                return new ComplexNumber(double.NaN, double.NaN);
            }

            return new ComplexNumber(
                (a.RealPart * b.RealPart + a.ImaginaryPart * b.ImaginaryPart) / denominator,
                (a.ImaginaryPart * b.RealPart - a.RealPart * b.ImaginaryPart) / denominator
            );
        }
        public double GetAngleInDegrees()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }
    }
}
