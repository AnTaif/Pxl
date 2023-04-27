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

        public Level(int currentFloor, int currentId)
        {
            Floor = currentFloor;
            Id = currentId;
            Size = (1856, 1024);

            Tiles = new List<Tile>
            {
                new Tile(new Rectangle(0, 864, 1920, 192), TileType.Ground), // Ground
                new Tile(new Rectangle(320, 752, 240, 32), TileType.Platform), // Platform
                new Tile(new Rectangle(1760, 760, 160, 320), TileType.Ground), // Wall
            };
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
                {
                    for (int j = startX; j < endX; j++)
                    {
                        if (i >= 0 && i < tileHeight && j >= 0 && j < tileWidth)
                        {
                            collisionMap[i, j] = 1;
                        }
                    }
                }
            }

            //for(int i = 0; i < Size.Height/ TileSize; i++)
            //{
            //    for (int j = 0; j < Size.Width/ TileSize; j++)
            //    {
            //        Console.Write(collisionMap[i, j]);
            //    }
            //    Console.WriteLine();
            //}

            return collisionMap;
        }
    }
}
