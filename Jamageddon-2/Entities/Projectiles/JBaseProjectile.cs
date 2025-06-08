using Jamageddon2.Entities.Components;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using System.Collections.Generic;

namespace Jamageddon2.Entities.Projectiles
{
    public class JBaseProjectile : JEntity
    {
        private const float DEFAULT_PROJECTILE_SPEED = 200f;
        private const float DEFAULT_PROJECTILE_SIZE = 5f;

        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; } = DEFAULT_PROJECTILE_SPEED;
        public Vector2 Size { get; set; } = new Vector2(DEFAULT_PROJECTILE_SIZE, DEFAULT_PROJECTILE_SIZE);

        protected JBaseProjectile(string spritePath)
        {
            Tags = new HashSet<string> { "Projectile" };
            AddComponent(new JSpriteComponent(spritePath));
            AddComponent(new JProjectileInputPath());

            AddComponent(new JOnCollisionExclusivelyEnemyComponent());
            AddComponent(new JOnCollisionDistinctEntityComponent());
        }

        public override void LoadContent()
        {
            if (!HasComponent<JColliderComponent>())
                AddComponent(new JBoxColliderComponent() { Size = Size, IsSolid = false, Centered = true });
            GetComponent<JColliderComponent>().OnCollision += OnCollisionProjectile;

            AddComponent(new JTransformComponent() { Position = Position });
            AddComponent(new JMovementComponent() { Speed = Speed });

            GetComponent<JProjectileInputPath>().MoveIntent = Direction;
            base.LoadContent();

            // Start animation after all components are loaded
            GetComponent<JSpriteComponent>().PlayAnimation("Default", true);
        }

        protected virtual void OnCollisionProjectile(JColliderComponent self, JColliderComponent other)
        {
            foreach (JOnCollisionComponent component in self.Parent.GetComponentsOf<JOnCollisionComponent>())
            {
                bool success = component.OnCollision(self, other);
                if (!success)
                    break;
            }
        }
    }
}