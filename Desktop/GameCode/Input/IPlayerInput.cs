using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINRGame.Components
{
    interface IUserInput
    {
        bool MousePressed { get; }
        Point mousePosition { get; }
    }
}
