using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Pxl
{
    public class PlayerSprite : Sprite
    {
        public Texture2D Texture { get; private set; }

        // Animations
        private AnimationManager _animationManager;
        private Dictionary<string, Animation> _animations;

        private Vector2 direction = Vector2.UnitX;
        private float _timer;

        private Texture2D _currentTexture;

        public PlayerSprite()
        {
            _animationManager = new AnimationManager();
        }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Owlet/owlet");
            _currentTexture = Texture;

            _animationManager.PlayAnimation(new Animation(new List<Texture2D>() { Texture }, 0.2f));

            var rightIdleFrames = GameView.LoadContentFolder(content, "Owlet/idle/right");
            var leftIdleFrames = GameView.LoadContentFolder(content, "Owlet/idle/left");
            var rightWalkFrames = GameView.LoadContentFolder(content, "Owlet/walk/right");
            var leftWalkFrames = GameView.LoadContentFolder(content, "Owlet/walk/left");
            var rightJumpFrames = GameView.LoadContentFolder(content, "Owlet/jump/right");
            var leftJumpFrames = GameView.LoadContentFolder(content, "Owlet/jump/left");

            _animations = new Dictionary<string, Animation>()
            {
                { "idle/right", new Animation(rightIdleFrames,  0.2f) },
                { "idle/left", new Animation(leftIdleFrames,  0.2f) },
                { "walk/right", new Animation(rightWalkFrames, 0.1f) },
                { "walk/left", new Animation(leftWalkFrames, 0.1f) },
                { "jump/right", new Animation(rightJumpFrames, 0.3f) },
                { "jump/left", new Animation(leftJumpFrames, 0.3f) },
            };
        }

        public void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime);
        }

        public void PlayAnimation(string rootAnimationName, Vector2 inputDirection)
        {

            if (inputDirection.X != 0)
                direction = inputDirection;

            rootAnimationName = rootAnimationName.ToLower();
            var directedAnimationName = $"{rootAnimationName}/{GetDirectionName(direction)}";

            var anim = directedAnimationName;

            if (!_animations.ContainsKey(directedAnimationName))
            {
                anim = rootAnimationName;
                //if (!_animations.ContainsKey(rootAnimationName))
                //    throw new ArgumentException("Passed animation does not exist: " + rootAnimationName);
            }

            _animationManager.PlayAnimation(_animations[anim]);
        }

        private string GetDirectionName(Vector2 direction) => direction.X >= 0 ? "right" : "left";

        public void Draw(SpriteBatch spriteBatch, Vector2 position) => spriteBatch.Draw(_animationManager.GetCurrentFrame(), position, Color.White);
    }
}
