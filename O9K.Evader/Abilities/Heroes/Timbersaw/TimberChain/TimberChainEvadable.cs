namespace O9K.Evader.Abilities.Heroes.Timbersaw.TimberChain
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class TimberChainEvadable : LinearProjectileEvadable
    {
        public TimberChainEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);
        }

        protected override void AddObstacle()
        {
            var obstacle = new TimberChainObstacle(this, this.Ability.Owner.Position)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}