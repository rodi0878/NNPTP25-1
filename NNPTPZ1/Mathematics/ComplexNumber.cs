using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public static readonly ComplexNumber Zero = new ComplexNumber
        {
            Real = 0,
            Imaginary = 0
        };

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return other.Real.Equals(Real) && other.Imaginary.Equals(Imaginary);
            }
            return false;
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber
            {
                Real = this.Real + other.Real,
                Imaginary = this.Imaginary + other.Imaginary
            };
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber
            {
                Real = this.Real - other.Real,
                Imaginary = this.Imaginary - other.Imaginary
            };
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            return new ComplexNumber
            {
                Real = this.Real * other.Real - this.Imaginary * other.Imaginary,
                Imaginary = this.Real * other.Imaginary + this.Imaginary * other.Real
            };
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;
            var numerator = this.Multiply(new ComplexNumber { Real = other.Real, Imaginary = -other.Imaginary });

            return new ComplexNumber
            {
                Real = numerator.Real / denominator,
                Imaginary = numerator.Imaginary / denominator
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(Imaginary / Real);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }
    }
}