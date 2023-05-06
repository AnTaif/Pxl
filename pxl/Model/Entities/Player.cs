        public void HandleCollisionsWithLevel()
        {
            UpdateCollisionTiles();

            CheckHorizontalCollision();
            if (velocity.Y > 0)
                CheckBottomCollision();
            else if (velocity.Y < 0)
                CheckTopCollision();
        }

        public void CheckHorizontalCollision()
        {
            for (int i = 1; i < CollisionTiles.Count - 1; i++)
            {
                var tile = velocity.X > 0 ? CollisionTiles[i].Last() : CollisionTiles[i].First();
                if (!level.InCollisionBounds(tile))
                    continue;
                if (level.CollisionMap[tile.Y, tile.X] == 1)
                {
                    velocity.X = velocity.X > 0 ? -6 : 6;
                }
            }
        }

        public void CheckTopCollision()
        {
            foreach (var tile in CollisionTiles[0])
            {
                if (!level.InCollisionBounds(tile))
                    continue;
                if (level.CollisionMap[tile.Y, tile.X] == 1 && level.CollisionMap[tile.Y+1, tile.X] != 1)
                {
                    var tileInGlobal = ConvertToGlobal(tile);

                    //Position = new Vector2(Position.X, tileInGlobal.Bottom-1);
                    velocity.Y = 0;
                    onGround = false;
                    return;
                }
            }
        }

        public void CheckBottomCollision()
        {

            foreach (var tile in CollisionTiles[CollisionTiles.Count - 1])
            {
                if (!level.InCollisionBounds(tile))
                    continue;
                if (level.CollisionMap[tile.Y, tile.X] == 1 && level.CollisionMap[tile.Y-1, tile.X] != 1)
                {
                    var tileInGlobal = ConvertToGlobal(tile);

                    if (Position.Y + Size.Height - tileInGlobal.Y > 2) continue;

                    Position = new Vector2(Position.X, tileInGlobal.Y - Size.Height + 1);
                    velocity.Y = 0;
                    onGround = true;
                    return;
                }
            }
            onGround = false;
        }
