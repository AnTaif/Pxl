using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pxl.States;
using System;
using System.IO;

namespace Pxl
{
    public class MainGame : Game
    {
        public static readonly Size RenderSize = new(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public static readonly Size WorkingSize = new(960, 540); //60, 34 in 16px tiles
        public static readonly string RootDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\.."));

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public GameState GameState { get; private set; }
        public MainMenuState MainMenuState { get; private set; }
        public GameMenuState GameMenuState { get; private set; }
        public IState CurrentState { get; private set; }

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
            InputHandler.SetScreen(screen);

            GameState = new GameState(this);
            MainMenuState = new MainMenuState(this);
            GameMenuState = new GameMenuState(this);

            CurrentState = MainMenuState;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameState.LoadContent(spriteBatch, Content);
            MainMenuState.LoadContent(spriteBatch, Content);
            GameMenuState.LoadContent(spriteBatch, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            InputHandler.UpdateState();

            CurrentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            screen.Set();
            GraphicsDevice.Clear(Color.Black);

            CurrentState.Draw(gameTime);

            screen.UnSet();
            screen.Present(spriteBatch);

            base.Draw(gameTime);
        }

        public void ChangeState(IState state) => CurrentState = state;
    }
}