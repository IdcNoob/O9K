namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.item_abyssal_blade)]
    public class AbyssalBlade : RangedAbility, IDisable
    {
        public AbyssalBlade(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override bool CanHitSpellImmuneEnemy { get; } = true;

        public override float GetCastDelay(Unit9 unit)
        {
            return base.GetCastDelay(unit) + 0.1f;
        }
    }
}