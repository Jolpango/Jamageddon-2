using Jamageddon2.Entities.Components;
using Jamageddon2.UI;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using static Jamageddon2.JGameConstants;

namespace Jamageddon2.Entities.Towers
{
    public class TowerPlacer : JEntity
    {
        private readonly JSpriteComponent spriteComponent;
        private readonly JTransformComponent transformComponent;
        private readonly JMouseFollowerComponent mouseFollowerComponent;
        private readonly JLeftMouseClickComponent leftMouseClickComponent;
        private readonly JPlaceTowerComponent placeTowerComponent;
        private readonly JParticleEffectComponent particleEffectComponent;
        private readonly JColliderComponent colliderComponent;
        public TowerDefinition TowerDefinition { get; set; }

        public TowerPlacer(string spritePath, TowerDefinition towerDefinition)
        {
            spriteComponent = new JSpriteComponent(spritePath);
            transformComponent = new JTransformComponent();
            mouseFollowerComponent = new JMouseFollowerComponent();
            leftMouseClickComponent = new JLeftMouseClickComponent();
            placeTowerComponent = new JPlaceTowerComponent();
            colliderComponent = new JColliderComponent()
            {
                Size = towerDefinition.Footprint.Size,
                IsSolid = false,
                Offset = towerDefinition.Footprint.Offset
            };
            particleEffectComponent = new JParticleEffectComponent("Content/Emitters/random.json");

            TowerDefinition = towerDefinition;

            AddComponent(spriteComponent);
            AddComponent(transformComponent);
            AddComponent(mouseFollowerComponent);
            AddComponent(leftMouseClickComponent);
            AddComponent(placeTowerComponent);
            AddComponent(particleEffectComponent);
            AddComponent(colliderComponent);
        }
    }
}
