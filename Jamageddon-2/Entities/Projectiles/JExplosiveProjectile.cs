using Jamageddon2.Entities.Components;

namespace Jamageddon2.Entities.Projectiles
{
    public class JExplosiveProjectile : JBaseProjectile
    {
        public float Radius { get; set; } = 64;
        public float Damage { get; set; } = 1;

        public JExplosiveProjectile() : base("Content/Animation/plate.json")
        {
            AddComponent(new JExplodesOnDeathComponent
            {
                Prefab = new JExplosionPrefab("Content/Animation/axe.json") { Radius = Radius, Damage = Damage }
            });
        }
    }
}