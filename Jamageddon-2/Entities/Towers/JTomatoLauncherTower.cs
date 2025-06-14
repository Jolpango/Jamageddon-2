using Microsoft.Xna.Framework;
using Jamageddon2.Entities.Projectiles;

namespace Jamageddon2.Entities.Towers
{
    internal class JTomatoLauncherTower : JBaseTower
    {
        private const string SPRITE_PATH = "Content/Animation/busboy.json";
        private const float DEFAULT_DAMAGE = 3f;
        private const float DEFAULT_RANGE = 100f;
        private const float DEFAULT_FIRE_RATE = 5f;

        public JTomatoLauncherTower() :
            base(
                spritePath: SPRITE_PATH,
                damage: DEFAULT_DAMAGE,
                range: DEFAULT_RANGE,
                fireRate: DEFAULT_FIRE_RATE)
        {
            Name = "TomatoLauncherTower";
        }

        public override JBaseProjectile GetProjectile()
        {
            return new JExplosionPrefab() { Radius = 100, Damage = 5, Speed = 0 };
        }
    }
}