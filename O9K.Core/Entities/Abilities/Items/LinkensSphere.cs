namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.item_sphere)]
    public class LinkensSphere : RangedAbility, IShield
    {
        public LinkensSphere(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = 0;

        public string ShieldModifierName { get; } = "modifier_item_sphere_target";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = false;

        public override void Dispose()
        {
            base.Dispose();
            this.Owner.LinkensSphere = null;
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);
            owner.LinkensSphere = this;
        }
    }
}