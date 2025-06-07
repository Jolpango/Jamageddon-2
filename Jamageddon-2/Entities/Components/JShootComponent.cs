using Jamageddon2.Entities.Towers;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamageddon2.Entities.Components
{
    internal class JShootComponent : JComponent, IJInjectable<JGameScene>
    {
        private JGameScene scene;
        public override void LoadContent()
        {
            Parent.GetComponent<JTargetEnemyComponent>().OnTarget += JShootComponent_OnTarget;
            base.LoadContent();
        }

        private void JShootComponent_OnTarget(Enemies.JBaseEnemy obj)
        {
            if (Parent is JBaseTower tower)
            {
                Vector2 direction = (obj.GetComponent<JTransformComponent>().Position - Parent.GetComponent<JTransformComponent>().Position);
                direction.Normalize();

                var bullet = new JProjectile(tower.AttackSpritePath, Parent.GetComponent<JTransformComponent>().Position, direction, 200, tower.Damage);
                scene.AddEntity(bullet);
                
            }
        }

 
        public void Inject(JGameScene service)
        {
            scene = service;
        }
    }
}
