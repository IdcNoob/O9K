namespace O9K.Core.Entities.Abilities.Heroes.Juggernaut
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.juggernaut_blade_fury)]
    public class BladeFury : AreaOfEffectAbility, IShield
    {
        public BladeFury(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "blade_fury_radius");
            this.DamageData = new SpecialData(baseAbility, "blade_fury_damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.MagicImmune;

        public string ShieldModifierName { get; } = "modifier_juggernaut_blade_fury";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}