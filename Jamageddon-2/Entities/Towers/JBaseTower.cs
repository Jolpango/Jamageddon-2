using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Enemies;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using System.Collections.Generic;


namespace Jamageddon2.Entities.Towers
{
    public class JProjectile : JEntity
    {
        public float Speed { get; set; }
        public float Damage { get; set; }

        public JProjectile(string attackSpritePath, Vector2 position, Vector2 direction, float speed, float damage)
        {
            Tags = new HashSet<string>{ "Projectile" };
            AddComponent(new JSpriteComponent(attackSpritePath));
            AddComponent(new JTransformComponent() { Position = position });
            AddComponent(new JMovementComponent() { Speed = speed });
            AddComponent(new JColliderComponent() { Size = new Vector2(5, 5), IsSolid = false  });
            GetComponent<JColliderComponent>().OnCollision += OnCollisionProjectile;
           
            AddComponent(new JProjectileInputPath());
            GetComponent<JProjectileInputPath>().MoveIntent = direction;

            Speed = speed;
            Damage = damage;
        }

        
        //TODO fix enemies take damage and remove bullet from list when hit
        private void OnCollisionProjectile(JColliderComponent self, JColliderComponent other)
        {
            if (other.Parent.Tags.Contains("Enemy"))
            {
                self.Parent.DestroyEntity();
                if (other.Parent is JBaseEnemy enemy)
                    enemy.TakeDamage(Damage);
            }
        }
    }


    public abstract class JBaseTower : JEntity
    {
        public float Damage { get; protected set; }
        public float Range { get; protected set; }
        public float FireRate { get; protected set; }
        public string AttackSpritePath;

        protected int currentWaypointIndex = 0;
        protected float waypointReachedDistance = 5f;

        protected JBaseTower(string spritePath,string attackSpritePath, float damage, float range, float fireRate) 
        {
            Damage = damage;
            Range = range;
            FireRate = fireRate;
            AttackSpritePath = attackSpritePath;


            AddComponent(new JSpriteComponent(spritePath));
            AddComponent(new JTransformComponent());
            AddComponent(new JTopDownPlayerInputComponent());
            AddComponent(new JColliderComponent() { Size = new Vector2(32, 32), IsSolid = false });
            AddComponent(new JTargetEnemyComponent(){ FireRate = fireRate });
            AddComponent(new JShootComponent());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
