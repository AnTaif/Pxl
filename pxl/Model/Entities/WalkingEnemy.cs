using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    [Serializable]
    public class WalkingEnemy : Enemy
    {
        private const float Speed = 10f;
        private const float Gravity = 23f;
        private const float MaxSpeed = 200f;
        private const float MaxFallSpeed = 500f;

        public Vector2 LeftRange { get; private set; }
        public Vector2 RightRange { get; private set; }

        public float Direction { get; private set; }

        [JsonConstructor]
        public WalkingEnemy(RectangleF bounds, float direction = 1, Vector2? leftRange = null, Vector2? rightRange = null) : base(bounds)
        {
            Collider = new Rectangle((int)bounds.X + 4, (int)bounds.Y, (int)bounds.Width - 4, (int)bounds.Height);
            CollisionTiles = new List<List<Rectangle>>();
            LeftRange = leftRange ?? new Vector2(0, 0);
            RightRange = rightRange ?? new Vector2(MainGame.WorkingSize.Width, MainGame.WorkingSize.Height);
            Direction = direction;
        }

        public override void Update(GameTime gameTime)
        {
            if (velocity.Y < MaxFallSpeed)
                ApplyGravity(Gravity);

            ApplyHorizontalMove();

            HandleCollisionsWithLevel();

            base.Update(gameTime);
        }

        public void HandleCollisionsWithLevel()
        {
            var collisionsWithLevel = CollisionManager.GetCollisionsWithLevel(this);
            var switchedDirection = false;


            foreach (var collision in collisionsWithLevel)
            {
                switch (collision.Direction)
                {
                    case CollisionDirection.Right:
                    case CollisionDirection.Left:
                        ChangeDirection();
                        continue;

                    case CollisionDirection.Top:
                        if (collision.Type == CollisionType.None)
                            continue;

                        velocity.Y = 0;
                        continue;

                    case CollisionDirection.Bottom:
                        if (collision.Type == CollisionType.None)
                        {
                            if (!switchedDirection)
                            {
                                ChangeDirection();
                                switchedDirection = !switchedDirection;
                            }
                            continue;
                        }

                        bounds.Position = new Vector2(bounds.X, collision.InteractionTile.Y - bounds.Height + 1);
                        velocity.Y = 0;
                        continue;
                }
            }
        }

        public void ApplyGravity()
        {
            if (velocity.Y < MaxFallSpeed)
                velocity.Y += Gravity;
        }

        private void ApplyHorizontalMove()
        {
            if (Math.Abs(velocity.X) < MaxSpeed)
            {
                velocity.X += Direction * Speed;
                if (Math.Abs(velocity.X) > MaxSpeed)
                {
                    velocity.X = Math.Sign(velocity.X) * MaxSpeed;
                }
            }

            if (velocity.X > 0 && Bounds.Right >= RightRange.X)
            {
                ChangeDirection();
            }
            else if (velocity.X < 0 && Bounds.X <= LeftRange.X)
            {
                ChangeDirection();
            }
        }

        private void ChangeDirection()
        {
            Direction = -Direction;
            velocity.X = Direction * Speed;
        }
    }
}
