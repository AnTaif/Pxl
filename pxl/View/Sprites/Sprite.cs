using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pxl
{
    public abstract class Sprite : ISprite
    {
        public virtual Texture2D Texture { get; protected set; }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, Color.White);
        }
    }

    public interface ISprite
    {
        public void LoadContent(ContentManager content);
        public void Draw(SpriteBatch spriteBatch, Vector2 position);
        public void Update(GameTime gameTime);
    }
}
