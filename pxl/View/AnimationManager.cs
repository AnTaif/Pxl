using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pxl
{
    public class AnimationManager
    {
        public Animation CurrentAnimation { get; private set; }
        private float _timer;

        public Vector2 Position { get; set; }
        public Texture2D CurrentFrame { get; private set; }

        public AnimationManager()
        {

        }

        public void PlayAnimation(Animation animation, string rootAnimationName)
        {
            if (CurrentAnimation == animation)
                return;

            if (CurrentAnimation?.RootName.Equals(rootAnimationName) ?? false)
            {
                 animation.SetFrameNumber(CurrentAnimation.FrameNumber);
                CurrentAnimation = animation;
            } else
            {
                CurrentAnimation = animation;
                Reset();
            }

        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > CurrentAnimation.AnimationSpeed)
            {
                _timer = 0;
                CurrentAnimation.NextFrame();
            }
        }

        public void Reset()
        {
            _timer = 0;
            CurrentAnimation.Reset();
        }

        public Texture2D GetCurrentFrame()
        {
            CurrentFrame = CurrentAnimation.GetCurrentFrame();
            return CurrentFrame;
        }
    }
}
