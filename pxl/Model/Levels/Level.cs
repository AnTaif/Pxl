using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pxl
{
    public class Level
    {
        public readonly int Stage;
        public readonly int Id;
        public readonly Point Size;

        public int[,] StationaryMap { get; private set; }
        public List<IGameObject> MovingObjects { get; private set; }
        public List<IEntity> Entities { get; private set; }
        public Point SpawnPoint { get; private set; }

        public Tile[,] TileMap { get; private set; }

        [JsonConstructor]
        public Level(
            int stage, int id,
            Point size, 
            int[,] stationaryMap, 
            List<IGameObject> movingObjects,
            List<IEntity> entities,
            Point spawnPoint
        ) {
            Stage = stage;
            Id = id;
            Size = size;
            StationaryMap = stationaryMap;
            MovingObjects = movingObjects;
            Entities = entities;
            SpawnPoint = spawnPoint;

            TileMap = ConvertToCollisionMap(StationaryMap);
        }

        public Tile[,] ConvertToCollisionMap(int[,] stationaryMap)
        {
            var height = stationaryMap.GetLength(0);
            var width = stationaryMap.GetLength(1);
            Tile[,] collisionMap = new Tile[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var tileId = stationaryMap[i, j];
                    collisionMap[i, j] = LevelManager.TileSet.Tiles[tileId];
                }
            }

            return collisionMap;
        }
    }
}
