namespace O9K.Core.Entities.Abilities.Heroes.PhantomLancer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.phantom_lancer_doppelwalk)]
    public class Doppelganger : CircleAbility, IShield
    {
        public Doppelganger(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "target_aoe");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public string ShieldModifierName { get; } = "modifier_phantomlancer_dopplewalk_phase";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}