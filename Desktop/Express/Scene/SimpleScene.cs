using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Desktop.Express.Scene
{
    class SimpleScene : GameComponent, IScene
    {
        private List<object> _list = new List<object>(10);

        public SimpleScene(Game game) : base(game)
        {
        }

        public List<object> sceneObjects { get => _list; }

        public bool Enabled => throw new NotImplementedException();

        public int UpdateOrder => throw new NotImplementedException();

        public event EventHandler objectAdded;
        public event EventHandler objectRemoved;
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public void addItem(object o)
        {
            _list.Add(o);
            objectAdded?.Invoke(this, new SceneEventArgs(o));
        }

        public void clear()
        {
            _list.Clear();
        }

        public void removeItem(object o)
        {
            _list.Remove(o);
            objectRemoved?.Invoke(this, new SceneEventArgs(o));
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
