using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Scene.Objects
{
    class Lifetime
    {
        TimeSpan _start, _dutation, _progress;

        public bool IsAlive
        { get => _progress < _dutation; }

        public float Percentage
        { get => (float)(_progress.TotalMilliseconds/_dutation.TotalMilliseconds); }


        public Lifetime(TimeSpan start, TimeSpan duration)
        {
            _start = start;
            _dutation = duration;
            _progress = new TimeSpan();
        }

        public void updateWithGameTime(GameTime gameTime)
        {
            if(IsAlive)
            {
                _progress += gameTime.ElapsedGameTime;
                if(!IsAlive)
                {
                    _progress = _dutation;
                }
            }
        }
    }
}
