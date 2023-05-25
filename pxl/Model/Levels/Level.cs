using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pxl
{
    public class Level
    {
        public readonly int TileSize = MainGame.TileSize;
        public readonly int Id;
        public readonly int Stage;
        public readonly (int Width, int Height) Size;

        public Vector2 SpawnPoint { get; private set; }
        public List<IEntity> Entities { get; private set; }
        public List<IGameObject> GameObjects { get; private set; }
        public CollisionType[,] CollisionMap { get; private set; }

        public Level(
            int currentStage, int currentId,
            (int Width, int Height) size, 
            List<IGameObject> gameObjects, List<IEntity> entities,
            Vector2? spawnPoint = null
        ) {
            Stage = currentStage;
            Id = currentId;
            Size = size;

            SpawnPoint = spawnPoint ?? Vector2.Zero;
            Entities = entities;
            GameObjects = gameObjects;
            CollisionMap = ConvertToCollisionMap(GameObjects);
        }

        public CollisionType[,] ConvertToCollisionMap(List<IGameObject> gameObjects)
        {
            var tileHeight = Size.Height / TileSize;
            var tileWidth = Size.Width / TileSize;
            CollisionType[,] collisionMap = new CollisionType[tileHeight, tileWidth];

            foreach (var gameObject in gameObjects)
            {
                var rect = gameObject.Bounds;
                int startX = rect.X / TileSize;
                int startY = rect.Y / TileSize;
                int endX = (rect.X + rect.Width) / TileSize;
                int endY = (rect.Y + rect.Height) / TileSize;

                for (int i = startY; i < endY; i++)
                    for (int j = startX; j < endX; j++)
                    {
                        if (i >= 0 && i < tileHeight && j >= 0 && j < tileWidth && gameObject.CollisionType != CollisionType.None)
                            collisionMap[i, j] = gameObject.CollisionType;
                    } 
            }

            return collisionMap;
        }
    }
}
