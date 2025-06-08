using Microsoft.Xna.Framework;
using Jamageddon2.Entities.Projectiles;

namespace Jamageddon2.Entities.Towers
{
    internal class JFireSpitterTower : JBaseTower
    {
        private const string SPRITE_PATH = "Content/Animation/busboy.json";
        private const float DEFAULT_DAMAGE = 4f;
        private const float DEFAULT_RANGE = 110f;
        private const float DEFAULT_FIRE_RATE = 0.2f;

        public JFireSpitterTower() :
            base(
                spritePath: SPRITE_PATH,
                damage: DEFAULT_DAMAGE,
                range: DEFAULT_RANGE,
                fireRate: DEFAULT_FIRE_RATE)
        {
            Name = "FireSpitterTower";
        }

        public override JBaseProjectile GetProjectile()
        {
            return new JPlateProjectile();
        }
    }
}