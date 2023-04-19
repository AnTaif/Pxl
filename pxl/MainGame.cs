using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using pxl.Model;
using pxl.View;
using System.Reflection;

namespace Pxl
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameModel _model;
        private View _view;

        private Texture2D playerTexture;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            var player =
                new Player(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), 200f);
            var level =
                new Level(new Rectangle(0, 0, 300, 400));
            _model = new GameModel(player, level);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _model.Player.Texture = Content.Load<Texture2D>("Owlet_Monster");
            //_model.Player.Texture = playerTexture;

            _view = new View(_spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left))
            {
                _model.Player.Move(new Vector2(-(float)gameTime.ElapsedGameTime.TotalSeconds, 0));
            }
            if (state.IsKeyDown(Keys.Right))
            {
                _model.Player.Move(new Vector2((float)gameTime.ElapsedGameTime.TotalSeconds, 0));
            }
            if (state.IsKeyDown(Keys.Up))
            {
                _model.Player.Move(new Vector2(0, -(float)gameTime.ElapsedGameTime.TotalSeconds));
            }
            if (state.IsKeyDown(Keys.Down))
            {
                _model.Player.Move(new Vector2(0, (float)gameTime.ElapsedGameTime.TotalSeconds));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _view.Draw(_model, gameTime);

            base.Draw(gameTime);
        }
    }
}