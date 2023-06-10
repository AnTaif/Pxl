using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pxl
{
    public class Screen
    {
        private readonly static int MinDim = 64;
        private readonly static int MaxDim = 4096;

        public Size WorldSize { get; private set; }

        private GraphicsDevice graphics;
        private RenderTarget2D target;

        public Screen(GraphicsDevice graphics, Size size)
        {
            WorldSize = new(MathHelper.Clamp(size.Width, MinDim, MaxDim), MathHelper.Clamp(size.Height, MinDim, MaxDim));

            this.graphics = graphics;

            target = new RenderTarget2D(this.graphics, WorldSize.Width, WorldSize.Height);
        }

        public void Set() => graphics.SetRenderTarget(target);

        public void UnSet() => graphics.SetRenderTarget(null);

        public void Present(SpriteBatch spriteBatch)
        {
            var destinationRectangle = CalculateDestinationRectangle();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));
            spriteBatch.Draw(target, destinationRectangle, Color.White);
            spriteBatch.End();
        }

        private Rectangle CalculateDestinationRectangle()
        {
            var backbufferBounds = graphics.PresentationParameters.Bounds;
            var backbufferAspectRatio = (float)backbufferBounds.Width / backbufferBounds.Height;
            var screenAspectRatio = (float)WorldSize.Width / WorldSize.Height;

            var rx = 0f;
            var ry = 0f;
            var rw = (float)backbufferBounds.Width;
            var rh = (float)backbufferBounds.Height;

            if (backbufferAspectRatio > screenAspectRatio)
            {
                rw = rh * screenAspectRatio;
                rx = (backbufferBounds.Width - rw) / 2f;
            } 
            else if (backbufferAspectRatio < screenAspectRatio)
            {
                rh = rw / screenAspectRatio;
                ry = (backbufferBounds.Height - rh) / 2f;
            }
             
            return new Rectangle((int)rx, (int)ry, (int)rw, (int)rh);
        }

        public Vector2 ConvertScreenPositionToWorld(Vector2 screenPosition)
        {
            var destinationRect = CalculateDestinationRectangle();

            var scaleX = (float)destinationRect.Width / WorldSize.Width;
            var scaleY = (float)destinationRect.Height / WorldSize.Height;

            var worldX = (screenPosition.X - destinationRect.X) / scaleX;
            var worldY = (screenPosition.Y - destinationRect.Y) / scaleY;

            return new Vector2(worldX, worldY);
        }
    }
}
