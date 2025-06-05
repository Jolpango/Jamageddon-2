using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JTopDownPlayerInputComponent : JInputComponent, IJInjectable<JKeyboardInput>
    {
        private JKeyboardInput keyboardInput;
        public Keys LeftKey { get; set; } = Keys.A;
        public Keys RightKey { get; set; } = Keys.D;
        public Keys UpKey { get; set; } = Keys.W;
        public Keys DownKey { get; set; } = Keys.S;

        public void Inject(JKeyboardInput service)
        {
            keyboardInput = service;
        }

        public override void UpdateIntent(GameTime gameTime)
        {
            MoveIntent = Vector2.Zero;
            if (keyboardInput.IsKeyDown(UpKey)) MoveIntent.Y -= 1;
            if (keyboardInput.IsKeyDown(LeftKey)) MoveIntent.X -= 1;
            if (keyboardInput.IsKeyDown(DownKey)) MoveIntent.Y += 1;
            if (keyboardInput.IsKeyDown(RightKey)) MoveIntent.X += 1;
        }
    }
}
