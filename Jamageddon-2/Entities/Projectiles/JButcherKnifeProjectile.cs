using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Enemies;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS.Components;
using System.Collections.Generic;

namespace Jamageddon2.Entities.Projectiles
{
    public class JButcherKnifeProjectile : JBaseProjectile
    {
        private const float KNIFE_SIZE = 5f;
        private const float KNIFE_DAMAGE = 4f;
        private const string ATTACK_SPRITE_PATH = "Content/Animation/butcherknife.json";
        private const float ATTACK_ANIMATION_SPEED = 70f;

        public JButcherKnifeProjectile()
            : base(ATTACK_SPRITE_PATH)
        {
            Size = new Vector2(KNIFE_SIZE, KNIFE_SIZE);
            Speed = ATTACK_ANIMATION_SPEED;
        }

        public override void LoadContent()
        {
            AddComponent(new JDamageOnHitComponent() { Damage = KNIFE_DAMAGE });
            AddComponent(new JPiercingComponent() { PiercesRemaining = 3 });

            base.LoadContent();
            GetComponent<JTransformComponent>().Scale = new Vector2(0.5f, 0.5f);

        }
    }
}