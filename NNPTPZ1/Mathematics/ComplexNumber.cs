using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber();

        public ComplexNumber()
        {
            RealPart = 0;
            ImaginaryPart = 0;
        }

        public ComplexNumber(double realPart, double imaginaryPart)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                RealPart = this.RealPart + other.RealPart,
                ImaginaryPart = this.ImaginaryPart + other.ImaginaryPart
            };
        }
        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                RealPart = this.RealPart - other.RealPart,
                ImaginaryPart = this.ImaginaryPart - other.ImaginaryPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                RealPart = this.RealPart * other.RealPart - this.ImaginaryPart * other.ImaginaryPart,
                ImaginaryPart = this.RealPart * other.ImaginaryPart + this.ImaginaryPart * other.RealPart
            };
        }

        // TODO
        internal ComplexNumber Divide(ComplexNumber other)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            // (aRe*aIm + bRe*bIm) + (bRe*aIm - aRe*bIm)i / (bRe*bRe + bIm*bIm)
            // (aRe*aIm + bRe*bIm)/(bRe*bRe + bIm*bIm) + (bRe*aIm - aRe*bIm)i/(bRe*bRe + bIm*bIm)
            double denominator = other.GetMagnitudeSquared();
            return new ComplexNumber()
            {
                RealPart = (this.RealPart * this.ImaginaryPart + other.RealPart * other.ImaginaryPart) / denominator,
                ImaginaryPart = (other.RealPart * this.ImaginaryPart - this.RealPart * other.ImaginaryPart) / denominator
            };
        }

        public double GetMagnitudeSquared()
        {
            return RealPart * RealPart + ImaginaryPart * ImaginaryPart;
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(this.GetMagnitudeSquared());
        }

        public ComplexNumber GetConjugate()
        {
            return new ComplexNumber()
            {
                RealPart = this.RealPart,
                ImaginaryPart = -this.ImaginaryPart
            };
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public double GetAngleInDegrees()
        {
            return this.GetAngleInRadians() * 180 / Math.PI;
        }



        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        public override bool Equals(object obj)
        {
            return obj is ComplexNumber other &&
                RealPart == other.RealPart &&
                ImaginaryPart == other.ImaginaryPart;
        }

        public override int GetHashCode()
        {
            int hashCode = 1382181547;
            hashCode = hashCode * -1521134295 + RealPart.GetHashCode();
            hashCode = hashCode * -1521134295 + ImaginaryPart.GetHashCode();
            return hashCode;
        }
    }
}
