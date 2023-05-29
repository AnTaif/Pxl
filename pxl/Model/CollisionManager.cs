using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pxl
{
    public enum CollisionType { None, Solid, Spikes, Enemy}
    public enum CollisionDirection { All, Left, Top, Right, Bottom}

    public class CollisionInfo
    {
        public Rectangle InteractionTile { get; }
        public CollisionType Type { get; }
        public CollisionDirection Direction { get; }

        public CollisionInfo(CollisionType type, CollisionDirection direction)
        {
            Type = type;
            Direction = direction;
        }

        public CollisionInfo(CollisionType type, CollisionDirection direction, Rectangle tileInGlobal)
        {
            Type = type;
            Direction = direction;
            InteractionTile = tileInGlobal;
        }
    }

    public static class CollisionManager
    {
        private static Level _level;
        private static Tile[,] tileMap;
        private static List<Entity> _entities;

        public static List<List<Rectangle>> PlayerCollisions { get; private set; }

        public static void SetLevel(Level level)
        {
            _level = level;
            tileMap = _level.TileMap;
            _entities = _level.Entities;
        }

        public static void AddEntity(Entity entity) => _entities.Add(entity);

        public static void RemoveEntity(Entity entity) => _entities.Remove(entity);

        public static List<CollisionInfo> GetCollisionsWithLevel(IEntity entity)
        {
            var collisionsWithLevel = new List<CollisionInfo>();

            var direction = entity.Velocity;

            entity.UpdateCollisionTiles();

            direction.Normalize();

            if (direction.X != 0)
            {
                var collisionInfo = CheckHorizontalCollision(direction, entity.CollisionTiles);
                if (collisionInfo.Type != CollisionType.None)
                    collisionsWithLevel.Add(collisionInfo);
            }
   
            if (direction.Y != 0)
            {
                var collisionInfo = CheckVerticalCollision(direction, entity.CollisionTiles);
                collisionsWithLevel.Add(collisionInfo);
            }

            return collisionsWithLevel;
        }

        private static CollisionInfo CheckHorizontalCollision(Vector2 direction, List<List<Rectangle>> collisionTiles)
        {
            CollisionDirection collisionDirection;

            if (direction.X > 0)
                collisionDirection = CollisionDirection.Right;
            else
                collisionDirection = CollisionDirection.Left;

            var collisionType = CollisionType.None;
            var tileInGlobal = Rectangle.Empty;

            for (int i = 1; i < collisionTiles.Count - 1; i++)
            {
                var tile = direction.X > 0 ? collisionTiles[i].Last() : collisionTiles[i].First();

                if (!InCollisionBounds(tile))
                    continue;

                var currentCollision = tileMap[tile.Y, tile.X];
                
                if (currentCollision.CollisionType != CollisionType.None)
                {
                    if (TargetDirectionSameWithCollision(currentCollision.TargetDirection, collisionDirection))
                    {
                        tileInGlobal = GetTileInGlobal(tile);
                        if (currentCollision.CollisionType == CollisionType.Solid)
                        {
                            collisionType = CollisionType.Solid;
                            break;
                        }
                        else
                            collisionType = currentCollision.CollisionType;
                    }
                    else
                        collisionType = CollisionType.Solid;
                }
            }

            return new CollisionInfo(collisionType, collisionDirection, tileInGlobal);
        }

        private static CollisionInfo CheckVerticalCollision(Vector2 direction, List<List<Rectangle>> collisionTiles)
        {
            var collisionDirection = (direction.Y >= 0) ? CollisionDirection.Bottom : CollisionDirection.Top;
            var collisionsToCheck = (direction.Y >= 0) ? collisionTiles[collisionTiles.Count - 1] : collisionTiles[0];

            var collisionType = CollisionType.None;
            var tileInGlobal = Rectangle.Empty;

            foreach (var tile in collisionsToCheck)
            {
                if (!InCollisionBounds(tile))
                    continue;

                var currentCollision = tileMap[tile.Y, tile.X];
                var nextCollision = tileMap[tile.Y + ((direction.Y >= 0) ? -1 : 1), tile.X];

                if (currentCollision.CollisionType != CollisionType.None && nextCollision.CollisionType == CollisionType.None)
                {
                    tileInGlobal = GetTileInGlobal(tile);
                    if (TargetDirectionSameWithCollision(currentCollision.TargetDirection, collisionDirection))
                    {
                        if (currentCollision.CollisionType == CollisionType.Solid)
                        {
                            collisionType = CollisionType.Solid;
                            break;
                        }
                        else
                            collisionType = currentCollision.CollisionType;
                    }
                    else
                        collisionType = CollisionType.Solid;
                }
            }
            return new CollisionInfo(collisionType, collisionDirection, tileInGlobal);
        }

        private static bool TargetDirectionSameWithCollision(CollisionDirection target, CollisionDirection collision)
            => target == CollisionDirection.All || target == collision;

        public static List<List<Rectangle>> GetCollisionTiles(IEntity entity)
        {
            var collisionTiles = new List<List<Rectangle>>();
            var collider = entity.Collider;

            var tileSize = LevelManager.TileSet.TileSize;
            var colliderPos = new Point(collider.X / tileSize, collider.Y / tileSize);
            var rightColliderPos =
                new Point((int)Math.Ceiling((collider.X + collider.Width) / (float)tileSize), 
                (int)Math.Ceiling((collider.Y + collider.Height) / (float)tileSize));

            var tileCollider =
                (Width: (rightColliderPos.X - colliderPos.X),
                Height: (rightColliderPos.Y - colliderPos.Y));

            for (int j = 0; j < tileCollider.Height; j++)
            {
                collisionTiles.Add(new List<Rectangle>());
                for (int i = 0; i < tileCollider.Width; i++)
                {
                    var bounds = new Rectangle(colliderPos.X + i, colliderPos.Y + j, tileSize, tileSize);
                    collisionTiles[j].Add(bounds);
                }
            }

            return collisionTiles;
        }

        private static bool InCollisionBounds(Rectangle rect) 
            => rect.Y >= 0 && rect.Y < tileMap.GetLength(0) && rect.X >= 0 && rect.X < tileMap.GetLength(1);

        public static Rectangle GetTileInGlobal(Rectangle tile) 
            => new(tile.X * tile.Width, tile.Y * tile.Height, tile.Width, tile.Height);
    }
}
