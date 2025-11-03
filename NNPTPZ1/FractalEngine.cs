using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal static class FractalEngine
    {
        public static Bitmap Generate(FractalConfig config, Polynomial polynomial, Polynomial derivative)
        {
            var bmp = new Bitmap(config.Width, config.Height);
            var roots = new List<Complex>();

            for (int i = 0; i < config.Width; i++)
            {
                for (int j = 0; j < config.Height; j++)
                {
                    var ox = FractalOperations.PixelToWorld(i, j, config.XMin, config.YMin, config.XStep, config.YStep, config.Epsilon);
                    int it = FractalOperations.NewtonIterate(ref ox, polynomial, derivative, config.MaxIterations, config.ConvergenceThreshold);

                    int rootId = FractalOperations.FindRootIndex(roots, ox, config.RootTolerance);
                    var baseColor = config.Colors[rootId % config.Colors.Length];
                    var shaded = FractalOperations.ShadeByIterations(baseColor, it, config.ShadingSpeed);

                    bmp.SetPixel(i, j, shaded);
                }
            }

            return bmp;
        }
    }
}
