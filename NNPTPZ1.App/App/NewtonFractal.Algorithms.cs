using System;
using System.Collections.Generic;
using System.Drawing;
using MathCore;

namespace App;

internal static partial class NewtonFractal
{
    private static readonly Color[] Palette =
        { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Magenta, Color.Gold, Color.Cyan };

    /// <summary>Small result holder for beginners (no tuples).</summary>
    internal class RootResult
    {
        public ComplexNumber Z;
        public int Iterations;
        public RootResult(ComplexNumber z, int iterations) { Z = z; Iterations = iterations; }
    }

    /// <summary>
    /// Newton's method from z0, up to MaxIterations, stop when step is tiny.
    /// </summary>
    private static RootResult FindRoot(ComplexNumber z, Polynomial p, Polynomial d, Config cfg)
    {
        for (int it = 0; it < cfg.MaxIterations; it++)
        {
            var f  = p.Evaluate(z);
            var df = d.Evaluate(z);

            // If derivative is almost zero, nudge z a bit to avoid division by zero.
            if (df.MagnitudeSquared < 1e-20)
            {
                z = new ComplexNumber(z.Real + 1e-6, z.Imag + 1e-6);
                continue;
            }

            // Newton update: z = z - f(z)/f'(z)
            var step = f / df;
            z -= step;

            // Converged if step is tiny
            if (step.MagnitudeSquared < cfg.Tolerance * cfg.Tolerance)
                return new RootResult(z, it);
        }

        return new RootResult(z, cfg.MaxIterations);
    }

    /// <summary>
    /// Return index of existing root close to z; add new if none is close.
    /// </summary>
    private static int GetOrAddRootId(ComplexNumber z, List<ComplexNumber> roots)
    {
        for (int i = 0; i < roots.Count; i++)
            if ((z - roots[i]).MagnitudeSquared < 1e-4) return i;

        roots.Add(z);
        return roots.Count - 1;
    }

    /// <summary>
    /// Pick base color by root and darken by iteration count.
    /// </summary>
    private static Color Colorize(int rootId, int iterations)
    {
        var baseColor = Palette[rootId % Palette.Length];
        int dark = Math.Min(iterations * 3, 200);

        int r = Math.Clamp(baseColor.R - dark, 0, 255);
        int g = Math.Clamp(baseColor.G - dark, 0, 255);
        int b = Math.Clamp(baseColor.B - dark, 0, 255);

        return Color.FromArgb(r, g, b);
    }
}