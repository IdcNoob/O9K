namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_pipe)]
    public class PipeOfInsight : AreaOfEffectAbility, IHasDamageBlock, IShield
    {
        private readonly SpecialData blockData;

        public PipeOfInsight(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "barrier_radius");
            this.blockData = new SpecialData(baseAbility, "barrier_block");
        }

        public UnitState AppliesUnitState { get; } = 0;

        public DamageType BlockDamageType { get; } = DamageType.Magical;

        public string BlockModifierName { get; } = "modifier_item_pipe_barrier";

        public bool BlocksDamageAfterReduction { get; } = false;

        public bool IsDamageBlockPermanent { get; } = false;

        public string ShieldModifierName { get; } = "modifier_item_pipe_barrier";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;

        public float BlockValue(Unit9 target)
        {
            return this.blockData.GetValue(this.Level);
        }
    }
}