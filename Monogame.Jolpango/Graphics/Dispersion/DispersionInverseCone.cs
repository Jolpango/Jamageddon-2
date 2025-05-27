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
    public class DispersionInverseCone : IDispersionMethod
    {
        public float Radius { get; set; } = 1;
        public float Length { get; set; }
        public Vector2 Target { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Center { get; set; }
        public DispersionInverseCone(Vector2 direction, float radius, float length)
        {
            Direction = direction;
            Radius = radius;
            Length = length;
        }
        public DispersionInverseCone(Vector2 direction, float radius, float length, Vector2 origin, Vector2 center)
        {
            Direction = direction;
            Radius = radius;
            Length = length;
            Origin = origin;
            Center = center;
            Direction.Normalize();
            Target = Direction * Length + Center;
            Vector2 randomDir = JMath.GetRandomDirection();
            Target += randomDir * (float)JMath.GetRandomNumber(0, radius);
        }
        public void Update(GameTime gameTime, float easing, Particle particle)
        {
            particle.Position = Vector2.Lerp(Origin, Target, easing);
        }
    }
}
