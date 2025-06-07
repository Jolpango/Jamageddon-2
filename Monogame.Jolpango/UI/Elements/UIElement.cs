using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public abstract class UIElement
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 MinSize { get; set; } = Vector2.Zero;
        public Color BackgroundColor { get; set; } = Color.Transparent;
        public Texture2D BackgroundTexture { get; set; } = JTextureCache.White;
        public Vector2 EffectiveSize => new Vector2(
            Math.Max(Size.X, MinSize.X),
            Math.Max(Size.Y, MinSize.Y)
        );
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

        public Rectangle BoundingBox => new Rectangle(GlobalPosition.ToPoint(), new Vector2(Math.Max(Size.X, MinSize.X), Math.Max(Size.Y, MinSize.Y)).ToPoint());

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
            spriteBatch.Draw(BackgroundTexture, BoundingBox, BackgroundColor);
        }
    }
}
