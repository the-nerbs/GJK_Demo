using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GJK_Demo
{
    class Simplex
    {
        private readonly List<Vector2> _points = new List<Vector2>(capacity: 3);


        /// <summary>
        /// Gets "point A" for the Contains algorithm.
        /// </summary>
        private Vector2 PointA
        {
            get
            {
                Debug.Assert(_points.Count >= 1, "Could not get point A: not enough points.");
                return _points[_points.Count - 1];
            }
        }

        /// <summary>
        /// Gets "point B" for the Contains algorithm.
        /// </summary>
        private Vector2 PointB
        {
            get
            {
                Debug.Assert(_points.Count >= 2, "Could not get point B: not enough points.");
                return _points[_points.Count - 2];
            }
        }

        /// <summary>
        /// Gets "point C" for the Contains algorithm.
        /// </summary>
        private Vector2 PointC
        {
            get
            {
                Debug.Assert(_points.Count >= 3, "Could not get point C: not enough points.");
                return _points[_points.Count - 3];
            }
        }


        /// <summary>
        /// Adds a points to the simplex.
        /// </summary>
        /// <param name="pt">The point being added.</param>
        public void Add(Vector2 pt)
        {
            _points.Add(pt);
        }


        /// <summary>
        /// Determines if the simplex contains the given point and updates the direction vector.
        /// </summary>
        /// <param name="p">The points to check.</param>
        /// <param name="direction">[out] If the point is not contained by the simplex, the updated direction vector.</param>
        /// <returns>True if the point is contained by the simplex; false if not.</returns>
        public bool Contains(Vector2 p, ref Vector2 direction)
        {
            Vector2 a = PointA;
            Vector2 ap = p - a;

            if (_points.Count == 3)
            {
                // we have a triangle
                Vector2 b = PointB;
                Vector2 c = PointC;

                Vector2 ab = b - a;
                Vector2 ac = c - a;


                // explanation of region #s:
                //
                //V  A         1         B
                //V    o---------------o
                //V     \      2      /
                //V      \           /
                //V       \ 4     6 /
                //V     3  \       /  5
                //V         \     /
                //V          \   /
                //V           \ /
                //V            o
                //V            C
                //
                // Each region is defined as being on one side of a line. If the point is
                // contained by all regions 2, 4, and 6, then it is within the simplex. If it is
                // contained by any of region 1, 3, or 5, then it is outside the simplex.
                //
                // NerbsNote: I believe GJK already ensures that p is within region 6 when this is
                // called, by means of the direction vectors selected here. This is why we only
                // need to check against the AB and AC lines here.


                // check against AB.
                Vector2 abPerp = Vector2.LeftTriple(ac, ab, ab);    // points into region 1

                if (abPerp.Dot(ap) > 0)
                {
                    // p is in region 1. Point C is least optimal, so remove it
                    _points.RemoveAt(0);

                    // update direction to point towards p.
                    direction = abPerp;
                }
                else
                {
                    // p is in region 2. Check against AC.
                    Vector2 acPerp = Vector2.LeftTriple(ab, ac, ac);    // points into region 3

                    if (acPerp.Dot(ap) > 0)
                    {
                        // p is in region 3. Point B is least optimal, so remove it.
                        _points.Remove(PointB);

                        // update direction to point towards p.
                        direction = acPerp;
                    }
                    else
                    {
                        // p is within the simplex.
                        return true;
                    }
                }
            }
            else if (_points.Count == 2)
            {
                // we have a line segment.
                Vector2 b = PointB;
                Vector2 ab = b - a;

                Vector2 abPerp = Vector2.LeftTriple(ab, ap, ab);
                direction = abPerp;
            }

            return false;
        }
    }
}
