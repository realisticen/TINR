using Desktop.Express.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINRGame
{
    class AnimatedSpriteFrame
    {
        public Sprite Sprite;

        public int duration { get; set; }
        public int startTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sprite">Picture to draw</param>
        /// <param name="_startTime">When it starts in miliseconds</param>
        public AnimatedSpriteFrame(Sprite _sprite, int _duration)
        {
            Sprite = _sprite;
            duration = _duration;
        }
    }
}
