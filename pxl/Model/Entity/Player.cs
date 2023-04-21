using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class Player
    {
        // CONSTANTS
        const float GRAVITY = 30;
        const float MAX_FALL_SPEED = 500;
        const float MAX_SPEED = 400;
        const float SPEED = 24f;
        const float JUMP_SPEED = -800;

        private Vector2 velocity = Vector2.Zero;
        private Level level;
        private bool isJumping;
        private bool isAlive = true;

        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle Collider { get; set; }

        public Player(Vector2 startPosition, Level level)
        {
            Position = startPosition;
            Collider = new Rectangle((int)Position.X, (int)Position.Y, 100, 100);
            this.level = level;
        }

        public void Update(GameTime gameTime)
        {
            // GRAVITY
            if (velocity.Y < MAX_FALL_SPEED) velocity.Y += GRAVITY;

            // GET INPUT DIRECTION
            var inputDirection = InputHandler.GetMoveDirection();
            if (Math.Abs(velocity.X) < MAX_SPEED) velocity.X += inputDirection.X * SPEED;
            if (inputDirection == Vector2.Zero) velocity.X = 0;

            if (CollidesWithLevel(level))
            {
                velocity.Y = 0;
                isJumping = false;
            }

            // JUMP
            if (InputHandler.IsJumpPress() && !isJumping && CollidesWithLevel(level))
            {
                isJumping = true;
                velocity.Y = JUMP_SPEED;
            }


            // MOVE PLAYER'S POSITION
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateCollider();
        }

        private void UpdateCollider()
        {
            Collider = new Rectangle((int)Position.X, (int)Position.Y, 100, 100);
        }

        public bool CollidesWithLevel(Level level)
        {
            return Collider.Intersects(level.Bounds);
        }
    }
}
