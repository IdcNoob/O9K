namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_pupils_gift)]
    public class PupilsGift : PassiveAbility
    {
        public PupilsGift(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}