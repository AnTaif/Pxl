using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        private PlayerSprite playerSprite;
       

        public GameView()
        {
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            _spriteBatch = spriteBatch;

            background = new Background(_spriteBatch);
            playerSprite = new PlayerSprite();

            playerSprite.LoadContent(content);

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

            _spriteBatch.Draw(playerSprite.Texture, model.Player.Position, Color.White);

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
    }
}
