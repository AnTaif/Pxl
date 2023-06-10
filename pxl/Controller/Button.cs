using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Pxl
{
    public class Button
    {
        private SpriteFont font;

        private bool isHovering;


        private Color defaultColor;
        private Color hoveringColor;
        private Color textColor;

        private Texture2D texture;

        public event EventHandler Click;

        public bool Clicked { get; private set; }
        
        public string Text { get; private set; }

        public Vector2 Position => Bounds.Location.ToVector2();

        public Rectangle Bounds { get; private set; }

        public Button(
            string text, Rectangle bounds,
            Color? textColor = null, Color? defaultColor = null, Color? hoveringColor = null
        ){
            Text = text;
            Bounds = bounds;

            this.textColor = textColor ?? Color.Black;
            this.defaultColor = defaultColor ?? Color.White;
            this.hoveringColor = hoveringColor ?? Color.Gray;
        }

        public void LoadContent(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = isHovering ? hoveringColor : defaultColor;

            spriteBatch.Draw(texture, Bounds, color);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Bounds.X + (Bounds.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Bounds.Y + (Bounds.Height / 2)) - (font.MeasureString(Text).Y / 2) - 2;

                spriteBatch.DrawString(font, Text, new Vector2(x, y), textColor);
            }
        }

        public void Update(GameTime gameTime)
        {
            var mouseRectangle = InputHandler.GetMouseRectangle();

            isHovering = false;

            if (mouseRectangle.Intersects(Bounds))
            {
                isHovering = true;

                if (InputHandler.Clicked())
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public void SetText(string text) => Text = text;

        public void SetPosition(Vector2 position) => Bounds = new ((int)position.X, (int)position.Y, Bounds.Width, Bounds.Height);
    }
}