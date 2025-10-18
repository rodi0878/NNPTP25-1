using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;
using System.Threading;

namespace Mathematics
{
    public class ComplexNumber
    {
	public double Re { get; set; }
	public float Imaginari { get; set; }

	public override bool Equals(object obj)
	{
	    if (obj is ComplexNumber)
	    {
		ComplexNumber x = obj as ComplexNumber;

		return x.Re == Re && x.Imaginari == Imaginari;
	    }
	    
	    return base.Equals(obj);
	}

	public readonly static ComplexNumber Zero = new ComplexNumber()
	{
	    Re = 0,
	    Imaginari = 0
	};

	public ComplexNumber Multiply(ComplexNumber b)
	{
	    ComplexNumber a = this;

	    return new ComplexNumber()
	    {
		Re = a.Re * b.Re - a.Imaginari * b.Imaginari,
		Imaginari = (float)(a.Re * b.Imaginari + a.Imaginari * b.Re)
	    };
	}
	
	public double GetAbs()
	{
	    return Math.Sqrt( Re * Re + Imaginari * Imaginari);
	}

	public ComplexNumber Add(ComplexNumber b)
	{
	    ComplexNumber a = this;

	    return new ComplexNumber()
	    {
		Re = a.Re + b.Re,
		Imaginari = a.Imaginari + b.Imaginari
	    };
	}
	
	public double GetAngleInDegrees()
	{
	    return Math.Atan(Imaginari / Re);
	}
	
	public ComplexNumber Subtract(ComplexNumber b)
	{
	    ComplexNumber a = this;
	    return new ComplexNumber()
	    {
		Re = a.Re - b.Re,
		Imaginari = a.Imaginari - b.Imaginari
	    };
	}

	public override string ToString()
	{
	    return $"({Re} + {Imaginari}i)";
	}

	internal ComplexNumber Divide(ComplexNumber b)
	{
	    var tmp = this.Multiply(new ComplexNumber() { Re = b.Re, Imaginari = -b.Imaginari });
	    var tmp2 = b.Re * b.Re + b.Imaginari * b.Imaginari;

	    return new ComplexNumber()
	    {
		Re = tmp.Re / tmp2,
		Imaginari = (float)(tmp.Imaginari / tmp2)
	    };
	}
    }
}
