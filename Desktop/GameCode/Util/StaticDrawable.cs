using Desktop.Express.Scene.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Express.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desktop.GameCode.Util
{
    class StaticDrawable : IDrawableSceneObject
    {
        public bool Visible { get; set; } = true;
        public float Roataion { get; set; } = 0;
        public Color Color { get; set; } = Color.White;
        public float Scale { get; set; } = 1;
        public SpriteEffects SpriteEffect { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0;

        public Sprite Sprite { get; set; }
        public Rectangle DestinationRectangle { get; set; }

        public StaticDrawable(Sprite sprite, Point pos)
        {
            Sprite = sprite;
            DestinationRectangle = new Rectangle(pos, sprite.sourceRectangle.Size);
        }
    }
}
