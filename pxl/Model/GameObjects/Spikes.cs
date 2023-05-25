using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class Spikes : IGameObject
    {
        public Rectangle Bounds { get; private set; }
        public Vector2 Position { get; private set; }
        public CollisionType CollisionType { get; private set; }

        public Spikes(Rectangle bounds, Vector2 position)
        {
            Bounds = bounds;
            Position = position;
            CollisionType = CollisionType.Spikes;
        }
    }
}
