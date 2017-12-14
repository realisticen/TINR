using Desktop.Express.Graphics;
using Desktop.Express.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;

namespace TINRGame.Components
{
    class Level1 : GameComponent
    {
        IScene scene;
        GameRenderer gameRenderer;
        DebugRenderer debugRenderer;
        PhysicsBasicEngine phys;
        IPlayerInput playerInput;
        public Level1(Game game, IPlayerInput _playerInput) : base(game)
        {
            playerInput = _playerInput;
            scene = new SimpleScene(game);
        }

        public override void Initialize()
        {
            gameRenderer = new GameRenderer(Game, scene);
            phys = new PhysicsBasicEngine(Game, scene);
            debugRenderer = new DebugRenderer(Game, scene);
            debugRenderer.Visible = false;

            Game.Components.Add(debugRenderer);
            Game.Components.Add(gameRenderer);
            Game.Components.Add(phys);

            var testTexture = Game.Content.Load<Texture2D>("test");
            Sprite background = new Sprite(
                testTexture,
                testTexture.Bounds
                );
            var testSTudent = new MeleeStudent(background);
            var testSTudent2 = new MeleeStudent(background);
            testSTudent2.Velocity = new Vector2(-100, 0);
            testSTudent2.Position += new Vector2(500, 0); 
            scene.addItem(testSTudent);
            scene.addItem(testSTudent2);
            //AnimatedSprite testAnimation = new AnimatedSprite(true);
            //var frametime = 40;
            //for (int i = 1; i < 11; i++)
            //{
            //    var texture = Game.Content.Load<Texture2D>(string.Format("test/Attack ({0})", i));
            //    var sprite = new Sprite(texture, texture.Bounds, new Rectangle(0,0, 100, 160));
            //    var frame = new AnimatedSpriteFrame(sprite, frametime);
            //    testAnimation.addFrame(frame);
            //}
            //BasicUnit basicUnit = new BasicUnit(testAnimation);
            //basicUnit.Acceleration = new Vector2(0, 0);
            //scene.addObject(basicUnit);

            //basicUnit = new BasicUnit(testAnimation);
            //basicUnit.Postion = new Vector2(200, 0);
            //basicUnit.Acceleration = new Vector2(0, 0);
            //scene.addObject(basicUnit);

            //var backgroundTexture = Game.Content.Load<Texture2D>("background");
            //Sprite background = new Sprite(
            //    backgroundTexture,
            //    backgroundTexture.Bounds
            //    );
            base.Initialize();
        }

        bool wait = false;
        public override void Update(GameTime gameTime)
        {
            if (!wait && Keyboard.GetState().IsKeyDown(Keys.D))
            {
                debugRenderer.Visible = !debugRenderer.Visible;
                wait = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.D))
                wait = false;

            base.Update(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            Game.Components.Remove(gameRenderer);
            // TODO: Odstrani vse
            base.Dispose(disposing);
        }
    }
}
