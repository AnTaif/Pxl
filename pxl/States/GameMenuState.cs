﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pxl.States
{
    public class GameMenuState : MenuState
    {
        public GameMenuState(MainGame game) : base(game)
        {
            var resumeGameButton = new Button("Продолжить", new Rectangle(395, 200, 170, 40));
            resumeGameButton.Click += ResumeGameClick;

            var mainMenuButton = new Button("Главное Меню", new Rectangle(395, 300, 170, 40));
            mainMenuButton.Click += MainMenuClick;

            var quitGameButton = new Button("Выход", new Rectangle(395, 400, 170, 40));
            quitGameButton.Click += QuitGameClick;

            AddButton(resumeGameButton);
            AddButton(mainMenuButton);
            AddButton(quitGameButton);
        }

        public override void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            base.LoadContent(spriteBatch, content);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.IsPressedOnce(Keys.Escape))
                game.ChangeState(game.GameState);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
