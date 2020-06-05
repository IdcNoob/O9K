namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_ward_dispenser)]
    public class WardDispenser : RangedAbility
    {
        public WardDispenser(Ability ability)
            : base(ability)
        {
        }
    }
}