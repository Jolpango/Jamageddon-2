using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI.Elements;
using System;
using System.Collections.Generic;

namespace MonoGame.Jolpango.UI
{
    public class UIManager : IJInjectable<JMouseInput>, IJInjectable<JKeyboardInput>
    {
        private JMouseInput mouseInput;
        private JKeyboardInput keyboardInput;

        private List<UIElement> elements;

        public UIManager()
        {
            elements = new List<UIElement>();
        }
        public void AddElement(UIElement element) => elements.Add(element);
        public void RemoveElement(UIElement element) => elements.Remove(element);
        public void Inject(JMouseInput service)
        {
            mouseInput = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void Inject(JKeyboardInput service)
        {
            keyboardInput = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void Initialize()
        {
            // Initialize UI components, load styles, etc.
        }

        public void LoadContent()
        {
            // Load textures, fonts, and other content needed for UI
        }

        public void Update(GameTime gameTime)
        {
            // Update UI components, handle input, etc.
            mouseInput.IsUIFocused = false;
            foreach (var element in elements)
            {
                if(element.IsMouseOver(mouseInput.Position))
                {
                    mouseInput.IsUIFocused = true;
                }
                element.Update(gameTime, mouseInput, keyboardInput);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw UI components
            foreach(var element in elements)
            {
                element.Draw(spriteBatch);
            }
        }

    }
}
