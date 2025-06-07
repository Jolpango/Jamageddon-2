using Microsoft.Xna.Framework;
using Jamageddon2.Entities.Projectiles;

namespace Jamageddon2.Entities.Towers
{
    internal class JButcherChefTower : JBaseTower
    {
        private const string BUTCHERCHEF_SPRITE_PATH = "Content/Animation/butcherchef.json";
        private const float DEFAULT_DAMAGE = 1f;
        private const float DEFAULT_RANGE = 100f;
        private const float DEFAULT_FIRE_RATE = 1f;

        public JButcherChefTower() :
            base(
                spritePath: BUTCHERCHEF_SPRITE_PATH,
                damage: DEFAULT_DAMAGE,
                range: DEFAULT_RANGE,
                fireRate: DEFAULT_FIRE_RATE)
        {
            Name = "ButcherChefTower";
        }

        public override JBaseProjectile GetProjectile()
        {
            return new JButcherKnifeProjectile();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

