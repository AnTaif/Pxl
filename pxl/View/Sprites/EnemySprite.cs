using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Pxl
{
    public class EnemySprite : AnimatedSprite
    {
        public EnemySprite(string rootName) : base(rootName)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            var rootTexture = content.Load<Texture2D>($"{rootName}/{Path.GetFileName(rootName.ToLower())}");

            animationManager.PlayAnimation(new Animation(new List<Texture2D>() { rootTexture }, 0.2f, "default"), "default");

            var rightWalkFrames = GameView.LoadContentFolder(content, $"{rootName}/walk/right");
            var leftWalkFrames = GameView.LoadContentFolder(content, $"{rootName}/walk/left");

            animations = new Dictionary<string, Animation>()
            {
                { "walk/right", new Animation(rightWalkFrames, 0.1f, "walk") },
                { "walk/left", new Animation(leftWalkFrames, 0.1f, "walk") },
            };
        }
    }
}
