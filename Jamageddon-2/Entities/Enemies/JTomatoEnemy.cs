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
                moveSpeed: 75f,
                name: "Tomato Enemy"
                )
        {
        }

        public override void StartMovement()
        {
            base.StartMovement();
            spriteComponent.PlayAnimation("Default", true);
        }

        public override void StopMovement()
        {
            base.StopMovement();
            spriteComponent.PlayAnimation("Default", false);
        }
    }
}