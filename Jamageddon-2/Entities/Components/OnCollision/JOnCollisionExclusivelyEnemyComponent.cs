using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public class JOnCollisionExclusivelyEnemyComponent : JOnCollisionComponent
    {
        public override bool OnCollision(JColliderComponent self, JColliderComponent other)
        {
            return other.Parent.Tags.Contains("Enemy");
        }
    }
}