using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GJK_Demo
{
    public abstract class Shape
    {
        internal const double ControlPointRadius = 3;


        public string Name { get; set; } = "NewShape";
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Color Color { get; set; } = Color.Black;
        public object Tag { get; set; }

        /// <summary>
        /// Gets an axis-aligned bounding box containing the whole shape.
        /// </summary>
        public Rectangle AABB
        {
            get
            {
                float l = (float)(SupportFunction(-Vector2.UnitX).X);
                float r = (float)(SupportFunction(Vector2.UnitX).X);
                float t = (float)(SupportFunction(-Vector2.UnitY).Y);
                float b = (float)(SupportFunction(Vector2.UnitY).Y);

                float w = r - l;
                float h = b - t;

                return new Rectangle(
                    (int)Math.Floor(l), (int)Math.Floor(t),
                    (int)Math.Ceiling(w), (int)Math.Ceiling(h));
            }
        }



        /// <summary>
        /// Draws this shape.
        /// </summary>
        /// <param name="g">The graphics used to draw this shape.</param>
        public void Draw(Graphics g)
        {
            var prevTransform = g.Transform;

            try
            {
                g.TranslateTransform((float)Position.X, (float)Position.Y);
                var shapeTransform = g.Transform;

                DrawImpl(g);


                g.Transform = shapeTransform;

                DrawControlPoint(g);
            }
            finally
            {
                g.Transform = prevTransform;
            }
        }

        /// <summary>
        /// Creates a copy of this shape.
        /// </summary>
        public Shape Copy()
        {
            Shape copy = CopyImpl();

            copy.Name = Name;
            copy.Position = Position;
            copy.Color = Color;
            copy.Tag = Tag;

            return copy;
        }


        /// <summary>
        /// Gets the point on this shape that is furthest in a given direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns>The point furthest in the given direction.</returns>
        public abstract Vector2 SupportFunction(Vector2 direction);

        /// <summary>
        /// Draws this shape.
        /// </summary>
        /// <param name="g">The graphics used to draw this shape.</param>
        /// <remarks>
        /// Called by Draw(), which performs the translation to this shapes Position.
        /// </remarks>
        protected abstract void DrawImpl(Graphics g);

        /// <summary>
        /// Creates a copy of this shape's implementation details.
        /// </summary>
        protected abstract Shape CopyImpl();


        /// <summary>
        /// Draws to control point, which the user can click on to move this shape.
        /// </summary>
        /// <param name="g">The graphics to draw to.</param>
        private void DrawControlPoint(Graphics g)
        {
            using (var fill = new SolidBrush(Color))
            {
                float xy = (float)(-ControlPointRadius);
                float wh = (float)(2 * ControlPointRadius);

                g.FillRectangle(fill, xy, xy, wh, wh);
            }
        }


        /// <summary>
        /// Makes a Pen used to draw the outline of the shape.
        /// </summary>
        protected Pen MakeOutlinePen()
        {
            return new Pen(Color, 1.5f);
        }

        /// <summary>
        /// Makes a Brush used to draw the points of this shape.
        /// </summary>
        protected Brush MakePointBrush()
        {
            return new SolidBrush(ControlPaint.Dark(Color));
        }
    }
}
