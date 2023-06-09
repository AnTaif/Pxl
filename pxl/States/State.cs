using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pxl
{
    public interface IState
    {
        public void LoadContent(SpriteBatch spriteBatch, ContentManager content);
        public void Update(GameTime gameTime);
        public void Draw(GameTime gameTime);
    }
}
