using System;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Complex
        {
            public double RealPart { get; set; }
            public double ImaginaryPart { get; set; }

            public static readonly Complex Zero = new Complex()
            {
                RealPart = 0,
                ImaginaryPart = 0
            };

            public override bool Equals(object obj)
            {
                if (obj is Complex)
                {
                    Complex x = obj as Complex;
                    return x.RealPart == RealPart && x.ImaginaryPart == ImaginaryPart;
                }
                return base.Equals(obj);
            }

            public Complex Multiply(Complex operand) => new Complex()
            {
                RealPart = RealPart * operand.RealPart - ImaginaryPart * operand.ImaginaryPart,
                ImaginaryPart = (float)(RealPart * operand.ImaginaryPart + ImaginaryPart * operand.RealPart)
            };

            public Complex Add(Complex operand) => new Complex()
            {
                RealPart = RealPart + operand.RealPart,
                ImaginaryPart = ImaginaryPart + operand.ImaginaryPart
            };

            public Complex Subtract(Complex operand) => new Complex()
            {
                RealPart = RealPart - operand.RealPart,
                ImaginaryPart = ImaginaryPart - operand.ImaginaryPart
            };

            internal Complex Divide(Complex operand)
            {
                var dividend = Multiply(new Complex() { RealPart = operand.RealPart, ImaginaryPart = -operand.ImaginaryPart });
                var divisor = operand.RealPart * operand.RealPart + operand.ImaginaryPart * operand.ImaginaryPart;

                return new Complex()
                {
                    RealPart = dividend.RealPart / divisor,
                    ImaginaryPart = (float)(dividend.ImaginaryPart / divisor)
                };
            }

            public override string ToString() => $"({RealPart} + {ImaginaryPart}i)";
        }
    }
}
