using Jamageddon2.Entities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;


namespace Jamageddon2.Entities.Towers
{
    public class JProjectile : JEntity
    {
        public float Speed { get; set; }
        public float Damage { get; set; }

        public JProjectile(string attackSpritePath, Vector2 position, Vector2 direction, float speed, float damage)
        {
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
            Debug.WriteLine("Hit");
            if (other.Parent.Name == "Player")
            {
                self.Parent.DestroyEntity();
                //removeBullet(self.Parent);
            }
        }
    }


    public abstract class JBaseTower : JEntity
    {
        public float Damage { get; protected set; }
        public float Range { get; protected set; }
        public float FireRate { get; protected set; }
        public string AttackSpritePath;
        
        private float FireTime;

 

        /*
        private JSpriteComponent JSpriteComponent;
        private JTransformComponent JTransformComponent;
        private JTargetEnemyComponent TargetEnemyComponent;
        private JShootComponent JShootComponent;
  

        protected JSpriteComponent spriteComponent;
        protected JColliderComponent colliderComponent;
        protected JTransformComponent transformComponent;
        protected JMovementComponent movementComponent;
        protected JInputComponent inputComponent;*/
       // protected List<JProjectile> bullets;
        protected int currentWaypointIndex = 0;
        protected float waypointReachedDistance = 5f;

        protected JBaseTower(string spritePath,string attackSpritePath, float damage, float range, float fireRate) 
        {
            Debug.WriteLine("Create tower inside JBaseTower");
            Damage = damage;
            Range = range;
            FireRate = fireRate;
            //bullets = new List<JProjectile>();

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
            // Debug.WriteLine("Update");
            base.Update(gameTime);

        }
    }
}
