using Desktop.GameCode.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Express.Graphics;

namespace Desktop.GameCode.Units
{
    class PlayerBase : StaticDrawable
    {
        public int Money;

        public PlayerBase(Sprite sprite, Point pos) : base(sprite, pos)
        {
        }
    }
}
