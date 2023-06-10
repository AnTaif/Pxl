using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Pxl
{
    public class PlayerSprite : AnimatedSprite
    {
        public PlayerSprite(string rootName) : base(rootName)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            var rootTexture = content.Load<Texture2D>($"{rootName}/{rootName.ToLower()}");

            var rightIdleFrames = GameView.LoadContentFolder(content, $"{rootName}/idle/right");
            var leftIdleFrames = GameView.LoadContentFolder(content, $"{rootName}/idle/left");
            var rightWalkFrames = GameView.LoadContentFolder(content, $"{rootName}/walk/right");
            var leftWalkFrames = GameView.LoadContentFolder(content, $"{rootName}/walk/left");
            var rightJumpFrames = GameView.LoadContentFolder(content, $"{rootName}/jump/right");
            var leftJumpFrames = GameView.LoadContentFolder(content, $"{rootName}/jump/left");

            animations = new Dictionary<string, Animation>()
            {
                {"default",  new Animation(new List<Texture2D>() { rootTexture }, 0.2f, "default")},
                { "idle/right", new Animation(rightIdleFrames,  0.2f, "idle") },
                { "idle/left", new Animation(leftIdleFrames,  0.2f, "idle") },
                { "walk/right", new Animation(rightWalkFrames, 0.1f, "walk") },
                { "walk/left", new Animation(leftWalkFrames, 0.1f, "walk") },
                { "jump/right", new Animation(rightJumpFrames, 0.235f, "jump") },
                { "jump/left", new Animation(leftJumpFrames, 0.235f, "jump") },
            };

            animationManager.PlayAnimation(animations["default"], "default");
        }
    }
}
