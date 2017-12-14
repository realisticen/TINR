using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Graphics
{
    class Sprite
    {
        public Texture2D texture;
        public Rectangle sourceRectangle;
        public Vector2 origin;

        public Sprite(Texture2D _texture) : this(_texture, _texture.Bounds)
        {
        }

        public Sprite(Texture2D _texture, Rectangle _sourceRectangle) : this(_texture, _sourceRectangle, Vector2.Zero)
        {
        }

        public Sprite(Texture2D _texture, Rectangle _sourceRectangle, Vector2 _origin)
        {
            texture = _texture;
            sourceRectangle = _sourceRectangle;
            origin = _origin;
        }
    }
}
