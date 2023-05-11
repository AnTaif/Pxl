using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pxl
{
    public class Animation
    {
        private List<Texture2D> frames;
        private int currentFrame;

        public float AnimationSpeed { get; private set; }

        public Animation(List<Texture2D> frames, float speed)
        {
            this.frames = frames;
            currentFrame = 0;
            AnimationSpeed = speed;
        }

        public void NextFrame()
        {
            if (++currentFrame == frames.Count)
                currentFrame = 0;
        }

        public void Reset() => currentFrame = 0;

        public Texture2D GetCurrentFrame() => frames[currentFrame];
    }
}
