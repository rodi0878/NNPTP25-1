using System;

namespace NNPTPZ1.Mathematics
{
    /// <summary>Simple complex number (API & behavior kept intact).</summary>
    public sealed class Cplx
    {
        public double Re { get; set; }
        public float Imaginari { get; set; }

        public static readonly Cplx Zero = new Cplx { Re = 0, Imaginari = 0 };

        public override bool Equals(object obj)
        {
            if (obj is Cplx x)
                return x.Re == Re && x.Imaginari == Imaginari;
            return base.Equals(obj);
        }

        public Cplx Multiply(Cplx b)
        {
            Cplx a = this;
            return new Cplx
            {
                Re = a.Re * b.Re - a.Imaginari * b.Imaginari,
                Imaginari = (float)(a.Re * b.Imaginari + a.Imaginari * b.Re)
            };
        }

        public double GetAbS() => Math.Sqrt(Re * Re + Imaginari * Imaginari);

        public Cplx Add(Cplx b)
        {
            Cplx a = this;
            return new Cplx
            {
                Re = a.Re + b.Re,
                Imaginari = a.Imaginari + b.Imaginari
            };
        }

        public double GetAngleInDegrees() => Math.Atan(Imaginari / Re);

        public Cplx Subtract(Cplx b)
        {
            Cplx a = this;
            return new Cplx
            {
                Re = a.Re - b.Re,
                Imaginari = a.Imaginari - b.Imaginari
            };
        }

        public Cplx Divide(Cplx b)
        {
            var tmp = this.Multiply(new Cplx { Re = b.Re, Imaginari = -b.Imaginari });
            var tmp2 = b.Re * b.Re + b.Imaginari * b.Imaginari;

            return new Cplx
            {
                Re = tmp.Re / tmp2,
                Imaginari = (float)(tmp.Imaginari / tmp2)
            };
        }

        public override string ToString() => $"({Re} + {Imaginari}i)";
    }
}
