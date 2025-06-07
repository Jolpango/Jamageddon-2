using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;

namespace MonoGame.Jolpango.Graphics.Transitions
{
    public interface IParticleTransition
    {
        public void Update(GameTime gameTime, float weight, Particle particle);
    }
}
