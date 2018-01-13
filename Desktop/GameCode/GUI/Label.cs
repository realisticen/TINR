using Desktop.GameCode.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Express.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desktop.GameCode.GUI
{
    class Label
    {

        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public Vector2 Position;
        public Color TextColor { get; set; } = Color.White;
        public float Roataion { get; set; } = 0;
        public float Scale { get; set; } = 2;
        public SpriteEffects SpriteEffect { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0;

        public Label(string text, Vector2 pos, Color color, SpriteFont font)
        {
            Text = text;
            TextColor = color;
            Position = pos;
            Font = font;
        }
    }
}
