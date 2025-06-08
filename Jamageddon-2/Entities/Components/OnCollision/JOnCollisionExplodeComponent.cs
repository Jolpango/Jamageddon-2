using Jamageddon2.Entities.Projectiles;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public class JOnCollisionExplodeComponent : JOnCollisionComponent, IJInjectable<JGameScene>
    {
        public JGameScene Scene { get; set; }
        public JExplosionPrefab Prefab { get; set; } = new();

        public void Inject(JGameScene service)
        {
            Scene = service;
        }

        public override bool OnCollision(JColliderComponent self, JColliderComponent other)
        {
            var halfRadius = Prefab.Radius / 2;
            Prefab.Position = self.Parent.GetComponent<JTransformComponent>().Position - new Vector2(halfRadius, halfRadius);

            Scene.AddEntity(Prefab);

            self.Parent.DestroyEntity();
            self.Parent.Tags.Add("MarkedForDeletion");

            return false;
        }
    }
}