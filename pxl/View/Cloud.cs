using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class Cloud
    {
        public readonly Texture2D Texture;
        public Vector2 Position;
        public Point Size;
        private float speed;

        public Cloud(Texture2D texture, Vector2 position, Point size, float speed)
        {
            Texture = texture;
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

        public void Update(GameTime gameTime)
        {
            var xPos = Position.X - speed;
            Position = new Vector2(xPos, Position.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
