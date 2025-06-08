using Microsoft.Xna.Framework;
using Jamageddon2.Entities.Projectiles;

namespace Jamageddon2.Entities.Towers
{
    internal class JSpatulaSniperTower : JBaseTower
    {
        private const string SPRITE_PATH = "Content/Animation/busboy.json";
        private const float DEFAULT_DAMAGE = 10f;
        private const float DEFAULT_RANGE = 200f;
        private const float DEFAULT_FIRE_RATE = 2.0f;

        public JSpatulaSniperTower() :
            base(
                spritePath: SPRITE_PATH,
                damage: DEFAULT_DAMAGE,
                range: DEFAULT_RANGE,
                fireRate: DEFAULT_FIRE_RATE)
        {
            Name = "SpatulaSniperTower";
        }

        public override JBaseProjectile GetProjectile()
        {
            return new JButcherKnifeProjectile();
        }
    }
}