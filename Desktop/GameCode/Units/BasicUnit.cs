using Desktop.Express.Graphics;
using Desktop.Express.Scene.Objects;
using Desktop.GameCode.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TINRGame;

namespace Desktop.GameCode.Units
{
    class BasicUnit : IUnit, IMass, IDrawableSceneObject, IMovable, IAARectangleCollider, ICustomCollider, IAnimatedSceneObject
    {
        public PlayerBase Owner { get; set; }
        public float Health { get; set; }
        public bool Dead { get => Health <= 0; }
        public bool Remove { get => _death.Finished; }

        public int UnitSpawnDelay { get; set; } = 2000;
        public static int UnitCost { get; set; } = 30;
        public virtual int KillReward => 40;

        public UnitState State => _state;
        protected UnitState _state;
        protected AnimatedSprite _walk;
        protected AnimatedSprite _attack;
        protected AnimatedSprite _idle;
        protected AnimatedSprite _death;

        public virtual Sprite Sprite { get; set; }
        public bool Visible { get; set; } = true;
        public virtual Rectangle DestinationRectangle { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Width { get; set; } = 100;
        public float Height { get; set; } = 100;
        public float Roataion { get; set; } = 0;
        public Color Color { get; set; } = Color.White;
        public float Scale { get; set; } = 1;
        public SpriteEffects SpriteEffect { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0;
        public float Mass { get; set; } = 1;


        public virtual void collidedWithItem(object o)
        {
            throw new NotImplementedException();
        }

        public virtual bool collidingWithItem(object o)
        {
            throw new NotImplementedException();
        }

        public void updateAnimationWithGameTime(GameTime gameTime)
        {
            switch (_state)
            {
                default:
                case UnitState.IDLE:
                    Sprite = _idle.getFrame(gameTime);
                    break;
                case UnitState.WALKING:
                    Sprite = _walk.getFrame(gameTime);
                    break;
                case UnitState.ATTACKING:
                    Sprite = _attack.getFrame(gameTime);
                    break;
                case UnitState.DYING:
                    Sprite = _death.getFrame(gameTime);
                    break;
            }
        }
    }

    enum UnitState
    {
        IDLE,
        WALKING,
        ATTACKING,
        DYING
    }
}
