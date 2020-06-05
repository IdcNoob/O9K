namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_mithril_hammer)]
    public class MithrilHammer : PassiveAbility
    {
        public MithrilHammer(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}