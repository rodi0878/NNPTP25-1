using System;
namespace Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }


        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public ComplexNumber Multiply(ComplexNumber multiplicand)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart * multiplicand.RealPart - ImaginaryPart * multiplicand.ImaginaryPart,
                ImaginaryPart = RealPart * multiplicand.ImaginaryPart + ImaginaryPart * multiplicand.RealPart
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public ComplexNumber Add(ComplexNumber addend)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart + addend.RealPart,
                ImaginaryPart = ImaginaryPart + addend.ImaginaryPart
            };
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart - subtrahend.RealPart,
                ImaginaryPart = ImaginaryPart - subtrahend.ImaginaryPart
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        internal ComplexNumber Divide(ComplexNumber divisor)
        {
            var denominator = divisor.RealPart * divisor.RealPart + divisor.ImaginaryPart * divisor.ImaginaryPart;

            var real = (RealPart * divisor.RealPart + ImaginaryPart * divisor.ImaginaryPart) / denominator;
            var imaginary = (ImaginaryPart * divisor.RealPart - RealPart * divisor.ImaginaryPart) / denominator;

            return new ComplexNumber()
            {
                RealPart = real,
                ImaginaryPart = imaginary
            };
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = obj as ComplexNumber;
                return x.RealPart == RealPart && x.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }
    }
}
