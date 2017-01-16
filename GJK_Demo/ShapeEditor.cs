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
    public partial class ShapeEditor : Form
    {
        private IList<TypeSelection> ShapeTypes = new[]
        {
            new TypeSelection(typeof(Circle)),
            new TypeSelection(typeof(Polygon))
        };


        // The position of the shape this was initialized with. We edit the shape at
        // (0,0) for simplicity, and then set this position when the user gets Shape.
        private Vector2 _shapePosition;

        // The shape being edited.
        private Shape _shape;


        /// <summary>
        /// Gets the shape being edited.
        /// </summary>
        private Shape ShapeInternal
        {
            get { return _shape; }
            set
            {
                if (_shape != null)
                {
                    preview.UnregisterShape(_shape);
                }

                _shape = value;

                if (_shape != null)
                {
                    _shape.Position = Vector2.Zero;
                    preview.RegisterShape(_shape);
                }

                InitializeForNewShape();
            }
        }

        /// <summary>
        /// Gets a copy of the shape being edited.
        /// </summary>
        public Shape Shape
        {
            get
            {
                Shape copy = ShapeInternal.Copy();
                copy.Position = _shapePosition;
                return copy;
            }
        }


        public ShapeEditor()
            : this(MakeDefaultShape())
        { }

        public ShapeEditor(Shape shape)
        {
            InitializeComponent();

            // init the type selection combo box items.
            cbType.DisplayMember = nameof(TypeSelection.Name);
            foreach (var item in ShapeTypes)
            {
                cbType.Items.Add(item);
            }


            // init the shape being edited
            _shapePosition = shape?.Position ?? Vector2.Zero;
            ShapeInternal = shape?.Copy();

            // init the preview pane.
            preview.CenterToOrigin();
        }


        /// <summary>
        /// Handles the shape type being changed.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = (TypeSelection)cbType.SelectedItem;

            string name = ShapeInternal.Name;
            Color color = ShapeInternal.Color;
            object tag = ShapeInternal.Tag;

            ShapeInternal = selection.CreateShape();

            ShapeInternal.Name = name;
            ShapeInternal.Color = color;
            ShapeInternal.Tag = tag;
        }

        /// <summary>
        /// Handles the shape's properties being changed by the editor page.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void Editor_ShapeChanged(object sender, EventArgs e)
        {
            preview.Invalidate();
        }


        /// <summary>
        /// Initializes the editor window for a new shape.
        /// </summary>
        private void InitializeForNewShape()
        {
            // clear the editor page
            if (editorPanel.Controls.Count > 0)
            {
                foreach (IShapeEditor editor in editorPanel.Controls.OfType<IShapeEditor>())
                {
                    editor.ShapeChanged -= Editor_ShapeChanged;
                }
            }
            editorPanel.Controls.Clear();

            if (ShapeInternal != null)
            {
                txtName.Text = ShapeInternal.Name;

                cbType.SelectedIndexChanged -= cbType_SelectedIndexChanged;
                cbType.SelectedItem = ShapeTypes.First(s => s.Type == ShapeInternal.GetType());
                cbType.SelectedIndexChanged += cbType_SelectedIndexChanged;

                Control editorControl = GetEditorForShape(_shape);

                editorControl.Dock = DockStyle.Fill;
                editorPanel.Controls.Add(editorControl);

                var editor = ((IShapeEditor)editorControl);
                editor.Initialize(_shape);
                editor.ShapeChanged += Editor_ShapeChanged;
            }
        }


        /// <summary>
        /// Gets the editor page for a shape.
        /// </summary>
        /// <param name="shape">The shape to get the editor page for.</param>
        private static Control GetEditorForShape(Shape shape)
        {
            if (shape == null)
            {
                return null;
            }
            else if (shape is Circle)
            {
                return new CircleEditorPage();
            }
            else // if (shape is Polygon)
            {
                return new PolygonEditorPage();
            }
        }

        /// <summary>
        /// Gets the default shape to edit when none is passed to the constructor.
        /// </summary>
        private static Shape MakeDefaultShape()
        {
            const double DegreesToRadians = Math.PI / 180.0;

            var polygon = new Polygon();

            for (int i = 0; i < 6; i++)
            {
                polygon.Points.Add(new Vector2(
                    Math.Cos(i * 60 * DegreesToRadians),
                    Math.Sign(i * 60 * DegreesToRadians)
                ));
            }

            return polygon;
        }


        /// <summary>
        /// Helper class used as the items of the Type selection combo box.
        /// </summary>
        private class TypeSelection
        {
            public Type Type { get; }

            public string Name
            {
                get { return Type.Name; }
            }


            public TypeSelection(Type type)
            {
                Type = type;
            }


            public Shape CreateShape()
            {
                return (Shape)Activator.CreateInstance(Type);
            }
        }
    }
}
