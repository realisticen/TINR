using Desktop.Express.Graphics;
using Desktop.Express.Scene;
using Desktop.GameCode.Levels;
using Desktop.GameCode.Units;
using Desktop.GameCode.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;

namespace TINRGame.Components
{
    class Level1 : Level
    {
        public Level1(Game game) : base(game)
        {
        }

        public override void InitLevel(IScene scene)
        {
            base.InitLevel(scene);

            textureLoader.LoadTexture("atlas", TextureKey.ATLAS);
            SpriteMaker maker = new SpriteMaker(textureLoader.GetTexture(TextureKey.ATLAS),
                                    content.Load<Dictionary<string, Rectangle>>("atlasPOS"));

            UnitSpawner.Maker = maker;

            player1Base = new PlayerBase(maker.getSprite("base"), new Point(10, 130));
            player2Base = new PlayerBase(maker.getSprite("base"), new Point(1200,130));
            player2Base.SpriteEffect = SpriteEffects.FlipHorizontally;

            scene.addItem(player1Base);
            scene.addItem(player2Base);
        }
    }
}
