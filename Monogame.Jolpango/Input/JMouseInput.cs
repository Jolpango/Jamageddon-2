using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace MonoGame.Jolpango.Input
{
    public class JMouseInput
    {
        private MouseState currentState;
        private MouseState previousState;
        public event Action<Vector2> MouseMoved;
        public event Action<MouseState> LeftButtonClicked;
        public event Action<MouseState> RightButtonClicked;
        public event Action<MouseState> MiddleButtonClicked;

        public Vector2 Position => currentState.Position.ToVector2();
        public JMouseInput()
        {
        }

        public void Update()
        {
            previousState = currentState;
            currentState = Mouse.GetState();
            if (currentState.Position != previousState.Position)
            {
                MouseMoved?.Invoke(currentState.Position.ToVector2());
            }
            if (IsLeftButtonClicked())
            {
                LeftButtonClicked?.Invoke(currentState);
            }
            if (IsRightButtonClicked())
            {
                RightButtonClicked?.Invoke(currentState);
            }
            if (IsMiddleButtonClicked())
            {
                MiddleButtonClicked?.Invoke(currentState);
            }
        }

        public bool IsLeftButtonDown()
        {
            return currentState.LeftButton == ButtonState.Pressed;
        }
        public bool IsLeftButtonUp()
        {
            return currentState.LeftButton == ButtonState.Released;
        }
        public bool IsRightButtonDown()
        {
            return currentState.RightButton == ButtonState.Pressed;
        }
        public bool IsRightButtonUp()
        {
            return currentState.RightButton == ButtonState.Released;
        }
        public bool IsMiddleButtonDown()
        {
            return currentState.MiddleButton == ButtonState.Pressed;
        }
        public bool IsMiddleButtonUp()
        {
            return currentState.MiddleButton == ButtonState.Released;
        }
        public bool IsLeftButtonClicked()
        {
            return currentState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released;
        }
        public bool IsRightButtonClicked()
        {
            return currentState.RightButton == ButtonState.Pressed && previousState.RightButton == ButtonState.Released;
        }
        public bool IsMiddleButtonClicked()
        {
            return currentState.MiddleButton == ButtonState.Pressed && previousState.MiddleButton == ButtonState.Released;
        }
    }
}
