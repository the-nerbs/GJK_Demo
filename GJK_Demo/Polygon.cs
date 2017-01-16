using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GJK_Demo
{
    /// <summary>
    /// A Shape defined as a closed set of connected points.
    /// </summary>
    class Polygon : Shape
    {
        private readonly List<Vector2> _points = new List<Vector2>();


        /// <summary>
        /// Gets the list of points.
        /// </summary>
        public IList<Vector2> Points
        {
            get { return _points; }
        }


        public Polygon()
        { }

        public Polygon(IEnumerable<Vector2> points)
        {
            _points.AddRange(points);
        }

        public Polygon(params Vector2[] points)
            : this((IEnumerable<Vector2>)points)
        { }


        /// <inheritdoc />
        public override Vector2 SupportFunction(Vector2 direction)
        {
            if (_points.Count == 0)
                throw new InvalidOperationException();

            int idxMax = 0;
            double distMax = direction.Dot(_points[0]);

            for (int i = 1; i < _points.Count; i++)
            {
                double distPt = direction.Dot(_points[i]);
                if (distPt > distMax)
                {
                    idxMax = i;
                    distMax = distPt;
                }
            }

            return _points[idxMax];
        }

        /// <inheritdoc />
        protected override void DrawImpl(Graphics g)
        {
            var pts = Points
                .Select(p => new PointF((float)p.X, (float)p.Y))
                .ToArray();

            if (Points.Count > 3)
            {

                using (var linePen = MakeOutlinePen())
                {
                    g.DrawPolygon(linePen, pts);
                }
            }

            using (var ptBrush = MakePointBrush())
            {
                foreach (var pt in pts)
                {
                    float x = (pt.X - 1.5f);
                    float y = (pt.Y - 1.5f);

                    g.FillRectangle(ptBrush,
                        x, y,
                        3, 3);
                }
            }
        }

        /// <inheritdoc />
        protected override Shape CopyImpl()
        {
            var shape = new Polygon();

            foreach (var pt in Points)
            {
                shape.Points.Add(pt);
            }

            return shape;
        }
    }
}
