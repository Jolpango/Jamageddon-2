using Jamageddon2.Entities.Enemies;
using Jamageddon2.Entities.Towers;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using System;
namespace Jamageddon2.Entities.Components
{
    public enum TargetingMode
    {
        Closest,
        Farthest, // Closest to the end of the path
        Toughest, // Highest maxhealth
    }
    public class JTargetEnemyComponent : JComponent, IJInjectable<JGameScene>
    {
        public event Action<JBaseEnemy> OnTarget;
        private JGameScene gameScene;

        public TargetingMode TargetingMode { get; set; } = TargetingMode.Closest;
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
                    switch(TargetingMode)
                    {
                        case TargetingMode.Closest:
                            TargetClosest(tower);
                            break;
                        case TargetingMode.Farthest:
                            TargetFarthest(tower);
                            break;
                        case TargetingMode.Toughest:
                            TargetToughest(tower);
                            break;
                    }
                }
            }
            base.Update(gameTime);
        }

        private void TargetClosest(JBaseTower tower)
        {
            JBaseEnemy closestEnemy = null;
            float closestDistance = float.MaxValue;
            foreach (var entity in gameScene.GetEntitiesByTag("Enemy"))
            {
                if (entity is JBaseEnemy enemy)
                {
                    float distance = Vector2.Distance(enemy.GetComponent<JTransformComponent>().Position, tower.GetComponent<JTransformComponent>().Position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemy;
                    }
                }
            }

            if (closestDistance <= tower.Range)
            {
                FireRate = tower.FireRate;
                if (closestEnemy is not null)
                    ShootEnemy(closestEnemy);
            }
        }

        private void TargetFarthest(JBaseTower tower)
        {
            JBaseEnemy farthestEnemy = null;
            float leastDistanceLeft = float.MaxValue;
            foreach (var entity in gameScene.GetEntitiesByTag("Enemy"))
            {
                if (entity is JBaseEnemy enemy)
                {
                    float distance = Vector2.Distance(enemy.GetComponent<JTransformComponent>().Position, tower.GetComponent<JTransformComponent>().Position);
                    if (distance < tower.Range)
                    {
                        var pathComponent = enemy.GetComponent<JPathInputComponent>();
                        var distanceLeft = pathComponent?.DistanceLeftToEnd ?? float.MaxValue;
                        if (distanceLeft < leastDistanceLeft)
                        {
                            leastDistanceLeft = distanceLeft;
                            farthestEnemy = enemy;
                        }
                    }
                }
            }
            if (farthestEnemy is not null)
            {
                FireRate = tower.FireRate;
                ShootEnemy(farthestEnemy);
            }
        }

        private void TargetToughest(JBaseTower tower)
        {
            JBaseEnemy toughestEnemy = null;
            foreach (var entity in gameScene.GetEntitiesByTag("Enemy"))
            {
                if (entity is JBaseEnemy enemy)
                {
                    float distance = Vector2.Distance(enemy.GetComponent<JTransformComponent>().Position, tower.GetComponent<JTransformComponent>().Position);
                    if (distance < tower.Range)
                    {
                        var maxHealth = enemy.GetComponent<JHealthComponent>()?.MaxHealth ?? 0;
                        if ((toughestEnemy?.GetComponent<JHealthComponent>()?.MaxHealth ?? 0) < maxHealth)
                        {
                            toughestEnemy = enemy;
                        }
                    }
                }
            }
            if (toughestEnemy is not null)
            {
                FireRate = tower.FireRate;
                ShootEnemy(toughestEnemy);
            }
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
