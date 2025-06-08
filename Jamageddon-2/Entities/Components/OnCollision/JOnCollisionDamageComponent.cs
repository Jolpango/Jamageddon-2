using Jamageddon2.Entities.Enemies;
using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public class JOnCollisionDamageComponent : JOnCollisionComponent
    {
        public float Damage { get; set; } = 1;

        public override bool OnCollision(JColliderComponent self, JColliderComponent other)
        {
            if (other.Parent is JBaseEnemy enemy)
                enemy.TakeDamage(Damage);

            return true;
        }
    }
}