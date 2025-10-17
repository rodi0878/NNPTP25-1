using System.Drawing;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// References used for refactor:
    ///
    /// </summary>
    class Program
    {
	///<summary>
	/// This method suffered from the Long Method issue, and needed to be split up into its implicit parts.
	///</summary>
        static void Main(string[] args)
        {
	    var argumentProcessor = new ArgumentProcessor(args);
	    var fractalRenderer = new FractalRenderer(argumentProcessor);
	    var bMPExporter = new BMPExporter(argumentProcessor);
	    
	    argumentProcessor.HandleArguments();
	    bMPExporter.ExportBitmap(fractalRenderer.PopulateBitmap());
        }
    }
}
