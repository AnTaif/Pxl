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
        private static KeyboardState previousState;

        public static KeyboardState GetState() => state;
        public static KeyboardState GetPreviousState() => previousState;

        public static void UpdateState()
        {
            previousState = state;
            state = Keyboard.GetState();
        }

        public static bool IsPressedOnce(Keys key) => state.IsKeyDown(key) && !previousState.IsKeyDown(key);

        public static Vector2 GetMoveDirection()
        {
            var direction = Vector2.Zero;

            if (IsRightPress())
                direction += new Vector2(1, 0);
            //direction.Normalize();

            if (IsLeftPress())
                direction -= new Vector2(1, 0);
            //direction.Normalize();

            return direction;
        }

        public static bool IsJumpPress() => state.IsKeyDown(Keys.Space);
        public static bool IsRightPress() => state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D);
        public static bool IsLeftPress() => state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A);
    }
}
