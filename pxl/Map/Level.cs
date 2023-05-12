using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pxl
{
    public class Level
    {
        public readonly int TileSize = 16;
        public readonly int Id;
        public readonly int Floor;
        public readonly (int Width, int Height) Size;

        public Vector2 SpawnPos { get; private set; }
        public List<Entity> Entities { get; private set; }
        public List<Tile> Tiles { get; private set; }
        public CollisionType[,] CollisionMap { get; private set; }

        public Level(int currentFloor, int currentId, (int Width, int Height) size, List<Tile> tiles, Vector2 spawn)
        {
            Floor = currentFloor;
            Id = currentId;
            Size = size;

            SpawnPos = spawn;
            Tiles = tiles;
            CollisionMap = ConvertToCollisionMap(Tiles);
        }

        public CollisionType[,] ConvertToCollisionMap(List<Tile> tiles)
        {
            var tileHeight = Size.Height / TileSize;
            var tileWidth = Size.Width / TileSize;
            CollisionType[,] collisionMap = new CollisionType[tileHeight, tileWidth];

            foreach (var tile in tiles)
            {
                var rect = tile.Bounds;
                int startX = rect.X / TileSize;
                int startY = rect.Y / TileSize;
                int endX = (rect.X + rect.Width) / TileSize;
                int endY = (rect.Y + rect.Height) / TileSize;

                for (int i = startY; i < endY; i++)
                    for (int j = startX; j < endX; j++)
                    {
                        if (i >= 0 && i < tileHeight && j >= 0 && j < tileWidth && tile.Type != TileType.Empty)
                            collisionMap[i, j] = tile.Type == TileType.Spikes ? CollisionType.Spikes : CollisionType.Solid;
                    }
                        
            }

            return collisionMap;
        }

        public bool InCollisionBounds(Rectangle rect)
        {
            return rect.Y >= 0 && rect.Y < CollisionMap.GetLength(0) && rect.X >= 0 && rect.X < CollisionMap.GetLength(1);
        }
    }
}
