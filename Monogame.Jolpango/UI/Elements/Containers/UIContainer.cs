using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Input;
using System.Collections.Generic;

namespace MonoGame.Jolpango.UI.Elements.Containers
{
    public class UIContainer : UIElement
    {
        protected List<UIElement> children;
        public Vector2 Padding { get; set; } = Vector2.Zero;

        public override bool IsMouseOver(Vector2 mousePosition)
        {
            foreach (var child in children)
            {
                if (child.IsMouseOver(mousePosition)) return true;
            }
            return false;
        }

        public UIContainer()
        {
            children = new List<UIElement>();
        }

        public void AddChild(UIElement child)
        {
            child.Parent = this;
            children.Add(child);
            RecalculateLayout();
        }

        public void RemoveChild(UIElement child)
        {
            child.Parent = null;
            children.Remove(child);
            RecalculateLayout();
        }

        public virtual void RecalculateLayout()
        {
            RecalculateSize();
        }

        public virtual void RecalculateSize()
        {
            if (children.Count > 0)
            {
                Vector2 min = children[0].Position;
                Vector2 max = children[0].Position + children[0].Size;

                foreach (var child in children)
                {
                    var topLeft = child.Position;
                    var bottomRight = child.Position + child.Size;

                    min = Vector2.Min(min, topLeft);
                    max = Vector2.Max(max, bottomRight);
                }

                Size = (max - min) + Padding;
            }
            else
            {
                Size = Padding;
            }
        }

        public override void LoadContent()
        {
            foreach(UIElement child in children)
            {
                child.LoadContent();
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime, JMouseInput mouseInput, JKeyboardInput keyboardInput)
        {
            foreach(UIElement child in children)
            {
                child.Update(gameTime, mouseInput, keyboardInput);
            }
            base.Update(gameTime, mouseInput, keyboardInput);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(IsVisible)
            {
                foreach(var child in children)
                {
                    child.Draw(spriteBatch);
                }
            }
        }
    }
}
