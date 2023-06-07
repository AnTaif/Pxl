using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Pxl
{
    public enum State { Pause, Play, Menu}

    public class MainGame : Game
    {
        public static readonly Size RenderSize = new(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public static readonly Size WorkingSize = new(960, 540); //60, 34 in 16px tiles
        public static readonly string RootDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\.."));

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameModel model;
        private GameView view;
        private GameController controller;

        private Screen screen;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1650,
                PreferredBackBufferHeight = 850,
                IsFullScreen = false
            };
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            screen = new Screen(GraphicsDevice, WorkingSize);
            model = new GameModel();
            view = new GameView();
            controller = new GameController(model, view);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            view.LoadContent(spriteBatch, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            controller.Update(gameTime);

            model.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            screen.Set();
            GraphicsDevice.Clear(Color.Black);

            view.Update(gameTime);

            view.Draw(gameTime, model);

            screen.UnSet();
            screen.Present(spriteBatch);

            base.Draw(gameTime);
        }
    }
}