using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTkTest.Level
{
    public enum borders
    {
        left, right, top, bottem
    }

    internal class Level
    {
        internal Level(Extensions.GraphicsWindow pGame){
            collisionGeomitry = new List<CollisionShape>();

            game = pGame;

            collisionGeomitry.Add(new CollisionEdge(borders.bottem, new Vector2(game.Width, game.Height)));
            collisionGeomitry.Add(new CollisionEdge(borders.left, new Vector2(game.Width, game.Height)));
            collisionGeomitry.Add(new CollisionEdge(borders.right, new Vector2(game.Width, game.Height)));
            collisionGeomitry.Add(new CollisionEdge(borders.top, new Vector2(game.Width, game.Height)));
        }

        Extensions.GraphicsWindow game;

        public List<CollisionShape> collisionGeomitry;

        /*
         * return null if colliding with nothing, else returns a list of things that the object is colliding with
         */
        public List<CollisionShape> CollidingWith (CollisionShape ShapeToTest)
        {
            List<CollisionShape> result = new List<CollisionShape>();
            foreach (var item in collisionGeomitry)
            {
                if (item is CollisionEdge)
                {
                    if (((CollisionEdge)item).isPointOverlapping(ShapeToTest.Position))
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }
    }

    public class CollisionShape
    {
        internal Vector2 Position;

        internal Vector2[] points;

        internal Object owner;

        internal virtual bool isPointOverlapping(Vector2 p)
        {            
            return false;
        }
    }

    public class CollisionSphere : CollisionShape
    {
        public CollisionSphere(int numberOfVerts, float pRadius){
            points = new Vector2[numberOfVerts];
            radius = pRadius;
        }

        public float radius;

        internal override bool isPointOverlapping(Vector2 p)
        {          
                float xd = Position.X - p.X;
                float yd = Position.Y - p.Y;

                float distance = (float)(Math.Sqrt(xd * xd + yd * yd));

                return distance <= radius;
        }
    }

    //Isn't implemented yet
    class CollisionBounds : CollisionShape
    {
        CollisionBounds(){
            points = new Vector2[2];
        }

    }

    public class CollisionEdge : CollisionShape
    {
        public borders borderType;

        Vector2 bounds;

        public CollisionEdge(borders pBorderType, Vector2 pBounds)
        {
            borderType = pBorderType;
            bounds = pBounds;
        }

        internal override bool isPointOverlapping(Vector2 p)
        {
            if (borderType == borders.left)
            {
                if (p.X < 0)
                {
                    return true;
                }
            }
            else if (borderType == borders.bottem)
            {
                if (p.Y < 0)
                {
                    return true;
                }
            }
            else if (borderType == borders.right)
            {
                if (p.X > bounds.X)
                {
                    return true;
                }
            }
            else if (borderType == borders.top)
            {
                if (p.Y > bounds.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
