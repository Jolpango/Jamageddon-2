using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.UI.Elements
{
    public class UIButton : UIElement
    {
        private Texture2D texture;
        public Color Color { get; set; } = Color.White;
        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public Color TextColor { get; set; } = Color.Black;

        public UIButton(Texture2D texture = null)
        {
            this.texture = texture ?? JTextureCache.White;
        }

        public override void LoadContent()
        {
            if (Font != null && !string.IsNullOrEmpty(Text))
            {
                var textSize = Font.MeasureString(Text);
                // Ensure button is at least as big as the text
                Size = new Vector2(
                    Math.Max(Size.X, textSize.X + 20), // Add padding
                    Math.Max(Size.Y, textSize.Y + 10)  // Add padding
                );
            }
            base.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, BoundingBox, Color);
            base.Draw(spriteBatch);
            // Draw button background
            spriteBatch.Draw(texture, BoundingBox, Color);

            // Draw text if available
            if (Font != null && !string.IsNullOrEmpty(Text))
            {
                var textSize = Font.MeasureString(Text);
                var textPosition = new Vector2(
                    GlobalPosition.X + (Size.X - textSize.X) / 2,
                    GlobalPosition.Y + (Size.Y - textSize.Y) / 2
                );
                spriteBatch.DrawString(Font, Text, textPosition, TextColor);
            }
        }
    }
}
