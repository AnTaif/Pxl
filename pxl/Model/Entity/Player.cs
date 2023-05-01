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
        private const float JumpSpeed = -700;
        

        private Vector2 velocity = Vector2.Zero;
        private Level level;
        private bool onGround;
        private Vector2 inputDirection;

        private GameTime gameTime;

        public readonly (int Width, int Height) Size = (21, 26);
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle Collider { get; set; }
        public List<List<Rectangle>> CollisionTiles { get; private set; }

        public Player(Vector2 startPosition, Level level)
        {
            Position = startPosition;
            Collider = new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height);
            this.level = level;
            CollisionTiles = new List<List<Rectangle>>();
        }

        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;

            ApplyGravity();

            // Get input direction
            inputDirection = InputHandler.GetMoveDirection();

            ApplyHorizontalMove(inputDirection);

            HandleCollisionsWithLevel();

            // Jump
            if (InputHandler.IsJumpPress())
                Jump();

            MovePlayer(gameTime);

            UpdateCollider(gameTime);
        }

        private void MovePlayer(GameTime gameTime) => Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            if (Math.Abs(velocity.X) < MaxSpeed) velocity.X += inputDirection.X * Speed;
            if (inputDirection == Vector2.Zero) velocity.X = 0;

            if (Position.X < 0 && inputDirection.X < 0)
                velocity.X = 0;
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

        public void Move(Vector2 direction) => Position += direction;

        public void HandleCollisionsWithLevel()
        {
            UpdateCollisionTiles();

            CheckHorizontalCollision();
            if (velocity.Y > 0)
                CheckBottomCollision();
            else if (velocity.Y < 0)
                CheckTopCollision();
        }

        public void CheckHorizontalCollision()
        {
            for (int i = 1; i < CollisionTiles.Count - 1; i++)
            {
                var tile = velocity.X > 0 ? CollisionTiles[i].Last() : CollisionTiles[i][0];
                if (!level.InCollisionBounds(tile))
                    continue;
                if (level.CollisionMap[tile.Y, tile.X] == 1)
                {
                    var tileInGlobal = ConvertToGlobal(tile);

                    Position = new Vector2(velocity.X > 0 ? tileInGlobal.X - Size.Width : tileInGlobal.Right, Position.Y);

                    velocity.X = 0;
                }
            }
        }

        public bool CheckTopCollision()
        {
            foreach (var tile in CollisionTiles[0])
            {
                if (!level.InCollisionBounds(tile))
                    continue;
                if (level.CollisionMap[tile.Y, tile.X] == 1)
                {
                    var tileInGlobal = ConvertToGlobal(tile);

                    Position = new Vector2(Position.X, tileInGlobal.Bottom);
                    velocity.Y = 0;
                    onGround = false;
                    return true;
                }
            }
            return false;
        }

        public void CheckBottomCollision()
        {

            foreach (var tile in CollisionTiles[CollisionTiles.Count - 1])
            {
                if (!level.InCollisionBounds(tile))
                    continue;
                if (level.CollisionMap[tile.Y, tile.X] == 1)
                {
                    var tileInGlobal = ConvertToGlobal(tile);

                    if (Position.Y + Size.Height - tileInGlobal.Y > 2) continue;

                    Position = new Vector2(Position.X, tileInGlobal.Y - Size.Height + 1);
                    velocity.Y = 0;
                    onGround = true;
                    return;
                }
            }
            onGround = false;
        }

        public void UpdateCollisionTiles()
        {
            CollisionTiles.Clear();

            var tileSize = level.TileSize;
            var colliderPos = new Point(Collider.X / tileSize, Collider.Y / tileSize);
            var rightColliderPos =
                new Point((int)Math.Ceiling((Collider.X + Collider.Width) / (float)tileSize), (int)Math.Ceiling((Collider.Y + Collider.Height) / (float)tileSize));

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

            foreach (var list in CollisionTiles)
                foreach (var tile in list)
                    collisionTiles.Add(ConvertToGlobal(tile));

            return collisionTiles;
        }

        private Rectangle ConvertToGlobal(Rectangle tile)
        {
            return new Rectangle(tile.X * tile.Width, tile.Y * tile.Height, tile.Width, tile.Height);
        }

        public void UpdateLevel(Level level)
        {
            this.level = level;
            Position = new Vector2(0, Position.Y);
        }
    }
}
