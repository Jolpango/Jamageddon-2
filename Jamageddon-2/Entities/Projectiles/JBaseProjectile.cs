using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Enemies;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using System;
using System.Collections.Generic;

namespace Jamageddon2.Entities.Projectiles
{
    public class JBaseProjectile : JEntity, IJInjectable<JGameScene>
    {
        private const float DEFAULT_PROJECTILE_SPEED = 200f;
        private const float DEFAULT_PROJECTILE_SIZE = 5f;

        public JGameScene Scene { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; } = DEFAULT_PROJECTILE_SPEED;
        public Vector2 Size { get; set; } = new Vector2(DEFAULT_PROJECTILE_SIZE, DEFAULT_PROJECTILE_SIZE);

        private List<JEntity> entitiesHit = new();

        protected JBaseProjectile(string spritePath)
        {
            Tags = new HashSet<string> { "Projectile" };
            AddComponent(new JSpriteComponent(spritePath));
            AddComponent(new JProjectileInputPath());
        }

        public void Inject(JGameScene service)
        {
            Scene = service;
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
            var entity = self.Parent;

            if (!other.Parent.Tags.Contains("Enemy"))
                return;

            if (entitiesHit.Contains(other.Parent))
                return;

            if (entity.TryGetComponent<JDamageOnHitComponent>(out var damageOnHit))
                if (other.Parent is JBaseEnemy enemy)
                    enemy.TakeDamage(damageOnHit.Damage);

            entitiesHit.Add(other.Parent);

            if (entity.TryGetComponent<JPiercingComponent>(out var pierce))
            {
                if (--pierce.PiercesRemaining <= 0)
                {
                    entity.DestroyEntity();
                    entity.Tags.Add("MarkedForDeletion");
                }
            }
            else if (entity.HasComponent<JFirstFrameHitDestroyComponent>())
                entity.DestroyEntity();
            else if (entity.HasComponent<JFirstHitDestroyComponent>())
            {
                entity.DestroyEntity();
                entity.Tags.Add("MarkedForDeletion");
            }
            else if (entity.TryGetComponent<JExplodesOnDeathComponent>(out var explodeOnDeath) && !entity.Tags.Contains("MarkedForDeletion"))
            {
                var explosion = explodeOnDeath.Prefab is null ? new JExplosionPrefab("Content/Animation/axe.json") : explodeOnDeath.Prefab;

                var halfRadius = explosion.Radius / 2;
                explosion.Position = entity.GetComponent<JTransformComponent>().Position - new Vector2(halfRadius, halfRadius);

                Scene.AddEntity(explosion);

                entity.DestroyEntity();
                entity.Tags.Add("MarkedForDeletion");
            }
        }
    }
}