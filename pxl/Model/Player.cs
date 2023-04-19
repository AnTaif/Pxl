using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace pxl.Model
{
    public class Player
    {
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }

        public Player(Vector2 position, float speed)
        {
            Position = position;
            Speed = speed;
        }

        public void Move(Vector2 delta)
        {
            Position += Speed * delta;
        }

    }
}
