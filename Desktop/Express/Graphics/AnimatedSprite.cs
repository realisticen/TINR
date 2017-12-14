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
        public int duration { get; private set; }

        List<AnimatedSpriteFrame> frames = new List<AnimatedSpriteFrame>(3);

        int lastFrame = 0;
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
                    return frames[frames.Count - 1].Sprite;
            }

            for (int i = lastFrame; i < frames.Count; i++)
            {
                if(frames[i].startTime > runTime)
                {
                    lastFrame = i;
                    Debug.WriteLine("Last frame: " + i);
                    return frames[i].Sprite;
                }
            }

            return frames[0].Sprite;
        }
    }
}
