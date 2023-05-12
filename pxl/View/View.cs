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
        private Dictionary<string, Texture2D> textures;

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

            textures = new Dictionary<string, Texture2D>()
            {
                {"ground", content.Load<Texture2D>("ground") },
                {"collision", content.Load<Texture2D>("collision") },
                {"player_collision", content.Load<Texture2D>("player_collision") },
                {"mountain_fill", content.Load<Texture2D>("mountain_fill") },
                {"spikes", content.Load<Texture2D>("spikes") },
            };

            background.LoadContent(content);
        }

        public void Draw(GameTime gameTime, GameModel model)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));
            var currentLevel = model.Map.CurrentLevel;

            background.Draw(gameTime);


            foreach (var tile in currentLevel.Tiles)
                DrawTile(tile);

            PlayerSprite.Update(gameTime);
            PlayerSprite.Draw(_spriteBatch, model.Player.Position);

            if (isDebugShowing)
                ShowDebug(_spriteBatch, model);

            _spriteBatch.End();
        }

        private void DrawGroundRectangle(Rectangle parentRect, Texture2D groundTexture, Texture2D fillingTexture)
        {
            var tileSize = groundTexture.Width;
            var tileXCount = parentRect.Width / tileSize;
            var tileYCount = parentRect.Height / tileSize;

            var ground = true;
            for (int j = 0; j < tileYCount; j++)
            {
                for (int i = 0; i < tileXCount; i++)
                {
                    var position = new Rectangle(parentRect.X + i * tileSize, parentRect.Y + j * tileSize, tileSize, tileSize);
                    if (ground)
                    {
                        _spriteBatch.Draw(groundTexture, position, Color.White);
                    } else
                        _spriteBatch.Draw(fillingTexture, position, Color.White);
                }
                ground = false;
            }
        }

        public void DrawTile(Tile tile)
        {
            switch (tile.Type)
            {
                case TileType.Ground:
                    DrawGroundRectangle(tile.Bounds, textures["ground"], textures["mountain_fill"]);
                    break;
                case TileType.Platform:
                    DrawPlatformRectangle(tile.Bounds, textures["ground"]);
                    break;
                case TileType.Spikes:
                    DrawSpikesRectangle(tile.Bounds, textures["spikes"]);
                    break;
            }
        }

        private void DrawPlatformRectangle(Rectangle bounds, Texture2D texture)
        {
            var tileSize = texture.Width;
            var tileXCount = bounds.Width / tileSize;
            var tileYCount = bounds.Height / tileSize;

            for (int j = 0; j < tileYCount; j++)
            {
                for (int i = 0; i < tileXCount; i++)
                {
                    var position = new Rectangle(bounds.X + i * tileSize, bounds.Y + j * tileSize, tileSize, tileSize);
                    _spriteBatch.Draw(texture, position, Color.White);
                }
            }
        }

        private void DrawSpikesRectangle(Rectangle bounds, Texture2D spikesTexture)
        {
            var tileSize = spikesTexture.Width;
            var tileXCount = bounds.Width / tileSize;

            for (int i = 0; i < tileXCount; i++)
            {
                var position = new Rectangle(bounds.X + i * tileSize, bounds.Y, tileSize, tileSize);
                _spriteBatch.Draw(spikesTexture, position, Color.White);
            }
        }

        private void ShowDebug(SpriteBatch spriteBatch, GameModel model)
        {
            DrawCollisions(_spriteBatch, model);
        }

        public void DrawCollisions(SpriteBatch spriteBatch, GameModel model)
        {
            _spriteBatch.Draw(textures["player_collision"], model.Player.Collider, Color.White); // Collider
            foreach (var collisionRow in CollisionManager.PlayerCollisions)
                foreach(var collision in collisionRow)
                    _spriteBatch.Draw(textures["collision"], CollisionManager.GetTileInGlobal(collision).Bounds, Color.White); // CollisionTile
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
