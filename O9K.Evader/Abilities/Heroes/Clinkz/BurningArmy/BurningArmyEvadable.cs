namespace O9K.Evader.Abilities.Heroes.Clinkz.BurningArmy
{
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    internal sealed class BurningArmyEvadable : AreaOfEffectEvadable, IModifierObstacle
    {
        public BurningArmyEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.CrimsonGuard);
            this.Counters.Add(Abilities.SolarCrest);
            this.Counters.Add(Abilities.MedallionOfCourage);
            this.Counters.Add(Abilities.WarCry);
            this.Counters.Add(Abilities.Buckler);
        }

        public bool AllyModifierObstacle { get; } = false;

        public override bool CanBeDodged { get; } = false;

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var obstacle = new AreaOfEffectModifierObstacle(this, sender.Position, modifier, 200);
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