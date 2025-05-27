using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Graphics.Dispersion;
using MonoGame.Jolpango.Graphics.Transitions;
using MonoGame.Jolpango.Utilities;
using System;
using System.Collections.Generic;

namespace MonoGame.Jolpango.Graphics.Particles
{
    public class Particle
    {
        public Texture2D Texture { get; set; }
        public float Rotation { get; set; } = 0.0f;
        public Vector2 Position { get; set; }
        public float Alpha { get; set; } = 1.0f;
        public float LayerDepth { get; set; } = 1.0f;
        public float TimeToLive { get; set; } = 1.0f;
        public float Timer { get; set; } = 0.0f;
        public Color Color { get; set; } = Color.White;
        public float Scale { get; set; } = 1.0f;
        public IParticleTransition[] Transitions { get; set; }
        public EasingFunction Easing { get; set; } = EasingFunction.EaseInOutQuad;
        public IDispersionMethod DispersionMethod { get; set; }
        public Func<double, double, double> EasingDelegate { get; set; } = JMath.EaseInOutQuad;
        public Particle(Texture2D texture, Vector2 origin)
        {
            Texture = texture;
            Position = origin;
        }
        public Vector2 Origin
        {
            get
            {
                return new Vector2(Texture.Width / 2, Texture.Height / 2 );
            }
        }
        public void Update(GameTime gameTime)
        {
            float weight = (float)EasingDelegate(Timer, TimeToLive);
            DispersionMethod.Update(gameTime, weight, this);
            updateTransitions(gameTime, weight);

            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color * Alpha, Rotation, Origin, Vector2.One * Scale, SpriteEffects.None, LayerDepth);
        }

        private void updateTransitions(GameTime gameTime, float weight)
        {
            foreach (var transition in Transitions)
            {
                transition.Update(gameTime, weight, this);
            }
        }

        public static Particle CreateParticle(
            Texture2D texture,
            Vector2 origin,
            Vector2 center,
            float layerDepth,
            float timeToLive,
            EasingFunction easing,
            IDispersionMethod dispersionMethod,
            params IParticleTransition[] transitions)
        {
            Particle particle = new(texture, origin)
            {
                LayerDepth = layerDepth,
                TimeToLive = timeToLive,
                Easing = easing,
                Transitions = transitions,
            };

            particle.DispersionMethod = createNewDispersionMethod(dispersionMethod, origin, center);

            switch (particle.Easing)
            {
                case EasingFunction.EaseInOutQuad:
                    particle.EasingDelegate = JMath.EaseInOutQuad;
                    break;
                case EasingFunction.EaseOutBack:
                    particle.EasingDelegate = JMath.EaseOutBack;
                    break;
            }
            return particle;
        }
        private static IDispersionMethod createNewDispersionMethod(IDispersionMethod dispersionMethod, Vector2 origin, Vector2 center)
        {
            if (dispersionMethod.GetType() == typeof(DispersionRandom))
            {
                DispersionRandom random = (DispersionRandom)dispersionMethod;
                return new DispersionRandom(random.Min, random.Max, origin);
            }
            else if (dispersionMethod.GetType() == typeof (DispersionCone))
            {
                DispersionCone cone = (DispersionCone)dispersionMethod;
                return new DispersionCone(cone.Direction, cone.Radius, cone.Min, cone.Max, origin);
            }
            else if (dispersionMethod.GetType() == typeof(DispersionInverseCone))
            {
                DispersionInverseCone cone = (DispersionInverseCone)dispersionMethod;
                return new DispersionInverseCone(cone.Direction, cone.Radius, cone.Length, origin, center);
            }

            return dispersionMethod;
        }
    }
}
