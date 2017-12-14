using Desktop.Express.Physics;
using Desktop.Express.Scene;
using Desktop.Express.Scene.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TINRGame.BasicClasses;

namespace TINRGame.Components
{
    class PhysicsBasicEngine : GameComponent
    {
        IScene scene;

        public PhysicsBasicEngine(Game game, IScene _scene) : base(game)
        {
            scene = _scene;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (object obj in scene.sceneObjects)
            {
                if (obj is IMovable movable)
                    MovementPhysics.simulateMovement(movable, gameTime);
                if (obj is IRotatable rotatable)
                    MovementPhysics.simulateMovement(rotatable, gameTime);
            }

            for (int i = 1; i < scene.sceneObjects.Count; i++)
            {
                Collision.collisionBetween(scene.sceneObjects[i-1], scene.sceneObjects[i]);
            }

            base.Update(gameTime);
        }
    }
}
