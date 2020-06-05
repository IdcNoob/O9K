namespace O9K.ItemManager.Modules.RecoveryAbuse.Abilities
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_bottle)]
    internal class Bottle : RecoveryAbility
    {
        private readonly Core.Entities.Abilities.Items.Bottle bottle;

        public Bottle(Ability9 ability)
            : base(ability)
        {
            this.bottle = (Core.Entities.Abilities.Items.Bottle)ability;
        }

        public override bool CanBeCasted()
        {
            return base.CanBeCasted() && this.bottle.StoredRune == RuneType.None;
        }

        public override bool CanPickUpItems()
        {
            if (!base.CanPickUpItems())
            {
                return false;
            }

            if (this.bottle.IsUsable && (this.RestoresHealth || this.RestoresMana) && this.bottle.BaseItem.CurrentCharges > 0)
            {
                return false;
            }

            return true;
        }
    }
}