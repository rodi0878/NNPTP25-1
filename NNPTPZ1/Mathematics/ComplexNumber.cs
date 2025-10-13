using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public float Imaginary { get; set; }
        public double Absolute => Math.Sqrt(Real * Real + Imaginary * Imaginary);
        public double AngleInRadians => Math.Atan(Imaginary / Real);
        public double AngleInDegrees => AngleInRadians * (180.0 / Math.PI);

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                Real = Real + other.Real,
                Imaginary = Imaginary + other.Imaginary
            };
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                Real = Real - other.Real,
                Imaginary = Imaginary - other.Imaginary
            };
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                Real = Real * other.Real - Imaginary * other.Imaginary,
                Imaginary = (float)(Real * other.Imaginary + Imaginary * other.Real)
            };
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            var numerator = Multiply(new ComplexNumber()
            {
                Real = other.Real,
                Imaginary = -other.Imaginary
            });

            var denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

            return new ComplexNumber()
            {
                Real = numerator.Real / denominator,
                Imaginary = (float)(numerator.Imaginary / denominator)
            };
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b) => a.Add(b);

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b) => a.Subtract(b);

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b) => a.Multiply(b);

        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b) => a.Divide(b);

        public static ComplexNumber operator -(ComplexNumber a) => new ComplexNumber()
        {
            Real = -a.Real,
            Imaginary = -a.Imaginary
        };

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return other.Real == Real && other.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }

        public override string ToString() => $"({Real} + {Imaginary}i)";
    }
}
