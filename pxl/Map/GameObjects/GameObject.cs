using Microsoft.Xna.Framework;

namespace Pxl
{
    public enum ObjectType { Empty, Ground, Platform, Spikes }

    public class GameObject
    {
        public Rectangle Bounds { get; private set; }
        public ObjectType Type { get; private set; }
        public ISprite Sprite { get; private set; }
        
        public GameObject(Rectangle bounds, ObjectType type)
        {
            Bounds = bounds;
            Type = type;
        }

        public void LoadSprite()
        {

        }
    }
}
