using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pxl
{
    public class TileSet
    {
        public Dictionary<int, Tile> Tiles { get; private set; }
        public int TileSize { get; private set; }

        [JsonConstructor]
        public TileSet(Dictionary<int, Tile> tiles, int tileSize)
        {
            Tiles = tiles;
            TileSize = tileSize;
        }
    }
}
