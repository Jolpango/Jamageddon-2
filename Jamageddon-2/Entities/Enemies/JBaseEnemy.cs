using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Input;
using Jamageddon2.Entities.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Jamageddon2.Entities.Enemies
{
    public abstract class JBaseEnemy : JEntity
    {
        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }
        public float MoveSpeed { get; protected set; }
        public string Name { get; protected set; }
        public bool IsAlive => Health > 0;
        public bool IsMoving => this.GetComponent<JPathInputComponent>() != null && this.GetComponent<JPathInputComponent>().IsMoving;

        protected JBaseEnemy(string spritePath, float maxHealth, float moveSpeed, string name)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            MoveSpeed = moveSpeed;
            Name = name;

            // Add enemy tag
            Tags = new HashSet<string> { "Enemy" };

            // Add required components
            var transformComponent = new JTransformComponent();
            var spriteComponent = new JSpriteComponent(spritePath);
            var colliderComponent = new JColliderComponent()
            {
                Size = new Vector2(32, 32),
                IsSolid = false
            };
            var movementComponent = new JMovementComponent() { Speed = moveSpeed };
            var HealthComponent = new JHealthComponent(maxHealth);
            var pathInputComponent = new JPathInputComponent();

            AddComponent(HealthComponent);
            AddComponent(spriteComponent);

            AddComponent(movementComponent);
            AddComponent(transformComponent);
            AddComponent(colliderComponent);
            AddComponent(pathInputComponent);

            pathInputComponent.OnPathComplete += OnPathComplete;
            HealthComponent.OnDeath += OnDeath;
        }

        public void SetPath(JPathComponent path)
        {
            this.GetComponent<JPathInputComponent>().SetPath(path);
            this.GetComponent<JTransformComponent>().Position = path.SpawnPoint;
        }

        public virtual void StartMovement()
        {
            this.GetComponent<JPathInputComponent>().StartMovement();
        }

        public virtual void StopMovement()
        {
            this.GetComponent<JPathInputComponent>().StopMovement();
        }

        public virtual void TakeDamage(float damage)
        {
            this.GetComponent<JHealthComponent>().TakeDamage(damage);
        }

        protected virtual void OnDeath()
        {
            // TODO: Add death animation
            this.GetComponent<JSpriteComponent>().PlayAnimation("Default", false, () =>
            {
                DestroyEntity();
            });
        }

        protected virtual void OnPathComplete()
        {
            // TODO: Add path complete animation
            this.GetComponent<JSpriteComponent>().PlayAnimation("Default", false);
            //DestroyEntity();
        }
    }
}