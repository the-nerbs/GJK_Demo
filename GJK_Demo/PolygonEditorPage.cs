using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GJK_Demo
{
    /// <summary>
    /// Control page for editing the properties of a <see cref="Polygon"/>.
    /// </summary>
    public partial class PolygonEditorPage : UserControl, IShapeEditor
    {
        private Polygon _shape;


        public event EventHandler ShapeChanged;


        public PolygonEditorPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Initializes this editor page for the given shape.
        /// </summary>
        /// <param name="shape">The shape to initialize with.</param>
        public void Initialize(Shape shape)
        {
            _shape = (Polygon)shape;

            pointsGrid.CellValueChanged -= pointsGrid_CellValueChanged;
            pointsGrid.RowsAdded -= pointsGrid_RowsAdded;
            pointsGrid.RowsRemoved -= pointsGrid_RowsRemoved;

            pointsGrid.Rows.Clear();

            foreach (var pt in _shape.Points)
            {
                pointsGrid.Rows.Add(pt.X, pt.Y);
            }

            pointsGrid.CellValueChanged += pointsGrid_CellValueChanged;
            pointsGrid.RowsAdded += pointsGrid_RowsAdded;
            pointsGrid.RowsRemoved += pointsGrid_RowsRemoved;
        }


        /// <summary>
        /// Handles a point coordinate being changed.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">The change event args.</param>
        private void pointsGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // This seems to get triggered for header cells with Index = -1. Ignore these.
            // Also ignore changes to the "new row" row.
            if (e.RowIndex >= 0 &&
                e.ColumnIndex >= 0 &&
                e.RowIndex != pointsGrid.NewRowIndex)
            {
                int pointIdx = e.RowIndex;
                Vector2 orig = _shape.Points[pointIdx];

                string cellText = pointsGrid[e.ColumnIndex, e.RowIndex].Value.ToString();
                double newCoord = double.Parse(cellText);

                if (e.ColumnIndex == colX.Index)
                {
                    _shape.Points[pointIdx] = new Vector2(newCoord, orig.Y);
                }
                else if (e.ColumnIndex == colY.Index)
                {
                    _shape.Points[pointIdx] = new Vector2(orig.X, newCoord);
                }

                OnShapeChanged();
            }
        }

        /// <summary>
        /// Handles new points being added to the polygon.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">The change event args.</param>
        private void pointsGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                int realIndex = e.RowIndex + i - 1;

                _shape.Points.Insert(realIndex, GetRowPointValue(realIndex));
            }

            OnShapeChanged();
        }

        /// <summary>
        /// Handles points being removed from the polygon.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">The change event args.</param>
        private void pointsGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                _shape.Points.RemoveAt(e.RowIndex);
            }

            OnShapeChanged();
        }


        private void OnShapeChanged()
        {
            ShapeChanged?.Invoke(this, EventArgs.Empty);
        }


        private Vector2 GetRowPointValue(int rowIndex)
        {
            object xObject = pointsGrid[colX.Index, rowIndex].Value;
            object yObject = pointsGrid[colY.Index, rowIndex].Value;

            double x, y;

            if (xObject == null ||
                !double.TryParse(xObject.ToString(), out x))
            {
                x = 0.0;
            }

            if (yObject == null ||
                !double.TryParse(yObject.ToString(), out y))
            {
                y = 0.0;
            }

            return new Vector2(x, y);
        }
    }
}
