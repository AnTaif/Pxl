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
        public Rectangle Bounds { get; private set; }
        public readonly Texture2D Texture;
        private float speed;

        public Cloud(Texture2D texture, Rectangle bounds, float speed)
        {
            Texture = texture;
            Bounds = bounds;
            this.speed = speed;
        }

        public void ReCreate(int xPos, int yPos)
        {
            Bounds = new Rectangle(xPos, yPos, Bounds.Width, Bounds.Height);
        }

        public void Update(GameTime gameTime)
        {
            var xPos = (int)(Bounds.X - speed);
            Bounds = new Rectangle(xPos, Bounds.Y, Bounds.Width, Bounds.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }
    }
}
