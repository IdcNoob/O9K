namespace O9K.Evader.Abilities.Heroes.ShadowFiend.Shadowraze
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class ShadowrazeEvadable : AreaOfEffectEvadable
    {
        public ShadowrazeEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);
        }

        protected override void AddObstacle()
        {
            var obstacle = new ShadowrazeObstacle(this, this.Ability.CastRange)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}