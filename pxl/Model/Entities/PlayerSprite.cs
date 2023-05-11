using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pxl.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class PlayerSprite : Sprite
    {
        public Texture2D Texture { get; private set; }

        // Animations
        private Animation idleAnimation;
        private Animation walkAnimation;
        private Animation jumpAnimation;

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Owlet/owlet");

            var walkFrames = new List<Texture2D>()
            {
                content.Load<Texture2D>("owlet/walk/walk1"),
                content.Load<Texture2D>("owlet/walk/walk2"),
                content.Load<Texture2D>("owlet/walk/walk3"),
                content.Load<Texture2D>("owlet/walk/walk4"),
                content.Load<Texture2D>("owlet/walk/walk5"),
                content.Load<Texture2D>("owlet/walk/walk6")
            };
            walkAnimation = new Animation(walkFrames);
        }
    }
}
