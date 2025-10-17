using System;
using System.Drawing;
using System.Windows.Forms; // for preview window
using MathCore;

namespace App
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // 1) Read config (args or defaults)
            var cfg = Config.FromArgs(args);

            // 2) Build polynomial p(z) = z^3 + 1
            var poly = Polynomial.BuildPolynomialZ3Plus1();

            // 3) Render and show (NO saving to file)
            using var bmp = NewtonFractal.Render(cfg, poly);
            NewtonFractal.Show(bmp, cfg);
        }
        
    }
}