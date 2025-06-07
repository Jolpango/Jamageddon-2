using Jamageddon2.Entities.Towers;
using Jamageddon2.UI;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Input;
using System;
using static Jamageddon2.JGameConstants;

namespace Jamageddon2.Entities.Components
{
    public class JPlaceTowerComponent : JComponent, IJInjectable<JGameScene>, IJInjectable<JMouseInput>, IJInjectable<Player>
    {
        private JGameScene scene;
        private JMouseInput mouseInput;
        private Player player;
        public void Inject(JGameScene service)
        {
            scene = service;
        }

        public void Inject(JMouseInput service)
        {
            mouseInput = service;
        }
        public void Inject(Player service)
        {
            player = service;
        }

        public override void LoadContent()
        {
            Parent.GetComponent<JLeftMouseClickComponent>().OnClick += JPlaceTowerComponent_OnClick;
            base.LoadContent();
        }

        private void JPlaceTowerComponent_OnClick(JLeftMouseClickComponent obj)
        {
            if (TowerCanBePlaced())
            {
                string className = "Jamageddon2.Entities.Towers." + Parent.Name;
                Type type = Type.GetType(className);
                if (type is not null)
                {
                    object instance = Activator.CreateInstance(type);
                    if (instance is JBaseTower tower)
                    {
                        if (Parent is TowerPlacer placer)
                        {
                            PlaceTower(tower, placer);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Class not found.");
                }
            }
        }

        private void PlaceTower(JBaseTower tower, TowerPlacer placer)
        {
            player.Gold = player.Gold - placer.TowerDefinition.Cost;

            var colliderComponent = tower.GetComponent<JColliderComponent>();
            colliderComponent.Size = placer.TowerDefinition.Footprint.Size;
            colliderComponent.Offset = placer.TowerDefinition.Footprint.Offset;
            tower.GetComponent<JTransformComponent>().Position = mouseInput.Position - (new Vector2(DEFAULT_ENTITY_SIZE, DEFAULT_ENTITY_SIZE) / 2);
            scene.AddEntity(tower);
            Parent.GetComponent<JLeftMouseClickComponent>().OnClick -= JPlaceTowerComponent_OnClick;
            Parent.DestroyEntity();
            Parent.GetComponent<JParticleEffectComponent>().Emit(mouseInput.Position, 10);
        }

        public override void Update(GameTime gameTime)
        {
            Parent.GetComponent<JSpriteComponent>().sprite.Color = TowerCanBePlaced()
                ? Color.White
                : Color.Red;


            Color green = new Color(0, 50, 0, 80);
            Color red = new Color(50, 0, 0, 80);
            Parent.GetComponent<JRangeIndicatorComponent>().Color = TowerCanBePlaced()
                ? green
                : red;
        }

        private bool TowerCanBePlaced()
        {
            if (scene.entityWorld.tileManager.TileIsFree(Parent.GetComponent<JColliderComponent>()))
            {
                return true;
            }
            return false;
        }

    }
}
