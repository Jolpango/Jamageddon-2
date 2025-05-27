using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Utilities;
using System;

namespace MonoGame.Jolpango.Graphics.Transitions
{
    public class AlphaTransition : IParticleTransition
    {
        public float[] Alphas { get; set; }
        public void Update(GameTime gameTime, float weight, Particle particle)
        {
            particle.Alpha = JMath.GetLerpedFloat(Alphas, weight);
        }

    }
}
