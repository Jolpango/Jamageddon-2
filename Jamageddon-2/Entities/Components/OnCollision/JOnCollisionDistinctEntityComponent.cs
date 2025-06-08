using System.Collections.Generic;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public class JOnCollisionDistinctEntityComponent : JOnCollisionComponent
    {
        private List<JEntity> entityCollisions = new();

        public override bool OnCollision(JColliderComponent self, JColliderComponent other)
        {
            if (entityCollisions.Contains(other.Parent))
                return false;

            entityCollisions.Add(other.Parent);
            return true;
        }
    }
}