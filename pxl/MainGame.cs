using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection;

namespace Pxl
{
    public class MainGame : Game
    {
        public readonly (int Width, int Height) ScreenSize = (1856, 1024);

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameModel model;
        private GameView view;
        private GameController _controller;

        public MainGame()
        {
            //var initialScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //var initialScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = ScreenSize.Width,
                PreferredBackBufferHeight = ScreenSize.Height
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            model = new GameModel((graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            _controller = new GameController(model);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            view = new GameView(spriteBatch);
            view.LoadContent(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _controller.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            view.Draw(model, gameTime);

            base.Draw(gameTime);
        }
    }
}