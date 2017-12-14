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
    class ParticleAARectangleCollision : CollisionAlgorithm<IParticleCollider, IAARectangleCollider>
    {
        private static ParticleAARectangleCollision _instance = new ParticleAARectangleCollision();
        public static ParticleAARectangleCollision Instance { get => _instance; }

        private static Vector2 calculateRelaxDistanceBetween;
        public Vector2 CalculateRelaxDistanceBetween(IParticleCollider particle, IAARectangleCollider aaRectangle)
        {
            Vector2 relaxDistance = Vector2.Zero;
            Vector2 nearestVertex = aaRectangle.Position;

            float halfWidth = aaRectangle.Width / 2;
            float halfHeight = aaRectangle.Height / 2;

            // Calculate overlap with all sides.
            float leftDifference = (aaRectangle.Position.X - halfWidth) - (particle.Position.X + particle.Radius);
            if (leftDifference > 0) return relaxDistance;

            float rightDifference = (particle.Position.X - particle.Radius) - (aaRectangle.Position.X + halfWidth);
            if (rightDifference > 0) return relaxDistance;

            float topDifference = (aaRectangle.Position.Y - halfHeight) - (particle.Position.Y + particle.Radius);
            if (topDifference > 0) return relaxDistance;

            float bottomDifference = (particle.Position.Y - particle.Radius) - (aaRectangle.Position.X + halfHeight);
            if (bottomDifference > 0) return relaxDistance;

            // Particle is under all sides.
            // Find the voronoi region's nearest vertex.
            bool horizontalyInside = false;
            bool verticalyInside = false;

            if (particle.Position.X < aaRectangle.Position.X - halfWidth)
            {
                nearestVertex.X -= halfWidth;
            }
            else if (particle.Position.X > aaRectangle.Position.X + halfWidth)
            {
                nearestVertex.X += halfWidth;
            }
            else
            {
                horizontalyInside = true;
            }

            if (particle.Position.Y < aaRectangle.Position.Y - halfHeight)
            {
                nearestVertex.Y -= halfHeight;
            }
            else if (particle.Position.Y > aaRectangle.Position.Y + halfHeight)
            {
                nearestVertex.Y += halfHeight;
            }
            else
            {
                verticalyInside = true;
            }

            if (!horizontalyInside && !verticalyInside)
            {
                // We have a possible collision with an edge vertex.
                Vector2 particleVertex = nearestVertex - particle.Position;
                float vertexDistance = particleVertex.Length();
                if (vertexDistance > particle.Radius)
                {
                    return relaxDistance;
                }
                else
                {
                    return Vector2.Normalize(particleVertex) * (particle.Radius - vertexDistance);
                }
            }

            // Find the smallest difference per axis.
            if (leftDifference > rightDifference)
            {
                relaxDistance.X = -leftDifference;
            }
            else
            {
                relaxDistance.X = rightDifference;
            }

            if (topDifference > bottomDifference)
            {
                relaxDistance.Y = -topDifference;
            }
            else
            {
                relaxDistance.Y = bottomDifference;
            }

            // Find the smallest difference between axises;
            if (System.Math.Abs(relaxDistance.X) < System.Math.Abs(relaxDistance.X))
            {
                relaxDistance.Y = 0;
            }
            else
            {
                relaxDistance.X = 0;
            }


            return relaxDistance;
        }

        public override bool detectCollisionBetween(IParticleCollider particle, IAARectangleCollider aaRectangle)
        {
            Vector2 relaxDistance = this.CalculateRelaxDistanceBetween(particle, aaRectangle);
            return relaxDistance.LengthSquared() > 0;
        }

        public override void resolveCollisionBetween(IParticleCollider particle, IAARectangleCollider aaRectangle)
        {
            Vector2 relaxDistance = this.CalculateRelaxDistanceBetween(particle, aaRectangle);

            Collision.relaxCollisionBetween(particle, aaRectangle, relaxDistance);

            Vector2 collisionNormal = Vector2.Normalize(relaxDistance);
            Collision.exchangeEnergyBetween(particle, aaRectangle, collisionNormal, Vector2.Zero);
        }
}
}
