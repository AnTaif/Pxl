using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Pxl
{
    public abstract class MenuState : IState
    {
        protected MainGame game;
        protected List<Button> buttons;
        protected Texture2D buttonTexture;
        protected SpriteFont buttonFont;
        protected SpriteBatch spriteBatch;

        public MenuState(MainGame game)
        {
            this.game = game;
            buttons = new List<Button>();
        }

        public virtual void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            this.spriteBatch = spriteBatch;

            buttonTexture = content.Load<Texture2D>("button");
            buttonFont = content.Load<SpriteFont>("Fonts/BitmapMC");

            foreach (var button in buttons)
            {
                button.LoadContent(buttonTexture, buttonFont);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var button in buttons)
                button.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (var button in buttons)
                button.Draw(gameTime, spriteBatch);
        }

        protected void AddButton(Button button)
        {
            buttons.Add(button);
        }

        protected void NewGameClick(object sender, EventArgs e) => game.NewGame();

        protected void ResumeGameClick(object sender, EventArgs e) => game.ChangeState(game.GameState);

        protected void MainMenuClick(object sender, EventArgs e) => game.ChangeState(game.MainMenuState);

        protected void QuitGameClick(object sender, EventArgs e) => game.Exit();
    }
}
