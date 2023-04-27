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
    public class Player
    {
        private const float Gravity = 30;
        private const float MaxFallSpeed = 500;
        private const float MaxSpeed = 400;
        private const float Speed = 24f;
        private const float JumpSpeed = -800;
        private const int SpriteWidth = 66, SpriteHeight = 81;

        private Vector2 velocity = Vector2.Zero;
        private Level level;
        private bool onGround;
        private Vector2 inputDirection;

        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle HitBox { get; set; }
        public List<List<Rectangle>> CollisionTiles { get; private set; }

        public Player(Vector2 startPosition, Level level)
        {
            Position = startPosition;
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, 64, 80);
            this.level = level;
            CollisionTiles = new List<List<Rectangle>>();
        }
       
        public void Update(GameTime gameTime)
        {
            // Gravity
            if (velocity.Y < MaxFallSpeed) velocity.Y += Gravity;

            // Get input direction
            inputDirection = InputHandler.GetMoveDirection();
            if (Math.Abs(velocity.X) < MaxSpeed) velocity.X += inputDirection.X * Speed;
            if (inputDirection == Vector2.Zero) velocity.X = 0;

            HandleCollisionsWithLevel();

            // Jump
            if (onGround && InputHandler.IsJumpPress())
            {
                onGround = false;
                velocity.Y = JumpSpeed;
            }

            velocity.Y -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Move player's position
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateCollider(gameTime);
        }

        private void UpdateCollider(GameTime gameTime)
        {
            HitBox = new Rectangle(
                (int)Position.X + (int)(velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds), 
                (int)Position.Y + (int)(velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds), 
                64, 80);
        }

        public void Move(Vector2 direction) => Position += direction;

        public void HandleCollisionsWithLevel()
        {
            UpdateCollisionTiles();
            var direction = velocity;
            direction.Normalize();
            if (direction.Y > 0)
                CheckBottomCollision();
            if (direction.Y < 0)
                CheckTopCollision();
            if (direction.X > 0)
                CheckRightCollision();
            if (direction.X < 0)
                CheckLeftCollision();
        }

        public bool CheckRightCollision()
        {
            for (int i = 0; i < CollisionTiles.Count - 1; i++)
            {
                var tile = CollisionTiles[i][CollisionTiles[0].Count-1];
                if (level.CollisionMap[tile.Y, tile.X] == 1)
                {
                    velocity.X = 0;
                }
            }
            return false;
        }

        public bool CheckLeftCollision()
        {
            for (int i = 0; i < CollisionTiles.Count - 1; i++)
            {
                var tile = CollisionTiles[i][0];
                if (level.CollisionMap[tile.Y, tile.X] == 1)
                {
                    velocity.X = 0;
                }
            }
            return false;
        }

        public bool CheckTopCollision()
        {
            foreach (var tile in CollisionTiles[0])
            {
                if (level.CollisionMap[tile.Y, tile.X] == 1)
                {
                    if (level.CollisionMap[tile.Y + 1, tile.X] == 1 &&
                        level.CollisionMap[tile.Y + 2, tile.X] == 1 &&
                        level.CollisionMap[tile.Y + 3, tile.X] == 1) continue;
                    velocity.Y = 0;
                    onGround = false;
                    return true;
                }
            }
            return false;
        }

        public bool CheckBottomCollision()
        {

            foreach (var tile in CollisionTiles[CollisionTiles.Count - 1])
            {
                if (level.CollisionMap[tile.Y, tile.X] == 1)
                {
                    if (level.CollisionMap[tile.Y - 1, tile.X] == 1) continue;
                    velocity.Y = 0;
                    onGround = true;
                    return true;
                }
            }
            return false;
        }

        public void UpdateCollisionTiles()
        {
            CollisionTiles.Clear();

            var tileSize = level.TileSize;
            var colliderPos = new Point(HitBox.X / tileSize, HitBox.Y / tileSize);
            var rightColliderPos = 
                new Point((int)Math.Ceiling((HitBox.X + HitBox.Width) / (float)tileSize), (int)Math.Ceiling((HitBox.Y + HitBox.Height) / (float)tileSize));

            var tileCollider = 
                (Width: (rightColliderPos.X - colliderPos.X), 
                Height: (rightColliderPos.Y - colliderPos.Y));

            for (int j = 0; j < tileCollider.Height; j++)
            {
                CollisionTiles.Add(new List<Rectangle>());
                for (int i = 0; i < tileCollider.Width; i++)
                {
                    CollisionTiles[j].Add(new Rectangle((colliderPos.X + i), (colliderPos.Y + j), level.TileSize, level.TileSize));
                }
            }
        }

        public List<Rectangle> GetCollisionTilesInGlobal()
        {
            var collisionTiles = new List<Rectangle>();

            foreach(var list in CollisionTiles)
                foreach(var tile in list)
                    collisionTiles.Add(new Rectangle(tile.X * tile.Width, tile.Y * tile.Height, tile.Width, tile.Height));

            return collisionTiles;
        }
    }
}
