using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public static readonly ComplexNumber Zero = new ComplexNumber
        {
            Real = 0,
            Imaginary = 0
        };

        public static readonly ComplexNumber One = new ComplexNumber
        {
            Real = 1,
            Imaginary = 0
        };

        public double Real { get; set; }
        public float Imaginary { get; set; } // Měl by být double (to by asi nebyla jen refaktorizace)

        public double GetMagnitudeSquared()
        {
            return Real * Real + Imaginary * Imaginary;
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(GetMagnitudeSquared());
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(Imaginary / Real);
        }

        public ComplexNumber Conjugate()
        {
            return new ComplexNumber { Real = this.Real, Imaginary = -this.Imaginary };
        }

        public double SquaredDistanceTo(ComplexNumber other)
        {
            double deltaReal = this.Real - other.Real;
            double deltaImaginary = this.Imaginary - other.Imaginary;
            return (deltaReal * deltaReal) + (deltaImaginary * deltaImaginary);
        }

        public ComplexNumber Add(ComplexNumber addend)
        {
            return new ComplexNumber
            {
                Real = this.Real + addend.Real,
                Imaginary = this.Imaginary + addend.Imaginary
            };
        }

        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            return new ComplexNumber
            {
                Real = this.Real - subtrahend.Real,
                Imaginary = this.Imaginary - subtrahend.Imaginary
            };
        }

        public ComplexNumber Multiply(ComplexNumber multiplier)
        {
            return new ComplexNumber
            {
                Real = this.Real * multiplier.Real - this.Imaginary * multiplier.Imaginary,
                Imaginary = (float)(this.Real * multiplier.Imaginary + this.Imaginary * multiplier.Real)
            };
        }


        public ComplexNumber Divide(ComplexNumber divisor)
        {
            double denominator = divisor.GetMagnitudeSquared();
            ComplexNumber numerator = this.Multiply(divisor.Conjugate());

            return new ComplexNumber
            {
                Real = numerator.Real / denominator,
                Imaginary = numerator.Imaginary / (float)denominator
            };
        }

        // Úprava Equals a vytvoření hashcode (to by asi nebyla jen refaktorizace)
        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = obj as ComplexNumber;
                return x.Real == Real && x.Imaginary == Imaginary;
            }

            return base.Equals(obj);
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }
    }
}