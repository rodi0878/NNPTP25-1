using System;

namespace NNPTPZ1.Mathematics
{
    public class Cplx
    {
        // --- Constants and properties ---
        public readonly static Cplx Zero = new Cplx()
        {
            Re = 0,
            Imaginari = 0
        };

        public double Re { get; set; }
        public float Imaginari { get; set; }

        // --- Methods ---
        public override bool Equals(object obj)
        {
            if (obj is Cplx other)
            {
                return other.Re == Re && other.Imaginari == Imaginari;
            }
            return base.Equals(obj);
        }

        public Cplx Multiply(Cplx b)
        {
            Cplx a = this;
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new Cplx()
            {
                Re = a.Re * b.Re - a.Imaginari * b.Imaginari,
                Imaginari = (float)(a.Re * b.Imaginari + a.Imaginari * b.Re)
            };
        }

        // fixed name: GetAbs (was GetAbS)
        public double GetAbs()
        {
            return Math.Sqrt(Re * Re + Imaginari * Imaginari);
        }

        public Cplx Add(Cplx b)
        {
            Cplx a = this;
            return new Cplx()
            {
                Re = a.Re + b.Re,
                Imaginari = a.Imaginari + b.Imaginari
            };
        }

        // returns angle in DEGREES
        public double GetAngleInDegrees()
        {
            return Math.Atan2(Imaginari, Re) * (180.0 / Math.PI);
        }

        public Cplx Subtract(Cplx b)
        {
            Cplx a = this;
            return new Cplx()
            {
                Re = a.Re - b.Re,
                Imaginari = a.Imaginari - b.Imaginari
            };
        }

        public override string ToString()
        {
            return $"({Re} + {Imaginari}i)";
        }

        internal Cplx Divide(Cplx b)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            // denominator: bRe*bRe + bIm*bIm
            var numerator = this.Multiply(new Cplx() { Re = b.Re, Imaginari = -b.Imaginari });
            var denominator = b.Re * b.Re + b.Imaginari * b.Imaginari;

            return new Cplx()
            {
                Re = numerator.Re / denominator,
                Imaginari = (float)(numerator.Imaginari / denominator)
            };
        }
    }
}
