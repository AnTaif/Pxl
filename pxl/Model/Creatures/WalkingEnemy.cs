using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public abstract class WalkingEnemy : Enemy
    {
        public Vector2 LeftRange { get; private set; }
        public Vector2 RightRange { get; private set; }

        [JsonConstructor]
        public WalkingEnemy(RectangleF bounds, float direction = 1, Point? leftRange = null, Point? rightRange = null)
            : base(bounds)
        {
            Collider = new Rectangle((int)bounds.X + 4, (int)bounds.Y, (int)bounds.Width - 4, (int)bounds.Height);
            CollisionTiles = new List<List<Rectangle>>();
            LeftRange = leftRange?.ToVector2() ?? new Vector2(0, 0);
            RightRange = rightRange?.ToVector2() ?? new Vector2(MainGame.WorkingSize.Width, MainGame.WorkingSize.Height);
            Direction = new Vector2(direction, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsAlive)
                return;

            if (velocity.Y < MaxFallSpeed)
                ApplyGravity(GameModel.Gravity);

            ApplyHorizontalMove();

            HandleCollisionsWithLevel();

            Move(gameTime);
        }

        protected override void HandleCollisionWithLevel(CollisionInfo collision)
        {
            switch (collision.Direction)
            {
                case CollisionDirection.Right:
                case CollisionDirection.Left:
                    ChangeHorizontalDirection();
                    break;

                case CollisionDirection.Top:
                    if (collision.Type == CollisionType.None)
                        break;

                    velocity.Y = 0;
                    break;

                case CollisionDirection.Bottom:
                    if (collision.Type == CollisionType.None)
                        break;

                    bounds.Position = new Vector2(bounds.X, collision.InteractionTile.Y - bounds.Height + 1);
                    velocity.Y = 0;
                    break;
            }
        }

        private void ApplyHorizontalMove()
        {
            velocity.X = Direction.X * Speed;

            if (velocity.X > 0 && Bounds.Right >= RightRange.X)
            {
                ChangeHorizontalDirection();
            }
            else if (velocity.X < 0 && Bounds.X <= LeftRange.X)
            {
                ChangeHorizontalDirection();
            }
        }
    }
}
