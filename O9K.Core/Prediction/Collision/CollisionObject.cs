namespace O9K.Core.Prediction.Collision
{
    using Ensage.SDK.Geometry;

    using Entities;
    using Entities.Units;

    using SharpDX;

    public class CollisionObject
    {
        public CollisionObject(Unit9 unit)
        {
            this.Entity = unit;
            this.Position = unit.Position.To2D();
            this.Radius = unit.HullRadius;
        }

        public CollisionObject(Entity9 entity, Vector2 position, float radius)
        {
            this.Entity = entity;
            this.Position = position;
            this.Radius = radius;
        }

        public CollisionObject(Entity9 entity, Vector3 position, float radius)
        {
            this.Entity = entity;
            this.Position = position.To2D();
            this.Radius = radius;
        }

        public Entity9 Entity { get; }

        public Vector2 Position { get; }

        public float Radius { get; }
    }
}