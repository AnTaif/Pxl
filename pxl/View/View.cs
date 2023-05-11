using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Pxl
{
    public class GameView
    {
        public bool isDebugShowing { get; set; }

        private SpriteBatch _spriteBatch;
        private Background background;

        // Textures
        private Texture2D playerTexture;
        private Texture2D groundTexture;
        private Texture2D collisionTexture;
        private Texture2D playerColliderTexture;

        // Sprites
        public PlayerSprite PlayerSprite { get; }
       

        public GameView()
        {
            PlayerSprite = new PlayerSprite();
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            _spriteBatch = spriteBatch;

            background = new Background(_spriteBatch);

            PlayerSprite.LoadContent(content);
           
            groundTexture = content.Load<Texture2D>("stone_brick");
            collisionTexture = content.Load<Texture2D>("collision");
            playerColliderTexture = content.Load<Texture2D>("player_collision");

            background.LoadContent(content);
        }

        public void Draw(GameTime gameTime, GameModel model)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));
            var currentLevel = model.Map.CurrentLevel;

            background.Draw(gameTime);


            foreach (var tile in currentLevel.Tiles)
                DrawFilledRectangle(tile.Bounds, groundTexture);

            PlayerSprite.Update(gameTime);
            PlayerSprite.Draw(_spriteBatch, model.Player.Position);

            if (isDebugShowing)
                ShowDebug(_spriteBatch, model);

            _spriteBatch.End();
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

        private void ShowDebug(SpriteBatch spriteBatch, GameModel model)
        {
            DrawCollisions(_spriteBatch, model);
        }

        public void DrawCollisions(SpriteBatch spriteBatch, GameModel model)
        {
            _spriteBatch.Draw(playerColliderTexture, model.Player.Collider, Color.White); // Collider
            //foreach (var collision in model.Player.GetCollisionTilesInGlobal())
            //{
            //    _spriteBatch.Draw(collisionTexture, collision, Color.White); // CollisionTile
            //}
        }

        public static List<Texture2D> LoadContentFolder(ContentManager content, string folder)
        {
            var dir = new DirectoryInfo(content.RootDirectory + "/" + folder);

            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            var result = new List<Texture2D>();

            var files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                result.Add(content.Load<Texture2D>(folder + "/" + file.Name.Split('.')[0]));
            }
            return result;
        }
    }
}
