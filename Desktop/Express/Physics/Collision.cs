using Desktop.Express.Scene.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Physics
{
    public static class Collision
    {
        public static void collisionBetween(object item1, object item2)
        {
            if (item1 is IAARectangleCollider ar && item2 is IAAHalfPlaneCollider ah)
                AARectangleAAHalfPlaneCollision.Instance.collisionBetween(ar, ah);
            else if (item1 is IAARectangleCollider ar1 && item2 is IAARectangleCollider ar2)
                AARectangleAARectangleCollision.Instance.collisionBetween(ar1, ar2);
            else if (item1 is IParticleCollider pc && item2 is IAAHalfPlaneCollider ah2)
                ParticleAAHalfPlaneCollision.Instance.collisionBetween(pc, ah2);
            else if (item1 is IParticleCollider p1 && item2 is IParticleCollider p2)
                ParticleParticleCollision.Instance.collisionBetween(p1, p2);
            else if (item1 is IParticleCollider pc2 && item2 is IHalfPlaneCollider hp)
                ParticleHalfPlaneCollision.Instance.collisionBetween(pc2, hp);
            else if (item1 is IParticleCollider pc3 && item2 is IAARectangleCollider ar3)
                ParticleAARectangleCollision.Instance.collisionBetween(pc3, ar3);
        }
        public static void relaxCollisionBetween(object item1, object item2, Vector2 relaxDistance)
        {
            float relaxPercentage1 = 0.5f;
            float relaxPercentage2 = 0.5f;
            IMass itemWithMass1 = item1 as IMass;
            IMass itemWithMass2 = item2 as IMass;
            IPosition itemWithPosition1 = item1 as IPosition;
            IPosition itemWithPosition2 = item2 as IPosition;

            if (itemWithMass1 != null && itemWithMass2 != null)
            {
                float mass1 = itemWithMass1.Mass;
                float mass2 = itemWithMass2.Mass;
                relaxPercentage1 = mass2 / (mass1 + mass2);
                relaxPercentage2 = mass1 / (mass1 + mass2);
            }
            else if (itemWithMass1 != null)
            {
                relaxPercentage1 = 1;
                relaxPercentage2 = 0;
            }
            else if (itemWithMass2 != null)
            {
                relaxPercentage1 = 0;
                relaxPercentage2 = 1;
            }
            else
            {
                // No item has mass, do the same logic, but with positions.
                if (itemWithPosition1 != null && !(itemWithPosition2 != null))
                {
                    relaxPercentage1 = 1;
                    relaxPercentage2 = 0;
                }
                else if (!(itemWithPosition1 != null) && itemWithPosition2 != null)
                {
                    relaxPercentage1 = 0;
                    relaxPercentage2 = 1;
                }
            }

            // Now we need to turn the percentages into real distances.	

            if (itemWithPosition1 != null)
            {
                itemWithPosition1.Position -= relaxDistance * relaxPercentage1;
	        }
	        if (itemWithPosition2 != null) {
                itemWithPosition2.Position += relaxDistance * relaxPercentage2;		
	        }

        }
        public static void exchangeEnergyBetween(object item1, object item2, Vector2 collisionNormal, Vector2 pointOfImpact)
        {
            // We calculate exchange of energy in a collision with respect to items' momentum. Momentum is mass times
            // velocity so the items need to conform both to IMass and IVelocity. If one of the items does not, it's
            // considered as though there is a collision with a static object.
            IPosition item1WithPosition = item1 as IPosition;
            IMovable movableItem1 = item1 as IMovable;
            IRotatable rotatableItem1 = item1 as IRotatable;

            IPosition item2WithPosition = item2 as IPosition;
            IMovable movableItem2 = item2 as IMovable;
            IRotatable rotatableItem2 = item2 as IRotatable;

            // Velocity due to translation.
            Vector2 velocity1 = movableItem1 !=null ? movableItem1.Velocity : Vector2.Zero;
	        Vector2 velocity2 = movableItem2 != null? movableItem2.Velocity : Vector2.Zero;
	
	        //Velocity due to rotation.
	        Vector2 lever1 = Vector2.Zero;
            Vector2 lever2 = Vector2.Zero;
            Vector2 tangentialDirection1 = Vector2.Zero;
            Vector2 tangentialDirection2 = Vector2.Zero;
	
	        if (pointOfImpact != null) {
		        if (item1WithPosition != null && rotatableItem1 != null)
                {
			        lever1 = pointOfImpact - item1WithPosition.Position;
			        tangentialDirection1 = new Vector2(-lever1.Y, lever1.X);
                    tangentialDirection1.Normalize();
                    Vector2 rotationalVelocity = tangentialDirection1 * lever1.Length() * rotatableItem1.AngularVelocity;
                    velocity1 += rotationalVelocity;
		        }
		        if (item2WithPosition != null  && rotatableItem2 != null ) {
			        lever2 = pointOfImpact - item2WithPosition.Position;
                    tangentialDirection2 = new Vector2(-lever2.Y, lever2.X);
                    tangentialDirection2.Normalize();
                    Vector2 rotationalVelocity = tangentialDirection2 * lever2.Length() * rotatableItem2.AngularVelocity;
                    velocity2 += rotationalVelocity;
		        }
	        }
	        // ENERGY EXCHANGE
	
	        // In a collision, energy is exchanged only along the collision normal, so we take into account only
	        // the speed in the direction of the normal.
	        float speed1 = velocity1 != null ? Vector2.Dot(velocity1, collisionNormal) : 0;
	        float speed2 = velocity2 != null ? Vector2.Dot(velocity2, collisionNormal) : 0;
	        float speedDifference = speed1 - speed2;
	
	        // Make sure the objects are coming towards each other. If they are coming together the collision has already been delt with.
	        if (speedDifference< 0) {
		        return;
	        }
	
	        // We can now calculate the impact impulse (the change of momentum). We take into account the cooeficient
	        // of restitution which controls how elastic the collision is. We use a simplified model in which the total
	        // COR is just the multiplication of coeficients of both items.
	        float cor1 = item1 is ICoefficientOfRestitution ? ((ICoefficientOfRestitution)item1).CoefficientOfRestitution : 1;
            float cor2 = item2 is ICoefficientOfRestitution ? ((ICoefficientOfRestitution)item2).CoefficientOfRestitution : 1;
            float cor = cor1 * cor2;

            // We prepare mass inverses. If the object has no mass we consider it is static, which is the same as having
            // infinite mass. The inverse will then be zero.
            float mass1inverse = 0;
            if(item1 is IMass m1)
                mass1inverse = 1.0f / m1.Mass;
            float mass2inverse = 0;
            if (item2 is IMass m2)
                mass1inverse = 1.0f / m2.Mass;

            IAngularMass item1WithAngularMass = item1 as IAngularMass;
            IAngularMass item2WithAngularMass = item2 as IAngularMass;

            float angularMass1Inverse = (item1WithAngularMass != null && tangentialDirection1 != Vector2.Zero) ?
            (float)System.Math.Pow(Vector2.Dot(tangentialDirection1, collisionNormal) * lever1.Length(), 2) / item1WithAngularMass.AngularMass : 0;

            float angularMass2Inverse = (item2WithAngularMass != null && tangentialDirection2 != Vector2.Zero) ?
            (float)System.Math.Pow(Vector2.Dot(tangentialDirection2, collisionNormal) * lever2.Length(), 2) / item2WithAngularMass.AngularMass : 0;	
	
	        // We derive the formula for the impact as the change of momentum.
	        float impact = -(cor + 1) * speedDifference / (mass1inverse + mass2inverse + angularMass1Inverse + angularMass2Inverse);
	
	
	        // TRANSLATION CHANGE
	        // If we divide the impact with item's mass we get the change in speed. We apply it
	        // along the collisions normal. We only do this for non-static items.
	        if (mass1inverse > 0 && movableItem1 != null) {
		        movableItem1.Velocity += collisionNormal * (impact * mass1inverse);
	        }
	
	        if (mass2inverse > 0 && movableItem2 != null) {
		        movableItem2.Velocity -= collisionNormal * (impact * mass2inverse);
	        }
	
	
	        // ROTATION CHANGE
	
	        if (item1WithAngularMass != null && tangentialDirection1 != Vector2.Zero) {
		        float tangentialForce = Vector2.Dot(tangentialDirection1, collisionNormal * impact);
                float change = tangentialForce * lever1.Length() / item1WithAngularMass.AngularMass;
                rotatableItem1.AngularVelocity += change;
	        }
	
	        if (item2WithAngularMass != null && tangentialDirection2 != Vector2.Zero) {
		        float tangentialForce = Vector2.Dot(tangentialDirection2, collisionNormal * -impact);
                float change = tangentialForce * lever2.Length() / item2WithAngularMass.AngularMass;
                rotatableItem2.AngularVelocity += change;
	        }
        }
        public static bool shouldResolveCollisionBetween(object item1, object item2)
        {
            bool res = true;
            if (item1 is ICustomCollider c1)
            {
                res &= c1.collidingWithItem(item2);
            }
            if (item2 is ICustomCollider c2)
            {
                res &= c2.collidingWithItem(item1);
            }
            return res;
        }
        public static void reportCollisionBetween(object item1, object item2)
        {
            if (item1 is ICustomCollider c1)
            {
                c1.collidedWithItem(item2);
            }
            if (item2 is ICustomCollider c2)
            {
                c2.collidedWithItem(item1);
            }
        }

    }
}
