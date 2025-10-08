using System;

namespace NNPTPZ1
{
    public class ComplexNumber
    {
        private double eta = 0.0000001;

        public double RealPart { get; set; }

        public double ImaginariPart { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginariPart = 0
        };

        public override string ToString()
        {
            return $"({RealPart} + {ImaginariPart}i)";
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber difference = this.Subtract(obj as ComplexNumber);
                return difference.GetAbS() < this.eta;
            }
            return base.Equals(obj);
        }

        public double GetAbS()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginariPart * ImaginariPart);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginariPart / RealPart);
        }

        public ComplexNumber Add(ComplexNumber addend)
        {
            return new ComplexNumber()
            {
                RealPart = this.RealPart + addend.RealPart,
                ImaginariPart = this.ImaginariPart + addend.ImaginariPart
            };
        }

        public ComplexNumber Subtract(ComplexNumber substrahend)
        {
            return new ComplexNumber()
            {
                RealPart = this.RealPart - substrahend.RealPart,
                ImaginariPart = this.ImaginariPart - substrahend.ImaginariPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber multiplicand)
        {
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                RealPart = this.RealPart * multiplicand.RealPart - this.ImaginariPart * multiplicand.ImaginariPart,
                ImaginariPart = this.RealPart * multiplicand.ImaginariPart + this.ImaginariPart * multiplicand.RealPart
            };
        }

        internal ComplexNumber Divide(ComplexNumber divisor)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            var dividedTimesConjugateOfDivisor = this.Multiply(new ComplexNumber() { RealPart = divisor.RealPart, ImaginariPart = -divisor.ImaginariPart });
            var divisorTimesConjugateOfDivisor = divisor.RealPart * divisor.RealPart + divisor.ImaginariPart * divisor.ImaginariPart;
            
            return new ComplexNumber()
            {
                RealPart = dividedTimesConjugateOfDivisor.RealPart / divisorTimesConjugateOfDivisor,
                ImaginariPart = dividedTimesConjugateOfDivisor.ImaginariPart / divisorTimesConjugateOfDivisor
            };
        }
    }
}
