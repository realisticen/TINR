using Desktop.Express.Scene.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Express.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TINRGame;

namespace Desktop.GameCode.Util
{
    class AnimatedDrawable : IAnimatedSceneObject
    {
        public bool Visible { get; set; } = true;
        public float Roataion { get; set; } = 0;
        public Color Color { get; set; } = Color.White;
        public float Scale { get; set; } = 1;
        public SpriteEffects SpriteEffect { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0;

        public Sprite Sprite { get; set; }
        private AnimatedSprite animatedSprite { get; set; }
        public Rectangle DestinationRectangle { get; set; }

        public AnimatedDrawable(AnimatedSprite animSprite, Point pos)
        {
            animatedSprite = animSprite;
            Sprite = animatedSprite.getFirstFrame();
            DestinationRectangle = new Rectangle(pos, Sprite.sourceRectangle.Size);
        }

        public void updateAnimationWithGameTime(GameTime gameTime)
        {
            Sprite = animatedSprite.getFrame(gameTime);
        }
    }
}
