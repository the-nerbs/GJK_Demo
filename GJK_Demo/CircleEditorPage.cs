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
    /// Control page for editing the properties of a <see cref="Circle"/>.
    /// </summary>
    public partial class CircleEditorPage : UserControl, IShapeEditor
    {
        private Circle _shape;


        public event EventHandler ShapeChanged;


        public CircleEditorPage()
        {
            InitializeComponent();
        }


        public void Initialize(Shape shape)
        {
            _shape = (Circle)shape;

            txtRadius.Text = _shape.Radius.ToString();
        }


        private void txtRadius_TextChanged(object sender, EventArgs e)
        {
            double value;

            if (double.TryParse(txtRadius.Text, out value))
            {
                _shape.Radius = value;
            }

            OnShapeChanged();
        }

        private void OnShapeChanged()
        {
            ShapeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
