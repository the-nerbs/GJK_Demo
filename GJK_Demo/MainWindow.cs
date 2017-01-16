using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GJK_Demo
{
    public partial class MainWindow : Form
    {
        // note: we only support 2 shapes for this demo.
        private readonly Shape[] _shapes = new Shape[2];


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                return handleParam;
            }
        }


        public MainWindow()
        {
            InitializeComponent();

            // make some shapes to start with
            var square = new Polygon();
            square.Name = "Square";
            square.Position = new Vector2(-75, -75);
            square.Color = Color.Red;
            square.Points.Add(new Vector2(-50, -50));
            square.Points.Add(new Vector2(+50, -50));
            square.Points.Add(new Vector2(+50, +50));
            square.Points.Add(new Vector2(-50, +50));


            var circle = new Circle();
            circle.Name = "Circle";
            circle.Position = new Vector2(25, 50);
            circle.Color = Color.Blue;
            circle.Radius = 75;


            _shapes[0] = square;
            _shapes[1] = circle;

            workspace.CenterToOrigin();

            foreach (var shape in _shapes)
            {
                workspace.RegisterShape(shape);
                shapesGrid.Rows.Add(shape.Name, ". . .");
            }
        }


        /// <summary>
        /// Handles the edit button being clicked in the shapes grid.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">The event arguments.</param>
        private void shapesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colEdit.Index &&
                e.RowIndex >= 0 && 
                e.RowIndex < _shapes.Length)
            {
                int shapeIndex = e.RowIndex;

                using (var editor = new ShapeEditor(_shapes[shapeIndex]))
                {
                    if (editor.ShowDialog(this) == DialogResult.OK)
                    {
                        workspace.UnregisterShape(_shapes[shapeIndex]);

                        _shapes[shapeIndex] = editor.Shape;
                        workspace.RegisterShape(_shapes[shapeIndex]);

                        Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Handles a shape being moved in the workspace.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void workspace_ShapeMoved(object sender, ShapeMovedEventArgs e)
        {
            if (GJK.Intersects(_shapes[0], _shapes[1]))
            {
                lblIntersecting.Text = "TRUE";
            }
            else
            {
                lblIntersecting.Text = "FALSE";
            }
        }
    }
}
