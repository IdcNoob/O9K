namespace O9K.Evader.Abilities.Heroes.Kunkka.Tidebringer
{
    using System.Collections.Generic;

    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Pathfinder.Obstacles.Abilities;
    using Pathfinder.Obstacles.Types;

    using SharpDX;

    internal class TidebringerObstacle : AbilityObstacle, IUpdatable
    {
        protected Dictionary<uint, Vector3> NavMeshObstacles;

        public TidebringerObstacle(TidebringerEvadable ability, Vector3 position)
            : base(ability)
        {
            const int RadiusIncrease = 75;
            const int EndRadiusIncrease = 100;
            const int RangeIncrease = 100;

            this.Position = position;
            this.Range = ability.Tidebringer.Range + RangeIncrease;
            this.Radius = ability.Tidebringer.Radius + RadiusIncrease;
            this.EndRadius = ability.Tidebringer.EndRadius + EndRadiusIncrease;
            this.IsUpdated = false;
        }

        public bool IsUpdated { get; protected set; }

        protected Vector3 EndPosition { get; set; }

        protected float EndRadius { get; }

        protected float Radius { get; }

        protected float Range { get; }

        public override void Dispose()
        {
            base.Dispose();
            if (this.NavMeshId != null)
            {
                this.Pathfinder.RemoveNavMeshObstacle(this.NavMeshObstacles);
            }
        }

        public override void Draw()
        {
            if (this.NavMeshId == null)
            {
                return;
            }

            this.Drawer.DrawArcRectangle(this.Position, this.EndPosition, this.Radius, this.EndRadius);
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return this.EndCastTime - Game.RawGameTime;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            return this.EndCastTime - Game.RawGameTime;
        }

        public void Update()
        {
            //if (this.Caster.IsRotating)
            //{
            //    return;
            //}

            this.EndPosition = this.Caster.InFront(this.Range);
            this.Polygon = new Polygon.Trapezoid(this.Position, this.EndPosition, this.Radius, this.EndRadius);
            this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.Position, this.EndPosition, this.Radius, this.EndRadius);
            this.NavMeshId = 1; // hack

            this.IsUpdated = true;
        }
    }
}