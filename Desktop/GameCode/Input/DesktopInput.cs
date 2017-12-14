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
    class DesktopInput : IPlayerInput
    {
        bool[] IPlayerInput.events => _events;

        public Vector2 mousePosition => Mouse.GetState(game.Window).Position.ToVector2();

        private bool[] _events = new bool[Enum.GetNames(typeof(PlayerInputEvent)).Length];

        public bool playerMousePress()
        {
            return Mouse.GetState(game.Window).LeftButton == ButtonState.Pressed;
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        Game game;
        public DesktopInput(Game _game)
        {
            game = _game;
        }
    }
}
