namespace O9K.Core.Entities.Abilities.Heroes.Beastmaster
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.beastmaster_primal_roar)]
    public class PrimalRoar : RangedAbility, IDisable
    {
        public PrimalRoar(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "damage_radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}