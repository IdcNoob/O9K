namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.item_ultimate_scepter)]
    public class AghanimsScepter : PassiveAbility
    {
        public AghanimsScepter(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            this.Owner.AghanimsScepter = null;
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);
            owner.AghanimsScepter = this;
        }
    }
}