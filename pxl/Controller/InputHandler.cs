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
        private static MouseState currentMouseState = Mouse.GetState();
        private static MouseState previousMouseState;

        private static KeyboardState currentKeyboardState = Keyboard.GetState();
        private static KeyboardState previousKeyboardState;

        private static Screen currentScreen;

        public static KeyboardState GetState() => currentKeyboardState;
        public static KeyboardState GetPreviousState() => previousKeyboardState;

        public static void UpdateState()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public static bool IsPressedOnce(Keys key) => currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);

        public static Vector2 GetMoveDirection()
        {
            var direction = Vector2.Zero;

            if (IsRightPress())
                direction += new Vector2(1, 0);

            if (IsLeftPress())
                direction -= new Vector2(1, 0);

            return direction;
        }

        public static bool IsJumpPress() => currentKeyboardState.IsKeyDown(Keys.Space);
        public static bool IsRightPress() => currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D);
        public static bool IsLeftPress() => currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A);

        public static void SetScreen(Screen screen)
        {
            currentScreen = screen;
        }

        public static Rectangle GetMouseRectangle()
        {
            var mouseScreenPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            var mouseWorldPosition = currentScreen.ConvertScreenPositionToWorld(mouseScreenPosition);

            return new Rectangle((int)mouseWorldPosition.X, (int)mouseWorldPosition.Y, 1, 1);
        }

        public static bool Clicked() 
            => currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed;
    }
}
