namespace O9K.Core.Helpers
{
    using System;

    using SharpDX;

    // ReSharper disable once InconsistentNaming
    public static class RNG
    {
        private static readonly Random Rng = new Random();

        public static Vector3 Randomize(Vector3 position, int offset)
        {
            return position + new Vector3(Rng.Next(-offset, offset), Rng.Next(-offset, offset), 0);
        }

        public static float Randomize(float value, float offset)
        {
            var intOffset = (int)(offset * 1000);
            return value + (Rng.Next(-intOffset, intOffset) / 1000f);
        }
    }
}