using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GJK_Demo
{
    /// <summary>
    /// A circle shape.
    /// </summary>
    class Circle : Shape
    {
        /// <summary>
        /// The radius.
        /// </summary>
        public double Radius { get; set; }


        public Circle()
            : this(100.0)
        { }

        public Circle(double radius)
        {
            Radius = radius;
        }


        /// <inheritdoc />
        public override Vector2 SupportFunction(Vector2 direction)
        {
            return (Radius * direction.Norm());
        }

        /// <inheritdoc />
        protected override void DrawImpl(Graphics g)
        {
            using (var p = MakeOutlinePen())
            {
                float xy = (float)(-Radius);
                float wh = (float)(2 * Radius);

                g.DrawEllipse(p,
                    xy, xy,
                    wh, wh);
            }
        }

        /// <inheritdoc />
        protected override Shape CopyImpl()
        {
            return new Circle(Radius);
        }
    }
}
