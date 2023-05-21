using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class PlatformSprite : ISprite
    {
        private Rectangle bounds;
        private Texture2D texture;

        public PlatformSprite(Rectangle bounds, Texture2D texture)
        {
            this.bounds = bounds;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            var tileSize = texture.Width;
            var tileXCount = bounds.Width / tileSize;
            var tileYCount = bounds.Height / tileSize;

            for (int j = 0; j < tileYCount; j++)
            {
                for (int i = 0; i < tileXCount; i++)
                {
                    var tilePosition = new Vector2(position.X + i * tileSize, position.Y + j * tileSize);
                    spriteBatch.Draw(texture, tilePosition, Color.White);
                }
            }
        }
    }
}
