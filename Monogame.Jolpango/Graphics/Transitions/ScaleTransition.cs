using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Utilities;

namespace MonoGame.Jolpango.Graphics.Transitions
{
    public class ScaleTransition : IParticleTransition
    {
        public float[] Scales { get; set; }
        public void Update(GameTime gameTime, float weight, Particle particle)
        {
            particle.Scale = JMath.GetLerpedFloat(Scales, weight);
        }
    }
}
