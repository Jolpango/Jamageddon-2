using Microsoft.Xna.Framework;
using System;
using System.Net;

namespace MonoGame.Jolpango.Utilities
{
    public enum EasingFunction
    {
        EaseInOutQuad,
        EaseOutBack,
        EaseInCubic,
        EaseOutCubic
    }
    public static class JMath
    {
        private static Random random = new Random();
        public static Vector2 GetRandomDirection()
        {
            Vector2 randomDirection = new Vector2(
                ((float)random.NextDouble() * 2.0f) - 1,
                ((float)random.NextDouble() * 2.0f) - 1
            );
            randomDirection.Normalize();
            return randomDirection;
        }
        public static double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static Vector2 GetRandomPosition(Vector2 origin, int radius)
        {
            return origin + GetRandomDirection() * radius;
        }
        public static double EaseInOutQuad(double x)
        {
            return x < 0.5 ? 2 * x * x : 1 - Math.Pow(-2 * x + 2, 2) / 2;
        }
        public static double EaseInOutQuad(double x, double end)
        {
            return EaseInOutQuad(x / end);
        }
        public static double EaseOutBack(double x)
        {
            const double c1 = 1.70158;
            const double c3 = c1 + 1;

            return 1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2);
        }
        public static double EaseOutBack(double x, double end)
        {
            return EaseOutBack(x / end);
        }
        public static double EaseInCubic(double x)
        {
            return x * x * x;
        }

        public static double EaseInCubic(double x, double end)
        {
            return EaseInCubic(x / end);
        }

        public static double EaseOutCubic(double x)
        {
            double f = x - 1.0;
            return f * f * f + 1.0;
        }

        public static double EaseOutCubic(double x, double end)
        {
            return EaseOutCubic(x / end);
        }
        public static float Lerp(float firstFloat, float secondFloat, float weight)
        {
            return firstFloat * (1 - weight) + secondFloat * weight;
        }
        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        public static float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }
        public static Vector2 RotateVector(Vector2 vector, float angleRadians)
        {
            float cos = (float)Math.Cos(angleRadians);
            float sin = (float)Math.Sin(angleRadians);

            float x = vector.X * cos - vector.Y * sin;
            float y = vector.X * sin + vector.Y * cos;

            return new Vector2(x, y);
        }
        public static float GetLerpedFloat(float[] values, float weight)
        {
            if (values is null || values.Length == 0)
            {
                // Return a default value if the array is empty or null
                return 0.0f; // You can change this to any default value you prefer
            }

            if (weight <= 0f)
            {
                // If weight is 0 or less, return the first value
                return values[0];
            }

            if (weight >= 1f)
            {
                // If weight is 1 or more, return the last value
                return values[values.Length - 1];
            }

            // Calculate the interval size between values
            float interval = 1f / (values.Length - 1);
            int startIndex = (int)Math.Floor(weight / interval);
            int endIndex = (int)Math.Ceiling(weight / interval);

            // Adjust weight for the current interval
            float adjustedWeight = (weight - startIndex * interval) / interval;

            // Interpolate between the values at startIndex and endIndex
            return values[startIndex] + (values[endIndex] - values[startIndex]) * adjustedWeight;
        }
        public static Vector2 CircularLerp(Vector2 start, Vector2 end, float t)
        {
            Vector2 direction = start - end; // Calculate the vector between start and end points
            float radius = direction.Length(); // Calculate the radius of the circle

            float startAngle = (float)Math.Atan2(direction.Y, direction.X); // Calculate starting angle

            float interpolatedAngle = MathHelper.WrapAngle(LerpAngle(startAngle, 0, t)); // Interpolate angles

            float x = start.X + radius * (float)Math.Cos(interpolatedAngle); // Calculate interpolated x coordinate
            float y = start.Y + radius * (float)Math.Sin(interpolatedAngle); // Calculate interpolated y coordinate

            return new Vector2(x, y);

        }
        public static float LerpAngle(float startAngle, float endAngle, float amount)
        {
            // Ensure the angles are in the range [0, 2π)
            startAngle = MathHelper.WrapAngle(startAngle);
            endAngle = MathHelper.WrapAngle(endAngle);

            // Calculate the absolute difference between the angles
            float difference = MathHelper.WrapAngle(endAngle - startAngle);

            // Calculate the shortest interpolation direction
            float shortestAngle = ((2 * difference) % MathHelper.TwoPi) - difference;
            float interpolatedAngle = startAngle + shortestAngle * amount;

            // Wrap the interpolated angle within the [0, 2π) range
            interpolatedAngle = MathHelper.WrapAngle(interpolatedAngle);

            return interpolatedAngle;
        }
    }
}
