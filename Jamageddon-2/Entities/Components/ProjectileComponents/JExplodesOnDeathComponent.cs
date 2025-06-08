using Jamageddon2.Entities.Projectiles;
using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public class JExplodesOnDeathComponent : JComponent
    {
        public JExplosionPrefab Prefab { get; set; }
    }
}