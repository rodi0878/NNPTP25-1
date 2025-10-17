using System;

namespace MathCore
{
    /// <summary>
    /// Very small complex number type with the basics we need.
    /// </summary>
    public struct ComplexNumber
    {
        public double Real { get; }
        public double Imag { get; }

        public ComplexNumber(double real, double imag)
        {
            Real = real;
            Imag = imag;
        }

        public static ComplexNumber Zero => new(0, 0);

        public double MagnitudeSquared => Real * Real + Imag * Imag;

        public ComplexNumber Conjugate() => new(Real, -Imag);

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
            => new(a.Real + b.Real, a.Imag + b.Imag);

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
            => new(a.Real - b.Real, a.Imag - b.Imag);

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
            => new(
                a.Real * b.Real - a.Imag * b.Imag,
                a.Real * b.Imag + a.Imag * b.Real
            );

        public static ComplexNumber operator *(ComplexNumber a, double k)
            => new(a.Real * k, a.Imag * k);

        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            double d = b.MagnitudeSquared;
            if (d == 0)
                return new(double.PositiveInfinity, double.PositiveInfinity);

            var n = a * b.Conjugate();
            return new(n.Real / d, n.Imag / d);
        }

        public override string ToString() => $"({Real} + {Imag}i)";

        public override bool Equals(object? obj)
            => obj is ComplexNumber o && o.Real == Real && o.Imag == Imag;

        public override int GetHashCode() => HashCode.Combine(Real, Imag);
    }
}