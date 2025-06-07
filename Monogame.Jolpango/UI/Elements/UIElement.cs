using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.UI.Elements
{
    public abstract class UIElement
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;
        public UIElement Parent { get; set; } = null;
        public event Action<UIElement> OnClick;

        public Vector2 GlobalPosition
        {
            get
            {
                return Parent != null ? Parent.GlobalPosition + Position : Position;
            }
        }

        public Rectangle BoundingBox => new Rectangle(GlobalPosition.ToPoint(), Size.ToPoint());

        public virtual bool IsMouseOver(Vector2 mousePosition)
        {
            return BoundingBox.Contains(mousePosition);
        }
        public virtual void LoadContent() { }
        public virtual void Update(GameTime gameTime, JMouseInput mouseInput, JKeyboardInput keyboardInput)
        {
            if (IsMouseOver(mouseInput.Position) && mouseInput.IsLeftButtonClicked())
            {
                OnClick?.Invoke(this);
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
