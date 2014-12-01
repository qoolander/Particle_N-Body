using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTkTest;
using OpenTkTest.Extensions;


class Program
{
    static Particle farticle;

    [STAThread]
    public static void Main()
    {
        

        using (var game = new GraphicsWindow())
        {
            game.Load += (sender, e) =>
            {
                // setup settings, load textures, sounds
                game.VSync = VSyncMode.On;

                game.WindowBorder = WindowBorder.Hidden;

                game.WindowState = WindowState.Fullscreen;

                farticle = new Particle(game);

                Console.WriteLine(game.Width);

                farticle.VelocityX = 1;
            };

            game.Resize += (sender, e) =>
            {
                GL.Viewport(0, 0, game.Width, game.Height);
            };

            game.UpdateFrame += (sender, e) =>
            {
                // add game logic, input handling
                if (game.Keyboard[Key.Escape])
                {
                    game.Exit();
                }
            };

            game.RenderFrame += (sender, e) =>
            {
                // render graphics
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

                farticle.Update();

                game.SwapBuffers();
            };

            // Run the game at 60 updates per second
            game.Run(60.0);
        }
    }
}