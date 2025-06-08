using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public class JOnCollisionDestroyComponent : JOnCollisionComponent
    {
        public override bool OnCollision(JColliderComponent self, JColliderComponent other)
        {
            self.Parent.DestroyEntity();
            self.Parent.Tags.Add("MarkedForDeletion");
            return false;
        }
    }
}