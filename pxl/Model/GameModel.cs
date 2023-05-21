using Microsoft.Xna.Framework;
using System;

namespace Pxl
{
    public enum GameState { Menu, Play, Pause, GameOver }

    public class GameModel
    {
        public readonly (int Width, int Height) Screen;
        public readonly LevelManager Map;
        
        public Player Player { get; private set; }
        public GameState State { get; private set; }

        public GameModel((int Width, int Height) screenSize)
        {
            Screen = screenSize;
            State = GameState.Play;
            Map = new LevelManager();
            Player = new Player(Map.CurrentLevel.SpawnPos);

            CollisionManager.SetLevel(Map.CurrentLevel);
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);

            if (Player.Position.X >= Map.CurrentLevel.Size.Width)
            {
                Map.SetNextLevel();
                CollisionManager.SetLevel(Map.CurrentLevel);
                Player.UpdatePosition(new Vector2(0, Player.Position.Y - 4));
                Player.SpawnPos = Map.CurrentLevel.SpawnPos;
            }
        }
    }
}
