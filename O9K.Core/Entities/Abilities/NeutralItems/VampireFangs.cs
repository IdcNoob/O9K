namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_vampire_fangs)]
    public class VampireFangs : PassiveAbility
    {
        public VampireFangs(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}