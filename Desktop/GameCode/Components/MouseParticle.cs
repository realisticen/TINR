using Desktop.Express.Scene.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Desktop.GameCode.Components
{
    class MouseParticle : IParticleCollider
    {
        public float Radius { get => 1; set => throw new NotImplementedException(); }
        public Vector2 Position { get; set; }
    }
}
