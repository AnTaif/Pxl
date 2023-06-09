using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pxl
{
    public class MainMenuState : Menu
    {
        private Texture2D buttonTexture;
        private SpriteFont buttonFont;
        private SpriteBatch spriteBatch;

        public MainMenuState(MainGame game) : base(game)
        {
            var newGameButton = new Button("New Game", new Rectangle(300, 200, 400, 10));
            newGameButton.Click += NewGameClick;

            var quitGameButton = new Button("Quit Game", new Rectangle(300, 300, 400, 10));
            quitGameButton.Click += QuitGameClick;

            buttons.Add(newGameButton);
            buttons.Add(quitGameButton);
        }

        public override void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            this.spriteBatch = spriteBatch;

            buttonTexture = content.Load<Texture2D>("collision");
            buttonFont = content.Load<SpriteFont>("Fonts/BitmapMC");

            foreach(var button in buttons)
            {
                button.LoadContent(buttonTexture, buttonFont);
            }
        }

        public override void Update(GameTime gameTime)
        {
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
