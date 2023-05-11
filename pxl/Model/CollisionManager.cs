using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pxl
{
    public enum CollisionType { None, Solid, Spikes, Enemy}
    public enum CollisionDirection { Left, Top, Right, Bottom}

    public class CollisionInfo
    {
        public Tile InteractionTile { get; }
        public CollisionType Type { get; }
        public CollisionDirection Direction { get; }

        public CollisionInfo(CollisionType type, CollisionDirection direction)
        {
            Type = type;
            Direction = direction;
        }

        public CollisionInfo(CollisionType type, CollisionDirection direction, Tile tileInGlobal)
        {
            Type = type;
            Direction = direction;
            InteractionTile = tileInGlobal;
        }
    }

    public static class CollisionManager
    {
        private static Level _level;
        private static int[,] _collisionMap;
        private static List<Entity> _entities;
        private static List<List<Tile>> _playerCollisions;

        public static void SetLevel(Level level)
        {
            _level = level;
            _collisionMap = _level.CollisionMap;
            _entities = _level.Entities;

        }

        public static void AddEntity(Entity entity) => _entities.Add(entity);

        public static void RemoveEntity(Entity entity) => _entities.Remove(entity);

        public static List<CollisionInfo> GetCollisionsWithLevel(Player player)
        {
            var collisionsWithLevel = new List<CollisionInfo>();

            var direction = player.Velocity;
            UpdatePlayerCollisionTiles(player);

            direction.Normalize();

            if (direction.X != 0)
            {
                var collisionInfo = CheckHorizontalCollision(direction, _playerCollisions);
                if (collisionInfo.Type != CollisionType.None)
                    collisionsWithLevel.Add(collisionInfo);
            }
   
            if (direction.Y != 0)
            {
                var collisionInfo = CheckVerticalCollision(direction, _playerCollisions);
                collisionsWithLevel.Add(collisionInfo);
            }

            return collisionsWithLevel;
        }

        private static CollisionInfo CheckHorizontalCollision(Vector2 direction, List<List<Tile>> collisionTiles)
        {
            CollisionDirection collisionDirection;

            if (direction.X > 0)
            {
                collisionDirection = CollisionDirection.Right;
            }
            else
            {
                collisionDirection = CollisionDirection.Left;
            }

            for (int i = 1; i < collisionTiles.Count - 1; i++)
            {
                var tile = direction.X > 0 ? collisionTiles[i].Last() : collisionTiles[i].First();
                if (!InCollisionBounds(tile.Bounds))
                    continue;
                if (_collisionMap[tile.Bounds.Y, tile.Bounds.X] == 1)
                {
                    return new CollisionInfo(CollisionType.Solid, collisionDirection, GetTileInGlobal(tile));
                }
            }
            return new CollisionInfo(CollisionType.None, collisionDirection);
        }

        private static CollisionInfo CheckVerticalCollision(Vector2 direction, List<List<Tile>> collisionTiles)
        {
            CollisionDirection collisionDirection;
            List<Tile> collisionsToCheck;

            if (direction.Y >= 0)
            {
                collisionDirection = CollisionDirection.Bottom;
                collisionsToCheck = collisionTiles[collisionTiles.Count - 1];
            }
            else
            {
                collisionDirection = CollisionDirection.Top;
                collisionsToCheck = collisionTiles[0];
            }

            foreach(var tile in collisionsToCheck)
            {
                if (!InCollisionBounds(tile.Bounds))
                    continue;

                if (_collisionMap[tile.Bounds.Y, tile.Bounds.X] == 1 &&
                    _collisionMap[tile.Bounds.Y + ((direction.Y >= 0) ? -1 : 1), tile.Bounds.X] != 1)
                {
                    return new CollisionInfo(CollisionType.Solid, collisionDirection, GetTileInGlobal(tile));
                }
            }
            return new CollisionInfo(CollisionType.None, collisionDirection);
        }

        private static List<List<Tile>> GetCollisionTiles(Player player)
        {
            var collisionTiles = new List<List<Tile>>();
            var collider = player.Collider;

            var tileSize = _level.TileSize;
            var colliderPos = new Point(collider.X / tileSize, collider.Y / tileSize);
            var rightColliderPos =
                new Point((int)Math.Ceiling((collider.X + collider.Width) / (float)tileSize), 
                (int)Math.Ceiling((collider.Y + collider.Height) / (float)tileSize));

            var tileCollider =
                (Width: (rightColliderPos.X - colliderPos.X),
                Height: (rightColliderPos.Y - colliderPos.Y));

            for (int j = 0; j < tileCollider.Height; j++)
            {
                collisionTiles.Add(new List<Tile>());
                for (int i = 0; i < tileCollider.Width; i++)
                {
                    var bounds = new Rectangle(colliderPos.X + i, colliderPos.Y + j, _level.TileSize, _level.TileSize);
                    collisionTiles[j].Add(new Tile(bounds, TileType.Empty));
                }
            }

            return collisionTiles;
        }

        private static void UpdatePlayerCollisionTiles(Player player)
        {
            _playerCollisions = GetCollisionTiles(player);
        }

        private static bool InCollisionBounds(Rectangle rect)
        {
            return rect.Y >= 0 && rect.Y < _collisionMap.GetLength(0) && rect.X >= 0 && rect.X < _collisionMap.GetLength(1);
        }

        private static Tile GetTileInGlobal(Tile tile)
        {
            var bounds = new Rectangle(
                    tile.Bounds.X * tile.Bounds.Width, 
                    tile.Bounds.Y * tile.Bounds.Height, 
                    tile.Bounds.Width, 
                    tile.Bounds.Height
                );
            return new Tile(bounds, tile.Type);
        }
    }
}
