namespace O9K.Evader.Abilities.Items.MedallionOfCourage
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    internal sealed class MedallionOfCourageEvadable : ModifierCounterEvadable
    {
        public MedallionOfCourageEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
        }

        public override bool ModifierAllyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new MedallionOfCourageModifierObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}