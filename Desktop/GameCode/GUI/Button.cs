using Desktop.Express.Graphics;
using Desktop.Express.Scene.Objects;
using Desktop.GameCode.Components;
using Desktop.GameCode.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.GameCode.GUI
{
    class Button : IAARectangleCollider, ICustomCollider
    {
        public Label Label;

        public Vector2 Position { get; set; }
        public Point Size;
        public Rectangle DestinationRectangle { get => new Rectangle(Position.ToPoint(), Size); }
        public float Width { get => Size.X; set => Size.X = (int)value; }
        public float Height { get => Size.Y; set => Size.Y = (int)value; }

        public Texture2D Texture;

        public Button(string text, Vector2 pos, SpriteFont font, Color color)
        {
            Position = pos;
            Texture = TextureLoader.CreateColoredTexture(color);
            Label = new Label(text, pos, Color.White, font);
            Size = font.MeasureString(text).ToPoint();
            Size += new Point(20, 5);
            Size = (Size.ToVector2() *  Label.Scale).ToPoint();
            Label.Position += new Vector2(20, 5);
        }

        public event EventHandler<EventArgs> Clicked;
        public bool collidingWithItem(object o)
        {
            if(o is MouseParticle)
            {
                Clicked?.Invoke(this, new EventArgs());
            }

            return false;
        }

        public void collidedWithItem(object o)
        {
            throw new NotImplementedException();
        }
    }
}
