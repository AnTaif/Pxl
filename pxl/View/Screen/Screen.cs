using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Size = System.Drawing.Size;

namespace Pxl
{
    public sealed class Screen : IDisposable
    {
        private readonly static int MinDim = 64;
        private readonly static int MaxDim = 4096;

        public Size Size { get; private set; }

        private bool isDisposed;
        private GraphicsDevice _graphics;
        private RenderTarget2D _target;
        private bool isSet;

        public Screen(GraphicsDevice graphics, Size size)
        {
            Size = new(MathHelper.Clamp(size.Width, MinDim, MaxDim), MathHelper.Clamp(size.Height, MinDim, MaxDim));

            _graphics = graphics;

            _target = new RenderTarget2D(_graphics, Size.Width, Size.Height);
            isSet = false;
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            _target?.Dispose();
            isDisposed = true;
        }

        public void Set()
        {
            if (isSet)
            {
                throw new ArgumentException("Render target is already set");
            }

            _graphics.SetRenderTarget(_target);
            isSet = true;
        }

        public void UnSet()
        {
            if (!isSet)
            {
                throw new ArgumentException("Render target is not set");
            }

            _graphics.SetRenderTarget(null);
            isSet = false;
        }

        public void Present(SpriteBatch spriteBatch)
        {
            var destinationRectangle = CalculateDestinationRectangle();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));
            spriteBatch.Draw(_target, destinationRectangle, Color.White);
            spriteBatch.End();
        }

        private Rectangle CalculateDestinationRectangle()
        {
            var backbufferBounds = _graphics.PresentationParameters.Bounds;
            var backbufferAspectRatio = (float)backbufferBounds.Width / backbufferBounds.Height;
            var screenAspectRatio = (float)Size.Width / Size.Height;

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
    }
}
