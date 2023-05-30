using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;

namespace Pxl
{
    public class GameView
    {
        private ContentManager content;

        public bool isDebugShowing { get; set; }

        private SpriteBatch _spriteBatch;
        private Background background;

        private Dictionary<string, Texture2D> textures;
        private SpriteFont bitmapMC;

        private Dictionary<IGameObject, ISprite> levelSprites;
        private Dictionary<Entity, ISprite> entitySprites;
        public PlayerSprite PlayerSprite { get; }

        public GameView()
        {
            PlayerSprite = new PlayerSprite("Owlet");
            levelSprites = new Dictionary<IGameObject, ISprite>();
            entitySprites = new Dictionary<Entity, ISprite>();
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            _spriteBatch = spriteBatch;
            this.content = content;

            background = new Background(_spriteBatch);

            PlayerSprite.LoadContent(content);

            bitmapMC = content.Load<SpriteFont>("Fonts/BitmapMC");

            textures = new Dictionary<string, Texture2D>()
            {
                {"collision", content.Load<Texture2D>("collision") },
                {"player_collision", content.Load<Texture2D>("player_collision") },
            };

            LoadTileTextures(content);

            background.LoadContent(content);
        }

        private void LoadTileTextures(ContentManager content)
        {
            var rootDirectory = MainGame.RootDirectory;
            var path = Path.Combine(rootDirectory, $"Content\\Level\\Tiles");
            var contentPath = "Level/Tiles/";

            var files = Directory.GetFiles(path);

            foreach(var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                textures.Add(fileName, content.Load<Texture2D>(contentPath + fileName));
            }
        }

        public void Draw(GameTime gameTime, GameModel model)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));
            var currentLevel = LevelManager.CurrentLevel;

            background.Draw(gameTime);
            var tileSize = LevelManager.TileSet.TileSize;

            var tileMap = LevelManager.CurrentLevel.TileMap;
            for (int i = 0; i < tileMap.GetLength(0); i++)
            {
                for (int j = 0; j < tileMap.GetLength(1); j++)
                {
                    var tileName = tileMap[i, j].Name;
                    if (tileName.Equals(""))
                        continue;

                    var texture = textures[tileName];
                    var position = new Vector2(j * tileSize, i * tileSize);

                    _spriteBatch.Draw(texture, position, Color.White);
                }
            }

            DrawMovingObjects(gameTime);

            DrawEntities(gameTime);

            PlayerSprite.Update(gameTime);
            PlayerSprite.Draw(_spriteBatch, model.Player.Bounds.Position);

            if (isDebugShowing)
                ShowDebug(_spriteBatch, model);

            _spriteBatch.End();
        }

        private void DrawEntities(GameTime gameTime)
        {
            foreach (var entity in LevelManager.CurrentLevel.Entities)
            {
                var sprite = GetOrCreateSprite(entity);

                sprite.Update(gameTime);
                sprite.Draw(_spriteBatch, entity.Bounds.Position);
            }
        }

        private void DrawMovingObjects(GameTime gameTime)
        {
            foreach (var movingObject in LevelManager.CurrentLevel.MovingObjects)
            {
                var sprite = GetOrCreateSprite(movingObject);

                sprite.Update(gameTime);
                sprite.Draw(_spriteBatch, movingObject.Position);
            }
        }

        private ISprite GetOrCreateSprite(IGameObject gameObject)
        {
            if (levelSprites.ContainsKey(gameObject))
                return levelSprites[gameObject];

            levelSprites[gameObject] = SpriteFactory.CreateSprite(gameObject, textures);
            levelSprites[gameObject].LoadContent(content);
            return levelSprites[gameObject];
        }

        private ISprite GetOrCreateSprite(Entity entity)
        {
            if (entitySprites.ContainsKey(entity))
                return entitySprites[entity];

            entitySprites[entity] = SpriteFactory.CreateSprite(entity, textures);
            entitySprites[entity].LoadContent(content);
            return entitySprites[entity];
        }

        private void ShowDebug(SpriteBatch spriteBatch, GameModel model)
        {
            spriteBatch.DrawString(bitmapMC,
                model.Player.Bounds.Position.ToString(), new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(bitmapMC,
                "On ground: " + model.Player.OnGround.ToString(), new Vector2(0, 20), Color.White);

            spriteBatch.DrawString(bitmapMC,
                "Death count: " + model.Player.DeathCount.ToString(), new Vector2(0, 40), Color.White);

            spriteBatch.DrawString(bitmapMC,
                $"Stage: {LevelManager.CurrentLevel.Stage} Level: {LevelManager.CurrentLevel.Id}", new Vector2(0, 60), Color.White);

            DrawCollisions(_spriteBatch, model.Player);
            foreach(var entity in LevelManager.CurrentLevel.Entities)
                DrawCollisions(_spriteBatch, entity);
        }

        public void DrawCollisions(SpriteBatch spriteBatch, IEntity entity)
        {
            _spriteBatch.Draw(textures["player_collision"], entity.Collider, Color.White); // Collider
            foreach (var collisionRow in entity.CollisionTiles)
                foreach (var collision in collisionRow)
                    _spriteBatch.Draw(textures["collision"], CollisionManager.GetTileInGlobal(collision), Color.White); // CollisionTile
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
