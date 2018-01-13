using Desktop.Express.Scene.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.GameCode.Units
{
    interface IUnit : IVelocity, IPosition
    {
        PlayerBase Owner { get; set; }
        float Health { get; set; }
        int KillReward { get; }
        bool Dead { get; }
        bool Remove { get; }
        UnitState State { get; }
    }
}
