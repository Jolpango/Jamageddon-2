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
            //throw new NotImplementedException("Farthest targeting mode is not implemented yet.");
        }

        private void TargetToughest(JBaseTower tower)
        {
            //throw new NotImplementedException("Toughest targeting mode is not implemented yet.");
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
