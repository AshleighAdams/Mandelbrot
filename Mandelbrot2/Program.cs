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
            Window window = new Window();
            window.Run(40);
        }
    }
}
