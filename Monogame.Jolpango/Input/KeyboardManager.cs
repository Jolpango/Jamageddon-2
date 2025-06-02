
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Jolpango.Input
{
    public class KeyboardManager
    {
        private KeyboardState previousState;
        private KeyboardState currentState;
        public void Update()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }
        public bool IsKeyClicked(Keys key)
        {
            return previousState.IsKeyUp(key) && currentState.IsKeyDown(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return previousState.IsKeyDown(key) && currentState.IsKeyUp(key);
        }

        public bool IsKeyDown(Keys key) { return previousState.IsKeyDown(key); }
        public bool IsKeyUp(Keys key) { return currentState.IsKeyUp(key); }

        public bool IsKeyCombinationDown(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (IsKeyUp(key))
                    return false;
            }
            return true;
        }
    }
}
