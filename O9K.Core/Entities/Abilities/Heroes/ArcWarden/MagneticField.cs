namespace O9K.Core.Entities.Abilities.Heroes.ArcWarden
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.arc_warden_magnetic_field)]
    public class MagneticField : CircleAbility, IShield
    {
        public MagneticField(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.AttackImmune;

        public string ShieldModifierName { get; } = "modifier_arc_warden_magnetic_field_evasion";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;
    }
}