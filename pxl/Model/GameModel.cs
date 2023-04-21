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
    public enum GameState { Menu, Play, GameOver, LoadLevel }

    public class GameModel
    {
        public (int Width, int Height) ScreenSize { get; set; }
        public Player Player { get; private set; }
        public Level Level { get; private set; }
        public GameState State { get; private set; }

        public GameModel((int Width, int Height) screenSize)
        {
            ScreenSize =
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            State = GameState.Menu;
            Level = new Level( new Rectangle(
                    0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 200,
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, 200)
                );
            Player = new Player(new Vector2(ScreenSize.Width / 2, ScreenSize.Height / 2), Level);
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
        }
    }
}
