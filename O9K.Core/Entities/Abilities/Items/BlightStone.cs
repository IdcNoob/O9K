namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_blight_stone)]
    public class BlightStone : PassiveAbility
    {
        public BlightStone(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}