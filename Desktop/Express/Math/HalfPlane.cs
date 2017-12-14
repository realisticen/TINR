using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Math
{
    class HalfPlane
    {
        public float distance;
        public Vector2 normal;

        public HalfPlane(Vector2 _normal, float _distance)
        {
            normal = _normal;
            distance = _distance;
        }

        public void setNormal(Vector2 _normal)
        {
            normal = _normal;
            if(normal.LengthSquared() != 1)
                normal.Normalize();
        }
    }
}
