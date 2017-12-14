using Desktop.Express.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Scene.Objects
{
    interface IConvexCollider
    {
        ConvexPolygon Bounds { get; set; }
    }
}
