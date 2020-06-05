namespace O9K.Core.Entities.Abilities.Heroes.Abaddon
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.abaddon_aphotic_shield)]
    public class AphoticShield : RangedAbility, IHasDamageBlock, IShield
    {
        private readonly SpecialData blockData;

        public AphoticShield(Ability baseAbility)
            : base(baseAbility)
        {
            this.blockData = new SpecialData(baseAbility, "damage_absorb");
        }

        public UnitState AppliesUnitState { get; } = 0;

        public DamageType BlockDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string BlockModifierName { get; } = "modifier_abaddon_aphotic_shield";

        public bool BlocksDamageAfterReduction { get; } = true;

        public bool IsDamageBlockPermanent { get; } = false;

        public string ShieldModifierName { get; } = "modifier_abaddon_aphotic_shield";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;

        public float BlockValue(Unit9 target)
        {
            return this.blockData.GetValue(this.Level);
        }
    }
}