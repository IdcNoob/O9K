namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_chainmail)]
    public class Chainmail : PassiveAbility
    {
        public Chainmail(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}