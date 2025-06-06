using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using System;

namespace Jamageddon2.Entities.Enemies
{
    public class JTomatoEnemy : JBaseEnemy
    {
        public JTomatoEnemy()
            : base(
                spritePath: "Content/Animation/axe.json", // TODO: Replace with tomato sprite
                maxHealth: 50f,
                moveSpeed: 75f
                )
        {
            Name = "Tomato Enemy";
        }

        public new void StartMovement()
        {
            base.StartMovement();
            spriteComponent.PlayAnimation("Default", true);
        }

        public new void StopMovement()
        {
            base.StopMovement();
            spriteComponent.PlayAnimation("Default", false);
        }

        protected override void OnDeath()
        {
            // Add tomato-specific death effects here
            DestroyEntity();
        }

        protected override void OnPathComplete()
        {
            // Add tomato-specific end-of-path behavior here
            // DestroyEntity();
        }
    }
}