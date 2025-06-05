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
            //Debug.WriteLine("Mouse over element " + this.ToString());
            return BoundingBox.Contains(mousePosition);
        }
        public virtual void LoadContent() { }
        public virtual void Update(GameTime gameTime, JMouseInput mouseInput, JKeyboardInput keyboardInput) { }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
