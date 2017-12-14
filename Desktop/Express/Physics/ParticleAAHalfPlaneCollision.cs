using Desktop.Express.Math;
using Desktop.Express.Scene.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Physics
{
    class ParticleAAHalfPlaneCollision : CollisionAlgorithm<IParticleCollider, IAAHalfPlaneCollider>
    {
        private static ParticleAAHalfPlaneCollision _instance = new ParticleAAHalfPlaneCollision();
        public static ParticleAAHalfPlaneCollision Instance { get => _instance; }

        public override bool detectCollisionBetween(IParticleCollider particle, IAAHalfPlaneCollider aaHalfPlane)
        {
            switch (aaHalfPlane.AaHalfPlane.direction)
            {
                default:
                case AxisDirection.AxisDirectionPositiveX:
                    return particle.Position.X - particle.Radius < aaHalfPlane.AaHalfPlane.distance;
                case AxisDirection.AxisDirectionNegativeX:
                    return particle.Position.X + particle.Radius > -aaHalfPlane.AaHalfPlane.distance;
                case AxisDirection.AxisDirectionPositiveY:
                    return particle.Position.Y - particle.Radius < aaHalfPlane.AaHalfPlane.distance;
                case AxisDirection.AxisDirectionNegativeY:
                    return particle.Position.Y + particle.Radius > -aaHalfPlane.AaHalfPlane.distance;
            }
        }

        public override void resolveCollisionBetween(IParticleCollider particle, IAAHalfPlaneCollider aaHalfPlane)
        {
            // RELAXATION STEP

            // First we relax the collision, so the two objects don't collide any more.
            Vector2 relaxDistance = Vector2.Zero;
            Vector2 pointOfImpact = Vector2.Zero;
            switch (aaHalfPlane.AaHalfPlane.direction)
            {
                case AxisDirection.AxisDirectionPositiveX:
                    relaxDistance = new Vector2 (particle.Position.X - particle.Radius - aaHalfPlane.AaHalfPlane.distance, 0);
                    pointOfImpact = new Vector2 (aaHalfPlane.AaHalfPlane.distance, particle.Position.Y);
                    break;
                case AxisDirection.AxisDirectionNegativeX:
                    relaxDistance = new Vector2(particle.Position.X + particle.Radius + aaHalfPlane.AaHalfPlane.distance, 0);
                    pointOfImpact = new Vector2(-aaHalfPlane.AaHalfPlane.distance, particle.Position.Y);
                    break;
                case AxisDirection.AxisDirectionPositiveY:
                    relaxDistance = new Vector2(0, y: particle.Position.Y - particle.Radius - aaHalfPlane.AaHalfPlane.distance);
                    pointOfImpact = new Vector2(particle.Position.X, aaHalfPlane.AaHalfPlane.distance);
                    break;
                case AxisDirection.AxisDirectionNegativeY:
                    relaxDistance = new Vector2(0, particle.Position.Y + particle.Radius + aaHalfPlane.AaHalfPlane.distance);
                    pointOfImpact = new Vector2(particle.Position.X, -aaHalfPlane.AaHalfPlane.distance);
                    break;
            }

            Collision.relaxCollisionBetween(particle, aaHalfPlane, relaxDistance);
	
	        // ENERGY EXCHANGE STEP
	
	        // In a collision, energy is exchanged only along the collision normal.
	        // For particles this is simply the line between both centers.
	        relaxDistance.Normalize();
	        Collision.exchangeEnergyBetween(particle, aaHalfPlane, relaxDistance, pointOfImpact);
	
        }
}
}
