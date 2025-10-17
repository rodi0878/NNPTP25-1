using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Threading;

namespace Mathematics
{
    public class Poly
    {
	/// <summary>
	/// Coe
	/// </summary>
	public List<Cplx> Coe { get; set; }

	/// <summary>
	/// Constructor
	/// </summary>
	public Poly() => Coe = new List<Cplx>();

	///Overloaded constructor for parametrization
	public Poly(Cplx arg1, Cplx arg2, Cplx arg3, Cplx arg4) {
	    Coe = new List<Cplx>();
	    this.Coe.Add(arg1);
	    this.Coe.Add(arg2);
	    this.Coe.Add(arg3);
	    this.Coe.Add(arg4);
	}
	
	public void Add(Cplx coe) =>
	    Coe.Add(coe);

	/// <summary>
	/// Derives this polynomial and creates new one
	/// </summary>
	/// <returns>Derivated polynomial</returns>
	public Poly Derive()
	{
	    Poly p = new Poly();

	    for (int q = 1; q < Coe.Count; ++q)
	    {
		p.Coe.Add(Coe[q].Multiply(new Cplx() { Re = q }));
	    }

	    return p;
	}

	/// <summary>
	/// Evaluates polynomial at given point
	/// </summary>
	/// <param name="x">point of evaluation</param>
	/// <returns>y</returns>
	public Cplx Eval(double x)
	{
	    var y = Eval(new Cplx() { Re = x, Imaginari = 0 });

	    return y;
	}

	/// <summary>
	/// Evaluates polynomial at given point
	/// </summary>
	/// <param name="x">point of evaluation</param>
	/// <returns>y</returns>
	public Cplx Eval(Cplx x)
	{
	    Cplx s = Cplx.Zero;

	    for (int i = 0; i < Coe.Count; ++i)
	    {
		Cplx coef = Coe[i];
		Cplx bx = x;
		int power = i;

		if (i > 0)
		{
		    for (int j = 0; j < power - 1; ++j)
			bx = bx.Multiply(x);

		    coef = coef.Multiply(bx);
		}

		s = s.Add(coef);
	    }

	    return s;
	}

	/// <summary>
	/// ToString
	/// </summary>
	/// <returns>String repr of polynomial</returns>
	public override string ToString()
	{
	    string s = "";
	    
	    for (var i = 0; i < Coe.Count; ++i)
	    {
		s += Coe[i];

		if (i > 0)
		{
		    for (var j = 0; j < i; j++)
		    {
			s += "x";
		    }
		}
		if (i+1 < Coe.Count)
                    s += " + ";
	    }
	    
	    return s;
	}
    }
}
