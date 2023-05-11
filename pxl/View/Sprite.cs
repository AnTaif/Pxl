using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public interface Sprite
    {
        public Texture2D Texture { get; }

        public void LoadContent(ContentManager content);
    }
}
