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

        public ComplexNumber Multiply(ComplexNumber b)
        {
            ComplexNumber a = this;
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                RealPart = a.RealPart * b.RealPart - a.ImaginaryPart * b.ImaginaryPart,
                ImaginaryPart = (float)(a.RealPart * b.ImaginaryPart + a.ImaginaryPart * b.RealPart)
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

        public ComplexNumber Subtract(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart - b.RealPart,
                ImaginaryPart = a.ImaginaryPart - b.ImaginaryPart
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        internal ComplexNumber Divide(ComplexNumber b)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            var tmp = this.Multiply(new ComplexNumber() { RealPart = b.RealPart, ImaginaryPart = -b.ImaginaryPart });
            var tmp2 = b.RealPart * b.RealPart + b.ImaginaryPart * b.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = tmp.RealPart / tmp2,
                ImaginaryPart = (float)(tmp.ImaginaryPart / tmp2)
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
