using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Physics
{
    class CollisionAlgorithm<T, U> : ICollisionAlgorithm<T, U>
    {
        public virtual bool detectCollisionBetween(T item1, U item2)
        {
            return false;
        }

        public virtual void resolveCollisionBetween(T item1, U item2)
        {

        }

        public virtual void collisionBetween(T item1, U item2)
        {
            if (detectCollisionBetween(item1, item2))
            {
                if (Collision.shouldResolveCollisionBetween(item1, item2))
                {
                    resolveCollisionBetween(item1, item2);
                    Collision.reportCollisionBetween(item1, item2);
                }
            }
        }
    }
}
