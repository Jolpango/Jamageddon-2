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
        public event Action<UIButton> OnClick;
        public UIButton(Texture2D texture = null)
        {
            this.texture = texture ?? JTextureCache.White;
        }

        public override void Update(GameTime gameTime, JMouseInput mouseInput, JKeyboardInput keyboardInput)
        {
            //Debug.WriteLine($"mouse: {mouseInput.Position.X}, {mouseInput.Position.Y}");
            //Debug.WriteLine($"rec: {BoundingBox.X}, {BoundingBox.Y}, {BoundingBox.Width}, {BoundingBox.Height}");
            if (IsMouseOver(mouseInput.Position) && mouseInput.IsLeftButtonClicked())
            {
                OnClick?.Invoke(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingBox, Color.White);
        }
    }
}
