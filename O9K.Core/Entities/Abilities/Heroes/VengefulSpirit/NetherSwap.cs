namespace O9K.Core.Entities.Abilities.Heroes.VengefulSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.vengefulspirit_nether_swap)]
    public class NetherSwap : RangedAbility, IShield
    {
        public NetherSwap(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = 0;

        public override bool IsDisplayingCharges
        {
            get
            {
                return this.Owner.HasAghanimsScepter;
            }
        }

        public string ShieldModifierName { get; } = string.Empty;

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = false;
    }
}