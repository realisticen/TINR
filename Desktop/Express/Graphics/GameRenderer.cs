using Desktop.Express.Graphics;
using Desktop.Express.Scene;
using Desktop.Express.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TINRGame.BasicClasses;

namespace TINRGame.Components
{
    class GameRenderer : DrawableGameComponent
    {
        public BlendState blendState = BlendState.Additive;
        public DepthStencilState depthStencilState = DepthStencilState.Default;
        public RasterizerState rasterizerState = RasterizerState.CullClockwise;
        public Effect effect;
        public Camera2D camera;

        IScene scene;
        SpriteBatch spriteBatch;
        public GameRenderer(Game game, IScene scene, Camera2D camera) : base(game)
        {
            this.scene = scene;
            this.camera = camera;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix:camera.ViewMatrix);
            foreach (object o in scene.sceneObjects)
            {
                if (o is IDrawableSceneObject obj)
                    if (obj.Visible)
                    {
                        spriteBatch.Draw(obj.Sprite.texture, 
                            obj.DestinationRectangle, 
                            obj.Sprite.sourceRectangle,
                            obj.Color,
                            obj.Roataion,
                            obj.Sprite.origin,
                            obj.SpriteEffect,
                            obj.LayerDepth);
                    }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
