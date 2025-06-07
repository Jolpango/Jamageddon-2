using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Projectiles;
using Jamageddon2.Scenes;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using System;
using static Jamageddon2.JGameConstants;

namespace Jamageddon2.Entities.Towers
{
    public abstract class JBaseTower : JEntity, IJInjectable<PlayScene>
    {
        public float Damage { get; protected set; }
        public float Range { get; protected set; }
        public float FireRate { get; protected set; }

        protected int currentWaypointIndex = 0;
        protected float waypointReachedDistance = 5f;

        protected PlayScene scene;

        public void Inject(PlayScene service)
        {
            scene = service;
        }

        protected JBaseTower(string spritePath, float damage, float range, float fireRate)
        {
            Damage = damage;
            Range = range;
            FireRate = fireRate;

            AddComponent(new JSpriteComponent(spritePath));
            AddComponent(new JTransformComponent());
            AddComponent(new JTopDownPlayerInputComponent());
            AddComponent(new JColliderComponent() { Size = new Vector2(DEFAULT_ENTITY_SIZE, DEFAULT_ENTITY_SIZE), IsSolid = false });
            AddComponent(new JTargetEnemyComponent() { FireRate = fireRate });
            AddComponent(new JShootComponent());
            AddComponent(new JColliderLeftClickComponent());

            GetComponent<JColliderLeftClickComponent>().OnClick += JBaseTower_OnClick;
        }

        public abstract JBaseProjectile GetProjectile();
        private void JBaseTower_OnClick(JEntity obj)
        {
            scene.SelectExistingTower(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
