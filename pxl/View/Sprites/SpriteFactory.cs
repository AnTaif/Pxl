using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Pxl
{
    public static class SpriteFactory
    {
        public static IAnimatedSprite CreateAnimatedSprite(IEntity entity) => entity switch
        {
            (Snake) => new EnemySprite("Enemies/snake"),

            (Mushroom) => new EnemySprite("Enemies/mushroom"),

            _ => throw new ArgumentException()
        };
    }
}
