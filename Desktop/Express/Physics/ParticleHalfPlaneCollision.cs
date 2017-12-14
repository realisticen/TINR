using Desktop.Express.Scene.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Physics
{
    class ParticleHalfPlaneCollision : CollisionAlgorithm<IParticleCollider, IHalfPlaneCollider>
    {
        private static ParticleHalfPlaneCollision _instance = new ParticleHalfPlaneCollision();
        public static ParticleHalfPlaneCollision Instance { get => _instance; }

        public override bool detectCollisionBetween(IParticleCollider particle, IHalfPlaneCollider halfPlane)
        {
            float nearPoint = Vector2.Dot(particle.Position, halfPlane.HalfPlane.normal) - particle.Radius;
            return nearPoint < halfPlane.HalfPlane.distance;
        }

        public override void resolveCollisionBetween(IParticleCollider particle, IHalfPlaneCollider halfPlane)
        {
            // First we relax the collision, so the two objects don't collide any more.
            float nearPoint = Vector2.Dot(particle.Position, halfPlane.HalfPlane.normal) - particle.Radius;
            float relaxDistance = nearPoint - halfPlane.HalfPlane.distance;
            Vector2 relaxDistanceVector = halfPlane.HalfPlane.normal * relaxDistance;
            Collision.relaxCollisionBetween(particle, halfPlane, relaxDistanceVector);
	
	        // ENERGY EXCHANGE STEP
	        // In a collision, energy is exchanged only along the collision normal.
	        // For particles this is simply the line between both centers.
	        Vector2 collisionNormal = Vector2.Normalize(relaxDistanceVector);
	        Vector2 pointOfImpact = particle.Position + (collisionNormal * (relaxDistance + particle.Radius));
            Collision.exchangeEnergyBetween(particle, halfPlane, collisionNormal, pointOfImpact);
        }
}
}
