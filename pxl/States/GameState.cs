using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class GameState : IState
    {
        private MainGame game;

        private GameModel model;
        private GameView view;
        private GameController controller;

        public GameState(MainGame game)
        {
            this.game = game;

            model = new GameModel();
            view = new GameView();
            controller = new GameController(model, view);
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            view.LoadContent(spriteBatch, content);
        }

        public void Update(GameTime gameTime)
        {
            if (InputHandler.IsPressedOnce(Keys.Escape))
                game.ChangeState(game.GameMenuState);

            controller.Update(gameTime);
            model.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            view.Update(gameTime);
            view.Draw(gameTime, model);
        }
    }
}
