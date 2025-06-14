using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.Core;
using Jamageddon2.Entities.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Jamageddon2.JGameConstants;

namespace Jamageddon2.Entities.Enemies
{
    public abstract class JBaseEnemy : JEntity
    {

        private float DEFAULT_ENEMY_COLLIDER_SIZE = DEFAULT_ENTITY_SIZE / 2;
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public float MoveSpeed { get; set; }
        public new string Name { get; set; }
        public Vector2 Scale { get; set; }
        public bool IsAlive => Health > 0;
        public bool IsMoving => this.GetComponent<JPathInputComponent>() != null && this.GetComponent<JPathInputComponent>().IsMoving;
        protected JBaseEnemy(string spritePath, string particleEffectPath = "Content/Emitters/random.json")
        {
            // Add enemy tag
            Tags = new HashSet<string> { "Enemy" };

            // Add required components
            AddComponent(new JHealthComponent());
            AddComponent(new JSpriteComponent(spritePath));
            AddComponent(new JBoxColliderComponent()
            {
                Size = new Vector2(DEFAULT_ENEMY_COLLIDER_SIZE, DEFAULT_ENEMY_COLLIDER_SIZE),
                IsSolid = false,
                Centered = true
            });
            AddComponent(new JMovementComponent());
            AddComponent(new JTransformComponent());
            AddComponent(new JPathInputComponent());
            AddComponent(new JHealthBarComponent());
            AddComponent(new JParticleEffectComponent(particleEffectPath));

            this.GetComponent<JPathInputComponent>().OnPathComplete += OnPathComplete;
            this.GetComponent<JHealthComponent>().OnDeath += OnDeath;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.GetComponent<JHealthComponent>().MaxHealth = MaxHealth;
            this.GetComponent<JHealthComponent>().CurrentHealth = MaxHealth;
            this.GetComponent<JTransformComponent>().Scale = Scale;
            this.GetComponent<JMovementComponent>().Speed = MoveSpeed;
            this.GetComponent<JTransformComponent>().Scale = Scale;
            this.GetComponent<JHealthBarComponent>().Size = new Vector2(DEFAULT_ENTITY_SIZE, 4);
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
            this.StopMovement();
            this.Tags = new HashSet<string> { "DeadEnemy" };
            this.GetComponent<JHealthBarComponent>().Enabled = false;
            var sprite = this.GetComponent<JSpriteComponent>();
            this.GetComponent<JParticleEffectComponent>().Emit(sprite.sprite.Center, 10);
            this.GetComponent<JSpriteComponent>().PlayAnimation("Default", false, () =>
            {
                DestroyEntity();
            });
        }

        protected virtual void OnPathComplete()
        {
            // TODO: Add path complete animation
            this.GetComponent<JSpriteComponent>().PlayAnimation("Default", false);
            DestroyEntity();
        }
    }
}