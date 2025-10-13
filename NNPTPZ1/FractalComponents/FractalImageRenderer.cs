using System;
using System.Drawing;

namespace NNPTPZ1.FractalComponents
{
    public class FractalImageRenderer
    {
        private readonly FractalParameters _parameters;
        private readonly PixelResult[,] _pixels;
        private readonly Bitmap _image;
        
        private static readonly Color[] RootColors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan,
            Color.Magenta
        };

        public FractalImageRenderer(FractalParameters parameters, PixelResult[,] pixels)
        {
            _parameters = parameters;
            _pixels = pixels;
            _image = new Bitmap(_parameters.Width, _parameters.Height);
        }


        public void RenderImage()
        {
            for (int pixelX = 0; pixelX < _parameters.Width; pixelX++)
            {
                for (int pixelY = 0; pixelY < _parameters.Height; pixelY++)
                {
                    int iterationCount = _pixels[pixelX, pixelY].IterationCount;
                    int rootIndex = _pixels[pixelX, pixelY].RootIndex;
                    
                    Color pixelColor = RootColors[rootIndex % RootColors.Length];
                    pixelColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                    pixelColor = Color.FromArgb(Math.Min(Math.Max(0, pixelColor.R - (int)iterationCount * 2), 255),
                        Math.Min(Math.Max(0, pixelColor.G - (int)iterationCount * 2), 255),
                        Math.Min(Math.Max(0, pixelColor.B - (int)iterationCount * 2), 255));
                    _image.SetPixel(pixelY, pixelX, pixelColor);
                }
            }
        }

        public void SaveImage()
        {
            _image.Save(_parameters.OutputPath);
        }
    }
}