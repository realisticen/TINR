using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINRGame.Components
{
    interface IPlayerInput
    {
        bool playerMousePress();
        Vector2 mousePosition { get; }
        bool[] events { get; }
        void Update(GameTime gameTime);
    }

    public enum PlayerInputEvent
    {
        MAIN_SCROLL_RIGHT,
        MAIN_SCROLL_LEFT,
        UNITS_SCROLL_RIGHT,
        UNITS_SCROLL_LEFT
    }
}
