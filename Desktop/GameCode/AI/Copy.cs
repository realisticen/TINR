using Desktop.Express.Scene;
using Desktop.GameCode.Units;
using Desktop.GameCode.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;

namespace Desktop.GameCode.AI
{
    class CopyAI
    {
        PlayerBase _aiBase;
        public CopyAI(IScene scene, PlayerBase aiBase)
        {
            _aiBase = aiBase;
            scene.objectAdded += Scene_objectAdded;
        }

        private void Scene_objectAdded(object sender, EventArgs e)
        {
            var obj = (e as SceneEventArgs).sceneObject;
            if (obj is MeleeStudent melee)
                if(melee.Owner != _aiBase)
                    UnitSpawner.SpawnMeleeUnit(_aiBase);
        }
    }
}
