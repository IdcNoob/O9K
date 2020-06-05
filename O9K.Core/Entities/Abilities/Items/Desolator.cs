namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_desolator)]
    public class Desolator : PassiveAbility
    {
        public Desolator(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}