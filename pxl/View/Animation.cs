using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pxl
{
    public class Animation
    {
        private List<Texture2D> frames;

        public int FrameNumber { get; private set; }
        public float AnimationSpeed { get; private set; }
        public string RootName { get; private set; }

        public Animation(List<Texture2D> frames, float speed, string rootName)
        {
            this.frames = frames;
            FrameNumber = 0;
            RootName = rootName;
            AnimationSpeed = speed;
        }

        public void NextFrame()
        {
            if (++FrameNumber == frames.Count)
                FrameNumber = 0;
        }

        public void Reset() => FrameNumber = 0;

        public void SetFrameNumber(int n) => FrameNumber = n;

        public Texture2D GetCurrentFrame() => frames[FrameNumber];
    }
}
