using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Graphics.Dispersion
{
    public class DispersionCone : IDispersionMethod
    {
        public Vector2 Direction { get; set; }
        public float Radius { get; set; } = 15;
        public float Speed { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public Vector2 Target { get; set; }
        public Vector2 Origin { get; set; }
        public DispersionCone(Vector2 direction, float radius, float min, float max)
        {
            Direction = direction;
            Radius = radius;
            Min = min;
            Max = max;
        }
        public DispersionCone(Vector2 direction, float radius, float min, float max, Vector2 origin)
        {
            Direction = direction;
            Radius = radius;
            Min = min;
            Max = max;
            Origin = origin;
            Speed = (float)JMath.GetRandomNumber(min, max);
            float randomAngle = (float)JMath.GetRandomNumber(-Radius, Radius);
            Direction = JMath.RotateVector(Direction, MathHelper.ToRadians(randomAngle));
            Direction.Normalize();
            Target = Direction * Speed + Origin;
        }

        public void Update(GameTime gameTime, float easing, Particle particle)
        {
            particle.Position = Vector2.Lerp(Origin, Target, easing);
        }
    }
}
