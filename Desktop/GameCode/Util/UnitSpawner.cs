using Desktop.Express.Scene;
using Desktop.GameCode.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;

namespace Desktop.GameCode.Util
{
    static class UnitSpawner
    {
        public static SpriteMaker Maker;
        public static IScene Scene;
        private static Dictionary<PlayerBase, Queue<BasicUnit>> spawnQueues = new Dictionary<PlayerBase, Queue<BasicUnit>>(2);

        public static bool SpawnMeleeUnit(PlayerBase playerBase)
        {
            if (!spawnQueues.ContainsKey(playerBase))
                spawnQueues.Add(playerBase, new Queue<BasicUnit>(100));

            var spawnQueue = spawnQueues[playerBase];

            if (playerBase.Money >= MeleeStudent.UnitCost)
            {
                playerBase.Money -= MeleeStudent.UnitCost;
                var student = MeleeStudent.GetMeleeStudent(Maker);
                student.Position = playerBase.Position;
                student.Position += new Vector2(0, playerBase.Height - student.Height);
                student.Owner = playerBase;
                if(playerBase.SpriteEffect != Microsoft.Xna.Framework.Graphics.SpriteEffects.None)
                {
                    student.ChangeDirection();
                }

                spawnQueue.Enqueue(student);
                return true;
            }

            return false;
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var spawnQueue in spawnQueues.Values)
            {
                if (spawnQueue.Count == 0)
                    continue;

                BasicUnit first = spawnQueue.Peek();
                if (first.UnitSpawnDelay <= 0)
                {
                    Scene.addItem(first);
                    spawnQueue.Dequeue();
                    return;
                }
                first.UnitSpawnDelay -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }
    }
}
