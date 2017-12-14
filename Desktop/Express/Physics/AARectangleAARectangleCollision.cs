using Desktop.Express.Scene.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Physics
{
    class AARectangleAARectangleCollision : CollisionAlgorithm<IAARectangleCollider, IAARectangleCollider>
    {
        private static AARectangleAARectangleCollision _instance = new AARectangleAARectangleCollision();
        public static AARectangleAARectangleCollision Instance { get => _instance; }

        public override bool detectCollisionBetween(IAARectangleCollider aaRectangle1, IAARectangleCollider aaRectangle2)
        {
            float horizontalDistancle = System.Math.Abs(aaRectangle1.Position.X - aaRectangle2.Position.X);
            float verticalDistancle = System.Math.Abs(aaRectangle1.Position.Y - aaRectangle2.Position.Y);
            return horizontalDistancle < aaRectangle1.Width / 2 + aaRectangle2.Width / 2 && verticalDistancle < aaRectangle1.Height / 2 + aaRectangle2.Height / 2;
        }

        public override void resolveCollisionBetween(IAARectangleCollider aaRectangle1, IAARectangleCollider aaRectangle2)
        {
            // RELAXATION STEP
            // First we relax the collision, so the two objects don't collide any more.
            // We need to calculate by how much to move them apart. We will move them in the shortest direction
            // possible which could be either horizontal or vertical.
            float horizontalDifference = aaRectangle1.Position.X - aaRectangle2.Position.X;
            float horizontalCollidedDistance = System.Math.Abs(horizontalDifference);
            float horizontalMinimumDistance = aaRectangle1.Width / 2 + aaRectangle2.Width / 2;
            float horizontalRelaxDistance = horizontalMinimumDistance - horizontalCollidedDistance;

            float verticalDifference = aaRectangle1.Position.Y - aaRectangle2.Position.Y;
            float verticalCollidedDistance = System.Math.Abs(verticalDifference);
            float verticalMinimumDistance = aaRectangle1.Height / 2 + aaRectangle2.Height / 2;
            float verticalRelaxDistance = verticalMinimumDistance - verticalCollidedDistance;

            Vector2 collisionNormal;
            float relaxDistance;

            if (horizontalRelaxDistance < verticalRelaxDistance)
            {
                relaxDistance = horizontalRelaxDistance;
                collisionNormal = new Vector2 (horizontalDifference < 0 ? 1 : -1, 0);
            }
            else
            {
                relaxDistance = verticalRelaxDistance;
                collisionNormal = new Vector2(0,verticalDifference < 0 ? 1 : -1);
            }

            Vector2 relaxDistanceVector = collisionNormal * relaxDistance;

            Collision.relaxCollisionBetween(aaRectangle1, aaRectangle2, relaxDistanceVector);
	
	        // ENERGY EXCHANGE STEP
	        // In a collision, energy is exchanged only along the collision normal.
	        // For particles this is simply the line between both centers.
	        Collision.exchangeEnergyBetween(aaRectangle1, aaRectangle2, collisionNormal, Vector2.Zero);
        }
    }
}
