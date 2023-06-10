using System;

namespace Pxl
{
    public static class SpriteFactory
    {
        public static IAnimatedSprite CreateAnimatedSprite(ICreature creature) => creature switch
        {
            (Snake) => new EnemySprite("Enemies/snake"),

            (Mushroom) => new EnemySprite("Enemies/mushroom"),

            _ => throw new ArgumentException()
        };
    }
}
