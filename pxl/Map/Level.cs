using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class Level
    {
        public readonly int TileSize = 16;
        public readonly int Id;
        public readonly int Floor;
        public readonly (int Width, int Height) Size;
            
        public List<Tile> Tiles { get; set; }
        public int[,] CollisionMap { get; set; }

        public Level(int currentFloor, int currentId, (int Width, int Height) size, List<Tile> tiles)
        {
            Floor = currentFloor;
            Id = currentId;
            Size = size;

            Tiles = tiles;
            CollisionMap = ConvertToCollisionMap(Tiles);
        }

        public int[,] ConvertToCollisionMap(List<Tile> tiles)
        {
            var tileHeight = Size.Height / TileSize;
            var tileWidth = Size.Width / TileSize;
            int[,] collisionMap = new int[tileHeight, tileWidth];

            foreach (var tile in tiles)
            {
                var rect = tile.Bounds;
                int startX = rect.X / TileSize;
                int startY = rect.Y / TileSize;
                int endX = (rect.X + rect.Width) / TileSize;
                int endY = (rect.Y + rect.Height) / TileSize;

                for (int i = startY; i < endY; i++)
                    for (int j = startX; j < endX; j++)
                        if (i >= 0 && i < tileHeight && j >= 0 && j < tileWidth)
                            collisionMap[i, j] = 1;
            }

            return collisionMap;
        }

        public bool InCollisionBounds(Rectangle rect)
        {
            return rect.Y >= 0 && rect.Y < CollisionMap.GetLength(0) && rect.X >= 0 && rect.X < CollisionMap.GetLength(1);
        }
    }
}
