namespace O9K.Evader.Abilities.Heroes.Mirana.SacredArrow
{
    using System;

    using Base.Evadable;

    using Core.Extensions;
    using Core.Logger;

    using Ensage;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;

    using Pathfinder.Obstacles.Abilities.LinearProjectile;

    using SharpDX;

    internal class SacredArrowObstacle : LinearProjectileUnitObstacle
    {
        private readonly float angle;

        public SacredArrowObstacle(LinearProjectileEvadable ability, Vector3 startPosition, float angle)
            : base(ability, startPosition)
        {
            this.angle = angle;
        }

        public override void AddUnit(Unit unit)
        {
            UpdateManager.BeginInvoke(
                () =>
                    {
                        try
                        {
                            if (this.ObstacleUnit != null || !unit.IsValid || !unit.IsVisible)
                            {
                                return;
                            }

                            if (this.Position.Extend2D(unit.NetworkPosition, this.Range).Distance2D(this.EndPosition) < 500)
                            {
                                this.ObstacleUnit = unit;
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    },
                300);
        }

        public override void Update()
        {
            if (this.NavMeshId == null /*&& !this.Caster.IsRotating*/)
            {
                this.EndPosition = this.Caster.InFront(this.Range, this.angle);
                this.Polygon = new Polygon.Rectangle(this.Position, this.EndPosition, this.Radius);
                this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.Position, this.EndPosition, this.Radius);
                this.NavMeshId = 1; //hack
            }
            else if (this.NavMeshId != null)
            {
                var time = Game.RawGameTime - this.EndCastTime /*- 0.35f*/;
                if (time < 0)
                {
                    return;
                }

                var currentPosition = this.Position.Extend2D(this.EndPosition, time * this.Speed);

                //this.Pathfinder.NavMesh.UpdateObstacle(this.NavMeshId.Value, currentPosition, this.Radius);
                var rectangle = (Polygon.Rectangle)this.Polygon;
                rectangle.Start = currentPosition.To2D();
                rectangle.UpdatePolygon();
            }
        }
    }
}