using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pxl
{
    public class Cloud : Sprite
    {
        public readonly string RootName;

        public Vector2 Position;
        public Point Size;
        private float speed;

        public Cloud(string name, Vector2 position, Point size, float speed)
        {
            RootName = $"background/{name}";
            Position = position;
            Size = size;
            this.speed = speed;
        }

        public void ReCreate(Vector2 position)
        {
            Position = position;
        }

        public void ReCreate(Vector2 position, Point size, float speed)
        {
            Position = position;
            Size = size;
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            var xPos = Position.X - speed;
            Position = new Vector2(xPos, Position.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>(RootName);
        }
    }
}
