using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl.States
{
    public class GameMenuState : Menu
    {
        private Texture2D buttonTexture;
        private SpriteFont buttonFont;
        private SpriteBatch spriteBatch;

        public GameMenuState(MainGame game) : base(game)
        {
            var resumeGameButton = new Button("Resume Game", new Rectangle(300, 200, 400, 10));
            resumeGameButton.Click += ResumeGameClick;

            var mainMenuButton = new Button("Main Menu", new Rectangle(300, 300, 400, 10));
            mainMenuButton.Click += MainMenuClick;

            var quitGameButton = new Button("Quit Game", new Rectangle(300, 400, 400, 10));
            quitGameButton.Click += QuitGameClick;

            buttons.Add(resumeGameButton);
            buttons.Add(mainMenuButton);
            buttons.Add(quitGameButton);
        }

        public override void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            this.spriteBatch = spriteBatch;

            buttonTexture = content.Load<Texture2D>("collision");
            buttonFont = content.Load<SpriteFont>("Fonts/BitmapMC");

            foreach (var button in buttons)
            {
                button.LoadContent(buttonTexture, buttonFont);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.IsPressedOnce(Keys.Escape))
                game.ChangeState(game.GameState);

            foreach (var button in buttons)
                button.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));

            foreach (var button in buttons)
                button.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
