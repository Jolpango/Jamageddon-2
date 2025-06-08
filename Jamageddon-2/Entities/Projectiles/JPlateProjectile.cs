using Jamageddon2.Entities.Components;
using Microsoft.Xna.Framework;

namespace Jamageddon2.Entities.Projectiles
{
    public class JPlateProjectile : JBaseProjectile
    {
        private const float PLATE_SIZE = 10f;
        private const float PLATE_DAMAGE = 1f;
        private const string ATTACK_SPRITE_PATH = "Content/Animation/plate.json";

        public JPlateProjectile() : base(ATTACK_SPRITE_PATH)
        {
            Size = new Vector2(PLATE_SIZE, PLATE_SIZE);

            AddComponent(new JDamageOnHitComponent() { Damage = PLATE_DAMAGE });
            AddComponent(new JFirstHitDestroyComponent());
        }
    }
}