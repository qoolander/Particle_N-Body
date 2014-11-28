using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTkTest
{
    class Level
    {
        public List<CollisionShape> collisionGeomitry;

        /*
         * return null if colliding with nothing, else returns a list of things that the object is colliding with
         */
        List<Object> CollidingWith ()
        {
            return null;
        }
    }

    class CollisionShape
    {
        internal List<Vector2> points = new List<Vector2>();

        internal Object owner;

        internal virtual bool isPointOverlapping(Vector2 p)
        {
            return false;
        }
    }

    class CollisionSphere : CollisionShape
    {
        public float radius;

        public Vector2 position;

        internal override bool isPointOverlapping(Vector2 p)
        {
            try
            {
                float xd = position.X - p.X;
                float yd = position.Y - p.Y;

                float distance = (float)(Math.Sqrt(xd * xd + yd * yd));

                return distance <= radius;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    class CollisionBounds : CollisionShape
    {

    }
}
