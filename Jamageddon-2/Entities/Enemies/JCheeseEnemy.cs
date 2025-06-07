using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using System;

namespace Jamageddon2.Entities.Enemies
{
    public class JCheeseEnemy : JBaseEnemy
    {
        public JCheeseEnemy() : base("Content/Animation/cheese.json")
        {
            MaxHealth = 5f;
            MoveSpeed = 50f;
            Name = "Cheese Enemy";
            Scale = new Vector2(1f, 1f);
        }

        public override void StartMovement()
        {
            base.StartMovement();
            this.GetComponent<JSpriteComponent>().PlayAnimation("Default", true);
        }

        public override void StopMovement()
        {
            base.StopMovement();
            this.GetComponent<JSpriteComponent>().PlayAnimation("Default", false);
        }
    }
}