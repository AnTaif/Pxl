using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    internal class GameView
    {
        private SpriteBatch _spriteBatch;
        private Texture2D playerTexture;
        private Texture2D groundTexture;
        private Texture2D collisionTexture;
        private Texture2D playerColliderTexture;

        public GameView(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Owlet_Monster");
            groundTexture = content.Load<Texture2D>("Ground");
            collisionTexture = content.Load<Texture2D>("collision");
            playerColliderTexture = content.Load<Texture2D>("player_collision");
        }

        public void Draw(GameModel model, GameTime gameTime)
        {
            var currentLevel = model.Map.CurrentLevel;

            _spriteBatch.Begin();
            foreach (var tile in currentLevel.Tiles)
            {
                _spriteBatch.Draw(groundTexture, tile.Bounds, Color.White);
            }
            _spriteBatch.Draw(playerTexture, model.Player.Position, Color.White);
            
            // TEMPORARY
            //_spriteBatch.Draw(playerColliderTexture, model.Player.HitBox, Color.White); // HitBox
            //foreach (var collision in model.Player.GetCollisionTilesInGlobal())
            //{
            //    _spriteBatch.Draw(collisionTexture, collision, Color.White); // CollisionTile
            //}
            // TEMPORARY

            _spriteBatch.End();
        }
    }
}
