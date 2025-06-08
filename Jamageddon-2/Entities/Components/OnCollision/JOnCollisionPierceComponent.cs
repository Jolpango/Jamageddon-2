using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public class JOnCollisionPierceComponent : JOnCollisionComponent
    {
        public int PiercesRemaining { get; set; } = 1;

        public override bool OnCollision(JColliderComponent self, JColliderComponent other)
        {
            if (--PiercesRemaining <= 0)
            {
                self.Parent.DestroyEntity();
                self.Parent.Tags.Add("MarkedForDeletion");
                return false;
            }

            return true;
        }
    }
}