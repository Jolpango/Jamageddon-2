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
        public Texture2D BackgroundTexture { get; set; } = null;
        public string Name { get; set; } = "UIElement";
        public float BorderThickness { get; set; } = 0f;
        public Color BorderColor { get; set; } = Color.Transparent;
        public Vector2 EffectiveSize => new Vector2(
            Math.Max(Size.X, MinSize.X),
            Math.Max(Size.Y, MinSize.Y)
        );
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;
        public UIElement Parent { get; set; } = null;


        private bool isMouseOver = false;
        public event Action<UIElement> OnMouseEnter;
        public event Action<UIElement> OnMouseLeave;
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
            if (!IsVisible || !IsEnabled) return false;
            return BoundingBox.Contains(mousePosition);
        }
        public virtual void LoadContent() { }
        public virtual void Update(GameTime gameTime, JMouseInput mouseInput, JKeyboardInput keyboardInput)
        {
            if(!IsEnabled) return;
            if (IsMouseOver(mouseInput.Position) && mouseInput.IsLeftButtonClicked())
            {
                OnClick?.Invoke(this);
            }
            if (!isMouseOver && IsMouseOver(mouseInput.Position))
            {
                isMouseOver = true;
                OnMouseEnter?.Invoke(this);
            }
            else if (isMouseOver && !IsMouseOver(mouseInput.Position))
            {
                isMouseOver = false;
                OnMouseLeave?.Invoke(this);
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!IsVisible) return;
            DrawBackgroundTexture(spriteBatch);
            DrawBorder(spriteBatch);
            DrawBackgroundColor(spriteBatch);
        }

        private void DrawBackgroundTexture(SpriteBatch spriteBatch)
        {
            if (BackgroundTexture is not null)
                spriteBatch.Draw(BackgroundTexture, BoundingBox, Color.White);
        }

        private void DrawBackgroundColor(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(JTextureCache.White, BoundingBox, BackgroundColor);
        }

        private void DrawBorder(SpriteBatch spriteBatch)
        {
            if (BorderColor != Color.Transparent && BorderThickness > 0)
            {
                var rect = BoundingBox;
                int t = (int)BorderThickness;
                // Top border
                spriteBatch.Draw(JTextureCache.White, new Rectangle(rect.X - t, rect.Y - t, rect.Width + t * 2, t), BorderColor);
                // Bottom border
                spriteBatch.Draw(JTextureCache.White, new Rectangle(rect.X - t, rect.Bottom, rect.Width + t * 2, t), BorderColor);
                // Left border
                spriteBatch.Draw(JTextureCache.White, new Rectangle(rect.X - t, rect.Y, t, rect.Height), BorderColor);
                // Right border
                spriteBatch.Draw(JTextureCache.White, new Rectangle(rect.Right, rect.Y, t, rect.Height), BorderColor);
            }
        }
    }
}
