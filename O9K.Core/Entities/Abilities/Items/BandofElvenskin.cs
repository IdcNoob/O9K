namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_boots_of_elves)]
    public class BandOfElvenskin : PassiveAbility
    {
        public BandOfElvenskin(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}