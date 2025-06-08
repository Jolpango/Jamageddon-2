using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.UI.Elements
{
    public class TextElement : UIElement
    {
        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public Color Color { get; set; } = Color.Black;

        public override void LoadContent()
        {
            RecalculateSize();
            base.LoadContent();
        }
        public override void Update(GameTime gameTime, JMouseInput mouseInput, JKeyboardInput keyboardInput)
        {
            RecalculateSize();
            base.Update(gameTime, mouseInput, keyboardInput);
        }
        public void RecalculateSize()
        {
            Size = Font.MeasureString(Text);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(Font, Text, GlobalPosition, Color);
        }
    }
}
