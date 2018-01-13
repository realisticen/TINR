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
                if (obj is ICustomUpdate updatable)
                    updatable.updateWithGameTime(gameTime);
            }

            for (int i = 0; i < scene.sceneObjects.Count + 1; i++)
            {
                for (int j = i+1; j < scene.sceneObjects.Count; j++)
                {
                    Collision.collisionBetween(scene.sceneObjects[i], scene.sceneObjects[j]);
                }
            }

            base.Update(gameTime);
        }
    }
}
