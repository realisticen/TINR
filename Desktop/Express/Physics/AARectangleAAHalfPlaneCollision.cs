using Desktop.Express.Math;
using Desktop.Express.Scene.Objects;
using Microsoft.Xna.Framework;
using System;

namespace Desktop.Express.Physics
{
    class AARectangleAAHalfPlaneCollision : CollisionAlgorithm<IAARectangleCollider, IAAHalfPlaneCollider>
    {
        private static AARectangleAAHalfPlaneCollision _instance = new AARectangleAAHalfPlaneCollision();
        public static AARectangleAAHalfPlaneCollision Instance { get => _instance; }

        public override bool detectCollisionBetween(IAARectangleCollider aaRectangle, IAAHalfPlaneCollider aaHalfPlane)
        {
            switch (aaHalfPlane.AaHalfPlane.direction)
            {
                default:
                case AxisDirection.AxisDirectionPositiveX:
                    return aaRectangle.Position.X - aaRectangle.Width / 2 < aaHalfPlane.AaHalfPlane.distance;
                case AxisDirection.AxisDirectionNegativeX:
                    return aaRectangle.Position.X + aaRectangle.Width / 2 > -aaHalfPlane.AaHalfPlane.distance;
                case AxisDirection.AxisDirectionPositiveY:
                    return aaRectangle.Position.Y - aaRectangle.Height / 2 < aaHalfPlane.AaHalfPlane.distance;
                case AxisDirection.AxisDirectionNegativeY:
                    return aaRectangle.Position.Y + aaRectangle.Height / 2 > -aaHalfPlane.AaHalfPlane.distance;
            }
        }

        public override void resolveCollisionBetween(IAARectangleCollider aaRectangle, IAAHalfPlaneCollider aaHalfPlane)
        {
            Vector2 relaxDistance = Vector2.Zero;
            switch (aaHalfPlane.AaHalfPlane.direction)
            {
                case AxisDirection.AxisDirectionPositiveX:
                    relaxDistance = new Vector2(aaRectangle.Position.X - aaRectangle.Width / 2 - aaHalfPlane.AaHalfPlane.distance ,0);
                    break;
                case AxisDirection.AxisDirectionNegativeX:
                    relaxDistance = new Vector2(aaRectangle.Position.X + aaRectangle.Width / 2 + aaHalfPlane.AaHalfPlane.distance, 0);
                    break;
                case AxisDirection.AxisDirectionPositiveY:
                    relaxDistance = new Vector2(0, y: aaRectangle.Position.Y - aaRectangle.Height / 2 - aaHalfPlane.AaHalfPlane.distance);
                    break;
                case AxisDirection.AxisDirectionNegativeY:
                    relaxDistance = new Vector2(0, y: aaRectangle.Position.Y + aaRectangle.Height / 2 + aaHalfPlane.AaHalfPlane.distance);
                    break;
            }

            Collision.relaxCollisionBetween(aaRectangle, aaHalfPlane, relaxDistance);
	        relaxDistance.Normalize();
            Collision.exchangeEnergyBetween(aaRectangle, aaHalfPlane, relaxDistance, Vector2.Zero);
        }
}
}
