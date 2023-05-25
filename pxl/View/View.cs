using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;

namespace Pxl
{
    public class GameView
    {
        public bool isDebugShowing { get; set; }

        private SpriteBatch _spriteBatch;
        private Background background;

        private Dictionary<string, Texture2D> textures;
        private SpriteFont bitmapMC;

        private Dictionary<IGameObject, ISprite> levelSprites;
        private Dictionary<IEntity, ISprite> entitySprites;
        public PlayerSprite PlayerSprite { get; }

        public GameView()
        {
            PlayerSprite = new PlayerSprite("Owlet");
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            _spriteBatch = spriteBatch;

            background = new Background(_spriteBatch);

            PlayerSprite.LoadContent(content);

            bitmapMC = content.Load<SpriteFont>("Fonts/BitmapMC");

            textures = new Dictionary<string, Texture2D>()
            {
                {"ground", content.Load<Texture2D>("ground") },
                {"collision", content.Load<Texture2D>("collision") },
                {"player_collision", content.Load<Texture2D>("player_collision") },
                {"mountain_fill", content.Load<Texture2D>("mountain_fill") },
                {"spikes", content.Load<Texture2D>("spikes") },
            };

            levelSprites = new Dictionary<IGameObject, ISprite>();
            entitySprites = new Dictionary<IEntity, ISprite>();

            background.LoadContent(content);
        }

        public void Draw(GameTime gameTime, GameModel model)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));
            var currentLevel = LevelManager.CurrentLevel;

            background.Draw(gameTime);

            foreach (var gameObject in currentLevel.GameObjects)
            {
                var sprite = GetOrCreateSprite(gameObject);

                sprite.Draw(_spriteBatch, gameObject.Position);
            }

            PlayerSprite.Update(gameTime);
            PlayerSprite.Draw(_spriteBatch, model.Player.Bounds.Position);

            if (isDebugShowing)
                ShowDebug(_spriteBatch, model);

            _spriteBatch.End();
        }

        public ISprite GetOrCreateSprite(IGameObject gameObject)
        {
            if (levelSprites.ContainsKey(gameObject))
                return levelSprites[gameObject];

            levelSprites[gameObject] = SpriteFactory.CreateSprite(gameObject, textures);
            return levelSprites[gameObject];
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
        }

        public void DrawCollisions(SpriteBatch spriteBatch, IEntity entity)
        {
            _spriteBatch.Draw(textures["player_collision"], entity.Collider, Color.White); // Collider
            foreach (var collisionRow in entity.Collisions)
                foreach(var collision in collisionRow)
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
