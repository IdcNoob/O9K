namespace O9K.Evader.Abilities.Items.BootsOfTravel
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    internal sealed class BootsOfTravelEvadable : ModifierCounterEvadable
    {
        public BootsOfTravelEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierCounters.Add(Abilities.Midas);
            this.ModifierCounters.Add(Abilities.Devour);
            this.ModifierCounters.Add(Abilities.DemonicConversion);
            this.ModifierCounters.Add(Abilities.Dominator);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            if (!modifierOwner.IsCreep)
            {
                return;
            }

            var obstacle = new BootsOfTravelObstacle(this, modifier, modifierOwner, 1000);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}