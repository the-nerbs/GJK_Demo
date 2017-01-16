using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GJK_Demo
{
    static class GJK
    {
        /// <summary>
        /// Determines if two convex shapes are intersecting.
        /// </summary>
        /// <param name="a">One of the shapes.</param>
        /// <param name="b">The other shape.</param>
        /// <returns>True if both shapes are convex and intersecting. Returned value is undefined if either shape is concave.</returns>
        public static bool Intersects(Shape a, Shape b)
        {
            Simplex simplex = new Simplex();

            // start with the axis = the vector between the two shapes.
            Vector2 d = (b.Position - a.Position);

            Vector2 supportPt = Support(a, b, d);
            simplex.Add(supportPt);

            d = (Vector2.Zero - supportPt);

            int itr = 0;    // To avoid any infinite loops.

            while (itr++ < 100)
            {
                Vector2 newPoint = Support(a, b, d);

                if (newPoint.Dot(d) < 0)
                {
                    // new point doesn't expand towards the origin any more.
                    return false;
                }

                simplex.Add(newPoint);

                if (simplex.Contains(Vector2.Zero, ref d))
                {
                    // simplex contains the origin -> shapes intersect.
                    return true;
                }
            }

            return false;
        }


        // Gets the Minkowski difference of the support functions along the given axis.
        private static Vector2 Support(Shape a, Shape b, Vector2 axis)
        {
            Vector2 supportA = a.Position + a.SupportFunction(axis);
            Vector2 supportB = b.Position + b.SupportFunction(-axis);

            return supportA - supportB;
        }
    }
}
