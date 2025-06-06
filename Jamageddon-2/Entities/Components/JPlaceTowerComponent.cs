using Jamageddon2.Entities.Enemies;
using Jamageddon2.Entities.Towers;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Input;
namespace Jamageddon2.Entities.Components
{
    public class JPlaceTowerComponent : JComponent, IJInjectable<JGameScene>, IJInjectable<JMouseInput>
    {
        private JGameScene scene;
        private JMouseInput mouseInput;
        public void Inject(JGameScene service)
        {
            scene = service;
        }

        public void Inject(JMouseInput service)
        {
            mouseInput = service;
        }

        public override void LoadContent()
        {
            Parent.GetComponent<JLeftMouseClickComponent>().OnClick += JPlaceTowerComponent_OnClick;
            base.LoadContent();
        }

        private void JPlaceTowerComponent_OnClick(JLeftMouseClickComponent obj)
        {
            JDishWasherTower tower = new JDishWasherTower();
            tower.GetComponent<JTransformComponent>().Position = mouseInput.Position;
            scene.AddEntity(tower);
            Parent.GetComponent<JLeftMouseClickComponent>().OnClick -= JPlaceTowerComponent_OnClick;
            Parent.DestroyEntity();
            Parent.GetComponent<JParticleEffectComponent>().Emit(mouseInput.Position, 10);
        }
    }
}
