using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Pxl
{
    public class Entity : IEntity
    {
        protected RectangleF bounds;
        public RectangleF Bounds { get { return bounds; } }

        public Rectangle Collider { get; protected set; }

        public List<List<Rectangle>> CollisionTiles { get; protected set; }

        public bool IsAlive { get; protected set; }

        protected Vector2 velocity;
        public Vector2 Velocity { get { return velocity; } }

        public Entity(RectangleF bounds)
        {
            this.bounds = bounds;
        }

        protected void Move(GameTime gameTime)
        {
            bounds.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateCollider(gameTime);
        }

        public void ApplyGravity(float gravity)
        {
            velocity.Y += gravity;
        }

        private void UpdateCollider(GameTime gameTime)
        {
            Collider = new Rectangle((int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height);
        }

        public virtual void Update(GameTime gameTime) => Move(gameTime);

        public void UpdateCollisionTiles() => CollisionTiles = CollisionManager.GetCollisionTiles(this);
    }

    public interface IEntity
    {
        public RectangleF Bounds { get; }

        public Rectangle Collider { get; }

        public List<List<Rectangle>> CollisionTiles { get; }

        public bool IsAlive { get; }

        public Vector2 Velocity { get; }

        public void Update(GameTime gameTime);

        public void UpdateCollisionTiles();
    }
}
