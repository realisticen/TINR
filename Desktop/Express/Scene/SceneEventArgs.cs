using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Scene
{
    class SceneEventArgs : EventArgs
    {
        public object sceneObject;
        public SceneEventArgs(object o)
        {
            sceneObject = o;
        }
    }
}
