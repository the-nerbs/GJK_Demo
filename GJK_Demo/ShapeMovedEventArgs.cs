using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GJK_Demo
{
    sealed class ShapeMovedEventArgs : EventArgs
    {
        public Shape Shape { get; }


        public ShapeMovedEventArgs(Shape shape)
        {
            Shape = shape;
        }
    }
}
