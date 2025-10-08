using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; private set; }
        public double ImaginaryPart { get; private set; }

        public readonly static ComplexNumber Zero = new ComplexNumber();

        public readonly static ComplexNumber One = new ComplexNumber(1);

        public ComplexNumber(double realPart = 0, double imaginaryPart = 0)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }

        public static ComplexNumber FromDouble(double realValue)
        {
            return new ComplexNumber(realValue);
        }

        public ComplexNumber Add(double other)
        {
            return new ComplexNumber(
                this.RealPart + other,
                this.ImaginaryPart
            );
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber(
                this.RealPart + other.RealPart,
                this.ImaginaryPart + other.ImaginaryPart
            );
        }

        public ComplexNumber Subtract(double other)
        {
            return new ComplexNumber(
                this.RealPart - other,
                this.ImaginaryPart
            );
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber(
                this.RealPart - other.RealPart,
                this.ImaginaryPart - other.ImaginaryPart
            );
        }

        public ComplexNumber Multiply(double other)
        {
            return new ComplexNumber(
                this.RealPart * other,
                this.ImaginaryPart * other
            );
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber(
                this.RealPart * other.RealPart - this.ImaginaryPart * other.ImaginaryPart,
                this.RealPart * other.ImaginaryPart + this.ImaginaryPart * other.RealPart
            );
        }

        public ComplexNumber Divide(double other)
        {
            if (other == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            return new ComplexNumber(
                this.RealPart / other,
                this.ImaginaryPart / other
            );
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            // (aRe*bRe + aIm*bIm) + (bRe*aIm - aRe*bIm)i / (bRe*bRe + bIm*bIm)
            // (aRe*bRe + aIm*bIm)/(bRe*bRe + bIm*bIm) + (bRe*aIm - aRe*bIm)i/(bRe*bRe + bIm*bIm)
            double denominator = other.GetMagnitudeSquared();
            if (denominator == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            return new ComplexNumber(
                (this.RealPart * other.RealPart + this.ImaginaryPart * other.ImaginaryPart) / denominator,
                (other.RealPart * this.ImaginaryPart - this.RealPart * other.ImaginaryPart) / denominator
            );
        }

        public ComplexNumber Power(int power)
        {
            ComplexNumber result = ComplexNumber.One;
            bool isPowerPositive = true;
            if (power < 0)
            {
                power = -power;
                isPowerPositive = false;
            }

            for (int i = 0; i < power; i++)
            {
                result = result.Multiply(this);
            }

            return (isPowerPositive) ? result : ComplexNumber.One.Divide(result);
        }

        public double GetMagnitudeSquared()
        {
            return RealPart * RealPart + ImaginaryPart * ImaginaryPart;
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(this.GetMagnitudeSquared());
        }

        public ComplexNumber GetConjugate()
        {
            return new ComplexNumber(this.RealPart, -this.ImaginaryPart);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan2(ImaginaryPart, RealPart);
        }

        public double GetAngleInDegrees()
        {
            return this.GetAngleInRadians() * 180 / Math.PI;
        }

        public double GetEuclideanDistanceTo(ComplexNumber other)
        {
            double realPartDifference = this.RealPart - other.RealPart;
            double imaginaryPartDifference = this.ImaginaryPart - other.ImaginaryPart;
            return Math.Sqrt(realPartDifference * realPartDifference + imaginaryPartDifference * imaginaryPartDifference);
        }

        public override string ToString()
        {
            string sign = (ImaginaryPart >= 0) ? "+" : "-";
            return $"({RealPart} {sign} {Math.Abs(ImaginaryPart)}i)";
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
