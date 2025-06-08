using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Enemies;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS.Components;
using System.Collections.Generic;

namespace Jamageddon2.Entities.Projectiles
{
    public class JButcherKnifeProjectileFast : JBaseProjectile
    {
        private const float KNIFE_SIZE = 5f;
        private const float KNIFE_DAMAGE_MULTIPLIER = 2f;
        private const string ATTACK_SPRITE_PATH = "Content/Animation/butcherknife.json";
        private const float ATTACK_ANIMATION_SPEED = 300f;

        public JButcherKnifeProjectileFast()
            : base(ATTACK_SPRITE_PATH)
        {
            Size = new Vector2(KNIFE_SIZE, KNIFE_SIZE);
            Speed = ATTACK_ANIMATION_SPEED;
            AddComponent(new JOnCollisionDamageComponent() { Damage = 1});
            AddComponent(new JOnCollisionDestroyComponent());
        }

        public override void LoadContent()
        {
            base.LoadContent();
            GetComponent<JTransformComponent>().Scale = new Vector2(0.5f, 0.5f);
        }
    }
}