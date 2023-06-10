using Microsoft.Xna.Framework;
using System;

namespace Pxl
{
    public class GameModel
    {
        public static readonly float Gravity = 23;

        public Player Player { get; private set; }

        public GameModel()
        {
            LevelManager.LoadMap("TestMap");
            Player = new Player(new RectangleF(LevelManager.CurrentLevel.SpawnPoint.ToVector2(), new Vector2(28, 35)));

            CollisionManager.SetLevel(LevelManager.CurrentLevel);
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);

            LevelManager.CurrentLevel.Update(gameTime);

            if (Player.Bounds.X >= LevelManager.CurrentLevel.Size.Width)
            {
                SetNextLevel();
            }
        }

        private void SetNextLevel()
        {
            LevelManager.SetNextLevel();
            CollisionManager.SetLevel(LevelManager.CurrentLevel);
            Player.UpdatePosition(new Vector2(0, Player.Bounds.Y - 4));
            Player.SetSpawn(LevelManager.CurrentLevel.SpawnPoint);
        }
    }
}
