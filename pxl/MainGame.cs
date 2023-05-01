using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection;

namespace Pxl
{
    public class MainGame : Game
    {
        public static readonly (int Width, int Height) RenderSize = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public static readonly (int Width, int Height) WorkingSize = (960, 540);

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameModel _model;
        private GameView _view;

        private Screen _screen;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1440,//RenderSize.Width,
                PreferredBackBufferHeight = 810,//RenderSize.Height,
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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _view = new GameView(_spriteBatch);
            _view.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.UpdateState();
            _model.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _screen.Set();
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1f));
            _view.Draw(gameTime, _model);
            _spriteBatch.End();

            _screen.UnSet();
            _screen.Present(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}