using Desktop.Express.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.GameCode.Util
{
    class SpriteMaker
    {
        private Texture2D _atlas;
        private Dictionary<string, Rectangle> _positions;

        public SpriteMaker(Texture2D atlas, Dictionary<string, Rectangle> positions)
        {
            _atlas = atlas;
            _positions = positions;
        }

        public Sprite getSprite(string name)
        {
            return new Sprite(_atlas, _positions[name]);
        }
    }
}
