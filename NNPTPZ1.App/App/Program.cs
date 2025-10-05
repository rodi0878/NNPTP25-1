using System;
using System.Drawing;
using System.Collections.Generic;
using MathCore;


namespace App
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Default settings used when no command-line arguments are provided
            // (image size, coordinate range, output file, iteration limit, tolerance).
            int imageWidth = 800, imageHeight = 800;
            double xmin = -2, xmax = 2, ymin = -2, ymax = 2;
            string outputFile = "fractal.png";
            int maxIterations = 40;
            double tolerance = 1e-8;

            // If 7 command-line arguments are provided, use them instead of defaults:
            // width, height, xmin, xmax, ymin, ymax, outputFile
            if (args.Length >= 7)
            {
                imageWidth = int.Parse(args[0]);
                imageHeight = int.Parse(args[1]);
                xmin = double.Parse(args[2]);
                xmax = double.Parse(args[3]);
                ymin = double.Parse(args[4]);
                ymax = double.Parse(args[5]);
                outputFile = args[6];
            }

            // Define polynomial p(x) = x^3 + 1 (coefficients in ascending order)
            // and compute its derivative p'(x)
            var polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber(1, 0)); // constant term
            polynomial.Coefficients.Add(new ComplexNumber(0, 0)); // x term (0*x)
            polynomial.Coefficients.Add(new ComplexNumber(0, 0)); // x^2 term (0*x^2)
            polynomial.Coefficients.Add(new ComplexNumber(1, 0)); // x^3 term (1*x^3)
            var derivative = polynomial.Derivative();

            // Create the output bitmap (image buffer)
            using var bmp = new Bitmap(imageWidth, imageHeight);

            // Predefine colors for different roots (cycled through if needed)
            var colors = new[] { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Magenta, Color.Gold, Color.Cyan };
            // List to keep track of discovered roots (approximate solutions)
            var knownRoots = new List<ComplexNumber>();

            // Pixel step sizes (map image pixels to complex plane coordinates)
            double stepX = (xmax - xmin) / (imageWidth - 1);
            double stepY = (ymax - ymin) / (imageHeight - 1);

            // Render Newton fractal (iterate over all pixels) 
            for (int py = 0; py < imageHeight; py++)
            {
                // Map row index -> imaginary axis value
                double y = ymin + py * stepY;

                for (int px = 0; px < imageWidth; px++)
                {
                    // Map column index -> real axis value
                    double x = xmin + px * stepX;

                    // Current complex point z0 for Newton's method
                    var z = new ComplexNumber(x, y);

                    int it = 0;
                    for (; it < maxIterations; it++)
                    {
                        var fz = polynomial.Evaluate(z);
                        var dfz = derivative.Evaluate(z);

                        // If derivative is (almost) zero, nudge z a tiny bit to avoid division by 0
                        if (dfz.MagnitudeSquared < 1e-20)
                        {
                            z = new ComplexNumber(z.Real + 1e-6, z.Imag + 1e-6);
                            continue;
                        }


                        // Newton step and update
                        var step = fz / dfz;
                        z -= step;

                        // Converged if the update is very small
                        if (step.MagnitudeSquared < tolerance * tolerance)
                            break;
                    }

                    // Find which root this pixel belongs to (by proximity),
                    // or register a new root if we haven't seen it yet.
                    int rootId = -1;
                    for (int r = 0; r < knownRoots.Count; r++)
                    {
                        if ((z - knownRoots[r]).MagnitudeSquared < 1e-4)
                        {
                            rootId = r;
                            break;
                        }
                    }
                    if (rootId == -1) { knownRoots.Add(z); rootId = knownRoots.Count - 1; }

                    // Color rule:
                    // - base color depends on the root
                    // - darker color means more iterations to converge
                    var baseColor = colors[rootId % colors.Length];
                    int dark = Math.Min(it * 3, 200);
                    var color = Color.FromArgb(
                        Math.Clamp(baseColor.R - dark, 0, 255),
                        Math.Clamp(baseColor.G - dark, 0, 255),
                        Math.Clamp(baseColor.B - dark, 0, 255));

                    // Paint the pixel
                    bmp.SetPixel(px, py, color);
                }
            }

            // show popup window 
            Console.WriteLine($"Fractal saved as: {outputFile}");

            // Show the image in a simple popup window
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            using var picture = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = new Bitmap(bmp) // clone to avoid disposal issues
            };

            using var form = new Form
            {
                Text = "Newton Fractal",
                StartPosition = FormStartPosition.CenterScreen,
                ClientSize = new Size(Math.Min(imageWidth, 1000), Math.Min(imageHeight, 800)),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = true,
                MinimizeBox = true
            };
            form.Controls.Add(picture);

            // Display the window (blocks until closed)
            Application.Run(form);
        }
    }
}