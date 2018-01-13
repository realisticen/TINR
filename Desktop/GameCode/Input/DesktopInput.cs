using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TINRGame.Components;
using Microsoft.Xna.Framework.Input;

namespace TINRGame.BasicClasses
{
    class DesktopInput : IUserInput
    {
        public Point mousePosition => Mouse.GetState(Window).Position;
        public bool MousePressed => Mouse.GetState(Window).LeftButton == ButtonState.Pressed;

        public GameWindow Window;

        public DesktopInput(GameWindow window)
        {
            Window = window;
        }
    }
}
