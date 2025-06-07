using Jamageddon2.Entities.Enemies;
using Jamageddon2.Entities.Towers;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamageddon2.Entities.Components
{
    internal class JTargetEnemyComponent : JComponent, IJInjectable<JGameScene>
    {
        public event Action<JBaseEnemy> OnTarget;
        private JGameScene gameScene;
        public float FireRate { get; set; }

        public void Inject(JGameScene service)
        {
            gameScene = service;
        }

        public override void Update(GameTime gameTime)
        {
            FireRate -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Parent is JBaseTower tower)
            {
                if (FireRate <= 0)
                {
                    JBaseEnemy closestEnemy = null;
                    float closestDistance = float.MaxValue;
                    foreach(var entity in gameScene.GetEntitiesByTag("Enemy"))
                    {
                        if(entity is JBaseEnemy enemy)
                        {
                            float distance = Vector2.Distance(enemy.GetComponent<JTransformComponent>().Position, tower.GetComponent<JTransformComponent>().Position);
                            if (distance < closestDistance)
                            {
                                closestDistance = distance;
                                closestEnemy = enemy;
                            }
                        }
                    }

                    if(closestDistance <= tower.Range)
                    {
                        FireRate = tower.FireRate;
                        if (closestEnemy is not null)
                            ShootEnemy(closestEnemy);
                    }

                    
                }
            }
            base.Update(gameTime);
        }

        private void ShootEnemy(JBaseEnemy closestEnemy)
        {
            Parent.GetComponent<JSpriteComponent>().PlayAnimation("AttackWindUp", false, () =>
            {
                OnTarget?.Invoke(closestEnemy);
                Parent.GetComponent<JSpriteComponent>().PlayAnimation("AttackWindDown", false);
            });
            
        }
    }
}
