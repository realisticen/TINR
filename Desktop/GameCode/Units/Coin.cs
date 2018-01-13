using Desktop.Express.Scene.Objects;
using Desktop.GameCode.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TINRGame;
using Microsoft.Xna.Framework.Graphics;

namespace Desktop.GameCode.Units
{
    class Coin : AnimatedDrawable, IMovable
    {
        public static Coin GetCoin(SpriteMaker maker, Point pos, bool loop = false)
        {
            AnimatedSprite animatedSprite = new AnimatedSprite(false);
            for (int i = 1; i < 10; i++)
            {
                animatedSprite.addFrame(new AnimatedSpriteFrame(maker.getSprite("goldCoin"+i), 80));
            }
            animatedSprite.spriteOnStop = 4;
            animatedSprite.Loop = loop;
            return new Coin(animatedSprite, pos);
        }

        public Coin(AnimatedSprite animSprite, Point pos) : base(animSprite, pos)
        {
            const int scale = 2;
            DestinationRectangle = new Rectangle(pos, new Point(32 * scale, 32 * scale));
        }
        
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
    }
}
