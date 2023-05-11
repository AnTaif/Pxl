using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl.Model.Entity
{
    public class Animation
    {
        private List<Texture2D> frames;
        private int currentFrame;
        private float timer;
        private float interval = 100f;


        public Animation(List<Texture2D> frames)
        {
            this.frames = frames;
            currentFrame = 0;
            timer = 0f;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame >= frames.Count)
                {
                    currentFrame = 0;
                }
                timer = 0f;
            }
        }

        public Texture2D GetCurrentFrame() => frames[currentFrame];
    }
}
