using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Express.Graphics
{
    class Camera2D
    {
        private readonly Viewport _viewport;
        private bool _dirty = true;
        private Matrix _viewMatrix;

        private Vector2 _stretch;
        private Vector2 _position;
        private Vector2 _origin;
        private float _rotation;

        public Vector2 Stretch {
            get => _stretch;
            set { _dirty = true; _stretch = value; }
        }
        public Vector2 Position {
            get => _position;
            set { _dirty = true; _position = value; }
        }
        public Vector2 Origin {
            get => _origin;
            set { _dirty = true; _origin = value; }
        }
        public float Rotation {
            get => _rotation;
            set { _dirty = true; _rotation = value; }
        }

        public Matrix ViewMatrix {
            get
            {
                if (_dirty)
                    CalculateViewMatrix();
                return _viewMatrix;
            }
        }

        public Camera2D(Viewport viewport) : this(viewport, Vector2.Zero)
        {
        }

        public Camera2D(Viewport viewport, Vector2 startPos)
        {
            _viewport = viewport;
            Rotation = 0;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = startPos;
            Stretch = new Vector2(1, 1);
        }

        private void CalculateViewMatrix()
        {
            _dirty = false;
            _viewMatrix = Matrix.CreateTranslation(new Vector3(_position, 0.0f)) *
                          Matrix.CreateScale(new Vector3(_stretch, 1)) *
                          Matrix.CreateTranslation(new Vector3(-_origin, 0.0f)) *
                          Matrix.CreateRotationZ(_rotation) *
                          Matrix.CreateTranslation(new Vector3(_origin, 0.0f));
        }

        public static Point ToCamera(Point screenPosition, Matrix viewMatrix, Vector2 cameraPos)
        {
            var v = Vector2.Transform(new Vector2(screenPosition.X, screenPosition.Y) - cameraPos, Matrix.Invert(viewMatrix));
            return new Point((int)v.X, (int)v.Y);
        }
    }
}
