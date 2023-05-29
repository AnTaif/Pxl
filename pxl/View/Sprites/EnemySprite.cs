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
    public class EnemySprite : ISprite
    {
        private Texture2D texture;

        public EnemySprite()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("collision");
        }

        public void Update(GameTime gameTime)
        {
            return;
        }
    }
}
