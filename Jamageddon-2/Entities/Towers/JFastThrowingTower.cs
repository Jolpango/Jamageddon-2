using Microsoft.Xna.Framework;
using Jamageddon2.Entities.Projectiles;

namespace Jamageddon2.Entities.Towers
{
    internal class JFastThrowingTower : JBaseTower
    {
        private const string SPRITE_PATH = "Content/Animation/butcherchef.json";
        private const float DEFAULT_DAMAGE = 2f;
        private const float DEFAULT_RANGE = 100f;
        private const float DEFAULT_FIRE_RATE = 0.1f;

        public JFastThrowingTower() :
            base(
                spritePath: SPRITE_PATH,
                damage: DEFAULT_DAMAGE,
                range: DEFAULT_RANGE,
                fireRate: DEFAULT_FIRE_RATE)
        {
            Name = "FastThrowingTower";
        }

        public override JBaseProjectile GetProjectile()
        {
            return new JButcherKnifeProjectileFast();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

