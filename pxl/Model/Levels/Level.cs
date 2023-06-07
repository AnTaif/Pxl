using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Pxl
{
    public class Level
    {
        public readonly int Stage;
        public readonly int Id;
        public readonly Size Size;

        public int[,] StationaryMap { get; private set; }
        public List<Entity> Entities { get; private set; }
        public Point SpawnPoint { get; private set; }

        public Tile[,] TileMap { get; private set; }

        [JsonConstructor]
        public Level(
            int stage, int id,
            Size size, 
            int[,] stationaryMap, 
            List<Entity> entities,
            Point spawnPoint
        ) {
            Stage = stage;
            Id = id;
            Size = size;
            StationaryMap = stationaryMap;
            Entities = entities;
            SpawnPoint = spawnPoint;

            TileMap = ConvertToCollisionMap(StationaryMap);
        }

        public void Update(GameTime gameTime)
        {
            UpdateEntities(gameTime);
        }

        public void UpdateEntities(GameTime gameTime)
        {
            Entities.ForEach(entity => entity.Update(gameTime));
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
