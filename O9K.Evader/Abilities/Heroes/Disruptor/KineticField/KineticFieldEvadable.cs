namespace O9K.Evader.Abilities.Heroes.Disruptor.KineticField
{
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal sealed class KineticFieldEvadable : AreaOfEffectEvadable, IModifierObstacle
    {
        public KineticFieldEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.IronBranch);
        }

        public bool AllyModifierObstacle { get; } = false;

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var formationObstacle = new KineticFieldFormationObstacle(this, sender.Position, modifier, this.Ability.ActivationDelay);
            this.Pathfinder.AddObstacle(formationObstacle);

            var obstacle = new KineticFieldObstacle(this, sender.Position, modifier, this.Ability.ActivationDelay);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return obstacle is KineticFieldObstacle;
        }

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }
    }
}