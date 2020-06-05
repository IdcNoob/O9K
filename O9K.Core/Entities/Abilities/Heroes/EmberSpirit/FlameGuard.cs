namespace O9K.Core.Entities.Abilities.Heroes.EmberSpirit
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.ember_spirit_flame_guard)]
    public class FlameGuard : AreaOfEffectAbility, IHasDamageBlock, IShield
    {
        private readonly SpecialData blockData;

        public FlameGuard(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.blockData = new SpecialData(baseAbility, "absorb_amount");
        }

        public UnitState AppliesUnitState { get; } = 0;

        public DamageType BlockDamageType { get; } = DamageType.Magical;

        public string BlockModifierName { get; } = "modifier_ember_spirit_flame_guard";

        public bool BlocksDamageAfterReduction { get; } = false;

        public bool IsDamageBlockPermanent { get; } = false;

        public string ShieldModifierName { get; } = "modifier_ember_spirit_flame_guard";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        public float BlockValue(Unit9 target)
        {
            return this.blockData.GetValue(this.Level);
        }
    }
}