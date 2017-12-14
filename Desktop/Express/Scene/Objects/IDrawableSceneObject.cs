using Desktop.Express.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Scene.Objects
{
    interface IDrawableSceneObject
    {
        Sprite Sprite { get; }
        bool Visible { get; set; }
        Rectangle DestinationRectangle { get; }
    }
}
