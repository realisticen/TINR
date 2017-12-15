using Desktop.Express.Scene.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Express.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Units
{
    class MeleeStudent : IDrawableSceneObject, IMovable, IAARectangleCollider
    {
        private Sprite _sprite;
        public MeleeStudent(Sprite sprite)
        {
            _sprite = sprite;
            this.Velocity = new Vector2(100, 0);
            this.Position = new Vector2(100, 100);
            this.Visible = true;
            this.Height = (_sprite.sourceRectangle.Size.ToVector2() / 4).Y;
            this.Width = (_sprite.sourceRectangle.Size.ToVector2() / 4).X;
        }

        public Sprite Sprite => _sprite;

        public bool Visible { get; set; }

        public Rectangle DestinationRectangle => new Rectangle(Position.ToPoint(), (_sprite.sourceRectangle.Size.ToVector2() / 4).ToPoint());

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Roataion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Scale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SpriteEffects SpriteEffect { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float LayerDepth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
