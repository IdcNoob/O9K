namespace O9K.Evader.Pathfinder.Obstacles.Abilities.LinearAreaOfEffect
{
    using System.Collections.Generic;

    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Geometry;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    using Types;

    internal class LinearAreaOfEffectObstacle : AbilityObstacle, IUpdatable
    {
        public LinearAreaOfEffectObstacle(LinearAreaOfEffectEvadable ability, Vector3 startPosition)
            : base(ability)
        {
            const int RadiusIncrease = 50;
            const int RangeIncrease = 100;

            this.Position = startPosition;
            this.Radius = ability.RangedAbility.Radius + RadiusIncrease;
            this.Range = ability.RangedAbility.Range + RangeIncrease;
            this.IsUpdated = false;
        }

        public bool IsUpdated { get; protected set; }

        protected Vector3 EndPosition { get; set; }

        protected List<uint> NavMeshObstacles { get; set; }

        protected float Radius { get; }

        protected float Range { get; }

        protected Vector3 StartPosition { get; set; }

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

            this.Drawer.DrawDoubleArcRectangle(this.StartPosition, this.EndPosition, this.Radius);
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return this.EndCastTime - Game.RawGameTime;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            return this.EndObstacleTime - Game.RawGameTime;
        }

        public void Update()
        {
            //if (this.Caster.IsRotating)
            //{
            //    return;
            //}

            this.StartPosition = this.Caster.InFront(-this.Radius * 0.75f);
            this.EndPosition = this.Caster.InFront(this.Range);
            this.Polygon = new Polygon.Rectangle(this.StartPosition, this.EndPosition, this.Radius);
            this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.StartPosition, this.EndPosition, this.Radius);
            this.NavMeshId = 1; //hack

            this.IsUpdated = true;
        }
    }
}