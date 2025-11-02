using System;
using System.Globalization;

namespace NNPTPZ1.Mathematics
{
    public class Complex
    {
        public double Real { get; set; }
        public float Imaginary { get; set; }

        public static readonly Complex Zero = new Complex(0, 0);

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = (float)imaginary;
        }

        public double GetAbS()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public override bool Equals(object obj)
        {
            if (obj is Complex)
            {
                Complex x = obj as Complex;
                return x.Real == Real && x.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int h = 17;
                h = h * 23 + Real.GetHashCode();
                h = h * 23 + Imaginary.GetHashCode();
                return h;
            }
        }

        public Complex Add(Complex b)
        {
            Complex a = this;
            return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }
        
        public Complex Subtract(Complex b)
        {
            Complex a = this;
            return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public Complex Multiply(Complex b)
        {
            Complex a = this;
            return new Complex(
                a.Real * b.Real - a.Imaginary * b.Imaginary,
                a.Real * b.Imaginary + a.Imaginary * b.Real);
        }

        internal Complex Divide(Complex b)
        {
            var tmp = Multiply(new Complex(b.Real, -b.Imaginary));
            var tmp2 = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new Complex(tmp.Real / tmp2, tmp.Imaginary / tmp2);
        }

        public override string ToString()
        {
            return $"({Real.ToString(CultureInfo.InvariantCulture)} + {Imaginary.ToString(CultureInfo.InvariantCulture)}i)";
        }

        public double GetAngleInRadians()
        {
            return Math.Atan2(Imaginary, Real);
        }

        public double GetAngleInDegrees()
        {
            return GetAngleInRadians() * (180.0 / Math.PI);
        }
    }
}
