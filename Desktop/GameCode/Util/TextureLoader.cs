using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.GameCode.Util
{
    class TextureLoader
    {
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private ContentManager _contentManager;

        public TextureLoader(ContentManager contentmng)
        {
            _contentManager = contentmng;
        }

        public Texture2D GetTexture(string name)
        {
            return textures[name];
        }

        public void LoadTexture(string filename, string key)
        {
            Texture2D texture = _contentManager.Load<Texture2D>(filename);
            textures.Add(key, texture);
        }

        public void UnloadTexture(string key)
        {
            textures.Remove(key);
        }

        public void UnloadAll()
        {
            textures.Clear();
        }
    }
}
