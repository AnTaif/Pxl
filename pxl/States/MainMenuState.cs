using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pxl
{
    public class MainMenuState : MenuState
    {
        public MainMenuState(MainGame game) : base(game)
        {
            var newGameButton = new Button("Новая Игра", new Rectangle(395, 200, 170, 40));
            newGameButton.Click += NewGameClick;

            var quitGameButton = new Button("Выход", new Rectangle(395, 300, 170, 40));
            quitGameButton.Click += QuitGameClick;

            AddButton(newGameButton);
            AddButton(quitGameButton);
        }

        public override void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            base.LoadContent(spriteBatch, content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
