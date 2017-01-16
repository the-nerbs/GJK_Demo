using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GJK_Demo
{
    /// <summary>
    /// Panel override that draws a grid and <see cref="Shape"/>s.
    /// </summary>
    internal sealed class WorkspacePanel : Panel
    {
        // the interval at which grid lines are drawn.
        private const float GridSize = 25;


        // The shapes that are registered.
        private readonly List<Shape> _shapes = new List<Shape>();

        // The current mouse action
        private MouseAction _currentAction = MouseAction.None;

        // The shape being interacted with by the mouse.
        private Shape _activeShape = null;

        // The camera position.
        private Vector2 _viewOffset = Vector2.Zero;

        // The previous mouse position (in control space/pixels).
        private Vector2 _prevMousePxPos = Vector2.Zero;


        public event EventHandler<ShapeMovedEventArgs> ShapeMoved;



        [Bindable(false)]
        [DefaultValue(true)]
        [Description("Toggles the ability to use the mouse to move shapes.")]
        public bool CanUserMoveShapes { get; set; } = true;

        [Bindable(false)]
        [DefaultValue(true)]
        [Description("Toggles the ability to use the mouse to pan the view.")]
        public bool CanUserMoveView { get; set; } = true;


        public WorkspacePanel()
        {
            BackColor = Color.White;
            BorderStyle = BorderStyle.Fixed3D;
        }


        /// <summary>
        /// Centers the view on the origin point.
        /// </summary>
        public void CenterToOrigin()
        {
            _viewOffset = new Vector2(
                Width / 2.0,
                Height / 2.0
            );

            Invalidate();
        }


        /// <summary>
        /// Registers a shape to be drawn and interacted with.
        /// </summary>
        /// <param name="shape">The shape to register.</param>
        public void RegisterShape(Shape shape)
        {
            _shapes.Add(shape);
            Invalidate();
        }

        /// <summary>
        /// Unregisters a shape from the panel.
        /// </summary>
        /// <param name="shape">The shape to unregister.</param>
        public void UnregisterShape(Shape shape)
        {
            _shapes.Remove(shape);
        }


        #region Drawing

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform((float)_viewOffset.X, (float)_viewOffset.Y);

            DrawGrid(e.Graphics, e.ClipRectangle);

            foreach (var shape in _shapes)
            {
                shape.Draw(e.Graphics);
            }
        }

        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="g">THe graphics to draw to.</param>
        /// <param name="bounds">The bounds of the area being redrawn.</param>
        private void DrawGrid(Graphics g, Rectangle bounds)
        {
            float left = (float)(-_viewOffset.X + bounds.X - GridSize);
            float right = (float)(-_viewOffset.X + bounds.X + bounds.Width + GridSize);

            left = NearestIntervalLessThan(left, GridSize);
            right = NearestIntervalGreaterThan(right, GridSize);

            float top = (float)(-_viewOffset.Y + bounds.Y - GridSize);
            float bottom = (float)(-_viewOffset.Y + bounds.Y + bounds.Height + GridSize);

            top = NearestIntervalLessThan(top, GridSize);
            bottom = NearestIntervalGreaterThan(bottom, GridSize);

            for (float x = left; x <= right; x += GridSize)
            {
                g.DrawLine(Pens.LightGray, x, top, x, bottom);
            }

            for (float y = top; y <= bottom; y += GridSize)
            {
                g.DrawLine(Pens.LightGray, left, y, right, y);
            }


            if (left < 0 && right > 0)
            {
                g.DrawLine(Pens.Green, 0, top, 0, bottom);
            }

            if (top < 0 && bottom > 0)
            {
                g.DrawLine(Pens.Green, left, 0, right, 0);
            }
        }

        /// <summary>
        /// Gets the nearest multiple of interval that is less than value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="interval">The interval.</param>
        /// <returns>the nearest multiple of interval that is less than value.</returns>
        private static float NearestIntervalLessThan(float value, float interval)
        {
            return (float)(interval * Math.Floor(value / interval));
        }

        /// <summary>
        /// Gets the nearest multiple of interval that is greater than value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="interval">The interval.</param>
        /// <returns>the nearest multiple of interval that is greater than value.</returns>
        private static float NearestIntervalGreaterThan(float value, float interval)
        {
            return (float)(interval * Math.Ceiling(value / interval));
        }

        #endregion

        #region Mouse handling

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                Vector2 mouseWorldPos = GetMouseWorldPos(e);
                _currentAction = MouseAction.None;


                if (CanUserMoveShapes)
                {
                    // check for hitting a shape's control point.
                    // note: done in reverse of drawing order to get the one that's "on top".
                    for (int i = _shapes.Count - 1; i >= 0; i--)
                    {
                        var shape = _shapes[i];
                        Vector2 delta = shape.Position - mouseWorldPos;

                        if (delta.Length < Shape.ControlPointRadius)
                        {
                            _currentAction = MouseAction.MoveShape;
                            _activeShape = shape;
                            break;
                        }
                    }
                }


                if (CanUserMoveView &&
                    _currentAction == MouseAction.None)
                {
                    // we only hit the background, so we're panning
                    _currentAction = MouseAction.PanView;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Vector2 mousePxPos = new Vector2(e.X, e.Y);

            if (_currentAction != MouseAction.None)
            {
                Vector2 delta = VectorToWorld(mousePxPos - _prevMousePxPos);

                switch (_currentAction)
                {
                    case MouseAction.MoveShape:
                        {
                            // move the shape
                            _activeShape.Position += delta;

                            // fire the ShapeMoved event now.  The client can (in theory) move the
                            // shape in their handlers. We want to invalidate the correct parts of
                            // the view, even in this case.
                            OnShapeMoved(_activeShape);

                            // invalidate new position
                            Rectangle shapeBounds = _activeShape.AABB;

                            // add a small area around, else we miss some parts.
                            shapeBounds.Inflate(5, 5);

                            shapeBounds.X += e.X;
                            shapeBounds.Y += e.Y;

                            Invalidate(shapeBounds);

                            // invalidate old position
                            shapeBounds.X -= (int)delta.X;
                            shapeBounds.Y -= (int)delta.Y;
                            Invalidate(shapeBounds);
                        }
                        break;


                    case MouseAction.PanView:
                        _viewOffset += delta;

                        Invalidate();
                        break;


                    default:
                        // not doing anything.
                        break;
                }
            }

            _prevMousePxPos = mousePxPos;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                _currentAction = MouseAction.None;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            // mouse moved out of bounds - stop trying to do stuff with moves until we get another mouse down.
            _currentAction = MouseAction.None;
        }

        /// <summary>
        /// Gets the world-space coordinates for the current mouse position.
        /// </summary>
        /// <param name="e">The mouse event args.</param>
        /// <returns>The world-space coordinates.</returns>
        private Vector2 GetMouseWorldPos(MouseEventArgs e)
        {
            Vector2 screenPoint = new Vector2(e.X, e.Y);
            return PointToWorld(screenPoint);
        }

        #endregion


        private void OnShapeMoved(Shape shape)
        {
            ShapeMoved?.Invoke(this, new ShapeMovedEventArgs(shape));
        }


        /// <summary>
        /// Transforms a screen-space vector to a world-space vector.
        /// </summary>
        /// <param name="screenVec">The vector in screen-space.</param>
        /// <returns>The vector in world-space.</returns>
        private Vector2 VectorToWorld(Vector2 screenVec)
        {
            // reserved for future use by zoom calculations.
            return screenVec;
        }

        /// <summary>
        /// Transforms a screen-space point to a world-space point.
        /// </summary>
        /// <param name="screenPoint">The point in screen-space.</param>
        /// <returns>The point in world-space.</returns>
        private Vector2 PointToWorld(Vector2 screenPoint)
        {
            var worldVec = VectorToWorld(screenPoint);
            return worldVec - _viewOffset;
        }

        /// <summary>
        /// Transforms a world-space vector to a screen-space vector.
        /// </summary>
        /// <param name="worldVec">The vector in world-space.</param>
        /// <returns>The vector in screen-space.</returns>
        private Vector2 VectorToScreen(Vector2 worldVec)
        {
            // reserved for future use by zoom calculations.
            return worldVec;
        }

        /// <summary>
        /// Transforms a world-space point to a screen-space point.
        /// </summary>
        /// <param name="worldPoint">The point in world-space.</param>
        /// <returns>The point in screen-space.</returns>
        private Vector2 PointToScreen(Vector2 worldPoint)
        {
            var screenVec = VectorToScreen(worldPoint);
            return screenVec + _viewOffset;
        }


        private enum MouseAction
        {
            None,
            MoveShape,
            PanView,
        }
    }
}
