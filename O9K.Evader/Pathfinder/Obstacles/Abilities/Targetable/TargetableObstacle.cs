namespace O9K.Evader.Pathfinder.Obstacles.Abilities.Targetable
{
    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Geometry;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    using Types;

    internal class TargetableObstacle : AbilityObstacle, IUpdatable
    {
        protected Vector3 EndPosition;

        public TargetableObstacle(EvadableAbility ability, float radius = 75)
            : base(ability)
        {
            //todo improve

            this.Position = ability.Owner.Position;
            this.Radius = radius;
            this.Range = (ability.Ability.CastRange * 1.1f) + 100;
            this.EndObstacleTime = ability.EndCastTime;
            this.Polygon = new Polygon.Rectangle(this.Position, this.EndPosition, this.Radius);
            this.IsUpdated = false;
        }

        public bool IsUpdated { get; }

        public float Radius { get; }

        public float Range { get; }

        public override void Draw()
        {
            this.Drawer.DrawRectangle(this.Position, this.EndPosition, this.Radius);
            this.Drawer.UpdateRectanglePosition(this.Position, this.EndPosition, this.Radius);
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
            var rectangle = (Polygon.Rectangle)this.Polygon;
            this.EndPosition = this.Caster.InFront(this.Range);
            rectangle.End = this.EndPosition.To2D();
            rectangle.UpdatePolygon();
        }
    }
}