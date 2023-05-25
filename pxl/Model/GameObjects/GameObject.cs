using Microsoft.Xna.Framework;

namespace Pxl
{

    public interface IGameObject
    {
        public Rectangle Bounds { get; }

        public Vector2 Position { get; }

        public CollisionType CollisionType { get; }
    }
}
