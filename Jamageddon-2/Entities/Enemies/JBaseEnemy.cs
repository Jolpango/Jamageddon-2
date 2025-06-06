using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Input;
using Jamageddon2.Entities.Path;
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
        public bool IsAlive => Health > 0;
        public bool IsMoving => pathInputComponent != null && pathInputComponent.IsMoving;

        protected JSpriteComponent spriteComponent;
        protected JColliderComponent colliderComponent;
        protected JTransformComponent transformComponent;
        private JPathInputComponent pathInputComponent;
        protected JHealthComponent HealthComponent;
        protected JMovementComponent movementComponent;

        protected JBaseEnemy(string spritePath, float maxHealth, float moveSpeed)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            MoveSpeed = moveSpeed;

            // Add enemy tag
            Tags = new HashSet<string> { "Enemy" };

            // Add required components
            transformComponent = new JTransformComponent();
            spriteComponent = new JSpriteComponent(spritePath);
            colliderComponent = new JColliderComponent()
            {
                Size = new Vector2(32, 32),
                IsSolid = true
            };
            movementComponent = new JMovementComponent() { Speed = moveSpeed };
            HealthComponent = new JHealthComponent(maxHealth);
            pathInputComponent = new JPathInputComponent();

            AddComponent(HealthComponent);
            AddComponent(spriteComponent);

            AddComponent(movementComponent);
            AddComponent(transformComponent);
            AddComponent(colliderComponent);
            AddComponent(pathInputComponent);

            pathInputComponent.OnPathComplete += OnPathComplete;
        }

        public void SetPath(JPathComponent path)
        {
            pathInputComponent.SetPath(path);
            transformComponent.Position = path.SpawnPoint;
        }

        public void StartMovement()
        {
            pathInputComponent.StartMovement();
        }

        public void StopMovement()
        {
            pathInputComponent.StopMovement();
        }

        protected abstract void OnDeath();
        protected abstract void OnPathComplete();
    }
}