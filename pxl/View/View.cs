using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using pxl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pxl.View
{
    internal class View
    {
        private SpriteBatch _spriteBatch;

        public View(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void Draw(GameModel model, GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(model.Player.Texture, model.Level.Bounds, Color.White);
            _spriteBatch.Draw(model.Player.Texture, model.Player.Position, Color.White);
            _spriteBatch.End();
        }
    }
}
