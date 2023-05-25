using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public interface IEntity
    {
        public Rectangle Collider { get; }
        public List<List<Rectangle>> Collisions { get; }

        public bool IsAlive { get; }
        public Vector2 Velocity { get; }

        public void UpdateCollisions();
    }
}
