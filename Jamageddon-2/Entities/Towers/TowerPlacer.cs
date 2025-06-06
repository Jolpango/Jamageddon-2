using Jamageddon2.Entities.Components;
using Jamageddon2.UI;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;

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
        public TowerDefinition TowerDefinition { get; set; }

        public TowerPlacer(string spritePath)
        {
            spriteComponent = new JSpriteComponent(spritePath);
            transformComponent = new JTransformComponent();
            mouseFollowerComponent = new JMouseFollowerComponent();
            leftMouseClickComponent = new JLeftMouseClickComponent();
            placeTowerComponent = new JPlaceTowerComponent();
            particleEffectComponent = new JParticleEffectComponent("Content/Emitters/random.json");

            AddComponent(spriteComponent);
            AddComponent(transformComponent);
            AddComponent(mouseFollowerComponent);
            AddComponent(leftMouseClickComponent);
            AddComponent(placeTowerComponent);
            AddComponent(particleEffectComponent);
        }
    }
}
