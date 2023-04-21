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
    internal class View
    {
        private SpriteBatch _spriteBatch;
        private Texture2D playerTexture;
        private Texture2D groundTexture;

        public View(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Owlet_Monster");
            groundTexture = content.Load<Texture2D>("Ground");
        }

        public void Draw(GameModel model, GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(groundTexture, model.Level.Bounds, Color.White);
            _spriteBatch.Draw(playerTexture, model.Player.Position, Color.White);
            _spriteBatch.End();
        }
    }
}
