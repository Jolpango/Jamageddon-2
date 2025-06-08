using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public abstract class JOnCollisionComponent : JComponent
    {
        public abstract bool OnCollision(JColliderComponent self, JColliderComponent other);
    }
}