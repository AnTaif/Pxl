﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security;

namespace Pxl
{
    public abstract class Creature : ICreature
    {
        private readonly int ColliderOffset = 4;
        protected readonly float MaxFallSpeed = 400;

        public bool IsAlive { get; protected set; }
        public float Speed { get; protected set; }
        public bool OnGround { get; protected set; }

        protected RectangleF bounds;
        public RectangleF Bounds => bounds;

        public Rectangle Collider { get; protected set; }
        public List<List<Rectangle>> CollisionTiles { get; protected set; }
        public CollisionType Type { get; protected set; }

        protected Vector2 velocity;
        public Vector2 Velocity => velocity;

        public Creature(RectangleF bounds)
        {
            this.bounds = bounds;
            IsAlive = true;
        }

        public Vector2 Direction { get; protected set; }

        protected void Move(GameTime gameTime)
        {
            bounds.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateCollider(gameTime);
        }

        protected void ApplyGravity(float gravity)
        {
            if (velocity.Y < MaxFallSpeed)
                velocity.Y += gravity;
        }

        private void UpdateCollider(GameTime gameTime)
        {
            Collider = new Rectangle((int)bounds.X + ColliderOffset, (int)bounds.Y, (int)bounds.Width - ColliderOffset * 2, (int)bounds.Height);
        }

        public void HandleCollisionsWithLevel()
        {
            var collisionsWithLevel = CollisionManager.GetCollisionsWithLevel(this);

            foreach (var collision in collisionsWithLevel)
            {
                HandleCollisionWithLevel(collision);
            }
        }

        protected abstract void HandleCollisionWithLevel(CollisionInfo collision);

        public abstract void HandleCollisionWithCreature(CollisionInfo collision);

        public virtual void Death() => IsAlive = false;

        public void SetVelocity(Vector2 velocity) => this.velocity = velocity;

        protected void ChangeHorizontalDirection()
        {
            var x = -Direction.X;
            Direction = new Vector2(x, Direction.Y);
            velocity.X = Direction.X * Speed;
        }

        protected void ChangeVerticalDirection()
        {
            var y = -Direction.Y;
            Direction = new Vector2(Direction.X, y);
            velocity.Y = Direction.Y * Speed;
        }

        public abstract void Update(GameTime gameTime);

        public void UpdateCollisionTiles() => CollisionTiles = CollisionManager.GetCollisionTiles(this);
    }

    public interface ICreature
    {
        public RectangleF Bounds { get; }
        public Rectangle Collider { get; }
        public List<List<Rectangle>> CollisionTiles { get; }
        public Vector2 Direction { get; }
        public bool IsAlive { get; }
        public void Death();
        public Vector2 Velocity { get; }
        public void Update(GameTime gameTime);
        public void UpdateCollisionTiles();
        public CollisionType Type { get; }
        public void HandleCollisionWithCreature(CollisionInfo collision);
    }
}
