using Desktop.Express.Graphics;
using Desktop.Express.Scene;
using Desktop.GameCode.AI;
using Desktop.GameCode.Components;
using Desktop.GameCode.GUI;
using Desktop.GameCode.Levels;
using Desktop.GameCode.Units;
using Desktop.GameCode.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TINRGame.Components;
using Units;

namespace Desktop.GameCode
{
    class GamePlay : GameComponent
    {
        Vector2 targetResolution = new Vector2(1366, 768);

        IScene gameScene;
        IUserInput _userInput;
        SpriteFont GUIFont;

        // COMPONENTS
        GameRenderer gameRenderer;
        Animator animator;
        DebugRenderer debugRenderer;
        Camera2D camera;
        PhysicsBasicEngine phys;
        GUILogic gui;

        Level level;

        public GamePlay(Game game, IUserInput userInput, Level level) : base(game)
        {
            this.level = level;
            _userInput = userInput;
            gameScene = new SimpleScene(game);
        }

        public override void Initialize()
        {
            GUIFont = Game.Content.Load<SpriteFont>("font");

            camera = new Camera2D(Game.GraphicsDevice.Viewport);
            camera.Stretch = new Vector2(
                    Game.GraphicsDevice.Viewport.Width / targetResolution.X,
                    Game.GraphicsDevice.Viewport.Height / targetResolution.Y
                );

            gameRenderer = new GameRenderer(Game, gameScene, camera);
            animator = new Animator(Game, gameScene);
            phys = new PhysicsBasicEngine(Game, gameScene);
            gui = new GUILogic(Game, camera, _userInput);
            debugRenderer = new DebugRenderer(Game, gameScene, camera);
            debugRenderer.Visible = false;

            Game.Components.Add(phys);
            Game.Components.Add(animator);
            Game.Components.Add(gameRenderer);
            Game.Components.Add(gui);
            Game.Components.Add(debugRenderer);

            level.InitLevel(gameScene);
            createGameGui();

            UnitSpawner.Scene = gameScene;

            CopyAI ai = new CopyAI(gameScene, level.player2Base);

            base.Initialize();
        }

        Coin userCoin;
        Label moneyLabel;
        private void createGameGui()
        {
            //userCoin = Coin.GetCoin(UnitSpawner.Maker, new Point(40, 8), true);
            //gameScene.addItem(userCoin);
            var melee = new Button("Melee", new Vector2(20, 20), GUIFont, Color.Brown);
            melee.Clicked += Melee_Clicked;
            gui.Scene.addItem(melee);
            moneyLabel = new Label("", new Vector2(20, targetResolution.Y - 60), Color.White, GUIFont);
            gui.Scene.addItem(moneyLabel);
        }

        private void Melee_Clicked(object sender, EventArgs e)
        {
            UnitSpawner.SpawnMeleeUnit(level.player1Base);
        }

        public override void Update(GameTime gameTime)
        {
            moneyLabel.Text = "Money: " + level.player1Base.Money;
            removeObjectsFromScene();
            ReadDebugKeyboard();
            UnitSpawner.Update(gameTime);
            base.Update(gameTime);
        }

        List<object> removeList = new List<object>(10);
        private void removeObjectsFromScene()
        {
            removeList.Clear();
            foreach (var item in gameScene.sceneObjects)
            {
                if (item is IUnit unit)
                    if (unit.Remove)
                        removeList.Add(unit);
            }

            foreach (var item in removeList)
            {
                SoundEngine.PlaySoundEffect(SoundEffectType.COIN_DROP);
                gameScene.removeItem(item);
            }
        }
        private void ReadDebugKeyboard()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                camera.Position += new Vector2(20, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                camera.Position += new Vector2(-20, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                camera.Position += new Vector2(0, 20);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                camera.Position += new Vector2(0, -20);

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
            Game.Components.Remove(animator);
            Game.Components.Remove(gui);
            Game.Components.Remove(phys);
            level.Dispose();
            base.Dispose(disposing);
        }
    }
}
