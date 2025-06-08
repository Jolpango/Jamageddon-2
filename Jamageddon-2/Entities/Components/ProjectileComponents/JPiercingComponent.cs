using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public class JPiercingComponent: JComponent
    {
        public int PiercesRemaining { get; set; } = 1;
    }
}