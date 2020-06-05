namespace O9K.Core.Extensions
{
    using System;

    using Ensage.SDK.Extensions;

    using Entities.Units;

    using SharpDX;

    public static class Vector3Extensions
    {
        //public static float Distance(this Vector3 start, Vector3 end)
        //{
        //    return Vector2.Distance(new Vector2(start.X, start.Y), new Vector2(end.X, end.Y));
        //}

        //public static float DistanceSquared(this Vector3 start, Vector3 end)
        //{
        //    return Vector2.DistanceSquared(new Vector2(start.X, start.Y), new Vector2(end.X, end.Y));
        //}

        public static float AngleBetween(this Vector3 start, Vector3 center, Vector3 end)
        {
            return (center - start).AngleBetween(end - center);
        }

        public static float AngleBetween(this Unit9 start, Unit9 center, Unit9 end)
        {
            return AngleBetween(start.Position, center.Position, end.Position);
        }

        public static Vector3 Extend2D(this Vector3 position, Vector3 to, float distance)
        {
            var v2 = position.ToVector2();
            var tov2 = to.ToVector2();

            return (v2 + (distance * (tov2 - v2).Normalized())).ToVector3();
        }

        public static Vector3 IncreaseX(this Vector3 vector, float x)
        {
            vector.X += x;
            return vector;
        }

        public static Vector3 IncreaseY(this Vector3 vector, float y)
        {
            vector.Y += y;
            return vector;
        }

        public static Vector3 IncreaseZ(this Vector3 vector, float z)
        {
            vector.Z += z;
            return vector;
        }

        public static Vector3 MultiplyX(this Vector3 vector, float x)
        {
            vector.X *= x;
            return vector;
        }

        public static Vector3 MultiplyY(this Vector3 vector, float y)
        {
            vector.Y *= y;
            return vector;
        }

        public static Vector3 MultiplyZ(this Vector3 vector, float z)
        {
            vector.Z *= z;
            return vector;
        }

        public static Vector3 PositionAfter(this Vector3[] path, float time, float speed, float delay = 0)
        {
            var distance = Math.Max(0, time - delay) * speed;
            for (var i = 0; i <= path.Length - 2; i++)
            {
                var from = path[i];
                var to = path[i + 1];
                var d = to.Distance(from);
                if (d > distance)
                {
                    return from + (distance * (to - from).Normalized());
                }

                distance -= d;
            }

            return path[path.Length - 1];
        }
    }
}