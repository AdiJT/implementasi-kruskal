using System.Numerics;

namespace Kruskal.Core
{
    public static class RandomExtension
    {
        public static Vector2 NextVector2(this Random r, Vector2 start, Vector2 end)
        {
            if (start.X >= end.X || start.Y >= end.Y)
                throw new ArgumentException("start must be less than end");

            return new Vector2(r.NextFloat(start.X, end.X), r.NextFloat(start.Y, end.Y));
        }

        public static float NextFloat(this Random random, float minValue, float maxValue)
        {
            if (minValue >= maxValue)
                throw new ArgumentException("minValue must be less than maxValue");

            var value = random.NextDouble();

            return (float)(value * (maxValue - minValue) + minValue);
        }
    }
}