using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public interface ISprite
    {
        public void LoadContent(ContentManager content);
        public void Draw(SpriteBatch spriteBatch, Vector2 position);
        public void Update(GameTime gameTime);
    }

    public static class SpriteFactory
    {
        public static ISprite CreateSprite(IGameObject gameObject, Dictionary<string, Texture2D> textures)
        {
            return gameObject switch
            {
                //(Platform) => new PlatformSprite(gameObject.Bounds, textures["ground"]),

                _ => throw new NotImplementedException("Error when getting sprite"),
            };
        }

        public static ISprite CreateSprite(Entity entity, Dictionary<string, Texture2D> textures)
        {
            return entity switch
            {
                (WalkingEnemy) => new PlayerSprite("Owlet"),

                _ => new EnemySprite(),
            };
        }
    }
}
