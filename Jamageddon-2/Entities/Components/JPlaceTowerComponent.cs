using Jamageddon2.Entities.Enemies;
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
    public class JPlaceTowerComponent : JComponent, IJInjectable<JGameScene>
    {
        private JGameScene scene;
        public void Inject(JGameScene service)
        {
            scene = service;
        }

        public override void LoadContent()
        {
            Parent.GetComponent<JLeftMouseClickComponent>().OnClick += JPlaceTowerComponent_OnClick;
            base.LoadContent();
        }

        private void JPlaceTowerComponent_OnClick(JLeftMouseClickComponent obj)
        {
            scene.AddEntity(new JTomatoEnemy());
            Parent.GetComponent<JLeftMouseClickComponent>().OnClick -= JPlaceTowerComponent_OnClick;
            Parent.DestroyEntity();
        }
    }
}
