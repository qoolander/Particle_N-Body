using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTkTest
{
    class Particle
    {
        GameWindow game;

        Vector2 position;
        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }
        Vector2 velocity;
        public float VelocityX { get { return velocity.X; } set { velocity.X = value; } }
        public float VelocityY { get { return velocity.Y; } set { velocity.Y = value; } }
        float Drag;

        public float Mass { get; set; }

        public Particle(GameWindow pGame)
        {
            game = pGame;
            X = game.Height/2;
            Y = game.Width/2;
            Mass = 10;
            Drag = 0.01f;
        }

        public Particle(float x, float y, float mass, float drag, GameWindow pGame)
        {
            game = pGame;
            X = x;
            Y = y;
            Mass = mass;
            Drag = drag;
        }

        void DrawParticle(Vector2 p, float radius)
        {
            Vector2[] points = new Vector2[45];

            for (int i = 0; i < 360; i+=8)
            {
                float degInRad = i * Globals.DEG2RAD;
                points[i/8] = new Vector2((float)((Math.Cos(degInRad) * radius) + p.X), (float)((Math.Sin(degInRad) * radius) + p.Y)).toScreenCoords(new Vector2(game.Width, game.Height));
            }
            GL.Begin(PrimitiveType.Triangles);

            for (int i = 0; i < points.Length; i++)
            {
                GL.Color3(Color.FromArgb(255, 0, 0, 50).Blend(Color.FromArgb(255, 150, 150, 150)));
                GL.Vertex2(position.toScreenCoords(new Vector2(game.Width, game.Height)));

                GL.Color3(Color.FromArgb(255, 15, 15, 60));
                GL.Vertex2(points[i]);
                if (i != points.Length-1)
                {
                    GL.Vertex2(points[i + 1]);
                }
                else
                {
                    GL.Vertex2(points[0]);
                }
            }

            GL.End();
        }

        public void Update()
        {
            DrawParticle(position, Mass);
            calculateVelocity();
        }

        void calculateVelocity()
        {
            X += VelocityX;
            Y += VelocityY;
            VelocityX = VelocityX>0 ? VelocityX-Drag : VelocityX<0 ? VelocityX + Drag : VelocityX;
            VelocityY = VelocityY > 0 ? VelocityY - Drag : VelocityY < 0 ? VelocityY + Drag : VelocityY;
        }
    }
}

//new Vector2((float)((Math.Cos(degInRad) * radius) + position.X), (float)((Math.Sin(degInRad) * radius) + position.Y)).toScreenCoords(new Vector2(game.Width, game.Height))