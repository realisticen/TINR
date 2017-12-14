using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Scene.Objects
{
    interface ISceneUser
    {
        IScene Scene { get; set; }
        void addedToScene(ISceneUser scene);
        void removedFromScene(ISceneUser scene);
    }
}
