using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Input
{
    public class JKeyboardInput
    {
        private KeyboardState previousState;
        private KeyboardState currentState;

        public bool IsUIFocused { get; set; } = false;

        public JKeyboardInput()
        {

        }

        public void Update()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        public bool IsKeyPressed(Keys key)
        {
            return currentState.IsKeyDown(key) && previousState.IsKeyUp(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return currentState.IsKeyUp(key) && previousState.IsKeyDown(key);
        }

        public bool IsKeyDown(Keys key)
        {
            return currentState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            return currentState.IsKeyDown(key);
        }
        public bool AreKeysDown(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (currentState.IsKeyUp(key))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
