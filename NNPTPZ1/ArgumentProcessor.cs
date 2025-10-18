using System.IO;
using System;

namespace NNPTPZ1 {
    ///<summary>
    /// Created so that arguments are related to the current instance of the program, and not static
    ///</summary>
    public class ArgumentProcessor {
	private readonly string[] args;
	private int[] intargs;
	private double[] doubleargs;
	
	public double Xmax {get; set;}
	public double Xmin {get; set;}
	public double Ymax {get; set;}
	public double Ymin {get; set;}
	public int X {get; set;}
	public int Y {get; set;}
	public string Output {get; set;}

	public ArgumentProcessor(string[] args) {
	    this.args = args;
	}
	
	public void HandleArguments() {
	    if (args[0] == "--help" || args[0] == "-h") {
		Console.WriteLine("This program is a Newton Fractal generator, written by Dr. Diviš¡ of the Pardubice university.");
		Console.WriteLine();
		Console.WriteLine("Possible command-line options are:");
		Console.WriteLine("--help or -h displays this message");
		Console.WriteLine("");
		Console.WriteLine("Example usage:");
		Console.WriteLine("NNPTPZ1.exe width, height, xmin, xmax, ymin, ymax, filename, iterations, shading speed, and Polynomial arguments");
		Console.WriteLine("NNPTPZ1.exe 800 600 -1.5 1.5 -1.0 1.0 fractals.bmp 40 1 3 0 6 1 5");
		Environment.Exit(0);
		
		return;
	    }

	    if (args.Length < 6 || args.Length > 14) {
		Console.WriteLine("Incorrent ammount of arguments supplied.");
		Console.WriteLine("Do --help or -h for more information");
		Environment.Exit(1);

		return;
	    }

	    intargs = new int[2];

	    for (int i = 0; i < intargs.Length; i++)
            {
                intargs[i] = int.Parse(args[i]);
            }

	    doubleargs = new double[4];

	    for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }
	    
	    if (args.Length >= 7)
		Output = args[6];
	    
	    X = intargs[0];
	    Y = intargs[1];
            Xmin = doubleargs[0];
            Xmax = doubleargs[1];
            Ymin = doubleargs[2];
            Ymax = doubleargs[3];   
	}
    }
}
