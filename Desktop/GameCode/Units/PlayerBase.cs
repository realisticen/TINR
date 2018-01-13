using Desktop.GameCode.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Express.Graphics;
using Desktop.Express.Scene.Objects;

namespace Desktop.GameCode.Units
{
    class PlayerBase : StaticDrawable, IUnit, IAARectangleCollider, ICustomCollider
    {
        public int Money = 100;
        public int KillReward => 1000;
        public PlayerBase Owner { get; set; }
        public float Health { get; set; } = 4;
        public bool Dead { get => Health <= 0; }
        public bool Remove { get => Dead; }

        public PlayerBase(Sprite sprite, Point pos) : base(sprite)
        {
            Position = pos.ToVector2();
            Width = 250;
            Height = 500;
        }

        public override Rectangle DestinationRectangle { get => new Rectangle(Position.ToPoint(), new Point((int)Width, (int)Height)); }
        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public UnitState State => UnitState.IDLE;

        public Vector2 Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void collidedWithItem(object o)
        {
        }

        public bool collidingWithItem(object o)
        {
            if(Dead)
                return false;

            if (o is BasicUnit unit)
                if (unit.Owner != this)
                    return true;
            return false;
        }
    }
}
