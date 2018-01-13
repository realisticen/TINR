using Desktop.Express.Graphics;
using Desktop.Express.Scene;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TINRGame.Components;

namespace Desktop.GameCode.Components
{
    class GUILogic : GameComponent
    {
        public IScene Scene { get => _scene; }
        private IScene _scene;
        public GUIRender render;
        private PhysicsBasicEngine physicsBasic;
        private IUserInput _userInput;

        private MouseParticle _mouseParticle;
        public GUILogic(Game game, Camera2D camera, IUserInput userInput) : base(game)
        {
            _scene = new SimpleScene(game);
            _userInput = userInput;
            render = new GUIRender(game, _scene, camera);
            physicsBasic = new PhysicsBasicEngine(game, _scene);
            _mouseParticle = new MouseParticle();
            _scene.addItem(_mouseParticle);
        }

        public override void Initialize()
        {
            Game.Components.Add(render);
            Game.Components.Add(physicsBasic);
            base.Initialize();
        }

        bool released = false;
        public override void Update(GameTime gameTime)
        {
            if (_userInput.MousePressed && released)
            {
                _mouseParticle.Position = Camera2D.ToCamera(_userInput.mousePosition, render.ViewMatrix, Vector2.Zero).ToVector2();
                released = false;
            }
            else
            {
                if(!_userInput.MousePressed)
                    released = true;
                _mouseParticle.Position = Vector2.Zero;
            }
            base.Update(gameTime);
        }

        protected override void OnEnabledChanged(object sender, EventArgs args)
        {
            render.Enabled = Enabled;
            physicsBasic.Enabled = Enabled;
            base.OnEnabledChanged(sender, args);
        }

        protected override void Dispose(bool disposing)
        {
            if (Game.Components != null)
            {
                Game.Components.Remove(render);
                Game.Components.Remove(physicsBasic);
            }
            base.Dispose(disposing);
        }
    }
}
