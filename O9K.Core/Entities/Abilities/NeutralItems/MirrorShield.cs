namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.item_mirror_shield)]
    public class MirrorShield : PassiveAbility
    {
        public MirrorShield(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            this.Owner.MirrorShield = null;
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);
            owner.MirrorShield = this;
        }
    }
}