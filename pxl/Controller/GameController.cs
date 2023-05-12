﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pxl
{
    public class GameController
    {
        private readonly GameModel _model;
        private readonly GameView _view;

        public GameController(GameModel model, GameView view)
        {
            _model = model;
            _view = view;
        }

        public void Update(GameTime gameTime)
        {
            InputHandler.UpdateState();

            var inputDirection = InputHandler.GetMoveDirection();
            _model.Player.ApplyHorizontalMove(inputDirection);

            if (_model.Player.OnGround)
            {
                if (inputDirection.X != 0)
                {
                    _view.PlayerSprite.PlayAnimation("walk", inputDirection);
                }
                else 
                    _view.PlayerSprite.PlayAnimation("idle", inputDirection);
            }

            if (InputHandler.IsJumpPress())
            {
                _model.Player.Jump();
                _view.PlayerSprite.PlayAnimation("jump", inputDirection);
            }

            if (InputHandler.IsPressedOnce(Keys.F3))
                _view.isDebugShowing = !_view.isDebugShowing;
        }
    }
}