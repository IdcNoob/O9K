namespace O9K.Core.Extensions
{
    using SharpDX;

    public static class Vector2Extensions
    {
        //public static float Distance(this Vector2 start, Vector2 end)
        //{
        //    return Vector2.Distance(start, end);
        //}

        //public static float DistanceSquared(this Vector2 start, Vector2 end)
        //{
        //    return Vector2.DistanceSquared(start, end);
        //}

        public static Vector2 IncreaseX(this Vector2 vector, float x)
        {
            vector.X += x;
            return vector;
        }

        public static Vector2 IncreaseY(this Vector2 vector, float y)
        {
            vector.Y += y;
            return vector;
        }

        public static Vector2 MultiplyX(this Vector2 vector, float x)
        {
            vector.X *= x;
            return vector;
        }

        public static Vector2 MultiplyY(this Vector2 vector, float y)
        {
            vector.Y *= y;
            return vector;
        }
    }
}