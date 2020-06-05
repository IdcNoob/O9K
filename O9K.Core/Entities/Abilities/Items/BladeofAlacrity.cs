namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_blade_of_alacrity)]
    public class BladeOfAlacrity : PassiveAbility
    {
        public BladeOfAlacrity(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}