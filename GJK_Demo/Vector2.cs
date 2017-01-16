using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GJK_Demo
{
    /// <summary>
    /// A 2-dimensional vector.
    /// </summary>
    [DebuggerDisplay("{DebugDisplay,nq}")]
    public struct Vector2 : IEquatable<Vector2>
    {
        // tolerance to avoid degenerate cases when normalizing.
        private const double Epsilon = 0.0000005;


        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 UnitX = new Vector2(1, 0);
        public static readonly Vector2 UnitY = new Vector2(0, 1);
        public static readonly Vector2 One = new Vector2(1, 1);


        public double X { get; set; }
        public double Y { get; set; }

        public double Length
        {
            get { return Math.Sqrt(Dot(this)); }
        }

        [DebuggerHidden]
        private string DebugDisplay
        {
            get { return $"{{ X={X}, Y={Y} }}"; }
        }


        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }


        public double Dot(Vector2 vec)
        {
            return X * vec.X + Y * vec.Y;
        }

        public Vector2 Perp()
        {
            return new Vector2(-Y, X);
        }

        public Vector2 Norm()
        {
            double magSquared = Dot(this);

            if (magSquared >= Epsilon)
            {
                return this / Math.Sqrt(magSquared);
            }

            return Zero;
        }

        public double AngleTo(Vector2 other)
        {
            // alternative: Math.Acos(Dot(other));
            return Math.Atan2(Y, X) - Math.Atan2(other.Y, other.X);
        }


        public override bool Equals(object obj)
        {
            return (obj is Vector2) &&
                   Equals((Vector2)obj);
        }

        public bool Equals(Vector2 other)
        {
            return X == other.X &&
                   Y == other.Y;
        }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 23;

                hash = (hash * 37) + (X.GetHashCode() * 17);
                hash = (hash * 37) + (Y.GetHashCode() * 17);

                return hash;
            }
        }

        public override string ToString()
        {
            return $"{{ X={X}, Y={Y} }}";
        }

        public static bool operator==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return !(lhs == rhs);
        }

        public static Vector2 operator-(Vector2 rhs)
        {
            return -1.0 * rhs;
        }

        public static Vector2 operator+(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(
                lhs.X + rhs.X,
                lhs.Y + rhs.Y
            );
        }

        public static Vector2 operator-(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(
                lhs.X - rhs.X,
                lhs.Y - rhs.Y
            );
        }

        public static Vector2 operator*(double lhs, Vector2 rhs)
        {
            return new Vector2(
                lhs * rhs.X,
                lhs * rhs.Y
            );
        }

        public static Vector2 operator*(Vector2 lhs, double rhs)
        {
            return rhs * lhs;
        }

        public static Vector2 operator/(Vector2 lhs, double rhs)
        {
            return new Vector2(
                lhs.X / rhs,
                lhs.Y / rhs
            );
        }


        /// <summary>
        /// Computes the left-sided vector triple product, (A x B) x C
        /// </summary>
        /// <param name="a">Vector A</param>
        /// <param name="b">Vector B</param>
        /// <param name="c">Vector C</param>
        /// <returns>A vector which is perpendicular to C as well as (A x B).</returns>
        public static Vector2 LeftTriple(Vector2 a, Vector2 b, Vector2 c)
        {
            // see: http://mathworld.wolfram.com/VectorTripleProduct.html
            double bcDot = b.Dot(c);
            double acDot = a.Dot(c);

            return b * acDot - a * bcDot;
        }

        /// <summary>
        /// Computes the right-sided vector triple product, A x (B x C)
        /// </summary>
        /// <param name="a">Vector A</param>
        /// <param name="b">Vector B</param>
        /// <param name="c">Vector C</param>
        /// <returns>A vector which is perpendicular to A as well as (B x C).</returns>
        public static Vector2 RightTriple(Vector2 a, Vector2 b, Vector2 c)
        {
            // see: http://mathworld.wolfram.com/VectorTripleProduct.html
            double acDot = a.Dot(c);
            double abDot = a.Dot(b);

            return b * acDot - c * abDot;
        }
    }
}
