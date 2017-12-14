using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Scene.Objects
{
    interface ICustomUpdate
    {
        void updateWithGameTime(GameTime gameTime);
    }
}
