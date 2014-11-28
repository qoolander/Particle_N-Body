using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTkTest.Level
{
    enum borders
    {
        left, right, top, bottem
    }

    class Level
    {
        Level(GameWindow pGame){
            game = pGame;
        }

        GameWindow game;

        public List<CollisionShape> collisionGeomitry;

        /*
         * return null if colliding with nothing, else returns a list of things that the object is colliding with
         */
        List<Object> CollidingWith (CollisionShape ShapeToTest)
        {
            List<Object> result = new List<Object>();
            if (ShapeToTest.Position.X > game.Width || ShapeToTest.Position.X < 0 || ShapeToTest.Position.Y > game.Height || ShapeToTest.Position.Y < 0)
            {
                result.Add(ShapeToTest.Position.X > game.Width ? borders.right : ShapeToTest.Position.X < 0 ? borders.left : ShapeToTest.Position.Y < 0 ? borders.bottem : ShapeToTest.Position.Y > game.Height ? borders.top);
                return result;
            }
            return null;
        }
    }

    class CollisionShape
    {
        internal Vector2 Position;

        internal Vector2[] points;

        internal Object owner;

        internal virtual bool isPointOverlapping(Vector2 p)
        {            
            return false;
        }
    }

    class CollisionSphere : CollisionShape
    {
        CollisionSphere(int numberOfVerts){
            points = new points[numberOfVerts]();
        }

        public float radius;

        internal override bool isPointOverlapping(Vector2 p)
        {          
                float xd = position.X - p.X;
                float yd = position.Y - p.Y;

                float distance = (float)(Math.Sqrt(xd * xd + yd * yd));

                return distance <= radius;
        }
    }

    class CollisionBounds : CollisionShape
    {
        CollisionBounds(){
            points = new Vector2[2]();
        }

    }
}
