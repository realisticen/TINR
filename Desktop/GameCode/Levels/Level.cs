using Desktop.Express.Graphics;
using Desktop.Express.Scene;
using Desktop.GameCode.Units;
using Desktop.GameCode.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.GameCode.Levels
{
    class Level
    {
        string backgroundFile = "background/background";

        protected TextureLoader textureLoader;
        protected ContentManager content;

        public PlayerBase player1Base, player2Base;

        public Level(Game game)
        {
            content = game.Content;
            textureLoader = new TextureLoader(game.Content);
            LoadTextures();
        }

        private void LoadTextures()
        {
            textureLoader.LoadTexture(backgroundFile, TextureKey.BACKGROUND);
        }

        public virtual void InitLevel(IScene scene)
        {
            scene.addItem(new StaticDrawable(new Sprite(textureLoader.GetTexture(TextureKey.BACKGROUND)), Point.Zero));
        }

        public void Dispose()
        {
            textureLoader.UnloadAll();
        }
    }
}
