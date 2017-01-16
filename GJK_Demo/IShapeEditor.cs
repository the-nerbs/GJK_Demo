using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GJK_Demo
{
    /// <summary>
    /// Interface for shape editor pages.
    /// </summary>
    interface IShapeEditor
    {
        event EventHandler ShapeChanged;

        void Initialize(Shape shape);
    }
}
