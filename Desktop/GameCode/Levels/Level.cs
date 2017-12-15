using Desktop.Express.Graphics;
using Desktop.Express.Scene;
using Desktop.GameCode.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.GameCode.Levels
{
    class Level
    {
        string backgroundFile = "background";

        TextureLoader textureLoader;

        public Level(Game game)
        {
            textureLoader = new TextureLoader(game.Content);
            LoadTextures();
        }

        private void LoadTextures()
        {
            textureLoader.LoadTexture(backgroundFile, "background");
        }

        public void InitLevel(IScene scene)
        {
            scene.addItem(new StaticDrawable(new Sprite(textureLoader.GetTexture("background")), Point.Zero));
        }

        public void Dispose()
        {
            textureLoader.UnloadAll();
        }
    }
}
