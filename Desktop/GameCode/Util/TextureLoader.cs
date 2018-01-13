using Microsoft.Xna.Framework;
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
        private Dictionary<TextureKey, Texture2D> textures = new Dictionary<TextureKey, Texture2D>();
        private ContentManager _contentManager;
        public static GraphicsDevice Device;
        public TextureLoader(ContentManager contentmng)
        {
            _contentManager = contentmng;
        }

        public Texture2D GetTexture(TextureKey key)
        {
            return textures[key];
        }

        public void LoadTexture(string filename, TextureKey key)
        {
            Texture2D texture = _contentManager.Load<Texture2D>(filename);
            textures.Add(key, texture);
        }

        public void UnloadTexture(TextureKey key)
        {
            textures.Remove(key);
        }

        public void UnloadAll()
        {
            textures.Clear();
        }

        public static Texture2D CreateColoredTexture(Color paint)
        {
            //initialize a texture
            Texture2D texture = new Texture2D(Device, 1, 1);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[1 * 1];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = paint;
            }

            //set the color
            texture.SetData(data);
            return texture;
        }
    }

    enum TextureKey
    {
        BACKGROUND,
        ATLAS
    }
}
