using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pxl.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class Player
    {
        private const float Gravity = 32;
        private const float MaxFallSpeed = 600;
        private const float MaxSpeed = 400;
        private const float Speed = 20f;
        private const float JumpSpeed = -632;

        private Vector2 velocity = Vector2.Zero;
        public Vector2 Velocity { get { return velocity; } }

        private bool onGround;


        public readonly (int Width, int Height) Size = (28, 35);
        public Vector2 Position { get; set; }
        public Rectangle Collider { get; set; }
        public List<List<Tile>> CollisionTiles { get; private set; }

        public Player(Vector2 startPosition)
        {
            Position = startPosition;
            Collider = new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height);
            CollisionTiles = new List<List<Tile>>();
        }

        public void Update(GameTime gameTime)
        {
            ApplyGravity();

            HandleCollisionsWithLevel();

            MovePlayer(gameTime);
        }

        private void MovePlayer(GameTime gameTime)
        {
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateCollider(gameTime);
        }

        public void Jump()
        {
            if (onGround)
            {
                onGround = false;
                velocity.Y = JumpSpeed;
            }
        }

        public void ApplyHorizontalMove(Vector2 inputDirection)
        {
            if (inputDirection == Vector2.Zero || (Position.X < 0 && inputDirection.X < 0))
                velocity.X = 0;

            else if (Math.Sign(inputDirection.X) == Math.Sign(velocity.X))
            {
                if (Math.Abs(velocity.X) < MaxSpeed)
                {
                    velocity.X += inputDirection.X * Speed;
                    if (Math.Abs(velocity.X) > MaxSpeed)
                    {
                        velocity.X = Math.Sign(velocity.X) * MaxSpeed;
                    }
                }
            }
            else // If the player is trying to move in the opposite direction, change direction immediately
                velocity.X = inputDirection.X * Speed;
        }

        public void ApplyGravity()
        {
            if (velocity.Y < MaxFallSpeed)
                velocity.Y += Gravity;
        }

        private void UpdateCollider(GameTime gameTime)
        {
            Collider = new Rectangle(
                (int)Position.X + (int)(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds),
                (int)Position.Y + (int)(velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds),
                Size.Width, Size.Height);
        }

        public void HandleCollisionsWithLevel()
        {
            var collisionsWithLevel = CollisionManager.GetCollisionsWithLevel(this);

            foreach (var collision in collisionsWithLevel)
            {
                switch (collision.Direction)
                {
                    case CollisionDirection.Right:
                        velocity.X = -4;
                        break;

                    case CollisionDirection.Left:
                        velocity.X = 4;
                        break;
                    case CollisionDirection.Top:
                        if (collision.Type == CollisionType.None)
                            break;

                        velocity.Y = 0;
                        onGround = false;
                        break;

                    case CollisionDirection.Bottom:
                        if (collision.Type == CollisionType.None)
                        {
                            onGround = false;
                            break;
                        }

                        Position = new Vector2(Position.X, collision.InteractionTile.Bounds.Y - Size.Height + 1);
                        velocity.Y = 0;
                        onGround = true;
                        break;
                }
            }
        }

        public void UpdatePosition(Vector2 newPosition) => Position = newPosition;
    }
}
