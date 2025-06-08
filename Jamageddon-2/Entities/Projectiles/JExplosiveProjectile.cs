using Jamageddon2.Entities.Components;

namespace Jamageddon2.Entities.Projectiles
{
    public class JExplosiveProjectile : JBaseProjectile
    {
        public float Radius { get; set; } = 64;
        public float Damage { get; set; } = 1;

        public JExplosiveProjectile() : base("Content/Animation/plate.json")
        {
            AddComponent(new JOnCollisionExplodeComponent());
        }

        public override void LoadContent()
        {
            var prefab = GetComponent<JOnCollisionExplodeComponent>().Prefab;
            prefab.Radius = Radius;
            prefab.Damage = Damage;

            base.LoadContent();
        }
    }
}