using Desktop.Express.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINRGame
{
    class AnimatedSprite
    {
        public bool Loop { get; set; }
        public bool Finished { get => runTime > duration; }
        public int duration { get; private set; }

        List<AnimatedSpriteFrame> frames = new List<AnimatedSpriteFrame>(3);

        int lastFrame = 0;
        public int spriteOnStop = 0;
        double runTime = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="looping">Should animation repeat</param>
        /// <param name="_duration"> Time in miliseconds</param>
        public AnimatedSprite(bool looping)
        {
            Loop = looping;
            duration = 0;
        }

        public void addFrame(AnimatedSpriteFrame sprite)
        {
            frames.Add(sprite);
            sprite.startTime = duration;
            duration += sprite.duration;
        }

        public Sprite getFirstFrame() { return frames[0].Sprite; }

        public void reset()
        {
            runTime = 0;
        }

        public Sprite getFrame(GameTime gameTime)
        {
            runTime += gameTime.ElapsedGameTime.TotalMilliseconds;


            if (runTime > duration)
            {
                if (Loop)
                {
                    double loops = Math.Floor(runTime / duration);
                    runTime -= loops * duration;
                    lastFrame = 0;
                }
                else
                    return frames[spriteOnStop].Sprite;
            }

            for (int i = lastFrame; i < frames.Count; i++)
            {
                if(frames[i].startTime > runTime)
                {
                    lastFrame = i;
                    return frames[i].Sprite;
                }
            }

            return frames[spriteOnStop].Sprite;
        }
    }
}
