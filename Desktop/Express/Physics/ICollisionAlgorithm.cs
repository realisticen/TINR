using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Physics
{
    interface ICollisionAlgorithm <T, U>
    {
        bool detectCollisionBetween(T item1, U item2);
        void resolveCollisionBetween(T item1, U item2);
    }
}
