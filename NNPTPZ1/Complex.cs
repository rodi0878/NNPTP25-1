using System;

namespace NNPTPZ1.Mathematics
{
    public class Complex
    {
        public double Real { get; set; }
        public float Imaginary { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Complex)
            {
                Complex x = obj as Complex;
                return x.Real == Real && x.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }

        public readonly static Complex Zero = new Complex()
        {
            Real = 0,
            Imaginary = 0
        };

        /// <summary>
        /// Multiplies this complex number by another
        /// aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
        /// </summary>
        public Complex Multiply(Complex other)
        {
            Complex current = this;
            return new Complex()
            {
                Real = current.Real * other.Real - current.Imaginary * other.Imaginary,
                Imaginary = (float)(current.Real * other.Imaginary + current.Imaginary * other.Real)
            };
        }
        public double GetAbsoluteValue()
        {
            return Math.Sqrt( Real * Real + Imaginary * Imaginary);
        }

        public Complex Add(Complex other)
        {
            Complex current = this;
            return new Complex()
            {
                Real = current.Real + other.Real,
                Imaginary = current.Imaginary + other.Imaginary
            };
        }
        public double GetAngleInRadians()
        {
            return Math.Atan(Imaginary / Real);
        }
        public Complex Subtract(Complex other)
        {
            Complex current = this;
            return new Complex()
            {
                Real = current.Real - other.Real,
                Imaginary = current.Imaginary - other.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        /// <summary>
        /// Divides this complex number by another
        /// (aRe + aIm*i) / (bRe + bIm*i)
        /// ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
        /// bRe*bRe - bIm*bIm*i*i
        /// </summary>
        internal Complex Divide(Complex other)
        {
            Complex numerator = this.Multiply(new Complex() { Real = other.Real, Imaginary = -other.Imaginary });
            double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

            return new Complex()
            {
                Real = numerator.Real / denominator,
                Imaginary = (float)(numerator.Imaginary / denominator)
            };
        }
    }
}

