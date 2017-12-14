using Desktop.Express.Scene.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Physics
{
    class ParticleParticleCollision : CollisionAlgorithm<IParticleCollider, IParticleCollider>
    {
        private static ParticleParticleCollision _instance = new ParticleParticleCollision();
        public static ParticleParticleCollision Instance { get => _instance; }

        public override bool detectCollisionBetween(IParticleCollider particle1, IParticleCollider particle2)
        {
            float distanceBetweenParticles = (particle1.Position - particle2.Position).Length();
            return distanceBetweenParticles < particle1.Radius + particle2.Radius;
        }

        public override void resolveCollisionBetween(IParticleCollider particle1, IParticleCollider particle2)
        {
            // RELAXATION STEP

            // First we relax the collision, so the two objects don't collide any more.
            // We need to calculate by how much to move them apart. We will move them in the shortest direction
            // possible which is simply the difference between both centers.
            Vector2 positionDifference = particle2.Position - particle1.Position;
            float collidedDistance = positionDifference.Length();
            float minimumDistance = particle1.Radius + particle2.Radius;
            float relaxDistance = minimumDistance - collidedDistance;

            Vector2 collisionNormal = collidedDistance != 0 ? Vector2.Normalize(positionDifference) : Vector2.UnitX;
	        Vector2 relaxDistanceVector = collisionNormal * relaxDistance;
            Collision.relaxCollisionBetween(particle1, particle2, relaxDistanceVector);
	 
	        // ENERGY EXCHANGE STEP
	        // In a collision, energy is exchanged only along the collision normal.
	        // For particles this is simply the line between both centers.
	        Collision.exchangeEnergyBetween(particle1, particle2, collisionNormal, Vector2.Zero);
        }
    }
}
