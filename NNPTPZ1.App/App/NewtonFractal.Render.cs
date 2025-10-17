using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MathCore;

namespace App;

internal static partial class NewtonFractal
{
    /// <summary>
    /// Render the fractal into a new bitmap and return it.
    /// </summary>
    public static Bitmap Render(Config cfg, Polynomial p)
    {
        var d = p.Derivative();
        var bmp = new Bitmap(cfg.Width, cfg.Height);
        var roots = new List<ComplexNumber>();

        double stepX = (cfg.XMax - cfg.XMin) / (cfg.Width  - 1.0);
        double stepY = (cfg.YMax - cfg.YMin) / (cfg.Height - 1.0);

        for (int py = 0; py < cfg.Height; py++)
        for (int px = 0; px < cfg.Width;  px++)
        {
            var z0 = new ComplexNumber(cfg.XMin + px * stepX, cfg.YMin + py * stepY);
            var res = FindRoot(z0, p, d, cfg);
            int id = GetOrAddRootId(res.Z, roots);
            bmp.SetPixel(px, py, Colorize(id, res.Iterations));
        }

        return bmp;
    }

    /// <summary>Show a very simple preview window.</summary>
    public static void Show(Bitmap bmp, Config cfg)
    {
        Application.EnableVisualStyles();
        Application.SetHighDpiMode(HighDpiMode.SystemAware);

        using var pic = new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = new Bitmap(bmp) // clone so disposing works safely
        };

        using var form = new Form
        {
            Text = "Newton Fractal",
            StartPosition = FormStartPosition.CenterScreen,
            ClientSize = new Size(Math.Min(cfg.Width, 1000), Math.Min(cfg.Height, 800)),
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = true,
            MinimizeBox = true
        };

        form.Controls.Add(pic);
        Application.Run(form);
    }
}