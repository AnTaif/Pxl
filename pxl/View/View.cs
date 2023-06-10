using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Pxl
{
    public class GameView : IGameView
    {
        private ContentManager content;

        public bool IsDebugShowing { get; set; }
        private DebugView debugView;

        private SpriteBatch spriteBatch;
        private Background background;

        private Dictionary<string, Texture2D> textures;
        private SpriteFont font;

        private Dictionary<ICreature, IAnimatedSprite> creatureSprites;
        public PlayerSprite PlayerSprite { get; }

        public GameView()
        {
            PlayerSprite = new PlayerSprite("Owlet");
            creatureSprites = new Dictionary<ICreature, IAnimatedSprite>();
            textures = new Dictionary<string, Texture2D>();
            debugView = new DebugView();
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;

            background = new Background(spriteBatch);

            PlayerSprite.LoadContent(content);

            font = content.Load<SpriteFont>("Fonts/BitmapMC");

            debugView.LoadContent(spriteBatch, content);

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

        public void Update(GameTime gameTime)
        {
            background.Update(gameTime);
            PlayerSprite.Update(gameTime);
            UpdateCreatures(gameTime);
        }

        public void UpdateCreatures(GameTime gameTime)
        {
            foreach(var entityItem in creatureSprites)
            {
                var entity = entityItem.Key;
                var entitySprite = entityItem.Value;

                entitySprite.Update(gameTime);
                entitySprite.PlayAnimation("walk", entity.Direction);
            }
        }

        public void Draw(GameTime gameTime, GameModel model)
        {
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

                    spriteBatch.Draw(texture, position, Color.White);
                }
            }

            DrawCreatures(gameTime);

            PlayerSprite.Draw(spriteBatch, model.Player.Bounds.Position);

            if (IsDebugShowing)
                debugView.Draw(gameTime, model);
        }

        private void DrawCreatures(GameTime gameTime)
        {
            foreach (var creature in LevelManager.CurrentLevel.Creatures)
            {
                if (!creature.IsAlive)
                    continue;

                var sprite = GetOrCreateSprite(creature);

                sprite.Draw(spriteBatch, creature.Bounds.Position);
            }
        }

        private IAnimatedSprite GetOrCreateSprite(ICreature creature)
        {
            if (creatureSprites.ContainsKey(creature))
                return creatureSprites[creature];

            creatureSprites[creature] = SpriteFactory.CreateAnimatedSprite(creature);
            creatureSprites[creature].LoadContent(content);
            return creatureSprites[creature];
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

    public interface IGameView
    {
        public void LoadContent(SpriteBatch spriteBatch, ContentManager content);
        public void Draw(GameTime gameTime, GameModel model);
    }
}
