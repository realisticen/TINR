using Desktop.Express.Scene.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Physics
{
    static class MovementPhysics
    {
        public static void simulateMovement(IMovable obj, GameTime time)
        {
            obj.Position += obj.Velocity * (float)time.ElapsedGameTime.TotalSeconds;
        }

        public static void simulateMovement(IRotatable obj, GameTime time)
        {
            obj.RotationAngle += obj.AngularVelocity * (float)time.ElapsedGameTime.TotalSeconds;
        }
    }
}
