﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pxl
{
    public class MainGame : Game
    {
        public static readonly (int Width, int Height) RenderSize = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public static readonly (int Width, int Height) WorkingSize = (960, 540); //60, 34 in tiles
        public static readonly int TileSize = 16;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameModel _model;
        private GameView _view;
        private GameController _controller;

        private Screen _screen;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1650, //RenderSize.Width, //1440
                PreferredBackBufferHeight = 930, //RenderSize.Height, //810
                //IsFullScreen = true,
                SynchronizeWithVerticalRetrace = true
            };
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _screen = new Screen(GraphicsDevice, WorkingSize.Width, WorkingSize.Height);
            _model = new GameModel((_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
            _view = new GameView();
            _controller = new GameController(_model, _view);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _view.LoadContent(_spriteBatch, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _controller.Update(gameTime);
            _model.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _screen.Set();
            GraphicsDevice.Clear(Color.Black);

            _view.Draw(gameTime, _model);

            _screen.UnSet();
            _screen.Present(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}