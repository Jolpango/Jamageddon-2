using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Utilities;

namespace MonoGame.Jolpango.Graphics.Transitions
{
    public class RotationTransition : IParticleTransition
    {
        public float StartRotation { get; set; } = 0.0f;
        public float EndRotation { get; set; } = 1.0f;
        public void Update(GameTime gameTime, float weight, Particle particle)
        {
            particle.Rotation = JMath.Lerp(StartRotation, EndRotation, weight);
        }
    }
}
