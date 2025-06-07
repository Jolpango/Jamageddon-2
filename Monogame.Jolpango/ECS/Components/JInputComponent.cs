using Microsoft.Xna.Framework;

namespace MonoGame.Jolpango.ECS.Components
{
    public abstract class JInputComponent : JComponent
    {
        public Vector2 MoveIntent = Vector2.Zero;
        public abstract void UpdateIntent(GameTime gameTime);
        public override void Update(GameTime gameTime) => UpdateIntent(gameTime);
    }

}
