namespace O9K.Evader.Abilities.Heroes.FacelessVoid.Chronosphere
{
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal sealed class ChronosphereAllyEvadable : LinearAreaOfEffectEvadable, IModifierObstacle
    {
        public ChronosphereAllyEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.Supernova);
            this.Counters.UnionWith(Abilities.StrongPhysShield);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.AttributeShift);

            this.Counters.Remove(Abilities.DarkPact);
        }

        public bool AllyModifierObstacle { get; } = true;

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var obstacle = new ChronosphereAllyObstacle(this, sender.Position, modifier);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return obstacle.IsModifierObstacle || obstacle is ChronosphereAllyObstacle;
        }

        protected override void AddObstacle()
        {
            var obstacle = new ChronosphereLinearAllyObstacle(this, this.Owner.Position)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + this.Ability.ActivationDelay,
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}