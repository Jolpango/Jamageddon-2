using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Projectiles;
using Jamageddon2.Scenes;
using Jamageddon2.UI;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using static Jamageddon2.JGameConstants;

namespace Jamageddon2.Entities.Towers
{
    public abstract class JBaseTower : JEntity, IJInjectable<ExistingTowerSelectedUI>
    {
        public float Damage { get; protected set; }
        public float Range { get; protected set; }
        public float FireRate { get; protected set; }

        protected int currentWaypointIndex = 0;
        protected float waypointReachedDistance = 5f;

        protected ExistingTowerSelectedUI existingTowerSelectedUI;

        public void Inject(ExistingTowerSelectedUI service)
        {
            existingTowerSelectedUI = service;
        }

        protected JBaseTower(string spritePath, float damage, float range, float fireRate)
        {
            Damage = damage;
            Range = range;
            FireRate = fireRate;

            AddComponent(new JSpriteComponent(spritePath));
            AddComponent(new JTransformComponent());
            AddComponent(new JTopDownPlayerInputComponent());
            AddComponent(new JBoxColliderComponent() { Size = Vector2.One, IsSolid = false });
            AddComponent(new JTargetEnemyComponent() { FireRate = fireRate });
            AddComponent(new JShootComponent());
            AddComponent(new JColliderLeftClickComponent());

            GetComponent<JColliderLeftClickComponent>().OnClick += JBaseTower_OnClick;
        }

        public abstract JBaseProjectile GetProjectile();
        private void JBaseTower_OnClick(JEntity obj)
        {
            existingTowerSelectedUI.SelectExistingTower(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
