using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Input;
using System.Collections.Generic;

namespace MonoGame.Jolpango.UI.Elements.Containers
{
    public class UIContainer : UIElement
    {
        public List<UIElement> Children { get; protected set; }
        public Vector2 Padding { get; set; } = Vector2.Zero;

        public override bool IsMouseOver(Vector2 mousePosition)
        {
            foreach (var child in Children)
            {
                if (child.IsMouseOver(mousePosition)) return true;
            }
            return false;
        }

        public UIContainer()
        {
            Children = new List<UIElement>();
        }

        public void AddChild(UIElement child)
        {
            child.Parent = this;
            Children.Add(child);
            RecalculateLayout();
        }

        public void RemoveChild(UIElement child)
        {
            child.Parent = null;
            Children.Remove(child);
            RecalculateLayout();
        }

        public virtual void RecalculateLayout()
        {
            RecalculateSize();
        }

        public virtual void RecalculateSize()
        {
            if (Children.Count > 0)
            {
                Vector2 min = Children[0].Position;
                Vector2 max = Children[0].Position + Children[0].EffectiveSize;

                foreach (var child in Children)
                {
                    var topLeft = child.Position;
                    var bottomRight = child.Position + child.EffectiveSize;

                    min = Vector2.Min(min, topLeft);
                    max = Vector2.Max(max, bottomRight);
                }

                Size = (max - min) + Padding * 2;
            }
            else
            {
                Size = Padding * 2;
            }
        }


        public override void LoadContent()
        {
            foreach(UIElement child in Children)
            {
                child.LoadContent();
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime, JMouseInput mouseInput, JKeyboardInput keyboardInput)
        {
            foreach(UIElement child in Children)
            {
                child.Update(gameTime, mouseInput, keyboardInput);
            }
            RecalculateSize();
            RecalculateLayout();
            base.Update(gameTime, mouseInput, keyboardInput);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(IsVisible)
            {
                base.Draw(spriteBatch);
                foreach(var child in Children)
                {
                    child.Draw(spriteBatch);
                }
            }
        }
    }
}
