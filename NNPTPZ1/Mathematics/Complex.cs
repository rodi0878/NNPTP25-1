using System;

namespace NNPTPZ1.Mathematics
{
    public class Complex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public readonly static Complex Zero = new Complex()
        {
            Real = 0,
            Imaginary = 0
        };

        public Complex Multiply(Complex b)
        {
            Complex a = this;
            return new Complex()
            {
                Real = a.Real * b.Real - a.Imaginary * b.Imaginary,
                Imaginary = (a.Real * b.Imaginary + a.Imaginary * b.Real)
            };
        }
        public double GetAbs()
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
            return Math.Atan2(Imaginary, Real) * 180.0 / Math.PI;
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
            var tmp = this.Multiply(new Complex() { Real = b.Real, Imaginary = -b.Imaginary });
            var tmp2 = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new Complex()
            {
                Real = tmp.Real / tmp2,
                Imaginary = (tmp.Imaginary / tmp2)
            };
        }

        public override bool Equals(object obj)
        {
            var other = obj as Complex;
            if (other == null)
                return false;

            const double eps = 1e-9;

            return Math.Abs(Real - other.Real) < eps
                && Math.Abs(Imaginary - other.Imaginary) < eps;
        }

        public override int GetHashCode()
        {
            int hashCode = -837395861;
            hashCode = hashCode * -1521134295 + Real.GetHashCode();
            hashCode = hashCode * -1521134295 + Imaginary.GetHashCode();
            return hashCode;
        }
    }
}
