using System;
using Jamageddon2.Entities.Components;
using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Projectiles
{
    public class JExplosionPrefab : JBaseProjectile
    {
        public float Damage { get; set; }
        public float Radius { get; set; }

        public JExplosionPrefab(string spritePath, string particleEffectPath = "Content/Emitters/random.json") : base(spritePath)
        {
            AddComponent(new JParticleEffectComponent(particleEffectPath));
            AddComponent(new JFirstFrameHitDestroyComponent());
        }

        public override void LoadContent()
        {
            AddComponent(new JCircleColliderComponent() { Radius = Radius });
            AddComponent(new JDamageOnHitComponent() { Damage = Damage });

            base.LoadContent();
        }
    }
}
