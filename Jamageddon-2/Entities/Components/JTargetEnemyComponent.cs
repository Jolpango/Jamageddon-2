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
                    var enemy = new JTomatoEnemy(); //TODO change to real enemy and find first enemy in range
                    float distance = Vector2.Distance(enemy.GetComponent<JTransformComponent>().Position, tower.GetComponent<JTransformComponent>().Position);
                    if(distance <= tower.Range)
                    {
                        FireRate = tower.FireRate;
                        OnTarget?.Invoke(enemy);

                    }

                    
                }
            }
            base.Update(gameTime);
        }
    }
}
