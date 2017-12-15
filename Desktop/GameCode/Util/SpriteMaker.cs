using Desktop.Express.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.GameCode.Util
{
    static class SpriteMaker
    {
        public static Sprite CreateBasicSprite(Texture2D tex)
        {
            Sprite sprite = new Sprite(tex);

            return sprite;
        }
    }
}
