using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Math
{
    class ConvexPolygon
    {
        public List<Vector2> vertices;
        public List<Vector2> edges = new List<Vector2>();
        public List<HalfPlane> halfPlanes = new List<HalfPlane>();

        public ConvexPolygon(List<Vector2> _vertices)
        {
            vertices = _vertices;
            for (int i = 0; i < vertices.Count; i++)
            {
                int j = (i + 1) % vertices.Count;
                Vector2 edge = Vector2.Subtract(vertices[j], vertices[i]);
                edges.Add(edge);

                Vector2 normal = new Vector2(edge.Y, -edge.X);
                normal.Normalize();

                float distance = Vector2.Dot(vertices[i], normal);
                halfPlanes.Add(new HalfPlane(normal, distance));
            }
        }
    }
}
