using System;

namespace NNPTPZ1.Mathematics
{
    /// <summary>Simple complex number with basic arithmetic.</summary>
    public sealed class ComplexNumber
    {
        public double Real { get; set; }
        public double Imag { get; set; }

        /// <summary>Zero constant as a get-only property.</summary>
        public static ComplexNumber Zero { get; } = new ComplexNumber(0, 0);

        public ComplexNumber() { }

        public ComplexNumber(double real, double imag)
        {
            Real = real;
            Imag = imag;
        }

        public ComplexNumber Add(ComplexNumber b) =>
            new ComplexNumber(Real + b.Real, Imag + b.Imag);

        public ComplexNumber Subtract(ComplexNumber b) =>
            new ComplexNumber(Real - b.Real, Imag - b.Imag);

        public ComplexNumber Multiply(ComplexNumber b) =>
            new ComplexNumber(
                Real * b.Real - Imag * b.Imag,
                Real * b.Imag + Imag * b.Real
            );

        public ComplexNumber Divide(ComplexNumber b)
        {
            // (a / b) = (a * conj(b)) / |b|^2
            var num = Multiply(new ComplexNumber(b.Real, -b.Imag));
            var den = b.Real * b.Real + b.Imag * b.Imag;
            return new ComplexNumber(num.Real / den, num.Imag / den);
        }

        /// <summary>Magnitude (absolute value).</summary>
        public double Magnitude() => Math.Sqrt(Real * Real + Imag * Imag);

        /// <summary>Angle in radians (uses Atan2 for correct quadrant).</summary>
        public double AngleInRadians() => Math.Atan2(Imag, Real);

        public override string ToString() => $"({Real} + {Imag}i)";

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber x)
                return x.Real == Real && x.Imag == Imag;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Real.GetHashCode();
                hash = hash * 23 + Imag.GetHashCode();
                return hash;
            }
        }
    }
}
