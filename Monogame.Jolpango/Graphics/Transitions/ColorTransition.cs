using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;
using System;
using System.Collections.Generic;

namespace MonoGame.Jolpango.Graphics.Transitions
{
    public class ColorTransition : IParticleTransition
    {
        public Color[] Colors { get; set; }

        public void Update(GameTime gameTime, float weight, Particle particle)
        {
            particle.Color = GetLerpedColor(weight);
        }

        public Color GetLerpedColor(float weight)
        {
            if (Colors is null || Colors.Length == 0)
            {
                // Return a default color if the list is empty or null
                return Color.White; // You can change this to any default color you prefer
            }

            if (weight <= 0f)
            {
                // If weight is 0 or less, return the first color
                return Colors[0];
            }

            if (weight >= 1f)
            {
                // If weight is 1 or more, return the last color
                return Colors[Colors.Length - 1];
            }

            // Calculate the interval size between colors
            float interval = 1f / (Colors.Length - 1);
            int startIndex = (int)Math.Floor(weight / interval);
            int endIndex = (int)Math.Ceiling(weight / interval);

            // Adjust weight for the current interval
            float adjustedWeight = (weight - startIndex * interval) / interval;

            // Interpolate between the colors at startIndex and endIndex
            return Color.Lerp(Colors[startIndex], Colors[endIndex], adjustedWeight);
        }
    }
}
