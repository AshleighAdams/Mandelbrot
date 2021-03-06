﻿using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Mandelbrot2
{

    public class Window : GameWindow
    {
        private double xoffset = -512/32;
        private double yoffset = 0;
        private double scale = 4;
        private int max_iterations = 100;
        private bool changeit = true;
        private int colormode = 0;
        private int maxcolormode=1;

        public Window()
            : base(512, 512, GraphicsMode.Default, "Mandelbrot")
        {
            this.WindowBorder = WindowBorder.Fixed;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            // Clear current viewport
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Pixles array
            byte[] pixels = new byte[512*512*3];

            // Set a variables outside the loop to save time
            int iterations = 0;

            int r = 0;
            int g = 0;
            int b = 0;

            double xx = 0;
            double yy = 0;

            double k = 0;
            double j = 0;

            for(int y = 0; y < 512; y++)
            {
                for (int x = 0; x < 512; x++)
                {
                    iterations = 0;
                    
                    xx = (x + (this.xoffset/this.scale) - (512 / 2)) * (this.scale / 512);
                    yy = (y + (this.yoffset/this.scale) - (512 / 2)) * (this.scale / 512);

                    j = 0;
                    k = 0;

                    while (j * j + k * k <= (2 * 2) & iterations < this.max_iterations)
                    {
                        double oj = j;
                        j = j * j - k * k + xx;
                        k = 2 * oj * k + yy; // Using the new j causes you to get strechy lines!
                        iterations++;
                    }

                    if (iterations == this.max_iterations)
                    {
                        if (this.colormode == 1)
                        {
                            r = (byte)(Math.Sqrt(j) * 255.0);
                            g = (byte)(Math.Sqrt(k) * 255.0);
                            b = (byte)(j * k);
                        }
                        else if (this.colormode == 0)
                        {
                            r = 255;
                            g = 240;
                            b = 0;
                        }
                    }
                    else
                    {
                        if (this.colormode == 1)
                        {
                            r = (byte)(Math.Sqrt(j)*255.0);
                            g = (byte)(Math.Sqrt(k)*255.0);
                            b = (byte)(j*k);
                        }
                        else if(this.colormode == 0)
                        {
                            r = iterations * 4;
                            g = iterations * 2;
                            b = iterations * 0;
                        }
                    }


                    pixels[(x + y * 512) * 3 + 0] = (byte) Math.Min(255,r);
                    pixels[(x + y * 512) * 3 + 1] = (byte) Math.Min(255,g);
                    pixels[(x + y * 512) * 3 + 2] = (byte) Math.Min(255,b);
                }
            }


            GL.DrawPixels<byte>(512, 512, PixelFormat.Rgb, PixelType.UnsignedByte, pixels);

            // Push graphics to screen
            this.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Our keybaord control stuff
            if (this.Keyboard[Key.Q])
                this.scale = this.scale + (0.1 * this.scale);

            if (this.Keyboard[Key.E])
                this.scale = this.scale - (0.1 * this.scale);

            if (this.Keyboard[Key.W])
                this.yoffset = this.yoffset + (1*this.scale);

            if (this.Keyboard[Key.A])
                this.xoffset = this.xoffset - (1 * this.scale);

            if (this.Keyboard[Key.S])
                this.yoffset = this.yoffset - (1 * this.scale);

            if (this.Keyboard[Key.D])
                this.xoffset = this.xoffset + (1 * this.scale);

            if (this.Keyboard[Key.R])
            {
                if (this.changeit)
                    this.max_iterations = this.max_iterations + 50;
                this.changeit = false;
            }

            if (this.Keyboard[Key.F])
            {
                if (this.changeit)
                    this.max_iterations = this.max_iterations - 50;
                this.changeit = false;
            }
            if (this.Keyboard[Key.C])
            {
                if (this.changeit)
                    this.colormode = this.colormode + 1;
                if (this.colormode > this.maxcolormode)
                    this.colormode = 0;
                this.changeit = false;
            }

            // Stop is form constantly zooming
            if( !this.Keyboard[Key.F] && !this.Keyboard[Key.R] && !this.Keyboard[Key.C] && !this.Mouse[MouseButton.Left] ) { this.changeit = true; }

            // Our mouse controls
            if (this.Mouse[MouseButton.Left])
            {
                if (this.changeit)
                {
                    int x = Mouse.X;
                    int center_x = 512 / 2;
                    int y = Mouse.Y;
                    int center_y = 512 / 2;

                    double xoff = (center_x - x)*this.scale;
                    double yoff = (center_y - y)*this.scale;

                    this.xoffset -= xoff;
                    this.yoffset += yoff;
                    
                }
                this.changeit = false;
            }
        }
    }
}
