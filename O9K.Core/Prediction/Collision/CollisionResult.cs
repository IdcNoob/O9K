namespace O9K.Core.Prediction.Collision
{
    using System.Collections.Generic;

    public class CollisionResult
    {
        public CollisionResult(IReadOnlyCollection<CollisionObject> collisionObjects)
        {
            this.CollisionObjects = collisionObjects;
        }

        public bool Collides
        {
            get
            {
                return this.CollisionObjects.Count > 0;
            }
        }

        public IReadOnlyCollection<CollisionObject> CollisionObjects { get; }
    }
}