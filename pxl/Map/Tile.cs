using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public enum TileType { Empty, Solid, Spikes }

    public class Tile
    {
        public Rectangle Bounds { get; private set; }
        public TileType Type { get; private set; }


        public Tile(Rectangle bounds, TileType type)
        {
            Bounds = bounds;
            Type = type;
        }
    }
}
