namespace O9K.Core.Entities.Abilities.Heroes.Oracle
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.oracle_fates_edict)]
    public class FatesEdict : RangedAbility, IShield, IDebuff
    {
        public FatesEdict(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.MagicImmune | UnitState.Disarmed;

        public string DebuffModifierName { get; } = "modifier_oracle_fates_edict";

        public string ShieldModifierName { get; } = "modifier_oracle_fates_edict";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;
    }
}