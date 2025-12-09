using System;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class ComplexNumber
        {
            public double RealPart { get; set; }
            public double ImaginaryPart { get; set; }

            public static readonly ComplexNumber Zero = new ComplexNumber();


            public ComplexNumber Add(ComplexNumber otherNumber)
            {
                return new ComplexNumber
                {
                    RealPart = RealPart + otherNumber.RealPart,
                    ImaginaryPart = ImaginaryPart + otherNumber.ImaginaryPart
                };
            }

            public ComplexNumber Subtract(ComplexNumber otherNumber)
            {
                return new ComplexNumber
                {
                    RealPart = RealPart - otherNumber.RealPart,
                    ImaginaryPart = ImaginaryPart - otherNumber.ImaginaryPart
                };
            }

            public ComplexNumber Multiply(ComplexNumber otherNumber)
            {
                return new ComplexNumber()
                {
                    RealPart = RealPart * otherNumber.RealPart - ImaginaryPart * otherNumber.ImaginaryPart,
                    ImaginaryPart = RealPart * otherNumber.ImaginaryPart + ImaginaryPart * otherNumber.RealPart
                };
            }

            public ComplexNumber Divide(ComplexNumber divisor)
            {

                ComplexNumber conjugate = new ComplexNumber
                {
                    RealPart = divisor.RealPart,
                    ImaginaryPart = -divisor.ImaginaryPart
                };

                ComplexNumber numerator = Multiply(conjugate);
                double denominator = divisor.RealPart * divisor.RealPart
                                   + divisor.ImaginaryPart * divisor.ImaginaryPart;

                return new ComplexNumber
                {
                    RealPart = numerator.RealPart / denominator,
                    ImaginaryPart = numerator.ImaginaryPart / denominator
                };
            }

            public double GetAbsoluteValue()
            {
                return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
            }

            public double GetAngleInRadians()
            {
                return Math.Atan(ImaginaryPart / RealPart);
            }


            public override bool Equals(object obj)
            {
                return obj is ComplexNumber number &&
             RealPart == number.RealPart &&
             ImaginaryPart == number.ImaginaryPart;
            }

            public override string ToString()
            {
                return $"({RealPart} + {ImaginaryPart}i)";
            }


        }
    }
}
