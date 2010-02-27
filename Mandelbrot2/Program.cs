using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace Mandelbrot2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n:::::::::::::::::::::::\nRealtime Mandelbrot by C0BRA\n:::::::::::::::::::::::\n\nControls:\n  Q     Zoom out\n  E     zoom In\n  WASD  Move\n  R     Increase Iterations\n  F     Decrease Iterations\n  C     Change color mode\n  M1    Centerise view");
            Window window = new Window();
            window.Run(40);
        }
    }
}
