using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class TileSet
    {
        public Dictionary<int, Tile> Tiles { get; set; }
        public int TileSize { get; set; }
    }

    public class Tile
    {
        public string Name { get; set; }
        public CollisionType CollisionType { get; set; }
    }
}
