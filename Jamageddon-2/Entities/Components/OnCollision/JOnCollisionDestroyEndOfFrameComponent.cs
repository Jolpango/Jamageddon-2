using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public class JOnCollisionDestroyEndOfFrameComponent : JOnCollisionComponent
    {
        public override bool OnCollision(JColliderComponent self, JColliderComponent other)
        {
            self.Parent.DestroyEntity();
            return false;
        }
    }
}