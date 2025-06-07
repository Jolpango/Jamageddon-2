using Jamageddon2.Entities.Towers;
using Microsoft.Xna.Framework;
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
            if (scene.entityWorld.tileManager.TileIsFree(mouseInput.Position))
            {
                JDishWasherTower tower = new JDishWasherTower();
                var size = tower.GetComponent<JColliderComponent>().Size;
                tower.GetComponent<JTransformComponent>().Position = mouseInput.Position - (size / 2);
                scene.AddEntity(tower);
                Parent.GetComponent<JLeftMouseClickComponent>().OnClick -= JPlaceTowerComponent_OnClick;
                Parent.DestroyEntity();
                Parent.GetComponent<JParticleEffectComponent>().Emit(mouseInput.Position, 10);
            }
        }

        public override void Update(GameTime gameTime)
        {
            Parent.GetComponent<JSpriteComponent>().sprite.Color = scene.entityWorld.tileManager.TileIsFree(mouseInput.Position)
                ? Color.White
                : Color.Red;
        }
    }
}
