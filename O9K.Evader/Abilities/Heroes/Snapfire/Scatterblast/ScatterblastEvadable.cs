namespace O9K.Evader.Abilities.Heroes.Snapfire.Scatterblast
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.ConeProjectile;

    internal sealed class ScatterblastEvadable : ConeProjectileEvadable
    {
        public ScatterblastEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.Bulwark);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
        }

        protected override void AddObstacle()
        {
            var obstacle = new ConeProjectileObstacle(this, this.Owner.Position, 0, 0, 0)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + (this.ConeAbility.Range / this.ConeAbility.Speed)
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}