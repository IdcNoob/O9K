namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_wraith_band)]
    public class WraithBand : PassiveAbility
    {
        public WraithBand(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}