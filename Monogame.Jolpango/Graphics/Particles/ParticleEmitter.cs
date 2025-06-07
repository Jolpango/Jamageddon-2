using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Graphics.Dispersion;
using MonoGame.Jolpango.Graphics.Transitions;
using MonoGame.Jolpango.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Graphics.Particles
{
    public class ParticleEmitter
    {
        public static ParticleEmitter Shared { get; set; }
        public List<Particle> Particles { get; set; }
        public Texture2D Texture { get; set; }
        public float LayerDepth { get; set; } = 1.0f;
        public Color Color { get; set; } = Color.White;
        public float TimeToLive { get; set; } = 1.0f;
        public int MinRadius { get; set; } = 1;
        public int MaxRadius { get; set; } = 1;
        public IParticleTransition[] Transitions { get; set; }
        public EasingFunction Easing { get; set; } = EasingFunction.EaseInOutQuad;
        public IDispersionMethod DispersionMethod { get; set; }
        public ParticleEmitter(Texture2D texture, IDispersionMethod dispersionMethod, params IParticleTransition[] transitions)
        {
            Texture = texture;
            Particles = new List<Particle>();
            Transitions = transitions;
            DispersionMethod = dispersionMethod;
        }
        public void Emit(Vector2 position)
        {
            Particles.Add(Particle.CreateParticle(
                Texture,
                JMath.GetRandomPosition(position, Random.Shared.Next(MinRadius, MaxRadius)),
                position,
                LayerDepth,
                TimeToLive,
                Easing,
                DispersionMethod,
                Transitions));
        }

        public void Emit(Vector2 position, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Emit(position);
            }
        }

        public void Emit(Particle particle)
        {
            Particles.Add(particle);
        }


        public void Update(GameTime gameTime)
        {
            Particles.RemoveAll(x =>
            {
                x.Update(gameTime);
                return x.Timer >= x.TimeToLive;
            });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var particle in Particles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}
