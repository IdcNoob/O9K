namespace O9K.Core.Prediction.Collision
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Geometry;

    using SharpDX;

    public static class Collision
    {
        public static CollisionResult GetCollision(
            Vector2 startPosition,
            Vector2 endPosition,
            float radius,
            IEnumerable<CollisionObject> collisionObjects)
        {
            var objects = new List<CollisionObject>();
            foreach (var obj in collisionObjects)
            {
                if (obj.Position.Distance2D(startPosition, endPosition, true, true) < (radius + obj.Radius) * (radius + obj.Radius))
                {
                    objects.Add(obj);
                }
            }

            objects = objects.OrderBy(x => startPosition.Distance2D(x.Position)).ToList();

            return new CollisionResult(objects);
        }
    }
}