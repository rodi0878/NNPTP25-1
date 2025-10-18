using System.Drawing;
using System.Collections.Generic;
using Mathematics;
using System;

namespace NNPTPZ1 {
    public class FractalRenderer {
	private readonly ArgumentProcessor argumentProcessor;
	private static readonly Color[] clrs = new Color[]
	{
	    Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
	};

	public FractalRenderer(ArgumentProcessor argumentProcessor) {
	    this.argumentProcessor = argumentProcessor;
	}
	
	public Bitmap PopulateBitmap() {
	    Bitmap bmp = new Bitmap(argumentProcessor.X, argumentProcessor.Y);
	    double xstep = (argumentProcessor.Xmax - argumentProcessor.Xmin) / argumentProcessor.X;
	    double ystep = (argumentProcessor.Ymax - argumentProcessor.Ymin) / argumentProcessor.Y;

	    List<ComplexNumber> koreny = new List<ComplexNumber>();
	    Polynomial p = new Polynomial(new ComplexNumber() { Re = 1 }, ComplexNumber.Zero, ComplexNumber.Zero, new ComplexNumber() { Re = 1 });
	    Polynomial ptmp = p;
	    Polynomial pd = p.Derive();

	    //Logging should be used instead
	    //Console.WriteLine(p);
	    //Console.WriteLine(pd);

	    var maxid = 0;

	    // for every pixel in image...
	    for (int i = 0; i < argumentProcessor.X; ++i)
	    {
		for (int j = 0; j < argumentProcessor.Y; ++j)
		{
		    // find "world" coordinates of pixel
		    double y = argumentProcessor.Ymin + i * ystep;
		    double x = argumentProcessor.Xmin + j * xstep;

		    ComplexNumber ox = new ComplexNumber()
		    {
			Re = x,
			Imaginari = (float)(y)
		    };

		    if (ox.Re == 0)
			ox.Re = 0.0001;
		    if (ox.Imaginari == 0)
			ox.Imaginari = 0.0001f;

		    // find solution of equation using newton's iteration
		    float it = 0;
		    for (int q = 0; q< 30; ++q)
		    {
			var diff = p.Eval(ox).Divide(pd.Eval(ox));
			ox = ox.Subtract(diff);
			
			if (Math.Pow(diff.Re, 2) + Math.Pow(diff.Imaginari, 2) >= 0.5)
			{
			    --q;
			}
			++it;
		    }

		    // find solution root number
		    var known = false;
		    var id = 0;
		    for (int w = 0; w < koreny.Count; ++w)
		    {
			if (Math.Pow(ox.Re- koreny[w].Re, 2) + Math.Pow(ox.Imaginari - koreny[w].Imaginari, 2) <= 0.01)
			{
			    known = true;
			    id = w;
			}
		    }
		    if (!known)
		    {
			koreny.Add(ox);
			id = koreny.Count;
			maxid = ++id;
		    }

		    // colorize pixel according to root number
		    var vv = clrs[id % clrs.Length];
		    vv = Color.FromArgb(vv.R, vv.G, vv.B);
		    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R-(int)it*2), 255), Math.Min(Math.Max(0, vv.G - (int)it*2), 255), Math.Min(Math.Max(0, vv.B - (int)it*2), 255));

		    bmp.SetPixel(i, j, vv);
		}
	    }

	    return bmp;
	}
    }
}
