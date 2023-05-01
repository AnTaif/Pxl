using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pxl.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    internal class GameView
    {
        private SpriteBatch _spriteBatch;
        private Background _background;

        // Textures
        private Texture2D playerTexture;
        private Texture2D groundTexture;
        private Texture2D collisionTexture;
        private Texture2D playerColliderTexture;


        // Animations
        private Animation idleAnimation;
        private Animation walkAnimation;
        private Animation jumpAnimation;

        public GameView(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _background = new Background(_spriteBatch);
        }

        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Owlet/owlet");

            var walkFrames = new List<Texture2D>()
            {
                content.Load<Texture2D>("owlet/walk/walk1"),
                content.Load<Texture2D>("owlet/walk/walk2"),
                content.Load<Texture2D>("owlet/walk/walk3"),
                content.Load<Texture2D>("owlet/walk/walk4"),
                content.Load<Texture2D>("owlet/walk/walk5"),
                content.Load<Texture2D>("owlet/walk/walk6")
            };
            walkAnimation = new Animation(walkFrames);

            groundTexture = content.Load<Texture2D>("stone_brick");
            collisionTexture = content.Load<Texture2D>("collision");
            playerColliderTexture = content.Load<Texture2D>("player_collision");

            _background.LoadContent(content);
        }

        public void Draw(GameTime gameTime, GameModel model)
        {
            var currentLevel = model.Map.CurrentLevel;

            _background.Draw(gameTime);

            foreach (var tile in currentLevel.Tiles)
                DrawFilledRectangle(tile.Bounds, groundTexture);

            _spriteBatch.Draw(playerTexture, model.Player.Position, Color.White);

            DrawCollisions(_spriteBatch, model);
        }

        public void DrawFilledRectangle(Rectangle parentRect, Texture2D tileTexture)
        {
            var tileSize = tileTexture.Width;
            var tileXCount = parentRect.Width / tileSize;
            var tileYCount = parentRect.Height / tileSize;

            for (int i = 0; i < tileXCount; i++)
            {
                for (int j = 0; j < tileYCount; j++)
                {
                    var position = new Rectangle(parentRect.X + i * tileSize, parentRect.Y + j * tileSize, tileSize, tileSize);
                    _spriteBatch.Draw(tileTexture, position, Color.White);
                }
            }
        }

        public void DrawCollisions(SpriteBatch spriteBatch, GameModel model)
        {
            _spriteBatch.Draw(playerColliderTexture, model.Player.Collider, Color.White); // Collider
            foreach (var collision in model.Player.GetCollisionTilesInGlobal())
            {
                _spriteBatch.Draw(collisionTexture, collision, Color.White); // CollisionTile
            }
        }
    }
}
