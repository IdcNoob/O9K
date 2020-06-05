namespace O9K.Evader.Abilities.Heroes.Disruptor.StaticStorm
{
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal sealed class StaticStormEvadable : AreaOfEffectEvadable, IModifierObstacle
    {
        public StaticStormEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
        }

        public bool AllyModifierObstacle { get; } = false;

        public override bool CanBeDodged { get; } = false;

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var formationObstacle = new StaticStormFormationObstacle(this, sender.Position, modifier, this.Ability.ActivationDelay);
            this.Pathfinder.AddObstacle(formationObstacle);

            if (this.Owner.HasAghanimsScepter)
            {
                return;
            }

            var obstacle = new StaticStormObstacle(this, sender.Position, modifier, this.Ability.ActivationDelay);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return true;
        }

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }
    }
}