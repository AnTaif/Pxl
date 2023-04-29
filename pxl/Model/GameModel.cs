using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public enum GameState { Menu, Play, Pause, GameOver }

    public class GameModel
    {
        public readonly (int Width, int Height) Screen;
        public readonly Map Map;
        
        public Player Player { get; private set; }
        public GameState State { get; private set; }

        public GameModel((int Width, int Height) screenSize)
        {
            Screen = screenSize;
            State = GameState.Play;
            Map = new Map();
            Player = new Player(new Vector2(200, Screen.Height - 300), Map.CurrentLevel);
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
            if (Player.Position.X >= Screen.Width)
            {
                Map.SetNextLevel();
                Console.WriteLine($"Floor: {Map.CurrentLevel.Floor} Id: { Map.CurrentLevel.Id}");
                Player.UpdateLevel(Map.CurrentLevel);
            }
        }
    }
}
