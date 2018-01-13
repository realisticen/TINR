using Desktop.Express.Graphics;
using Desktop.Express.Scene;
using Desktop.GameCode.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.GameCode.Components
{
    class GUIRender : DrawableGameComponent
    {
        public BlendState blendState = BlendState.Additive;
        public DepthStencilState depthStencilState = DepthStencilState.Default;
        public RasterizerState rasterizerState = RasterizerState.CullClockwise;
        public Effect effect;

        IScene scene;
        SpriteBatch spriteBatch;
        public Matrix ViewMatrix;
        public GUIRender(Game game, IScene scene, Camera2D camera) : base(game)
        {
            this.scene = scene;
            RecalibrateWithCamera(camera);

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public void RecalibrateWithCamera(Camera2D camera)
        {
            Vector2 pos = camera.Position;
            camera.Position = Vector2.Zero;
            ViewMatrix = camera.ViewMatrix;
            camera.Position = pos;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: ViewMatrix);
            foreach (object o in scene.sceneObjects)
            {
                if (o is Label label)
                    {
                        spriteBatch.DrawString(
                            label.Font,
                            label.Text,
                            label.Position,
                            label.TextColor,
                            label.Roataion,
                            Vector2.Zero,
                            label.Scale,
                            label.SpriteEffect,
                            label.LayerDepth
                            );
                    }
                else if(o is Button button)
                {
                    spriteBatch.Draw(button.Texture, button.DestinationRectangle, Color.White);
                    label = button.Label;
                    spriteBatch.DrawString(
                            label.Font,
                            label.Text,
                            label.Position,
                            label.TextColor,
                            label.Roataion,
                            Vector2.Zero,
                            label.Scale,
                            label.SpriteEffect,
                            label.LayerDepth
                            );
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
