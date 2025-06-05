using Microsoft.Xna.Framework;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JTransformComponent : JComponent
    {
        public Vector2 Position = Vector2.Zero;
        public float Rotation = 0f;
        public Vector2 Scale = Vector2.One;
    }
}
