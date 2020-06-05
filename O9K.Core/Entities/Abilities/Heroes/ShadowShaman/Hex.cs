namespace O9K.Core.Entities.Abilities.Heroes.ShadowShaman
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.shadow_shaman_voodoo)]
    public class Hex : RangedAbility, IDisable, IAppliesImmobility
    {
        public Hex(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Hexed | UnitState.Silenced | UnitState.Disarmed;

        public string ImmobilityModifierName { get; } = "modifier_shadow_shaman_voodoo";
    }
}