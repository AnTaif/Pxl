using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Pxl
{
    static class InputHandler
    {
        private static KeyboardState state = Keyboard.GetState();

        public static void UpdateState() => state = Keyboard.GetState();

        public static Vector2 GetMoveDirection()
        {
            var direction = Vector2.Zero;
            if (IsRightPress())
            {
                direction += new Vector2(1,0);
            }
            if (IsLeftPress())
            {
                direction -= new Vector2(1, 0);
            }
            return direction;
        }

        public static bool IsJumpPress() => state.IsKeyDown(Keys.Space);
        public static bool IsRightPress() => state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D);
        public static bool IsLeftPress() => state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A);
    }
}
