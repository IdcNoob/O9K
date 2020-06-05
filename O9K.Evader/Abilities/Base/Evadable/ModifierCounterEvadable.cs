namespace O9K.Evader.Abilities.Base.Evadable
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    internal abstract class ModifierCounterEvadable : EvadableAbility, IModifierCounter
    {
        protected ModifierCounterEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }

        public override bool CanBeDodged { get; } = false;

        public virtual bool ModifierAllyCounter { get; } = false;

        public virtual bool ModifierEnemyCounter { get; } = false;

        public abstract void AddModifier(Modifier modifier, Unit9 modifierOwner);

        public sealed override void PhaseCancel()
        {
        }

        public sealed override void PhaseStart()
        {
        }

        protected sealed override void AddObstacle()
        {
        }
    }
}