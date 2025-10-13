using System;
using System.Collections.Generic;

namespace Mathematics
{
    public class Complex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

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

        public Complex Multiply(Complex b)
        {
            Complex a = this;
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new Complex()
            {
                Real = a.Real * b.Real - a.Imaginary * b.Imaginary,
                Imaginary = (float)(a.Real * b.Imaginary + a.Imaginary * b.Real)
            };
        }
        public double GetAbS()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public Complex Add(Complex b)
        {
            Complex a = this;
            return new Complex()
            {
                Real = a.Real + b.Real,
                Imaginary = a.Imaginary + b.Imaginary
            };
        }
        public double GetAngleInDegrees()
        {
            return Math.Atan(Imaginary / Real);
        }
        public Complex Subtract(Complex b)
        {
            Complex a = this;
            return new Complex()
            {
                Real = a.Real - b.Real,
                Imaginary = a.Imaginary - b.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        internal Complex Divide(Complex b)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            Complex tmp = this.Multiply(new Complex() { Real = b.Real, Imaginary = -b.Imaginary });
            double tmp2 = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new Complex()
            {
                Real = tmp.Real / tmp2,
                Imaginary = (float)(tmp.Imaginary / tmp2)
            };
        }
    }
}