using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxl.States
{
    public class GameMenuState : IState
    {
        private Texture2D buttonTexture;
        private SpriteFont buttonFont;
        private SpriteBatch spriteBatch;

        private List<Button> buttons;

        private MainGame game;

        public GameMenuState(MainGame game)
        {
            this.game = game;

            var resumeGameButton = new Button("Resume Game", new Rectangle(300, 200, 400, 10));
            resumeGameButton.Click += ResumeGameClick;

            var quitGameButton = new Button("Quit Game", new Rectangle(300, 300, 400, 10));
            quitGameButton.Click += QuitGameClick;

            buttons = new List<Button>()
            {
                resumeGameButton,
                quitGameButton,
            };
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            this.spriteBatch = spriteBatch;

            buttonTexture = content.Load<Texture2D>("collision");
            buttonFont = content.Load<SpriteFont>("Fonts/BitmapMC");

            foreach (var button in buttons)
            {
                button.LoadContent(buttonTexture, buttonFont);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var component in buttons)
                component.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));

            foreach (var button in buttons)
                button.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void ResumeGameClick(object sender, EventArgs e)
        {
            game.ChangeState(game.GameState);
        }

        private void QuitGameClick(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
}
