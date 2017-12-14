using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Desktop.Express.Math
{
    class AAHalfPlane : HalfPlane
    {
        public AxisDirection direction;

        public AAHalfPlane(AxisDirection theDirection, float _distance) : base(Vector2.Zero, _distance)
        {
            setDirection(theDirection);
        }

        public void setDirection(AxisDirection _direction)
        {
            direction = _direction;

            switch (direction) {
		        default:
		        case AxisDirection.AxisDirectionPositiveX:
			        normal = Vector2.UnitX;
			        break;
		        case AxisDirection.AxisDirectionNegativeX:
			        normal = Vector2.UnitX * -1;
			        break;
		        case AxisDirection.AxisDirectionPositiveY:
			        normal = Vector2.UnitY;
			        break;
		        case AxisDirection.AxisDirectionNegativeY:
			        normal = Vector2.UnitY * -1;
			        break;
	        }
        }

        public new void setNormal(Vector2 _normal)
        {
	        if (_normal.X == 0 && _normal.Y == 0 || _normal.X != 0 && _normal.Y != 0) {
                throw new ArgumentException("Axis aligned half plane requires an axis aligned normal");
            }
            base.setNormal(_normal);
	        if (_normal.X > 0) {
		        direction = AxisDirection.AxisDirectionPositiveX;
	        } else if (_normal.X < 0) {
		        direction = AxisDirection.AxisDirectionNegativeX;
	        } else if (_normal.Y > 0) {
		        direction = AxisDirection.AxisDirectionPositiveY;
	        } else if (_normal.Y < 0) {
		        direction = AxisDirection.AxisDirectionNegativeY;
	        }
        }
    }
}
