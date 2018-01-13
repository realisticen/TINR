using Desktop.Express.Scene.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Express.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Desktop.GameCode.Units;
using TINRGame;
using Desktop.GameCode.Util;
using System.Diagnostics;

namespace Units
{
    class MeleeStudent : BasicUnit, ICustomUpdate
    {
        public static MeleeStudent GetMeleeStudent(SpriteMaker maker)
        {
            AnimatedSprite walk = new AnimatedSprite(true);
            AnimatedSprite idle = new AnimatedSprite(true);
            AnimatedSprite attack = new AnimatedSprite(true);
            AnimatedSprite death = new AnimatedSprite(false);
            for (int i = 1; i < 11; i++)
                walk.addFrame(new AnimatedSpriteFrame(maker.getSprite("Walk ("+i+")"), 50));
            for (int i = 1; i < 11; i++)
                idle.addFrame(new AnimatedSpriteFrame(maker.getSprite("Idle (" + i + ")"), 50));
            for (int i = 1; i < 11; i++)
                attack.addFrame(new AnimatedSpriteFrame(maker.getSprite("Attack (" + i + ")"), 50));
            for (int i = 1; i < 11; i++)
                death.addFrame(new AnimatedSpriteFrame(maker.getSprite("Dead (" + i + ")"), 100));

            death.spriteOnStop = 9;
            MeleeStudent unit = new MeleeStudent();
            unit._walk = walk;
            unit._idle = idle;
            unit._attack = attack;
            unit._death = death;
            unit._state = UnitState.WALKING;
            return unit;
        }

        public override Rectangle DestinationRectangle { get => new Rectangle(Position.ToPoint(), new Point((int)Width, (int)Height)); }
        private Vector2 speed = new Vector2(120, 0);
        public MeleeStudent()
        {
            Health = 5;
            Width = 118 * 2;
            Height = 142 * 2;
            Velocity = speed;
        }

        public void ChangeDirection()
        {
            speed *= -1;
            Velocity = speed;

            if (SpriteEffect == SpriteEffects.FlipHorizontally)
                SpriteEffect = SpriteEffects.None;
            else 
                SpriteEffect = SpriteEffects.FlipHorizontally;
        }

        private IUnit collidedWith;
        public override void collidedWithItem(object o)
        {
            if (_state == UnitState.ATTACKING)
                return;

            if(o is IUnit unit)
            {
                if (unit.Owner == Owner)
                {
                    if (!((Position.X - unit.Position.X) * speed.X < 0))
                        return;

                    if (unit.State != UnitState.WALKING)
                    {
                        collidedWith = unit;
                        _state = UnitState.IDLE;
                        Velocity = Vector2.Zero;
                    }
                }
                else if (!unit.Dead)
                {
                    collidedWith = unit;
                    _state = UnitState.ATTACKING;
                    Velocity = Vector2.Zero;
                }
            }
        }

        public override bool collidingWithItem(object o)
        {
            if (Dead)
                return false;
            return true;
        }

        public void updateWithGameTime(GameTime gameTime)
        {
            if (Dead && _state != UnitState.DYING)
            {
                _state = UnitState.DYING;
                Velocity = Vector2.Zero;
                Position += new Vector2(0, _attack.getFirstFrame().sourceRectangle.Height - _death.getFirstFrame().sourceRectangle.Height);
            }
            else if (_state == UnitState.ATTACKING)
            {
                Velocity = Vector2.Zero;
                collidedWith.Health -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (collidedWith.Dead)
                {
                    Owner.Money += collidedWith.KillReward;
                    _state = UnitState.WALKING;
                    Velocity = speed;
                    collidedWith = null;
                }
            }
            else if (_state == UnitState.IDLE)
            {
                Velocity = Vector2.Zero;
                if (collidedWith == null || collidedWith.State == UnitState.WALKING || collidedWith.Dead)
                {
                    _state = UnitState.WALKING;
                    Velocity = speed;
                    Mass = 1;
                }
            }
        }
    }
}
