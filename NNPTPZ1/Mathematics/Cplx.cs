using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class Cplx
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public static readonly Cplx Zero = new Cplx(0, 0);

        public Cplx(double re, double im)
        {
            Real = re;
            Imaginary = im;
        }

        public Cplx() : this(0, 0) { }

        public Cplx Add(Cplx b) => new Cplx(Real + b.Real, Imaginary + b.Imaginary);

        public Cplx Subtract(Cplx b) => new Cplx(Real - b.Real, Imaginary - b.Imaginary);

        public Cplx Multiply(Cplx b)
            => new Cplx(Real * b.Real - Imaginary * b.Imaginary, Real * b.Imaginary + Imaginary * b.Real);

        public Cplx Divide(Cplx b)
        {
            double denom = b.Real * b.Real + b.Imaginary * b.Imaginary;
            if (denom == 0) throw new DivideByZeroException("Cannot divide by zero complex number.");
            double real = (Real * b.Real + Imaginary * b.Imaginary) / denom;
            double imag = (Imaginary * b.Real - Real * b.Imaginary) / denom;
            return new Cplx(real, imag);
        }

        public double Abs() => Math.Sqrt(Real * Real + Imaginary * Imaginary);

        public double DistanceTo(Cplx other)
        {
            double dr = Real - other.Real;
            double di = Imaginary - other.Imaginary;
            return Math.Sqrt(dr * dr + di * di);
        }

        public override string ToString() => $"({Real} + {Imaginary}i)";

        public override bool Equals(object obj)
        {
            if (obj is Cplx other)
                return Math.Abs(Real - other.Real) < 1e-10 &&
                       Math.Abs(Imaginary - other.Imaginary) < 1e-10;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Math.Round(Real, 10), Math.Round(Imaginary, 10));
        }
    }
}
