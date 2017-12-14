using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Scene.Objects
{
    interface IParticle : IMovable, IMass, IParticleCollider
    {
    }
}
