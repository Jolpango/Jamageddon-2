using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Utilities;
using System;

namespace MonoGame.Jolpango.Graphics.Dispersion
{
    public class DispersionRandom : IDispersionMethod
    {
        public float Speed { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public Vector2 Target { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Direction { get; set; }
        public DispersionRandom(float min, float max)
        {
            Min = min;
            Max = max;
        }
        public DispersionRandom(float min, float max, Vector2 origin)
        {
            Direction = JMath.GetRandomDirection();
            Min = min;
            Max = max;
            Speed = (float)JMath.GetRandomNumber(min, max);
            Origin = origin;
            Target = Speed * Direction + Origin;
        }
        public void Update(GameTime gameTime, float easing, Particle particle)
        {
            particle.Position = Vector2.Lerp(Origin, Target, easing);
        }
    }
}
