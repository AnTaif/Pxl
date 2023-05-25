using Microsoft.Xna.Framework;
using System;

namespace Pxl
{
    public enum GameState { Menu, Play, Pause, GameOver }

    public class GameModel
    {
        public readonly (int Width, int Height) Screen;
        
        public Player Player { get; private set; }
        public GameState State { get; private set; }

        public GameModel((int Width, int Height) screenSize)
        {
            Screen = screenSize;
            State = GameState.Play;
            LevelManager.LoadMapFromFile("TestMap");
            LevelManager.LoadLevels();
            Player = new Player(new RectangleF(LevelManager.CurrentLevel.SpawnPoint, new Vector2(28, 35)));

            CollisionManager.SetLevel(LevelManager.CurrentLevel);
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);

            if (Player.Bounds.X >= LevelManager.CurrentLevel.Size.Width)
            {
                LevelManager.SetNextLevel();
                CollisionManager.SetLevel(LevelManager.CurrentLevel);
                Player.UpdatePosition(new Vector2(0, Player.Bounds.Y - 4));
                Player.SetSpawn(LevelManager.CurrentLevel.SpawnPoint);
            }
        }
    }
}
