using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pxl
{
    public abstract class AnimatedSprite : Sprite, IAnimatedSprite
    {
        protected readonly string rootName;

        public override Texture2D Texture => animationManager.GetCurrentFrame();

        protected AnimationManager animationManager;
        protected Dictionary<string, Animation> animations;

        protected Vector2 direction = Vector2.UnitX;

        public AnimatedSprite(string rootName)
        {
            this.rootName = rootName;
            animationManager = new AnimationManager();
        }

        public override void Update(GameTime gameTime)
        {
            animationManager.Update(gameTime);
        }

        public void PlayAnimation(string rootAnimationName, Vector2 inputDirection)
        {
            if (inputDirection.X != 0)
                direction = inputDirection;

            rootAnimationName = rootAnimationName.ToLower();
            var directedAnimationName = $"{rootAnimationName}/{GetDirectionName(direction)}";

            var anim = directedAnimationName;

            if (!animations.ContainsKey(directedAnimationName))
            {
                anim = rootAnimationName;
            }

            animationManager.PlayAnimation(animations[anim], rootAnimationName);
        }

        public void ChangeSpriteDirection(Vector2 inputDirection)
            => PlayAnimation(animationManager.CurrentAnimation.RootName, inputDirection);

        public Animation GetCurrentAnimation() => animationManager.CurrentAnimation;

        protected string GetDirectionName(Vector2 direction) => direction.X > 0 ? "right" : "left";
    }

    public interface IAnimatedSprite : ISprite
    {
        public void PlayAnimation(string rootAnimationName, Vector2 inputDirection);
    }
}
