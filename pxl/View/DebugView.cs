using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pxl
{
    public class DebugView : IGameView
    {
        private SpriteBatch spriteBatch;

        private SpriteFont font;
        private Dictionary<string, Texture2D> textures;

        public void Draw(GameTime gameTime, GameModel model)
        {
            spriteBatch.DrawString(font,
                model.Player.Bounds.Position.ToString(), new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(font,
                "On ground: " + model.Player.OnGround.ToString(), new Vector2(0, 20), Color.White);

            spriteBatch.DrawString(font,
                "Death count: " + model.Player.DeathCount.ToString(), new Vector2(0, 40), Color.White);

            spriteBatch.DrawString(font,
                $"Stage: {LevelManager.CurrentLevel.Stage} Level: {LevelManager.CurrentLevel.Id}", new Vector2(0, 60), Color.White);

            DrawCollisions(spriteBatch, model.Player);
            foreach (var entity in LevelManager.CurrentLevel.Entities)
                if (entity.IsAlive)
                    DrawCollisions(spriteBatch, entity);
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            this.spriteBatch = spriteBatch;

            font = content.Load<SpriteFont>("Fonts/BitmapMC");

            textures = new Dictionary<string, Texture2D>()
            {
                {"collision", content.Load<Texture2D>("collision") },
                {"player_collision", content.Load<Texture2D>("player_collision") },
            };
        }

        private void DrawCollisions(SpriteBatch spriteBatch, Entity entity)
        {
            spriteBatch.Draw(textures["player_collision"], entity.Collider, Color.White); // Collider
            foreach (var collisionRow in entity.CollisionTiles)
                foreach (var collision in collisionRow)
                    spriteBatch.Draw(textures["collision"], CollisionManager.GetTileInGlobal(collision), Color.White); // CollisionTile
        }
    }
}
