using Desktop.Express.Graphics;
using Desktop.Express.Scene;
using Desktop.GameCode.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TINRGame.Components;

namespace Desktop.GameCode
{
    class GamePlay : GameComponent
    {
        Vector2 targetResolution = new Vector2(1366, 768);

        IScene scene;
        IPlayerInput playerInput;

        GameRenderer gameRenderer;
        DebugRenderer debugRenderer;
        Camera2D camera;
        PhysicsBasicEngine phys;
        Level level;

        public GamePlay(Game game, IPlayerInput _playerInput, Level level) : base(game)
        {
            this.level = level;
            playerInput = _playerInput;
            scene = new SimpleScene(game);
        }

        public override void Initialize()
        {
            camera = new Camera2D(Game.GraphicsDevice.Viewport);
            // TODO: Stretch to fit only y axsis, then left and right scroll
            camera.Stretch = new Vector2(
                    Game.GraphicsDevice.Viewport.Width / targetResolution.X,
                    Game.GraphicsDevice.Viewport.Height / targetResolution.Y
                );
            gameRenderer = new GameRenderer(Game, scene, camera);
            phys = new PhysicsBasicEngine(Game, scene);
            debugRenderer = new DebugRenderer(Game, scene, camera);
            debugRenderer.Visible = false;

            Game.Components.Add(debugRenderer);
            Game.Components.Add(gameRenderer);
            Game.Components.Add(phys);

            level.InitLevel(scene);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            ReadDebugKeyboard();
            base.Update(gameTime);
        }

        private void ReadDebugKeyboard()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                camera.Position += new Vector2(1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                camera.Position += new Vector2(-1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                camera.Position += new Vector2(0, 1);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                camera.Position += new Vector2(0, -1);

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1))
                camera.Rotation += 0.03f;
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
                camera.Rotation -= 0.03f;

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
                camera.Stretch += new Vector2(0.01f, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad5))
                camera.Stretch -= new Vector2(0.01f, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad7))
                camera.Stretch += new Vector2(0, 0.01f);
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad8))
                camera.Stretch -= new Vector2(0, 0.01f);


            if (Keyboard.GetState().IsKeyDown(Keys.NumPad3))
                debugRenderer.Visible = true;
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad6))
                debugRenderer.Visible = false;
        }

        protected override void Dispose(bool disposing)
        {
            Game.Components.Remove(gameRenderer);
            Game.Components.Remove(debugRenderer);
            Game.Components.Remove(gameRenderer);
            Game.Components.Remove(phys);
            level.Dispose();
            base.Dispose(disposing);
        }
    }
}
