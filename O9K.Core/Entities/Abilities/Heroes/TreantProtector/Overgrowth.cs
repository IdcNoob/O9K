namespace O9K.Core.Entities.Abilities.Heroes.TreantProtector
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.treant_overgrowth)]
    public class Overgrowth : AreaOfEffectAbility, IDisable, IAppliesImmobility
    {
        public Overgrowth(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Rooted;

        public string ImmobilityModifierName { get; } = "modifier_treant_overgrowth";
    }
}