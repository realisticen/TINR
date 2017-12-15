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
    class DebugRenderer : DrawableGameComponent
    {
        public Color itemColor;
        public Color movmentColor;
        public Color colliderColor;

        public BlendState blendState = BlendState.Additive;
        public DepthStencilState depthStencilState = DepthStencilState.Default;
        public RasterizerState rasterizerState = RasterizerState.CullClockwise;
        public Effect effect;
        public Camera2D camera;

        IScene scene;
        SpriteBatch spriteBatch;
        public DebugRenderer(Game game, IScene scene, Camera2D camera) : base(game)
        {
            this.scene = scene;
            this.camera = camera;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            itemColor = Color.White;
            movmentColor = Color.SkyBlue;
            colliderColor = Color.Lime;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: camera.ViewMatrix);
            foreach (object o in scene.sceneObjects)
            {
                if (o is IParticleCollider circle)
                    Primitives2D.DrawCircle(spriteBatch, circle.Position, circle.Radius, 20, itemColor);
                else if (o is IRectangleCollider rect)
                    Primitives2D.DrawRectangle(spriteBatch, new Rectangle(rect.Position.ToPoint(), new Point((int)rect.Width, (int)rect.Height)), itemColor);
                else if (o is IAARectangleCollider rect2)
                    Primitives2D.DrawRectangle(spriteBatch, new Rectangle(rect2.Position.ToPoint(), new Point((int)rect2.Width, (int)rect2.Height)), itemColor);
                else if (o is IMovable moving)
                    Primitives2D.DrawLine(spriteBatch, moving.Position, moving.Position + moving.Velocity, movmentColor);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
