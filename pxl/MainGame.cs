using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection;

namespace Pxl
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private GameModel model;
        private View view;

        public MainGame()
        {
            //var initialScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //var initialScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            model = new GameModel((graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            view = new View(spriteBatch);
            view.LoadContent(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.UpdateState();
            model.Update(gameTime);

            //if (inputHandler.isRightPress) model.Player.Move(new Vector2((float)gameTime.ElapsedGameTime.TotalSeconds, 0));
            //if (inputHandler.isLeftPress) model.Player.Move(new Vector2(-(float)gameTime.ElapsedGameTime.TotalSeconds, 0));
            //if (inputHandler.isDownPress) model.Player.Move(new Vector2(0, (float)gameTime.ElapsedGameTime.TotalSeconds));
            //if (inputHandler.isUpPress) model.Player.Move(new Vector2(0, -(float)gameTime.ElapsedGameTime.TotalSeconds));

            //if (State.IsKeyDown(Keys.Left) || State.IsKeyDown(Keys.A))
            //{
            //    model.Player.Move(new Vector2(-(float)gameTime.ElapsedGameTime.TotalSeconds, 0));
            //}
            //if (State.IsKeyDown(Keys.Right) || State.IsKeyDown(Keys.D))
            //{
            //    model.Player.Move(new Vector2((float)gameTime.ElapsedGameTime.TotalSeconds, 0));
            //}
            //if (State.IsKeyDown(Keys.Up) || State.IsKeyDown(Keys.W))
            //{
            //    model.Player.Move(new Vector2(0, -(float)gameTime.ElapsedGameTime.TotalSeconds));
            //}
            //if ((State.IsKeyDown(Keys.Down) || State.IsKeyDown(Keys.S)) && !model.Player.CollidesWithLevel(model.Level))
            //{
            //   model.Player.Move(new Vector2(0, (float)gameTime.ElapsedGameTime.TotalSeconds));
            //}

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