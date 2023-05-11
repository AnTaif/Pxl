using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pxl
{
    public class AnimationManager
    {
        private Animation _animation;
        private float _timer;

        public Vector2 Position { get; set; }
        public Texture2D CurrentFrame { get; private set; }

        public AnimationManager()
        {

        }

        public void PlayAnimation(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;
            Reset();
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.AnimationSpeed)
            {
                _timer = 0;
                _animation.NextFrame();
            }
        }

        public void Reset()
        {
            _timer = 0;
            _animation.Reset();
        }

        public Texture2D GetCurrentFrame()
        {
            CurrentFrame = _animation.GetCurrentFrame();
            return CurrentFrame;
        }
    }
}
