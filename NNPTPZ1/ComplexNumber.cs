using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public static readonly ComplexNumber Zero = new ComplexNumber { Real = 0, Imaginary = 0 };

        public double Magnitude()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }
        public double GetAngleInRadians()
        {
            return Math.Atan(Imaginary / Real);
        }
        public ComplexNumber Add(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                Real = a.Real + b.Real,
                Imaginary = a.Imaginary + b.Imaginary
            };
        }
        public ComplexNumber Subtract(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                Real = a.Real - b.Real,
                Imaginary = a.Imaginary - b.Imaginary
            };
        }
        public ComplexNumber Multiply(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                Real = a.Real * b.Real - a.Imaginary * b.Imaginary,
                Imaginary = a.Real * b.Imaginary + a.Imaginary * b.Real
            };
        }
        public ComplexNumber Divide(ComplexNumber b)
        {
            var tmp = this.Multiply(new ComplexNumber() { Real = b.Real, Imaginary = -b.Imaginary });
            var tmp2 = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new ComplexNumber()
            {
                Real = tmp.Real / tmp2,
                Imaginary = tmp.Imaginary / tmp2
            };
        }
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
