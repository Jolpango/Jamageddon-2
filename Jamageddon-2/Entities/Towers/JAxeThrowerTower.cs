using Microsoft.Xna.Framework;
using Jamageddon2.Entities.Projectiles;

namespace Jamageddon2.Entities.Towers
{
    internal class JAxeThrowerTower : JBaseTower
    {
        private const string SPRITE_PATH = "Content/Animation/busboy.json";
        private const float DEFAULT_DAMAGE = 5f;
        private const float DEFAULT_RANGE = 120f;
        private const float DEFAULT_FIRE_RATE = 0.5f;

        public JAxeThrowerTower() :
            base(
                spritePath: SPRITE_PATH,
                damage: DEFAULT_DAMAGE,
                range: DEFAULT_RANGE,
                fireRate: DEFAULT_FIRE_RATE)
        {
            Name = "AxeThrowerTower";
        }

        public override JBaseProjectile GetProjectile()
        {
            return new JPlateProjectile();
        }
    }
}