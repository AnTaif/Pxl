using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pxl
{
    public class GameController
    {
        private readonly GameModel _model;

        public GameController(GameModel model)
        {
            _model = model;
        }

        public void Update(GameTime gameTime)
        {
            InputHandler.UpdateState();

            _model.Update(gameTime);
        }
    }
}
