using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Enemies;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using System;
using System.Collections.Generic;

namespace Jamageddon2.Entities.Projectiles
{
    public abstract class JBaseProjectile : JEntity
    {
        private const float DEFAULT_PROJECTILE_SPEED = 200f;
        private const float DEFAULT_PROJECTILE_DAMAGE = 1f;
        private const float DEFAULT_PROJECTILE_SIZE = 5f;

        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; } = DEFAULT_PROJECTILE_SPEED;
        public float Damage { get; set; } = DEFAULT_PROJECTILE_DAMAGE;
        public int Pierces { get; set; } = 1;
        public Vector2 Size { get; set; } = new Vector2(DEFAULT_PROJECTILE_SIZE, DEFAULT_PROJECTILE_SIZE);

        private List<JEntity> entitiesHit = new();

        protected JBaseProjectile(string spritePath)
        {
            Tags = new HashSet<string> { "Projectile" };
            AddComponent(new JSpriteComponent(spritePath));
        }

        public override void LoadContent()
        {
            AddComponent(new JTransformComponent() { Position = Position });
            AddComponent(new JMovementComponent() { Speed = Speed });
            AddComponent(new JColliderComponent() { Size = Size, IsSolid = false, Centered = true });
            GetComponent<JColliderComponent>().OnCollision += OnCollisionProjectile;

            AddComponent(new JProjectileInputPath());
            GetComponent<JProjectileInputPath>().MoveIntent = Direction;
            base.LoadContent();

            // Start animation after all components are loaded
            GetComponent<JSpriteComponent>().PlayAnimation("Default", true);
        }

        protected virtual void OnCollisionProjectile(JColliderComponent self, JColliderComponent other)
        {
            if (!other.Parent.Tags.Contains("Enemy") || entitiesHit.Contains(other.Parent) || Pierces <= 0)
                return;

            Pierces--;
            if (Pierces == 0)
                self.Parent.DestroyEntity();

            entitiesHit.Add(other.Parent);
            if (other.Parent is JBaseEnemy enemy)
                enemy.TakeDamage(Damage);
        }
    }
}