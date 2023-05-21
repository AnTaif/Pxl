using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class GroundSprite : ISprite
    {
        private Rectangle bounds;
        private Texture2D groundTexture;
        private Texture2D fillTexture;

        public GroundSprite(Rectangle bounds, Texture2D groundTexture, Texture2D fillTexture)
        {
            this.bounds = bounds;
            this.groundTexture = groundTexture;
            this.fillTexture = fillTexture;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            var tileSize = groundTexture.Width;
            var tileXCount = bounds.Width / tileSize;
            var tileYCount = bounds.Height / tileSize;

            var ground = true;
            for (int j = 0; j < tileYCount; j++)
            {
                for (int i = 0; i < tileXCount; i++)
                {
                    if (ground)
                    {
                        spriteBatch.Draw(groundTexture, position, Color.White);
                    }
                    else
                        spriteBatch.Draw(fillTexture, position, Color.White);
                }
                ground = false;
            }
        }
    }
}
