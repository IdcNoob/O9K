namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_basher)]
    public class SkullBasher : PassiveAbility
    {
        public SkullBasher(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}