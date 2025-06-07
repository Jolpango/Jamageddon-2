using Microsoft.Xna.Framework;
using Jamageddon2.Entities.Projectiles;

namespace Jamageddon2.Entities.Towers
{
    internal class JDishWasherTower : JBaseTower
    {
        private const string DISHWASHER_SPRITE_PATH = "Content/Animation/busboy.json";
        private const float DEFAULT_DAMAGE = 1f;
        private const float DEFAULT_RANGE = 500f;
        private const float DEFAULT_FIRE_RATE = 0.5f;

        public JDishWasherTower() :
            base(
                spritePath: DISHWASHER_SPRITE_PATH,
                damage: DEFAULT_DAMAGE,
                range: DEFAULT_RANGE,
                fireRate: DEFAULT_FIRE_RATE)
        {
            Name = "DishWasherTower";
        }

        public override JBaseProjectile GetProjectile()
        {
            return new JPlateProjectile();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

