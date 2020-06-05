namespace O9K.Evader.Abilities.Heroes.DrowRanger.Multishot
{
    using System.Collections.Generic;

    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Pathfinder.Obstacles.Abilities;
    using Pathfinder.Obstacles.Types;

    using SharpDX;

    internal class MultishotObstacle : AbilityObstacle, IUpdatable
    {
        protected Dictionary<uint, Vector3> NavMeshObstacles;

        private readonly Modifier modifier;

        public MultishotObstacle(MultishotEvadable ability, Vector3 position, Modifier modifier)
            : base(ability)
        {
            this.modifier = modifier;
            const int RadiusIncrease = 75;
            const int EndRadiusIncrease = 50;
            const int RangeIncrease = 50;

            this.Position = position;
            this.Range = ability.ActiveAbility.Range + RangeIncrease;
            this.Radius = ability.ActiveAbility.Radius + RadiusIncrease;
            this.EndRadius = ability.Multishot.EndRadius + EndRadiusIncrease;
            this.IsUpdated = false;
        }

        public override bool IsExpired
        {
            get
            {
                if (!this.modifier.IsValid)
                {
                    return true;
                }

                return base.IsExpired;
            }
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
            return this.EndObstacleTime - Game.RawGameTime;
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

            this.EndPosition = this.Caster.InFront(this.Range);
            this.Polygon = new Polygon.Trapezoid(this.Position, this.EndPosition, this.Radius, this.EndRadius);
            this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.Position, this.EndPosition, this.Radius, this.EndRadius);
            this.NavMeshId = 1; // hack

            this.IsUpdated = true;
        }
    }
}