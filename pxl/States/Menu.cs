using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public abstract class Menu : IState
    {
        protected MainGame game;
        protected List<Button> buttons;

        public Menu(MainGame game)
        {
            this.game = game;
            buttons = new List<Button>();
        }

        public abstract void LoadContent(SpriteBatch spriteBatch, ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        protected void NewGameClick(object sender, EventArgs e) => game.NewGame();

        protected void ResumeGameClick(object sender, EventArgs e) => game.ChangeState(game.GameState);

        protected void MainMenuClick(object sender, EventArgs e) => game.ChangeState(game.MainMenuState);

        protected void QuitGameClick(object sender, EventArgs e) => game.Exit();
    }
}
