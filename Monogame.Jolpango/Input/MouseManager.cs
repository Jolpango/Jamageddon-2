using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGame.Jolpango.Input
{
    public class MouseManager
    {
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        public void Update()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public Vector2 Position { get { return currentMouseState.Position.ToVector2(); } }

        private bool IsButtonPressed(Func<MouseState, ButtonState> buttonSelector) =>
            buttonSelector(previousMouseState) == ButtonState.Released &&
            buttonSelector(currentMouseState) == ButtonState.Pressed;

        private bool IsButtonReleased(Func<MouseState, ButtonState> buttonSelector) =>
            buttonSelector(previousMouseState) == ButtonState.Pressed &&
            buttonSelector(currentMouseState) == ButtonState.Released;

        public bool IsLeftButtonClicked() => IsButtonPressed(state => state.LeftButton);
        public bool IsLeftButtonReleased() => IsButtonReleased(state => state.LeftButton);

        public bool IsRightButtonClicked() => IsButtonPressed(state => state.RightButton);
        public bool IsRightButtonReleased() => IsButtonReleased(state => state.RightButton);

        public bool IsMiddleButtonClicked() => IsButtonPressed(state => state.MiddleButton);
        public bool IsMiddleButtonReleased() => IsButtonReleased(state => state.MiddleButton);

        public bool IsXButton1Clicked() => IsButtonPressed(state => state.XButton1);
        public bool IsXButton1Released() => IsButtonReleased(state => state.XButton1);

        public bool IsXButton2Clicked() => IsButtonPressed(state => state.XButton2);
        public bool IsXButton2Released() => IsButtonReleased(state => state.XButton2);
    }
}
