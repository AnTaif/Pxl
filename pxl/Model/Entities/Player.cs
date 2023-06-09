using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public class Player : Entity
    {
        private const float MaxSpeed = 220;
        private const float JumpSpeed = -500;

        public int DeathCount { get; private set; }
        public Point SpawnPosition { get; private set; }

        public Player(RectangleF bounds) : base(bounds)
        {
            Collider = new Rectangle((int)bounds.X+4, (int)bounds.Y, (int)bounds.Width-4, (int)bounds.Height);
            CollisionTiles = new List<List<Rectangle>>();
            DeathCount = 0;
            Speed = 15f;

            Type = CollisionType.Player;

            SetSpawn(LevelManager.CurrentLevel.SpawnPoint);
        }

        public Player(RectangleF bounds, float speed) : base(bounds)
        {
            Collider = new Rectangle((int)bounds.X + 4, (int)bounds.Y, (int)bounds.Width - 4, (int)bounds.Height);
            CollisionTiles = new List<List<Rectangle>>();
            DeathCount = 0;
            Speed = speed;

            Type = CollisionType.Player;

            SetSpawn(LevelManager.CurrentLevel.SpawnPoint);
        }

        public override void Update(GameTime gameTime)
        {
            ApplyGravity(GameModel.Gravity);

            CollisionManager.HandleCollisionWithEntities(this);

            HandleCollisionsWithLevel();

            Move(gameTime);
        }

        public void Jump()
        {
            if (OnGround)
            {
                OnGround = false;
                velocity.Y = JumpSpeed;
            }
        }

        public void ApplyHorizontalMove(Vector2 inputDirection)
        {
            if (inputDirection == Vector2.Zero || (bounds.X < 0 && inputDirection.X < 0))
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
                velocity.X = inputDirection.X * Speed; //- velocity.X;
        }

        protected override void HandleCollisionWithLevel(CollisionInfo collision)
        {
            if (collision.Type == CollisionType.Spikes)
            {
                ApplyDeath();
                return;
            }

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
                    OnGround = false;
                    break;

                case CollisionDirection.Bottom:
                    if (collision.Type == CollisionType.None)
                    {
                        OnGround = false;
                        break;
                    }

                    bounds.Position = new Vector2(bounds.X, collision.InteractionTile.Y - bounds.Height + 1);
                    velocity.Y = 0;
                    OnGround = true;
                    break;
            }
        }

        protected override void Death()
        {
            DeathCount++;
            LevelManager.ResetLevel(LevelManager.CurrentLevel);
            bounds.Position = SpawnPosition.ToVector2();
        }

        public void SetSpawn(Point position) => SpawnPosition = position;

        public void UpdatePosition(Vector2 newPosition) => bounds.Position = newPosition;

        public override void HandleCollisionWithEntity(CollisionInfo collision)
        {
            if (collision.Type == CollisionType.Enemy && collision.Direction != CollisionDirection.Bottom)
                ApplyDeath();

            else if (collision.Type == CollisionType.Enemy && collision.Direction == CollisionDirection.Bottom)
                Jump();
        }
    }
}
