namespace App;

/// <summary>
/// All settings used to render the fractal.
/// </summary>
internal class Config
{
    public int Width;
    public int Height;
    public double XMin, XMax, YMin, YMax;
    public string OutputFile = "fractal.png";
    public int MaxIterations;
    public double Tolerance;

    /// <summary>
    /// Create config from command-line args or return defaults.
    /// Expected args: width height xmin xmax ymin ymax outputFile
    /// </summary>
    public static Config FromArgs(string[] a)
    {
        var c = new Config
        {
            Width = 800,
            Height = 800,
            XMin = -2, XMax = 2,
            YMin = -2, YMax = 2,
            OutputFile = "fractal.png",
            MaxIterations = 40,
            Tolerance = 1e-8
        };

        if (a.Length >= 7)
        {
            c.Width  = int.Parse(a[0]);
            c.Height = int.Parse(a[1]);
            c.XMin   = double.Parse(a[2]);
            c.XMax   = double.Parse(a[3]);
            c.YMin   = double.Parse(a[4]);
            c.YMax   = double.Parse(a[5]);
            c.OutputFile = a[6];
        }
        return c;
    }
}
