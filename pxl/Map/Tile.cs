using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public enum TileType { Empty, Ground, Platform, Spikes }

    public class Tile
    {
        public Vector2 Position { get; private set; }
        public Rectangle Bounds { get; private set; }
        public TileType Type { get; private set; }


        public Tile(Rectangle bounds, TileType type)
        {
            Position = new Vector2(bounds.X, bounds.Y);
            Bounds = bounds;
            Type = type;
        }
    }
}
