using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Scene
{
    interface IScene : IUpdateable
    {
        List<object> sceneObjects { get; }
        event EventHandler objectAdded;
        event EventHandler objectRemoved;
        void addItem(object o);
        void removeItem(object o);
        void clear();
    }
}
