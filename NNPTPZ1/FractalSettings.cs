using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    /// <summary>
    /// Represents the settings for rendering the Newton fractal, 
    /// including image dimensions, coordinate ranges, and output path.
    /// </summary>
    public class FractalSettings
    {
        /// <summary>
        /// Width of the generated fractal image in pixels.
        /// Default is 800.
        /// </summary>
        public int Width { get; set; } = 800;
        /// <summary>
        /// Height of the generated fractal image in pixels.
        /// Default is 800.
        /// </summary>
        public int Height { get; set; } = 800;
        /// <summary>
        /// Minimum X-coordinate of the fractal plane.
        /// Default is -2.
        /// </summary>
        public double XMin { get; set; } = -2;
        /// <summary>
        /// Maximum X-coordinate of the fractal plane.
        /// Default is 2.
        /// </summary>
        public double XMax { get; set; } = 2;
        /// <summary>
        /// Minimum Y-coordinate of the fractal plane.
        /// Default is -2.
        /// </summary>
        public double YMin { get; set; } = -2;
        /// <summary>
        /// Maximum Y-coordinate of the fractal plane.
        /// Default is 2.
        /// </summary>
        public double YMax { get; set; } = 2;
        /// <summary>
        /// Path where the generated fractal image will be saved.
        /// Default is "../../../out.png".
        /// </summary>
        public string OutputPath { get; set; } = "../../../out.png";
    }
}
